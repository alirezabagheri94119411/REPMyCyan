using Microsoft.Win32;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Context;
using Saina.WPF.Behaviors;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Telerik.Windows.Persistence;
using Telerik.Windows.Persistence.Storage;

namespace Saina.WPF.BasicInformation.Accounts.TLAccount
{
    /// <summary>
    /// Interaction logic for TLListView.xaml
    /// </summary>
    public partial class TLListView : UserControl
    {
        private AccessUtility _accessUtility;
        private TLListViewModel _viewModel;
        private bool? _dialogResult;
        private ITLsService _tLsService;
        private IsolatedStorageProvider isolatedStorageProvider;

        public TLListView()
        {
            isolatedStorageProvider = new IsolatedStorageProvider();

            _tLsService = SmObjectFactory.Container.GetInstance<ITLsService>();

            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _viewModel = DataContext as TLListViewModel;
                _viewModel.Error += OnError;
                _viewModel.LoadTLs();
                //  RaiseCanExecuteChanged();
                //NavStateFalse();
                //var addNewTLCommand = RadDataFormCommands.AddNew as RoutedUICommand;
                TLDataForm.CommandProvider = new CustomCommandProvider();

                //addNewTLCommand.Execute(null, TLDataForm);
                TLRadGridView.SelectedItem = null;
                detailRadTabItem.IsSelected = true;
                //   DataContext = _viewModel;

                isolatedStorageProvider.LoadFromStorage();
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Error -= OnError;

                isolatedStorageProvider.SaveToStorage();
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
      

        #region Report
        private void Report()
        {
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            if (TLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (TLRadGridView.SelectedItems.Any())
                        data = TLRadGridView.SelectedItems.Cast<TL>()

                       .Select(tl => new
                       {
                           TLCode = tl.TLCode,
                           GLTitle = tl.GL.Title,
                           Title = tl.Title,
                           Title2 = tl.Title2,
                           Status = tl.Status == true ? "فعال" : "غیرفعال",
                       }).ToList();
                    else
                    {
                        data = _viewModel.TLs.Cast<TL>()
                            .Select(tl => new
                            {
                                TLCode = tl.TLCode,
                                GLTitle = tl.GL.Title,
                                Title = tl.Title,
                                Title2 = tl.Title2,
                                Status = tl.Status == true ? "فعال" : "غیرفعال",
                            }).ToList();

                    }
                    report.RegBusinessObject("TLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\tlReport.mrt");
                    report.Show();
                }
            }
        }
        private void ShowReportAll()
        {
            if (TLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    
                        data = _viewModel.TLs.Cast<TL>()
                            .Select(tl => new
                            {
                                TLCode = tl.TLCode,
                                GLTitle = tl.GL.Title,
                                Title = tl.Title,
                                Title2 = tl.Title2,
                                Status = tl.Status == true ? "فعال" : "غیرفعال",
                            }).ToList();

                   
                    report.RegBusinessObject("TLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\tlReport.mrt");
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
                var data = TLRadGridView.SelectedItems.Cast<TL>().ToList()
                      .Select(tl => new
                      {
                          TLCode = tl.TLCode,
                          GLTitle = tl.GL.Title,
                          Title = tl.Title,
                          Title2 = tl.Title2,
                          Status = tl.Status == true ? "فعال" : "غیرفعال",
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
                var path = $"{Environment.CurrentDirectory}\\Report\\tlReport.mrt";
                report.RegBusinessObject("TLReport", data);
                if (!File.Exists(path))
                    File.Copy($"{Environment.CurrentDirectory}\\Report\\Report.mrt", path, false);
                report.Load(path);
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
                var data = TLRadGridView.SelectedItems.Cast<TL>()
                       .Select(tl => new
                       {
                           TLCode = tl.TLCode,
                           GLTitle = tl.GL.Title,
                           Title = tl.Title,
                           Title2 = tl.Title2,
                           Status = tl.Status == true ? "فعال" : "غیرفعال",
                           //  AccountTLEnum = gl.AccountTLEnum
                       });
                report.RegBusinessObject("TLReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\tlReport.mrt");
                report.Print();
            }
        }
        private void PrintReportAll()
        {
            if (TLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;

                    data = _viewModel.TLs.Cast<TL>()
                        .Select(tl => new
                        {
                            TLCode = tl.TLCode,
                            GLTitle = tl.GL.Title,
                            Title = tl.Title,
                            Title2 = tl.Title2,
                            Status = tl.Status == true ? "فعال" : "غیرفعال",
                        }).ToList();


                    report.RegBusinessObject("TLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\tlReport.mrt");
                    report.Print();
                }
            }
        }



        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
            
        
        }
        #endregion
        #region GridView
        private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            if (detailRadTabItem.IsSelected && TLRadGridView.SelectedItem != null)
            {
                //NavStateFalse();
                //TLDataForm.CommitEdit();
               BeginEdit();

            }
        }
        private void TLRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            detailRadTabItem.IsSelected = true;
        }

        private void TLRadGridView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }

        //private void TLRadGridView_FilterOperatortLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatortLoadingEventArgs e)
        //{
        //    e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
        //    e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
        //    e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        //}
        private void TLDataForm_CurrentItemChanged(object sender, EventArgs e)
        {

        }

        private void TLDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                _viewModel.Save();
                commitEditCommand.Execute(null, TLDataForm);
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = "اطلاعات با موفقیت ثبت شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
                //  if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
                //  _viewModel.Save();
            }
            else if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel)
            {
                _viewModel.Reject();

            }//if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            //    _viewModel.Save();
        }
        #endregion
        #region Add
        public TL TLItem { get; set; }
        private void TLDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            if (_accessUtility.HasAccess(6))
            {
                var x = e.NewItem as TL;
                TLItem = ((TL)e.NewItem);
                _viewModel.AddTLCommand.Execute(null);
                //if (_viewModel.EnableSave==true)
                //{
                //x.TLCode = _viewModel.TL.TLCode;
                //x.Title = _viewModel.TL.Title;
                //x.Title2 = _viewModel.TL.Title2;
                //x.Status = _viewModel.TL.Status;
                // x.GL.GLId = _viewModel.TL.GL.GLId;

                // x.AccountTLEnum = _viewModel.TL.AccountTLEnum;
                _viewModel.AddTL((TL)e.NewItem);
            }
            //}
            //else
            //{
            //    saveButton.IsEnabled = false;
            //}

        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //NavStateFalse();
            TLDataForm.AddNewItem();
        }
        #endregion
        #region Delete
        private void TLDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_accessUtility.HasAccess(8))
            {
                var r = _viewModel.DeleteTL((TL));
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
        private void TLDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
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
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //RaiseCanExecuteChanged();
            //NavStateTrue();
            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(TLRadGridView.SelectedItem, TLRadGridView);
        }
        #endregion
        #region Validation
        //public void NavStateFalse()
        //{
        //    addButton.IsEnabled = false;
        //    saveButton.IsEnabled = true;
        //    //deleteButton.IsEnabled = false;
        //    cancelButton.IsEnabled = true;
        //    editButton.IsEnabled = false;
        //    firstButton.IsEnabled = false;
        //    nextButton.IsEnabled = false;
        //    lastButton.IsEnabled = false;
        //    previousButton.IsEnabled = false;
        //}
        //public void NavStateTrue()
        //{
        //    firstButton.IsEnabled = true;
        //    nextButton.IsEnabled = true;
        //    lastButton.IsEnabled = true;
        //    previousButton.IsEnabled = true;
        //}
        //private void RaiseCanExecuteChanged()
        //{
        //    addButton.IsEnabled = true;
        //    saveButton.IsEnabled = false;
        //    //deleteButton.IsEnabled = true;
        //    cancelButton.IsEnabled = false;
        //    editButton.IsEnabled = true;

        //}
        private void TLDataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (ValidateTL())
            {

                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        TL TL => TLDataForm.CurrentItem as TL;
        private bool ValidateTL()
        {
            if (TL==null)
            {
                return true;
            }
            var result = true;
            var errorMessage = "";
       //     TLDataForm.ValidationSummary.Errors.Clear();
            if (_tLsService.HasTitle(TL.Title, TL.TLId))
            {
                errorMessage += $"عنوان نباید تکراری باشد {Environment.NewLine}";

            }
            if (_tLsService.Hasduplicate(TL.TLCode, TL.TLId))
            {
                errorMessage += $"کد حساب نباید تکراری باشد {Environment.NewLine}";

            }

            if (TL.TLCode == 0)
            {
                errorMessage += $"کد حساب نباید 0 باشد {Environment.NewLine}";

            }
            if (TL.Title == null || TL.Title == "")
            {
                errorMessage += $"عنوان حساب نباید خالی باشد {Environment.NewLine}";

            }
            if (errorMessage.Length > 0)
            {
                result = false;
                MessageBox.Show(errorMessage);
            }
            return result;
        }
        #endregion
        #region Navigation
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            TLDataForm.MoveCurrentToFirst();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            TLDataForm.MoveCurrentToLast();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            TLDataForm.MoveCurrentToNext();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            TLDataForm.MoveCurrentToPrevious();
        }
        #endregion
        #region SaveAndCancel
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTL())
            {
                //RaiseCanExecuteChanged();
                //NavStateTrue();
                CommitAndBeginEdit();
                //TLDataForm.CommitEdit();
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
                commitEditCommand.Execute(null, TLDataForm);
                //TLDataForm.BeginEdit();
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
                if (TLDataForm != null)
                {
                    //RaiseCanExecuteChanged();
                    //NavStateTrue();
                    TLDataForm.CancelEdit();
                  //  TLDataForm.TLDataForm.BeginEdit();
                }
            }

        }
        #endregion
        #region Textbox
        private void tLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Focus();
        }
        #endregion
        private void RadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //tLCodeTextbox.IsEnabled = true;
            //tLCodeTextbox.Focus();
            //tLCodeTextbox.CaretIndex = tLCodeTextbox.Text.Length;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //NavStateFalse();
            (sender as FrameworkElement).Focus();

           BeginEdit();
        }
        private void BeginEdit()
        {
            var hasSL = ((TL)TLDataForm.CurrentItem).SLs?.Any() == true;
            if (hasSL == true)
            {
                _viewModel.Code = false;
            }
            else
            {
                _viewModel.Code = true;
            }
            //  ((GL)GLDataForm.CurrentItem).TLs
            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
            beginEditCommand.Execute(null, TLDataForm);

        }
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
                var col0Visibility = TLRadGridView.Columns[0].IsVisible;
                TLRadGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    TLRadGridView.Export(stream,
                     opt);
                }
                TLRadGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
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

        private void TLDataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_accessUtility.HasAccess(7))
            {
                e.Cancel = true;
            }
            }
        //private void tLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    ((TextBox)sender).SelectAll();
        //}
    }
}
