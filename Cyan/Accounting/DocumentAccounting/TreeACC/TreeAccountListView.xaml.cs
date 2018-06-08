using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Data.Context;
using Saina.Data.Services.BasicInformation.Accounts;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
    /// <summary>
    /// Interaction logic for TreeAccountListView.xaml
    /// </summary>
    public partial class TreeAccountListView : UserControl
    {
        private AccessUtility _accessUtility;
        private TreeAccountListViewModel _vm;
        private ISystemAccountingSettingsService _systemAccountingSettingsService;
        private IGLsService _gLsService;

        public TreeAccountListView()
        {
            _systemAccountingSettingsService = SmObjectFactory.Container.GetInstance<ISystemAccountingSettingsService>();
            _gLsService = SmObjectFactory.Container.GetInstance<IGLsService>();
            InitializeComponent();
            Loaded += (s, ea) =>
             {
                 _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                 _vm = DataContext as TreeAccountListViewModel;
                 _vm.Load();
             };

        }

        private RadTreeViewItem ClickedTreeViewItem
        {
            get
            {
                return ContextMenu.GetClickedElement<RadTreeViewItem>();
            }
        }

        private void ContextMenuOpened(object sender, RoutedEventArgs e)
        {

            if (ClickedTreeViewItem?.DataContext is SL)
            {
                (ContextMenu.Items[0] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[1] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[2] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[3] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[4] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[5] as RadMenuItem).Visibility = Visibility.Visible; // Delete


            }
            else if (ClickedTreeViewItem?.DataContext is TL)
            {
                (ContextMenu.Items[0] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[1] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[2] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[3] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[4] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[5] as RadMenuItem).Visibility = Visibility.Visible; // Delete
            }
            else if (ClickedTreeViewItem?.DataContext is GL)
            {
                //  (ContextMenu.Items[0] as RadMenuItem).IsEnabled = true;
                (ContextMenu.Items[0] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[1] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[2] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[3] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[4] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[5] as RadMenuItem).Visibility = Visibility.Visible; // Delete
            }
            else
            {
                (ContextMenu.Items[0] as RadMenuItem).Visibility = Visibility.Visible; // Delete
                (ContextMenu.Items[1] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[2] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[3] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[4] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
                (ContextMenu.Items[5] as RadMenuItem).Visibility = Visibility.Collapsed; // Delete
            }
        }
        private bool? _dialogResult;

        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
        private void ContextMenuClick(object sender, RoutedEventArgs e)
        {
            //GL gl = ClickedTreeViewItem.DataContext as GL;
            //TL tl = ClickedTreeViewItem.DataContext as TL;
            var SystemAccountingSettingModel = _systemAccountingSettingsService.GetSystemAccountingSettingModel();
            int.TryParse(SystemAccountingSettingModel.GLLength, out int SystemAccountingSettingModelGLLength);

           // var SystemAccountingSettingModelGLLength = int.Parse(SystemAccountingSettingModel.GLLength);
            long lastGLCode = _gLsService.GetLastGLCode() + 1;
            var lastGLLenght = (lastGLCode.ToString()).Length;
            string tag = (e.OriginalSource as RadMenuItem).Tag as string;
            if (ClickedTreeViewItem == null)
            {
                if (lastGLLenght == SystemAccountingSettingModelGLLength)
                {


                    AddGLTreeItemWindow addGLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddGLTreeItemWindow>();
                    var treeGL = addGLTreeItemWindow0.DataContext as AddGLTreeItemWindowViewModel;
                    //   treeGL.GL = gl;
                    treeGL.SaveClicked += (g) =>
                    {
                        //addTLTreeItemWindow0.DataItem.GL = gl;
                        g.ImageUrl = "../../Resources/cian.png";
                        _vm.AddGL(g);
                        g.GLId = _vm.GetGLId(g.GLCode);
                        _vm.Items.Add(g);
                        //_vm.Save();
                        addGLTreeItemWindow0.Close();
                        // gl.TLs.Add(addTLTreeItemWindow0.DataItem);
                        //     gl.IsExpanded = true; // Ensure that the new child is visible
                        //using (var uow = new SainaDbContext())
                        //{
                        //    uow.GLs.Add(g);
                        //    uow.SaveChanges();
                        //    addGLTreeItemWindow0.Close();
                        //}
                    };
                    addGLTreeItemWindow0.Width = 1000;
                    addGLTreeItemWindow0.Height = 500;
                    addGLTreeItemWindow0.CanClose = true;
                    addGLTreeItemWindow0.Owner = Window.GetWindow(this);
                    addGLTreeItemWindow0.Show();
                }
                else
                {
                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بستن";
                    parameters.Header = "اخطار";
                    parameters.Content = " شماره گذاری این حساب  به پایان رسیده است";
                    RadWindow.Alert(parameters);
                }
            }
            else if (ClickedTreeViewItem.DataContext is GL gl)
            {
                if (tag == "newGL")
                {
                    if (_accessUtility.HasAccess(70))
                    {
                        if (lastGLLenght == SystemAccountingSettingModelGLLength)
                        {


                            AddGLTreeItemWindow addGLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddGLTreeItemWindow>();
                            var treeGL = addGLTreeItemWindow0.DataContext as AddGLTreeItemWindowViewModel;
                            //   treeGL.GL = gl;
                            treeGL.SaveClicked += (g) =>
                            {
                                //addTLTreeItemWindow0.DataItem.GL = gl;
                                g.ImageUrl = "../../Resources/cian.png";
                                _vm.AddGL(g);
                                g.GLId = _vm.GetGLId(g.GLCode);
                                _vm.Items.Add(g);
                                //_vm.Save();
                                addGLTreeItemWindow0.Close();
                                // gl.TLs.Add(addTLTreeItemWindow0.DataItem);
                                gl.IsExpanded = true; // Ensure that the new child is visible
                                                      //using (var uow = new SainaDbContext())
                                                      //{
                                                      //    uow.GLs.Add(g);
                                                      //    uow.SaveChanges();
                                                      //    addGLTreeItemWindow0.Close();
                                                      //}
                            };
                            addGLTreeItemWindow0.Width = 1000;
                            addGLTreeItemWindow0.Height = 500;
                            addGLTreeItemWindow0.CanClose = true;
                            addGLTreeItemWindow0.Owner = Window.GetWindow(this);
                            addGLTreeItemWindow0.Show();
                        }

                        else
                        {
                            DialogParameters parameters = new DialogParameters();
                            parameters.OkButtonContent = "بستن";
                            parameters.Header = "اخطار";
                            parameters.Content = " شماره گذاری این حساب  به پایان رسیده است";
                            RadWindow.Alert(parameters);
                        }
                    }
                    // break;
                }
                else if (tag == "newTL")
                {
                    if (_accessUtility.HasAccess(70))
                    {
                        AddTLTreeItemWindow addTLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddTLTreeItemWindow>();
                        var treeTL = addTLTreeItemWindow0.DataContext as AddTLTreeItemWindowViewModel;

                        treeTL.TL = new TL();
                        treeTL.TL.GLId = gl.GLId;
                        treeTL.TL.GL = gl;
                        treeTL.SaveClicked += (t) =>
                        {
                        //addTLTreeItemWindow0.DataItem.GL = gl;
                        //t.GLId = gl.GLId;
                        t.ImageUrl = "../../Resources/cian.png";
                            _vm.AddTL(t);
                            t.GLId = _vm.GetTLId(t.TLCode);
                            gl.TLs.Add(t);
                        //_vm.Save();
                        addTLTreeItemWindow0.Close();
                            gl.IsExpanded = true; // Ensure that the new child is visible
                                                  //using (var uow = new SainaDbContext())
                                                  //{
                                                  //    uow.TLs.Add(t);
                                                  //    //uow.Entry(gl).State = EntityState.Modified;
                                                  //    var vv = uow.SaveChanges();
                                                  //    addTLTreeItemWindow0.Close();
                                                  //}
                    };
                        addTLTreeItemWindow0.Width = 1000;
                        addTLTreeItemWindow0.Height = 500;
                        addTLTreeItemWindow0.CanClose = true;
                        addTLTreeItemWindow0.Owner = Window.GetWindow(this);
                        addTLTreeItemWindow0.Show();
                    }
                }
                else if (tag == "edit")
                {
                    if (_accessUtility.HasAccess(71))
                    {
                        AddGLTreeItemWindow addGLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddGLTreeItemWindow>();
                        var treeGL = addGLTreeItemWindow0.DataContext as AddGLTreeItemWindowViewModel;
                        treeGL.GL = gl;
                        using (var uow = new SainaDbContext())
                        {
                            var hasGL = uow.GLs.FirstOrDefault(x => x.GLId == gl.GLId)?.TLs?.Any() == true;
                            if (hasGL == true)
                            {
                                addGLTreeItemWindow0.gLCodeTextbox.IsEnabled = false;
                            }
                            treeGL.SaveClicked += (g) =>
                            {
                            //addTLTreeItemWindow0.DataItem.GL = gl;
                            g.GLId = gl.GLId;
                                g.ImageUrl = "../../Resources/cian.png";
                            // gl.TLs.Add(addTLTreeItemWindow0.DataItem);
                            gl.IsExpanded = true; // Ensure that the new child is visible
                                                  // _vm.Save();

                            uow.GLs.Attach(g);
                                uow.Entry<GL>(g).State = EntityState.Modified;
                            //  uow.GLs(g);
                            var x = uow.SaveChanges();
                                addGLTreeItemWindow0.Close();
                            };
                        }
                        addGLTreeItemWindow0.Width = 1000;
                        addGLTreeItemWindow0.Height = 500;
                        addGLTreeItemWindow0.CanClose = true;
                        addGLTreeItemWindow0.Owner = Window.GetWindow(this);
                        addGLTreeItemWindow0.Show();
                    }
                    // break;
                }
                else if (tag == "delete")
                {
                    if (_accessUtility.HasAccess(72))
                    {
                        using (var uow = new SainaDbContext())
                        {
                            var hasGL = uow.GLs.FirstOrDefault(x => x.GLId == gl.GLId)?.TLs?.Any() == true;

                            if (hasGL == false)
                            {

                                DialogParameters parameters = new DialogParameters();
                                parameters.OkButtonContent = "بله، مطمئنم";
                                parameters.CancelButtonContent = "خیر";
                                parameters.Header = "اخطار";
                                parameters.Content = "آیا برای حذف  مطمئن هستید؟";
                                parameters.Closed = OnClosed;
                                RadWindow.Confirm(parameters);
                                _dialogResult = _dialogResult == true;
                                // _dialogResult == true;
                            }
                            else
                            {
                                DialogParameters parameters = new DialogParameters();
                                parameters.OkButtonContent = "بستن";
                                parameters.Header = "اخطار";
                                parameters.Content = ".امکان حذف وجود ندارد";
                                // parameters.Closed = OnClosed;
                                RadWindow.Alert(parameters);
                                _dialogResult = false;
                            }
                            if (_dialogResult == true)
                            {
                                //  uow.GLs.Attach(gl);
                                var test = uow.GLs.ToList().Select(x => x.GLId);
                                _vm.Items.Remove(gl);
                                uow.Database.ExecuteSqlCommand($"Delete Info.GLs where  GLId={gl.GLId} ");
                                // uow.RejectChanges();
                                // uow.GLs.Attach(new GL { GLId = gl.GLId });
                                //uow.Entry<GL>(gl).State = EntityState.Deleted;
                                //// uow.GLs.Remove(new GL { GLId=gl.GLId});
                                uow.SaveChanges();
                                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

                                var alert = new RadDesktopAlert
                                {
                                    FlowDirection = FlowDirection.RightToLeft,
                                    Header = "اطلاعات",
                                    Content = ".حذف با موفقیت انجام شد",
                                    ShowDuration = 5000,
                                };
                                manager.ShowAlert(alert);
                            }
                        }
                    }
                }
            }
            else if (ClickedTreeViewItem.DataContext is TL tl)
            {
                if (tag == "newTL")
                {
                    if (_accessUtility.HasAccess(70))
                    {
                        AddTLTreeItemWindow addTLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddTLTreeItemWindow>();
                        gl = tl.GL;
                        var treeTL = addTLTreeItemWindow0.DataContext as AddTLTreeItemWindowViewModel;
                        treeTL.TL = new TL();
                        treeTL.TL.GLId = gl.GLId;
                        treeTL.TL.GL = gl;
                        //treeTL.SelectedGL = gl;
                        //treeTL.TL = new TL { GL = gl, GLId = gl.GLId };
                        treeTL.SaveClicked += (t) =>
                        {
                        //   t.GLId = gl.GLId;
                        t.ImageUrl = "../../Resources/cian.png";
                            _vm.AddTL(t);
                            t.GLId = _vm.GetTLId(t.TLCode);
                            gl.TLs.Add(t);
                            addTLTreeItemWindow0.Close();

                            gl.IsExpanded = true; // Ensure that the new child is visible
                                                  //using (var uow = new SainaDbContext())
                                                  //{
                                                  //    uow.TLs.Add(t);
                                                  //   // uow.Entry(gl).State = EntityState.Modified;

                        //    uow.SaveChanges();
                        //    addTLTreeItemWindow0.Close();
                        //}
                    };
                        addTLTreeItemWindow0.Width = 1000;
                        addTLTreeItemWindow0.Height = 500;
                        addTLTreeItemWindow0.CanClose = true;
                        addTLTreeItemWindow0.Owner = Window.GetWindow(this);
                        addTLTreeItemWindow0.Show();
                    }
                }
                else if (tag == "newSL")
                {
                    if (_accessUtility.HasAccess(70))
                    {
                        AddSLTreeItemWindow addSLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddSLTreeItemWindow>();
                        var treeSL = addSLTreeItemWindow0.DataContext as AddSLTreeItemWindowViewModel;
                        treeSL.SL = new SL();
                        treeSL.SL.TLId = tl.TLId;
                        treeSL.SL.TL = tl;
                        //treeSL.SelectedTL = tl;
                        //treeSL.SL = new SL { TL = tl, TLId = tl.TLId };
                        treeSL.SaveClicked += (s) =>
                        {
                        //addTLTreeItemWindow0.DataItem.GL = gl;
                        // s.TLId = tl.TLId;
                        s.ImageUrl = "../../Resources/cian.png";
                            _vm.AddSL(s);
                            s.TLId = _vm.GetSLId(s.SLCode);

                            tl.SLs.Add(s);
                        // _vm.Save();
                        addSLTreeItemWindow0.Close();
                            tl.IsExpanded = true; // Ensure that the new child is visible
                                                  //using (var uow = new SainaDbContext())
                                                  //{
                                                  //    uow.SLs.Add(s);
                                                  //    uow.Entry(tl).State = EntityState.Modified;

                        //    uow.SaveChanges();
                        //    addSLTreeItemWindow0.Close();
                        //}
                    };
                        addSLTreeItemWindow0.Width = 1000;
                        addSLTreeItemWindow0.Height = 500;
                        addSLTreeItemWindow0.CanClose = true;
                        addSLTreeItemWindow0.Owner = Window.GetWindow(this);
                        addSLTreeItemWindow0.Show();
                    }
                }
                else if (tag == "edit")
                {
                    if (_accessUtility.HasAccess(71))
                    {
                        AddTLTreeItemWindow addTLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddTLTreeItemWindow>();
                        var treeTL = addTLTreeItemWindow0.DataContext as AddTLTreeItemWindowViewModel;
                        treeTL.TL = tl;
                        using (var uow = new SainaDbContext())
                        {
                            var hasTL = uow.TLs.FirstOrDefault(x => x.TLId == tl.TLId)?.SLs?.Any() == true;
                            if (hasTL == true)
                            {
                                addTLTreeItemWindow0.tLCodeTextbox.IsEnabled = false;
                            }
                            treeTL.SaveClicked += (t) =>
                            {
                            //addTLTreeItemWindow0.DataItem.GL = gl;
                            t.TLId = tl.TLId;
                                t.ImageUrl = "../../Resources/cian.png";
                            // gl.TLs.Add(addTLTreeItemWindow0.DataItem);
                            tl.IsExpanded = true; // Ensure that the new child is visible

                            uow.TLs.Attach(t);
                                uow.Entry<TL>(t).State = EntityState.Modified;
                            //  uow.GLs(g);
                            //  uow.GLs(g);
                            uow.SaveChanges();
                                addTLTreeItemWindow0.Close();

                            };
                        }
                        addTLTreeItemWindow0.Width = 1000;
                        addTLTreeItemWindow0.Height = 500;
                        addTLTreeItemWindow0.CanClose = true;
                        addTLTreeItemWindow0.Owner = Window.GetWindow(this);
                        addTLTreeItemWindow0.Show();
                    }
                }
                else if (tag == "delete")
                {
                    if (_accessUtility.HasAccess(72))
                    {
                        using (var uow = new SainaDbContext())
                        {
                            var hasTL = uow.TLs.FirstOrDefault(x => x.TLId == tl.TLId)?.SLs?.Any() == true;

                            if (hasTL == false)
                            {

                                DialogParameters parameters = new DialogParameters();
                                parameters.OkButtonContent = "بله، مطمئنم";
                                parameters.CancelButtonContent = "خیر";
                                parameters.Header = "اخطار";
                                parameters.Content = "آیا برای حذف  مطمئن هستید؟";
                                parameters.Closed = OnClosed;
                                RadWindow.Confirm(parameters);
                                _dialogResult = _dialogResult == true;
                                // _dialogResult == true;
                            }
                            else
                            {
                                DialogParameters parameters = new DialogParameters();
                                parameters.OkButtonContent = "بستن";
                                parameters.Header = "اخطار";
                                parameters.Content = ".امکان حذف وجود ندارد";
                                // parameters.Closed = OnClosed;
                                RadWindow.Alert(parameters);
                                _dialogResult = false;
                            }
                            if (_dialogResult == true)
                            {
                                //  uow.GLs.Attach(gl);
                                //   var test = uow.TLs.ToList().Select(x => x.TLId);
                                foreach (var item in _vm.Items)
                                {
                                    item.TLs.Remove(tl);
                                }
                                uow.Database.ExecuteSqlCommand($"Delete Info.TLs where  TLId={tl.TLId} ");
                                // uow.RejectChanges();
                                // uow.GLs.Attach(new GL { GLId = gl.GLId });
                                //uow.Entry<GL>(gl).State = EntityState.Deleted;
                                //// uow.GLs.Remove(new GL { GLId=gl.GLId});
                                uow.SaveChanges();
                                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

                                var alert = new RadDesktopAlert
                                {
                                    FlowDirection = FlowDirection.RightToLeft,
                                    Header = "اطلاعات",
                                    Content = ".حذف با موفقیت انجام شد",
                                    ShowDuration = 5000,
                                };
                                manager.ShowAlert(alert);
                            }
                        }
                    }
                }
            }
            else if (ClickedTreeViewItem.DataContext is SL)
            {
                SL sl = ClickedTreeViewItem.DataContext as SL;

                if (tag == "newSL")
                {
                    if (_accessUtility.HasAccess(70))
                    {
                        AddSLTreeItemWindow addSLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddSLTreeItemWindow>();

                        tl = sl.TL;
                        var treeSL = addSLTreeItemWindow0.DataContext as AddSLTreeItemWindowViewModel;
                        treeSL.SL = new SL();
                        treeSL.SL.TLId = tl.TLId;
                        treeSL.SL.TL = tl;
                        treeSL.SaveClicked += (s) =>
                            {

                                s.TLId = tl.TLId;
                                s.ImageUrl = "../../Resources/cian.png";
                                tl.SLs.Add(s);
                                _vm.AddSL(s);
                            // _vm.Save();
                            addSLTreeItemWindow0.Close();

                                tl.IsExpanded = true; // Ensure that the new child is visible
                                                      //using (var uow = new SainaDbContext())
                                                      //{
                                                      //    uow.SLs.Add(s);
                                                      ////    uow.Entry(tl).State = EntityState.Modified;

                            //    uow.SaveChanges();
                            //    addSLTreeItemWindow0.Close();
                            //}

                        };


                        addSLTreeItemWindow0.Width = 1000;
                        addSLTreeItemWindow0.Height = 500;
                        addSLTreeItemWindow0.CanClose = true;
                        addSLTreeItemWindow0.Owner = Window.GetWindow(this);
                        addSLTreeItemWindow0.Show();
                    }
                }
                else if (tag == "edit")
                {
                    if (_accessUtility.HasAccess(71))
                    {
                        AddSLTreeItemWindow addSLTreeItemWindow0 = SmObjectFactory.Container.GetInstance<AddSLTreeItemWindow>();
                        var treeSL = addSLTreeItemWindow0.DataContext as AddSLTreeItemWindowViewModel;
                        treeSL.SL = sl;
                        using (var uow = new SainaDbContext())
                        {
                            var hasItem = uow.AccDocumentItems.Any(x => x.SLId == sl.SLId);
                            if (hasItem == true)
                            {
                                addSLTreeItemWindow0.sLCodeTextbox.IsEnabled = false;
                                addSLTreeItemWindow0.DL1.IsEnabled = false;
                                addSLTreeItemWindow0.DL2.IsEnabled = false;
                            }
                            treeSL.SaveClicked += (s) =>
                            {
                            //addTLTreeItemWindow0.DataItem.GL = gl;
                            s.SLId = sl.SLId;
                                s.ImageUrl = "../../Resources/cian.png";
                            // gl.TLs.Add(addTLTreeItemWindow0.DataItem);
                            sl.IsExpanded = true; // Ensure that the new child is visible

                            uow.SLs.Attach(s);
                                uow.Entry<SL>(s).State = EntityState.Modified;
                            //  uow.GLs(g);
                            uow.SaveChanges();
                                addSLTreeItemWindow0.Close();

                            };
                        }
                        addSLTreeItemWindow0.Width = 1000;
                        addSLTreeItemWindow0.Height = 500;
                        addSLTreeItemWindow0.CanClose = true;
                        addSLTreeItemWindow0.Owner = Window.GetWindow(this);
                        addSLTreeItemWindow0.Show();
                    }
                }
                else if (tag == "delete")
                {
                    if (_accessUtility.HasAccess(72))
                    {
                        using (var uow = new SainaDbContext())
                        {
                            var hasItem = uow.AccDocumentItems.Any(x => x.SLId == sl.SLId);


                            if (hasItem == false)
                            {

                                DialogParameters parameters = new DialogParameters();
                                parameters.OkButtonContent = "بله، مطمئنم";
                                parameters.CancelButtonContent = "خیر";
                                parameters.Header = "اخطار";
                                parameters.Content = "آیا برای حذف  مطمئن هستید؟";
                                parameters.Closed = OnClosed;
                                RadWindow.Confirm(parameters);
                                _dialogResult = _dialogResult == true;
                                // _dialogResult == true;
                            }
                            else
                            {
                                DialogParameters parameters = new DialogParameters();
                                parameters.OkButtonContent = "بستن";
                                parameters.Header = "اخطار";
                                parameters.Content = ".امکان حذف وجود ندارد";
                                // parameters.Closed = OnClosed;
                                RadWindow.Alert(parameters);
                                _dialogResult = false;
                            }
                            if (_dialogResult == true)
                            {
                                //  uow.GLs.Attach(gl);
                                //   var test = uow.TLs.ToList().Select(x => x.TLId);
                                foreach (var item in _vm.Items)
                                {
                                    foreach (var x in item.TLs)
                                    {

                                        if (x.SLs.Remove(sl))
                                            break;

                                    }
                                }
                                uow.Database.ExecuteSqlCommand($"Delete Info.SLs where  SLId={sl.SLId} ");
                                // uow.RejectChanges();
                                // uow.GLs.Attach(new GL { GLId = gl.GLId });
                                //uow.Entry<GL>(gl).State = EntityState.Deleted;
                                //// uow.GLs.Remove(new GL { GLId=gl.GLId});
                                uow.SaveChanges();
                                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

                                var alert = new RadDesktopAlert
                                {
                                    FlowDirection = FlowDirection.RightToLeft,
                                    Header = "اطلاعات",
                                    Content = ".حذف با موفقیت انجام شد",
                                    ShowDuration = 5000,
                                };
                                manager.ShowAlert(alert);
                            }
                        }
                    }
                }
            }
        }

    }


}

