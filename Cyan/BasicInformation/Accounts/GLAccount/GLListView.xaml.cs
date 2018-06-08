using Microsoft.Win32;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Context;
using Saina.WPF.Behaviors;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
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
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Persistence.Storage;

namespace Saina.WPF.BasicInformation.Accounts.GLAccount
{
    /// <summary>
    /// Interaction logic for GLListView.xaml
    /// </summary>
    public partial class GLListView : UserControl
    {
        private AccessUtility _accessUtility;
        private GLListViewModel _viewModel;
        private IGLsService _gLsService;
        private bool? _dialogResult;
        private IsolatedStorageProvider isolateStorageProvider;
        // private ICompanyInformationsService companyInformationsService;

        public GLListView()
        {
            isolateStorageProvider = new IsolatedStorageProvider();

            _gLsService = SmObjectFactory.Container.GetInstance<IGLsService>();
            //    companyInformationsService = SmObjectFactory.Container.GetInstance<ICompanyInformationsService>();
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _viewModel = DataContext as GLListViewModel;
                _viewModel.Failed += OnFailed;
                _viewModel.Error += OnError;
                _viewModel.LoadGLs();
                GLDataForm.CommandProvider = new CustomCommandProvider();
                //
                //GLDataForm.AddNewItem();
                GLRadGridView.SelectedItem = null;
                detailRadTabItem.IsSelected = true;

              //  isolateStorageProvider.LoadFromStorage();
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Failed -= OnFailed;
                _viewModel.Error -= OnError;

              //  isolateStorageProvider.SaveToStorage();
            };
        }
        private void OnError(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اخطار";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }
        private void OnFailed(Exception ex)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = ex.Message;
            RadWindow.Alert(parameters);
        }

