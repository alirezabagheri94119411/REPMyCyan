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

namespace Saina.WPF.BasicInformation.Accounts.SLAccount
{
    /// <summary>
    /// Interaction logic for SLListView.xaml
    /// </summary>
    public partial class SLListView : UserControl
    {
        private SLListViewModel _viewModel;
        private bool? _dialogResult;
        private AccessUtility _accessUtility;
        private ISLsService _sLsService;
       // private IsolatedStorageProvider isolatedStorageProvider;

        public SLListView()
        {
            // isolatedStorageProvider = new IsolatedStorageProvider();
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

            _sLsService = SmObjectFactory.Container.GetInstance<ISLsService>();
            InitializeComponent();
          //  isolatedStorageProvider = new IsolatedStorageProvider();

            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as SLListViewModel;
                //   _viewModel.Failed += OnFailed;
                _viewModel.Error += OnError;
                _viewModel.LoadSLs();
                SLDataForm.CommandProvider = new CustomCommandProvider();
                RaiseCanExecuteChanged();
              //  NavStateFalse();
                //var addNewSLCommand = RadDataFormCommands.AddNew as RoutedUICommand;
                //addNewSLCommand.Execute(null, SLDataForm);
                SLRadGridView.SelectedItem = null;

                detailRadTabItem.IsSelected = true;
                //   DataContext = _viewModel;

               // isolatedStorageProvider.LoadFromStorage();
            };
            Unloaded += (s, ea) =>
            {
                //  _viewModel.UnLoaded();
                //  _viewModel.Failed -= OnFailed;
                _viewModel.Error -= OnError;

              //  isolatedStorageProvider.SaveToStorage();
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
        //private void OnFailed(Exception ex)
        //{
        //    DialogParameters parameters = new DialogParameters();
        //    parameters.OkButtonContent = "بستن";
        //    parameters.Header = "اخطار";
        //    parameters.Content = ex.Message;
        //    RadWindow.Alert(parameters);
        //}
        #region Report

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            if (SLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (SLRadGridView.SelectedItems.Any())
                        data = SLRadGridView.SelectedItems.Cast<SL>()

                         .Select(sl => new
                         {
                             SLCode = sl.SLCode,
                             TLTitle = sl.TL.Title,
                             Title = sl.Title,
                             Title2 = sl.Title2,
                             Status = sl.Status == true ? "فعال" : "غیرفعال",
                             DLType1 = sl.DLType1 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType1 == DLTypeEnum.Company ? "شرکت" : sl.DLType1 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType1 == DLTypeEnum.Others ? "سایر" : sl.DLType1 == DLTypeEnum.People ? "اشخاص" : sl.DLType1 == DLTypeEnum.Project ? "پروژه" : "",
                             DLType2 = sl.DLType2 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType2 == DLTypeEnum.Company ? "شرکت" : sl.DLType2 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType2 == DLTypeEnum.Others ? "سایر"
                             : sl.DLType2 == DLTypeEnum.People ? "اشخاص" : sl.DLType2 == DLTypeEnum.Project ? "پروژه" : "",
                             AccountsNature = sl.AccountsNatureEnum == AccountsNatureEnum.Cred ? "بستانکار" : sl.AccountsNatureEnum == AccountsNatureEnum.Debt ? "بدهکار" : sl.AccountsNatureEnum == AccountsNatureEnum.None ? "مهم نیست" : "",
                             Property = sl.Property == PropertyEnum.Consistency ? "پیگیری" : sl.Property == PropertyEnum.Count ? "تعدادی" : sl.Property == PropertyEnum.Exchange ? "ارز" : sl.Property == PropertyEnum.Litter ? "تسعیر" : "",
                         }).ToList();
                    else
                    {
                        data = _viewModel.SLs.Cast<SL>()
                                 .Select(sl => new
                                 {
                                     SLCode = sl.SLCode,
                                     TLTitle = sl.TL.Title,
                                     Title = sl.Title,
                                     Title2 = sl.Title2,
                                     Status = sl.Status == true ? "فعال" : "غیرفعال",
                                     DLType1 = sl.DLType1 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType1 == DLTypeEnum.Company ? "شرکت" : sl.DLType1 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType1 == DLTypeEnum.Others ? "سایر" : sl.DLType1 == DLTypeEnum.People ? "اشخاص" : sl.DLType1 == DLTypeEnum.Project ? "پروژه" : "",
                                     DLType2 = sl.DLType2 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType2 == DLTypeEnum.Company ? "شرکت" : sl.DLType2 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType2 == DLTypeEnum.Others ? "سایر"
                             : sl.DLType2 == DLTypeEnum.People ? "اشخاص" : sl.DLType2 == DLTypeEnum.Project ? "پروژه" : "",
                                     AccountsNature = sl.AccountsNatureEnum == AccountsNatureEnum.Cred ? "بستانکار" : sl.AccountsNatureEnum == AccountsNatureEnum.Debt ? "بدهکار" : sl.AccountsNatureEnum == AccountsNatureEnum.None ? "مهم نیست" : "",
                                     Property = sl.Property == PropertyEnum.Consistency ? "پیگیری" : sl.Property == PropertyEnum.Count ? "تعدادی" : sl.Property == PropertyEnum.Exchange ? "ارز" : sl.Property == PropertyEnum.Litter ? "تسعیر" : "",
                                 }).ToList();

                    }
                    report.RegBusinessObject("SLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\slReport.mrt");

                    report.Show();
                }
            }
        }
        private void ShowReportAll()
        {
            if (SLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (SLRadGridView.SelectedItems.Any())
                    {
                        data = _viewModel.SLs.Cast<SL>()
                                 .Select(sl => new
                                 {
                                     SLCode = sl.SLCode,
                                     TLTitle = sl.TL.Title,
                                     Title = sl.Title,
                                     Title2 = sl.Title2,
                                     Status = sl.Status == true ? "فعال" : "غیرفعال",
                                     DLType1 = sl.DLType1 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType1 == DLTypeEnum.Company ? "شرکت" : sl.DLType1 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType1 == DLTypeEnum.Others ? "سایر" : sl.DLType1 == DLTypeEnum.People ? "اشخاص" : sl.DLType1 == DLTypeEnum.Project ? "پروژه" : "",
                                     DLType2 = sl.DLType2 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType2 == DLTypeEnum.Company ? "شرکت" : sl.DLType2 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType2 == DLTypeEnum.Others ? "سایر"
                             : sl.DLType2 == DLTypeEnum.People ? "اشخاص" : sl.DLType2 == DLTypeEnum.Project ? "پروژه" : "",
                                     AccountsNature = sl.AccountsNatureEnum == AccountsNatureEnum.Cred ? "بستانکار" : sl.AccountsNatureEnum == AccountsNatureEnum.Debt ? "بدهکار" : sl.AccountsNatureEnum == AccountsNatureEnum.None ? "مهم نیست" : "",
                                     Property = sl.Property == PropertyEnum.Consistency ? "پیگیری" : sl.Property == PropertyEnum.Count ? "تعدادی" : sl.Property == PropertyEnum.Exchange ? "ارز" : sl.Property == PropertyEnum.Litter ? "تسعیر" : "",
                                 }).ToList();


                        report.RegBusinessObject("SLReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\slReport.mrt");

                        report.Show();
                    }
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
                var data = SLRadGridView.SelectedItems.Cast<SL>().ToList()
                     .Select(sl => new
                     {
                         SLCode = sl.SLCode,
                         TLTitle = sl.TL.Title,
                         Title = sl.Title,
                         Title2 = sl.Title2,
                         Status = sl.Status == true ? "فعال" : "غیرفعال",
                         DLType1 = sl.DLType1 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType1 == DLTypeEnum.Company ? "شرکت" : sl.DLType1 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType1 == DLTypeEnum.Others ? "سایر" : sl.DLType1 == DLTypeEnum.People ? "اشخاص" : sl.DLType1 == DLTypeEnum.Project ? "پروژه" : "",
                         DLType2 = sl.DLType2 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType2 == DLTypeEnum.Company ? "شرکت" : sl.DLType2 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType2 == DLTypeEnum.Others ? "سایر"
                            : sl.DLType2 == DLTypeEnum.People ? "اشخاص" : sl.DLType2 == DLTypeEnum.Project ? "پروژه" : "",
                         AccountsNature = sl.AccountsNatureEnum == AccountsNatureEnum.Cred ? "بستانکار" : sl.AccountsNatureEnum == AccountsNatureEnum.Debt ? "بدهکار" : sl.AccountsNatureEnum == AccountsNatureEnum.None ? "مهم نیست" : "",
                         Property = sl.Property == PropertyEnum.Consistency ? "پیگیری" : sl.Property == PropertyEnum.Count ? "تعدادی" : sl.Property == PropertyEnum.Exchange ? "ارز" : sl.Property == PropertyEnum.Litter ? "تسعیر" : "",
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

                var path = $"{Environment.CurrentDirectory}\\Report\\slReport.mrt";
                report.RegBusinessObject("SLReport", data);
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
                var data = SLRadGridView.SelectedItems.Cast<SL>()
                      .Select(sl => new
                      {
                          SLCode = sl.SLCode,
                          TLTitle = sl.TL.Title,
                          Title = sl.Title,
                          Title2 = sl.Title2,
                          Status = sl.Status == true ? "فعال" : "غیرفعال",
                          DLType1 = sl.DLType1 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType1 == DLTypeEnum.Company ? "شرکت" : sl.DLType1 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType1 == DLTypeEnum.Others ? "سایر" : sl.DLType1 == DLTypeEnum.People ? "اشخاص" : sl.DLType1 == DLTypeEnum.Project ? "پروژه" : "",
                          DLType2 = sl.DLType2 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType2 == DLTypeEnum.Company ? "شرکت" : sl.DLType2 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType2 == DLTypeEnum.Others ? "سایر"
                            : sl.DLType2 == DLTypeEnum.People ? "اشخاص" : sl.DLType2 == DLTypeEnum.Project ? "پروژه" : "",
                          AccountsNature = sl.AccountsNatureEnum == AccountsNatureEnum.Cred ? "بستانکار" : sl.AccountsNatureEnum == AccountsNatureEnum.Debt ? "بدهکار" : sl.AccountsNatureEnum == AccountsNatureEnum.None ? "مهم نیست" : "",
                          Property = sl.Property == PropertyEnum.Consistency ? "پیگیری" : sl.Property == PropertyEnum.Count ? "تعدادی" : sl.Property == PropertyEnum.Exchange ? "ارز" : sl.Property == PropertyEnum.Litter ? "تسعیر" : "",
                      });
                report.RegBusinessObject("GLReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\slReport.mrt");
                report.Print();
            }
        }
        private void PrintReportAll()
        {
            if (SLRadGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (SLRadGridView.SelectedItems.Any())
                    {
                        data = _viewModel.SLs.Cast<SL>()
                                 .Select(sl => new
                                 {
                                     SLCode = sl.SLCode,
                                     TLTitle = sl.TL.Title,
                                     Title = sl.Title,
                                     Title2 = sl.Title2,
                                     Status = sl.Status == true ? "فعال" : "غیرفعال",
                                     DLType1 = sl.DLType1 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType1 == DLTypeEnum.Company ? "شرکت" : sl.DLType1 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType1 == DLTypeEnum.Others ? "سایر" : sl.DLType1 == DLTypeEnum.People ? "اشخاص" : sl.DLType1 == DLTypeEnum.Project ? "پروژه" : "",
                                     DLType2 = sl.DLType2 == DLTypeEnum.BankAccount ? "حساب بانکی" : sl.DLType2 == DLTypeEnum.Company ? "شرکت" : sl.DLType2 == DLTypeEnum.Cost ? "مرکز هزینه" : sl.DLType2 == DLTypeEnum.Others ? "سایر"
                             : sl.DLType2 == DLTypeEnum.People ? "اشخاص" : sl.DLType2 == DLTypeEnum.Project ? "پروژه" : "",
                                     AccountsNature = sl.AccountsNatureEnum == AccountsNatureEnum.Cred ? "بستانکار" : sl.AccountsNatureEnum == AccountsNatureEnum.Debt ? "بدهکار" : sl.AccountsNatureEnum == AccountsNatureEnum.None ? "مهم نیست" : "",
                                     Property = sl.Property == PropertyEnum.Consistency ? "پیگیری" : sl.Property == PropertyEnum.Count ? "تعدادی" : sl.Property == PropertyEnum.Exchange ? "ارز" : sl.Property == PropertyEnum.Litter ? "تسعیر" : "",
                                 }).ToList();


                        report.RegBusinessObject("SLReport", data);
                        report.Load($"{Environment.CurrentDirectory}\\Report\\slReport.mrt");

                        report.Print();
                    }
                }
            }
        
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
                var col0Visibility = SLRadGridView.Columns[0].IsVisible;
                SLRadGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    SLRadGridView.Export(stream,
                     opt);
                }
                SLRadGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }
        #endregion
        #region Report1

        private void showButton1_Click(object sender, RoutedEventArgs e)
        {
            ShowReport1();
        }
        private void ShowReport1()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = SLRadGridView.SelectedItems.Cast<SLStandardDescription>()
                                     .Select(sLStandardDescription => new
                                     {
                                         SLStandardDescriptionTitle = sLStandardDescription.SLStandardDescriptionTitle,
                                         SLStandardDescriptionCode = sLStandardDescription.SLStandardDescriptionCode,
                                     });
                report.RegBusinessObject("SLStandardDescriptionReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\sLStandardDescriptionReport.mrt");
                report.Show();
            }
        }
        private void designButton1_Click(object sender, RoutedEventArgs e)
        {
            DesignReport1();
        }
        private void DesignReport1()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = SLRadGridView.SelectedItems.Cast<SLStandardDescription>()
                                     .Select(sLStandardDescription => new
                                     {
                                         SLStandardDescriptionTitle = sLStandardDescription.SLStandardDescriptionTitle,
                                         SLStandardDescriptionCode = sLStandardDescription.SLStandardDescriptionCode,
                                     });

                report.RegBusinessObject("SLStandardDescriptionReport", data);
                report.Design();
            }
        }
        private void printButton1_Click(object sender, RoutedEventArgs e)
        {
            PrintReport1();
        }
        private void PrintReport1()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = SLRadGridView.SelectedItems.Cast<SLStandardDescription>()
                      .Select(sLStandardDescription => new
                      {
                          SLStandardDescriptionTitle = sLStandardDescription.SLStandardDescriptionTitle,
                          SLStandardDescriptionCode = sLStandardDescription.SLStandardDescriptionCode,
                      });
                report.RegBusinessObject("SLStandardDescriptionReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\sLStandardDescriptionReport.mrt");
                report.Print();
            }
        }
        private void btnExport1_Click(object sender, RoutedEventArgs e)
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
                var col0Visibility = radGridView.Columns[0].IsVisible;
                radGridView.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    radGridView.Export(stream,
                     opt);
                }
                radGridView.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }
        #endregion
        #region GridView
        private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            if (detailRadTabItem.IsSelected && SLRadGridView.SelectedItem != null)
            {
                // NavStateFalse();
                BeginEdit();

            }
        }
        private void SLRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            detailRadTabItem.IsSelected = true;
        }

        private void SLRadGridView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }

        private void SLRadGridView_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        }
        private void SLDataForm_CurrentItemChanged(object sender, EventArgs e)
        {

        }

        private void SLDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                _viewModel.Save();
                commitEditCommand.Execute(null, SLDataForm);
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

            }
        }
        #endregion
        #region Add
        private void SLDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            var x = e.NewItem as SL;
            _viewModel.AddSLCommand.Execute(null);
            //x.SLCode = _viewModel.SL.SLCode;
            //x.Title = _viewModel.SL.Title;
            //x.Title2 = _viewModel.SL.Title2;
            //x.Status = _viewModel.SL.Status;

            _viewModel.AddSL((SL)e.NewItem);

        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            NavStateFalse();
            if (_accessUtility.HasAccess(9))
            {
                //(sender as FrameworkElement).Focus();
                var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
                addNewCommand.Execute(null, SLDataForm);
            }
            // if (_viewModel.AddSLCommand.CanExecute(null))
            //  SLDataForm.AddNewItem();
        }
        #endregion
        #region Delete
        private void SLDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var r = _viewModel.DeleteSL((SL));
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
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
        private void SLDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
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
            RaiseCanExecuteChanged();
          NavStateTrue();
            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(null, SLDataForm);

        }
        #endregion
        #region Validation
        public void NavStateFalse()
        {
            addButton.IsEnabled = false;
            saveButton.IsEnabled = true;
            //deleteButton.IsEnabled = false;
            cancelButton.IsEnabled = true;
            editButton.IsEnabled = false;
            firstButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            lastButton.IsEnabled = false;
            previousButton.IsEnabled = false;
        }
        public void NavStateTrue()
        {
            firstButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            lastButton.IsEnabled = true;
            previousButton.IsEnabled = true;
        }
        private void RaiseCanExecuteChanged()
        {
            addButton.IsEnabled = true;
            saveButton.IsEnabled = false;
            // deleteButton.IsEnabled = true;
            cancelButton.IsEnabled = false;
            editButton.IsEnabled = true;

        }

        private void SLDataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ValidateSL())
            {

                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        SL SL => SLDataForm.CurrentItem as SL;
        private bool ValidateSL()
        {
            if (SL == null)
            {
                return true;
            }
            var result = true;
            var errorMessage = "";
            // SLDataForm.ValidationSummary.Errors.Clear();
            if (_sLsService.HasTitle(SL.Title, SL.SLId))
            {
                errorMessage += $"عنوان نباید تکراری باشد {Environment.NewLine}";

            }
            if (_sLsService.HasDuplicate(SL.SLCode, SL.SLId))
            {
                errorMessage += $"کد حساب نباید تکراری باشد {Environment.NewLine}";

            }

            if (SL.SLCode == 0)
            {
                errorMessage += $"کد حساب نباید 0 باشد {Environment.NewLine}";

            }
            if (SL.Title == null || SL.Title == "")
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
            SLDataForm.MoveCurrentToFirst();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            SLDataForm.MoveCurrentToLast();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            SLDataForm.MoveCurrentToNext();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            SLDataForm.MoveCurrentToPrevious();
        }
        #endregion
        #region SaveAndCancel
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSL())
            {
                RaiseCanExecuteChanged();
              NavStateTrue();
                SLDataForm.CommitEdit();


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
                commitEditCommand.Execute(null, SLDataForm);
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
                if (SLDataForm != null)
                {
                    RaiseCanExecuteChanged();
                  NavStateTrue();
                    SLDataForm.CancelEdit();
                   // SLDataForm.BeginEdit();
                }
            }

        }
        private void BeginEdit()
        {
          var  hasAcc= _viewModel.HasSL(((SL)SLDataForm.CurrentItem).SLId);
          
            if (hasAcc == true)
            {
                _viewModel.Code = false;
            }
            else
            {
                _viewModel.Code = true;
            }
            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
            beginEditCommand.Execute(null, SLDataForm);
            //_viewModel.OnTLsDropDownOpened();

        }
        #endregion
        #region Textbox
        private void sLCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
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

            //sLCodeTextbox.IsEnabled = true;
            //sLCodeTextbox.Focus();
            //sLCodeTextbox.CaretIndex = sLCodeTextbox.Text.Length;
        }

        private void currencyCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavStateFalse();
            if (_accessUtility.HasAccess(10))
            {
                (sender as FrameworkElement).Focus();
                BeginEdit();
            }
        }

        private void radGridView_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {

        }

        private void SLStandardradGridView_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            var error = ((TextBox)e.EditingElement).Text;
            if (error==null || error=="")
            {
                e.ErrorMessage = "شرح استاندارد نباید خالی باشد";
                e.IsValid = false;
            }
            //e.ErrorMessage = "ggfgff";
            //e.IsValid = false;

            //  var ee = ((RadComboBox)e.EditingElement).SelectedValue==null;
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

       
        private void SLDataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {

        }
    }
    public static class Command
    {
        public static RoutedCommand Add = new RoutedCommand();
        public static RoutedCommand Save = new RoutedCommand();
        public static RoutedCommand Delete = new RoutedCommand();
        public static RoutedCommand Cancel = new RoutedCommand();
        public static RoutedCommand Edit = new RoutedCommand();
    }
}