        #region Report

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            if (GLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (GLRadGridView.SelectedItems.Any())
                        data = GLRadGridView.SelectedItems.Cast<GL>()

                               .Select(gl => new
                               {
                                   GLCode = gl.GLCode,
                                   Title = gl.Title,
                                   Title2 = gl.Title2,
                                   Status = gl.Status == true ? "فعال" : "غیرفعال",
                                   AccountGLEnum = gl.AccountGLEnum == AccountGLEnum.BalanceSheet ? "ترازنامه ای" : gl.AccountGLEnum == AccountGLEnum.Disciplinary ? "انتظامی" : gl.AccountGLEnum == AccountGLEnum.ProfitLoss ? "سود و زیانی" : "",
                                   Address = _viewModel.CompanyInformationModel.Address
                               }).ToList();
                    else
                    {
                        data = _viewModel.GLs.Cast<GL>()
                            .Select(gl => new
                            {
                                GLCode = gl.GLCode,
                                Title = gl.Title,
                                Title2 = gl.Title2,
                                Status = gl.Status == true ? "فعال" : "غیرفعال",
                                AccountGLEnum = gl.AccountGLEnum == AccountGLEnum.BalanceSheet ? "ترازنامه ای" : gl.AccountGLEnum == AccountGLEnum.Disciplinary ? "انتظامی" : gl.AccountGLEnum == AccountGLEnum.ProfitLoss ? "سود و زیانی" : "",
                                Address = _viewModel.CompanyInformationModel.Address

                            }).ToList();

                    }
                    report.RegBusinessObject("GLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\glReport.mrt");
                    report.Show();
                }
            }
        }
        private void ShowReportAll()
        {
            if (GLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    data = _viewModel.GLs.Cast<GL>()
                        .Select(gl => new
                        {
                            GLCode = gl.GLCode,
                            Title = gl.Title,
                            Title2 = gl.Title2,
                            Status = gl.Status == true ? "فعال" : "غیرفعال",
                            AccountGLEnum = gl.AccountGLEnum == AccountGLEnum.BalanceSheet ? "ترازنامه ای" : gl.AccountGLEnum == AccountGLEnum.Disciplinary ? "انتظامی" : gl.AccountGLEnum == AccountGLEnum.ProfitLoss ? "سود و زیانی" : "",
                            Address = _viewModel.CompanyInformationModel.Address
                        }).ToList();
                    report.RegBusinessObject("GLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\glReport.mrt");
                    report.Show();
                }
            }
        }
        private void designButton_Click(object sender, RoutedEventArgs e)
        {
            DesignReport();
        }
        private void DesignReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var AddressCompany = _viewModel.CompanyInformationModel.Address;
                var CityCompany = _viewModel.CompanyInformationModel.City;
                var EconomicStoreCodeCompany = _viewModel.CompanyInformationModel.EconomicStoreCode;
                var FaxCompany = _viewModel.CompanyInformationModel.Fax;
                var LogoCompany = _viewModel.CompanyInformationModel.Logo;
                var ManagerNameCompany = _viewModel.CompanyInformationModel.ManagerName;
                var MobileCompany = _viewModel.CompanyInformationModel.Mobile;
                var PhoneCompany = _viewModel.CompanyInformationModel.Phone;
                var Phone2Company = _viewModel.CompanyInformationModel.Phone2;
                var PostalCodeCompany = _viewModel.CompanyInformationModel.PostalCode;
                var ProvinceCompany = _viewModel.CompanyInformationModel.Province;
                var StoreNameCompany = _viewModel.CompanyInformationModel.StoreName;
                var StoreCodeCompany = _viewModel.CompanyInformationModel.StoreCode;
                var TownCompany = _viewModel.CompanyInformationModel.Town;
                var WebSiteCompany = _viewModel.CompanyInformationModel.WebSite;

                var data = GLRadGridView.SelectedItems.Cast<GL>().ToList()
                      .Select(gl => new
                      {
                          GLCode = gl.GLCode,
                          Title = gl.Title,
                          Title2 = gl.Title2,
                          Status = gl.Status == true ? "فعال" : "غیرفعال",
                          AccountGLEnum = gl.AccountGLEnum == AccountGLEnum.BalanceSheet ? "ترازنامه ای" : gl.AccountGLEnum == AccountGLEnum.Disciplinary ? "انتظامی" : gl.AccountGLEnum == AccountGLEnum.ProfitLoss ? "سود و زیانی" : "",
                          AddressCompany = AddressCompany,
                          CityCompany = CityCompany,
                          EconomicStoreCode = EconomicStoreCodeCompany,
                          FaxCompany = FaxCompany,
                          LogoCompany = LogoCompany,
                          ManagerNameCompany = ManagerNameCompany,
                          MobileCompany = MobileCompany,
                          PhoneCompany = PhoneCompany,
                          Phone2Company = Phone2Company,
                          PostalCodeCompany = PostalCodeCompany,
                          ProvinceCompany = ProvinceCompany,
                          StoreNameCompany = StoreNameCompany,
                          StoreCodeCompany = StoreCodeCompany,
                          TownCompany = TownCompany,
                          WebSiteCompany = WebSiteCompany,
                      });                    



                var path = $"{Environment.CurrentDirectory}\\Report\\glReport.mrt";
                report.RegBusinessObject("GLReport", data);

                if (!File.Exists(path))
                    File.Copy($"{Environment.CurrentDirectory}\\Report\\Report.mrt", path, false);
                report.Load(path);
                //  report.Dictionary.Variables["Variable2"].Value = DateTime.Now.ToShortDateString();
                report.Design();
            }
        }
        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }
        private void PrintReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = GLRadGridView.SelectedItems.Cast<GL>()
                       .Select(gl => new
                       {
                           GLCode = gl.GLCode,
                           Title = gl.Title,
                           Title2 = gl.Title2,
                           Status = gl.Status == true ? "فعال" : "غیرفعال",
                           AccountGLEnum = gl.AccountGLEnum == AccountGLEnum.BalanceSheet ? "ترازنامه ای" : gl.AccountGLEnum == AccountGLEnum.Disciplinary ? "انتظامی" : gl.AccountGLEnum == AccountGLEnum.ProfitLoss ? "سود و زیانی" : "",
                           Address = _viewModel.CompanyInformationModel.Address

                       });
                report.RegBusinessObject("GLReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\glReport.mrt");
                report.Print();
            }
        }
        private void PrintReportAll()
        {
            if (GLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    data = _viewModel.GLs.Cast<GL>()
                        .Select(gl => new
                        {
                            GLCode = gl.GLCode,
                            Title = gl.Title,
                            Title2 = gl.Title2,
                            Status = gl.Status == true ? "فعال" : "غیرفعال",
                            AccountGLEnum = gl.AccountGLEnum == AccountGLEnum.BalanceSheet ? "ترازنامه ای" : gl.AccountGLEnum == AccountGLEnum.Disciplinary ? "انتظامی" : gl.AccountGLEnum == AccountGLEnum.ProfitLoss ? "سود و زیانی" : "",
                            Address = _viewModel.CompanyInformationModel.Address
                        }).ToList();
                    report.RegBusinessObject("GLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\glReport.mrt");
                    report.Print();
                }
            }

        }
        #endregion
        #region GridView
        private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            if (detailRadTabItem.IsSelected && GLRadGridView.SelectedItem != null)
            {
                BeginEdit();

            }
            //else
            //{
            //    GLDataForm.AddNewItem();
            //}
        }

        private void GLRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            detailRadTabItem.IsSelected = true;
        }

        private void GLRadGridView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }

        private void GLRadGridView_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        }
        private void GLDataForm_CurrentItemChanged(object sender, EventArgs e)
        {
            //GLDataForm.CancelEdit();
            //_viewModel.Reject();
        }

        private void GLDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                _viewModel.Save();
                commitEditCommand.Execute(null, GLDataForm);
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = "اطلاعات با موفقیت ثبت شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
                //  GLDataForm.CommitEdit();

                // _viewModel.SaveCommand
                //  if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
                //  _viewModel.Save();
            }
            else if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel)
            {
                _viewModel.Reject();

            }
        }
        #endregion
        #region Add
        private void GLDataForm_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {

        }
        private void GLDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            if (_accessUtility.HasAccess(3))
            {
            _viewModel.AddGL((GL)e.NewItem);
                
            }
        }

        private void BeginEdit()
        {
            var hasTL = ((GL)GLDataForm.CurrentItem).TLs?.Any() == true;
            if (hasTL == true)
            {
                _viewModel.Code = false;
            }
            else
            {
                _viewModel.Code = true;
            }
            //  ((GL)GLDataForm.CurrentItem).TLs
            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
            beginEditCommand.Execute(null, GLDataForm);

        }
        #endregion
        #region Delete
        private void GLDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_accessUtility.HasAccess(5))
            {
                var r = _viewModel.DeleteGL((GL));
                if (r > 0)
                {

                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بله، مطمئنم";
                    parameters.CancelButtonContent = "خیر";
                    parameters.Header = "اخطار";
                    parameters.Content = "آیا برای حذف  مطمئن هستید؟";
                    parameters.Closed = OnClosed;
                    RadWindow.Confirm(parameters);
                    e.Cancel = _dialogResult == false;
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
                    e.Cancel = true;
                }
            }
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
        private void GLDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {

            // var r = _viewModel.DeleteGL((GL));
            //// var r = _viewModel.DeleteGL((GL)e.DeletedItem);

            if (_dialogResult == true)
            {
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

            //else
            //{
            //    DialogParameters parameters = new DialogParameters();
            //    parameters.OkButtonContent = "بله";
            //    parameters.Header = "اخطار";
            //    parameters.Content = ".امکان حذف وجود ندارد";
            //    // parameters.Closed = OnClosed;
            //    RadWindow.Alert(parameters);
            //}

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //RaiseCanExecuteChanged();
            //NavStateTrue();
            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(null, GLDataForm);

        }
        #endregion
        #region Validation

        private void GLDataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidateGL())
            {

                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        GL GL => GLDataForm.CurrentItem as GL;
        private bool ValidateGL()
        {
            var result = true;
            var errorMessage = "";
            if (GL != null)
            {


                if (_gLsService.HasTitle(GL.Title, GL.GLId))
                {
                    errorMessage += $"عنوان نباید تکراری باشد {Environment.NewLine}";

                }
                if (_gLsService.Hasduplicate(GL.GLCode, GL.GLId))
                {
                    errorMessage += $"کد حساب نباید تکراری باشد {Environment.NewLine}";

                }

                if (GL.GLCode == 0)
                {
                    errorMessage += $"کد حساب نباید 0 باشد {Environment.NewLine}";

                }
                if (GL.Title == null || GL.Title == "")
                {
                    errorMessage += $"عنوان حساب نباید خالی باشد {Environment.NewLine}";

                }
                if (errorMessage.Length > 0)
                {

                    result = false;
                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بستن";
                    parameters.Header = "!اخطار";
                    parameters.Content = errorMessage;
                    RadWindow.Alert(parameters);
                }
                return result;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region Navigation
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            GLDataForm.MoveCurrentToFirst();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            GLDataForm.MoveCurrentToLast();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            GLDataForm.MoveCurrentToNext();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            GLDataForm.MoveCurrentToPrevious();
        }
        #endregion
        #region SaveAndCancel
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateGL())
            {

                //if (_viewModel.AddGLCommand.CanExecute(null))
                //{
                //    RaiseCanExecuteChanged();
                //}
                //else
                //{
                //    RaiseNoCanExecuteChanged();

                //}
                //NavStateTrue();
                GLDataForm.CommitEdit();
                _viewModel.Save();
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = "اطلاعات با موفقیت ثبت شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
            }// _viewModel.SaveCommand
        }
        private void CommitAndBeginEdit()
        {
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            if (_viewModel != null)
            {

                _viewModel.Save();
                commitEditCommand.Execute(null, GLDataForm);
                BeginEdit();
            }

        }
        private bool OnCanceling()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            return _dialogResult == true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            OnCanceling();
            if (_dialogResult == true)
            {
                if (GLDataForm != null)
                {
                    GLDataForm.CancelEdit();
                    _viewModel.Reject();
                }
                // GLDataForm.BeginEdit();
            }
        }



        #endregion
        #region Textbox
        private void gLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Focus();
        }






        #endregion

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string extension = "xls";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (.{0})|.{0}|All files (.)|.", extension, "Excel"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                var col0Visibility = GLRadGridView.Columns[0].IsVisible;
                GLRadGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    GLRadGridView.Export(stream,
                     opt);
                }
                GLRadGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // NavStateFalse();
            BeginEdit();
        }
        private void textbox_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Focus();
        }



        private void ListBoxItemReport_Selected(object sender, RoutedEventArgs e)
        {
            ShowReport();

            ReportMenu.IsOpen = false;
        }



        private void ListBoxItemReportALL_Selected(object sender, RoutedEventArgs e)
        {
            ShowReportAll();
            ReportMenu.IsOpen = false;
        }

        private void ListBoxItemPrintALL_Selected(object sender, RoutedEventArgs e)
        {
            PrintReportAll();
            PrintMenu.IsOpen = false;
        }

        private void ListBoxItemPrint_Selected(object sender, RoutedEventArgs e)
        {
            PrintReport();
            PrintMenu.IsOpen = false;

        }

        private void GLDataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
            {
            if (!_accessUtility.HasAccess(4))
            {
                e.Cancel = true;
            }

            }
    }

    public static class Command
    {
        public static RoutedCommand Add = new RoutedCommand();
        public static RoutedCommand Delete = new RoutedCommand();
        public static RoutedCommand Cancel = new RoutedCommand();
        public static RoutedCommand Save = new RoutedCommand();
        public static RoutedCommand Show = new RoutedCommand();
        public static RoutedCommand Design = new RoutedCommand();
        public static RoutedCommand Print = new RoutedCommand();

    }
}




