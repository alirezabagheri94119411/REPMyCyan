using Microsoft.Win32;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Information.BankAccounts;
using Saina.WPF.BasicInformation.Information.CompanyInfo;
using Saina.WPF.Behaviors;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    public partial class DLListView : UserControl
    {
        private AccessUtility _accessUtility;
        private DLListViewModel _viewModel;
        private bool? _dialogResult;
        private IDLsService _dLsService;
        private bool isModified;
        private DL oldDL;

        public DLListView()
        {
            InitializeComponent();
            _dLsService = SmObjectFactory.Container.GetInstance<IDLsService>();

            Loaded += (s, e) =>
            {
                _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

                _viewModel = DataContext as DLListViewModel;
                _viewModel.Loaded();
                _viewModel.Active = true;
                // headerDataForm.BeginEdit();
                //if (headerDataForm?.ItemsSource != null)
                //{
                //    var headers = headerDataForm.ItemsSource.Cast<DL>();
                //    foreach (var header in headers)
                //    {
                //        header.PropertyChanging += Header_PropertyChanging;
                //        header.PropertyChanged += DL_PropertyChanged;
                //    }

                //}
                radGridView.SelectedItem = null;

                detailRadTabItem.IsSelected = true;
                // headerDataForm.AddNewItem();
            };
        }

        private void Header_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            //if (e.PropertyName == "DLType")
            oldDL = (DL)sender;
        }

        private void HeaderDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            //if (headerDataForm.CurrentItem is DL dl)
            //{
            //    if (dl.Title == null)
            //    {
            //        _viewModel.Active = true;
            //    }
            //}
            if (_accessUtility.HasAccess(12))
            {
                var newItem = ((DL)e.NewItem);
                var x = newItem.Title;
                newItem.DLCode = _viewModel.GetLastDLCode(newItem.DLType);
                //if (newItem.DLCode != 0 && newItem.Title == null)
                //{
                //    _viewModel.Active = true;
                //}
                //else
                if (newItem.DLCode != 0)
                {
                    _viewModel.Active = true;
                    ((DL)e.NewItem).PropertyChanged += DL_PropertyChanged;
                    //((ObservableCollection<BankAccount>)newItem.BankAccounts).CollectionChanged += BankAccounts_CollectionChanged;

                    _viewModel.AddDL((DL)e.NewItem);
                }
            }
        }

        private void DL_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is DL dl && e.PropertyName == "DLType")
            {
                //if (dl.DLType != 0 &&( (int)dl.DLType==2 || Math.Sqrt((int)dl.DLType) % 1 == 0))
                //{
                if (dl.DLType == oldDL?.DLType)
                    dl.DLCode = oldDL.DLCode;
                else
                    dl.DLCode = _viewModel.GetLastDLCode(dl.DLType);
                //}

            }
            RaiseCanExecuteChanged();
        }


        private void RadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
                _viewModel.Save();
        }

        private void RadDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
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

        private void headerDataForm_DeletingItem(object sender, CancelEventArgs e)
        {
            if (_accessUtility.HasAccess(14))
            {
                var r = _viewModel.DeleteDL((DL));
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
        private void DeleteRadPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (_dialogResult == false) return;
            headerDataForm.DeleteItem();
            _viewModel.Save();

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
        //  DL DL => headerDataForm.CurrentItem as DL;
        private void dLDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            detailRadTabItem.IsSelected = true;
        }
        private void RaiseCanExecuteChanged()
        {

        }

        private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            //if (detailRadTabItem.IsSelected && headerDataForm.CurrentItem != null)
            //{

            //   BeginEdit();
            //}
            //if (detailRadTabItem.IsSelected)
            //{
            //    RaiseCanExecuteChanged();
            //}
        }
        private void BeginEdit()
        { }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            if (radGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    if (radGridView.SelectedItems.Any())
                        data = radGridView.SelectedItems.Cast<DL>()
                                       .Select(dl => new
                                       {
                                           DLCode = dl.DLCode,
                                           Title = dl.Title,
                                           Title2 = dl.Title2,
                                           Status = dl.Status == true ? "فعال" : "غیرفعال",
                                           DLType = dl.DLType == DLTypeEnum.BankAccount ? "حساب بانکی" : dl.DLType == DLTypeEnum.Company ? "شرکت" : dl.DLType == DLTypeEnum.Cost ? "مرکز هزینه" : dl.DLType == DLTypeEnum.Others ? "سایر" : dl.DLType == DLTypeEnum.People ? "اشخاص" : dl.DLType == DLTypeEnum.Project ? "پروژه" : "",
                                           Address = dl.Address,
                                           Address2 = dl.Address2,
                                           BankName = dl.Bank?.BankName,
                                           BirthDate = dl.BirthDate,
                                           BirthPlace = dl.BirthPlace,
                                           Branch = dl.Branch,
                                           CardNumber = dl.CardNumber,
                                           CertificateNumber = dl.CertificateNumber,
                                           CertificatePlace = dl.CertificatePlace,
                                           CertificateSeries = dl.CertificateSeries,
                                           City = dl.City,
                                           CurrencyTitle = dl.CurrencyType?.CurrencyTitle,
                                           DateRegistration = dl.DateRegistration,
                                           DLAccountsNature = dl.DLAccountsNature,
                                           EconomicalNumber = dl.EconomicalNumber,
                                           ExchangeRate = dl.ExchangeRate,
                                           Family = dl.Family,
                                           FatherName = dl.FatherName,
                                           Fax = dl.Fax,
                                           FirstAccountBalance = dl.FirstAccountBalance,
                                           FirstReservePeriod = dl.FirstReservePeriod,
                                           InventoryReservation = dl.InventoryReservation,
                                           LatinName = dl.LatinName,
                                           Logo = dl.Logo,
                                           ManagerName = dl.ManagerName,
                                           Name = dl.Name,
                                           NationalCode = dl.NationalCode,
                                           NumberOfChildren = dl.NumberOfChildren,
                                           OpeningDate = dl.OpeningDate,
                                           Phone1 = dl.Phone1,
                                           Phone2 = dl.Phone2,
                                           Phone3 = dl.Phone3,
                                           PhoneManager = dl.PhoneManager,
                                           PoseId = dl.PoseId,
                                           PostalCode = dl.PostalCode,
                                           Province = dl.Province,
                                           RegistrationNumber = dl.RegistrationNumber,
                                           Sexuality = dl.Sexuality,
                                           ShabaNumber = dl.ShabaNumber,
                                           Town = dl.Town,
                                           WebSite = dl.WebSite,
                                           RelatedPeople = dl.RelatedPeople
                                       }).ToList();
                    else
                    {
                        data = _viewModel.DLs.Cast<DL>()
                              .Select(dl => new
                              {

                                  DLCode = dl.DLCode,
                                  Title = dl.Title,
                                  Title2 = dl.Title2,
                                  Status = dl.Status == true ? "فعال" : "غیرفعال",
                                  DLType = dl.DLType == DLTypeEnum.BankAccount ? "حساب بانکی" : dl.DLType == DLTypeEnum.Company ? "شرکت" : dl.DLType == DLTypeEnum.Cost ? "مرکز هزینه" : dl.DLType == DLTypeEnum.Others ? "سایر" : dl.DLType == DLTypeEnum.People ? "اشخاص" : dl.DLType == DLTypeEnum.Project ? "پروژه" : "",
                                  Address = dl.Address,
                                  Address2 = dl.Address2,
                                  BankName = dl.Bank?.BankName,
                                  BirthDate = dl.BirthDate,
                                  BirthPlace = dl.BirthPlace,
                                  Branch = dl.Branch,
                                  CardNumber = dl.CardNumber,
                                  CertificateNumber = dl.CertificateNumber,
                                  CertificatePlace = dl.CertificatePlace,
                                  CertificateSeries = dl.CertificateSeries,
                                  City = dl.City,
                                  CurrencyTitle = dl.CurrencyType?.CurrencyTitle,
                                  DateRegistration = dl.DateRegistration,
                                  DLAccountsNature = dl.DLAccountsNature,
                                  EconomicalNumber = dl.EconomicalNumber,
                                  ExchangeRate = dl.ExchangeRate,
                                  Family = dl.Family,
                                  FatherName = dl.FatherName,
                                  Fax = dl.Fax,
                                  FirstAccountBalance = dl.FirstAccountBalance,
                                  FirstReservePeriod = dl.FirstReservePeriod,
                                  InventoryReservation = dl.InventoryReservation,
                                  LatinName = dl.LatinName,
                                  Logo = dl.Logo,
                                  ManagerName = dl.ManagerName,
                                  Name = dl.Name,
                                  NationalCode = dl.NationalCode,
                                  NumberOfChildren = dl.NumberOfChildren,
                                  OpeningDate = dl.OpeningDate,
                                  Phone1 = dl.Phone1,
                                  Phone2 = dl.Phone2,
                                  Phone3 = dl.Phone3,
                                  PhoneManager = dl.PhoneManager,
                                  PoseId = dl.PoseId,
                                  PostalCode = dl.PostalCode,
                                  Province = dl.Province,
                                  RegistrationNumber = dl.RegistrationNumber,
                                  Sexuality = dl.Sexuality,
                                  ShabaNumber = dl.ShabaNumber,
                                  Town = dl.Town,
                                  WebSite = dl.WebSite,
                                  RelatedPeople = dl.RelatedPeople
                              }).ToList();

                    }
                    report.RegBusinessObject("DLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\dlReport.mrt");
                    report.Show();
                }
            }
        }
        private void ShowReportAll()
        {
            if (radGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;
                    var bb = ((ICollectionView)radGridView.ItemsSource).Cast<DL>();
                    data = bb
                              .Select(dl => new
                               {

                                   DLCode = dl.DLCode,
                                   Title = dl.Title,
                                   Title2 = dl.Title2,
                                   Status = dl.Status == true ? "فعال" : "غیرفعال",
                                   DLType = dl.DLType == DLTypeEnum.BankAccount ? "حساب بانکی" : dl.DLType == DLTypeEnum.Company ? "شرکت" : dl.DLType == DLTypeEnum.Cost ? "مرکز هزینه" : dl.DLType == DLTypeEnum.Others ? "سایر" : dl.DLType == DLTypeEnum.People ? "اشخاص" : dl.DLType == DLTypeEnum.Project ? "پروژه" : "",
                                  AccountType=  dl.AccountType?.AccountTypeId,
                                  Address =  dl.Address,
                                  Address2 =  dl.Address2,
                                  BankName =  dl.Bank?.BankName,
                                  BirthDate =  dl.BirthDate,
                                  BirthPlace =  dl.BirthPlace,
                                  Branch =  dl.Branch,
                                  CardNumber =  dl.CardNumber,
                                  CertificateNumber =  dl.CertificateNumber,
                                  CertificatePlace =  dl.CertificatePlace,
                                  CertificateSeries =  dl.CertificateSeries,
                                  City =  dl.City,
                                  CurrencyTitle =  dl.CurrencyType?.CurrencyTitle,
                                  DateRegistration =  dl.DateRegistration,
                                  DLAccountsNature =  dl.DLAccountsNature,
                                  EconomicalNumber =  dl.EconomicalNumber,
                                  ExchangeRate =  dl.ExchangeRate,
                                  Family =  dl.Family,
                                  FatherName =  dl.FatherName,
                                  Fax =  dl.Fax,
                                  FirstAccountBalance =  dl.FirstAccountBalance,
                                  FirstReservePeriod =  dl.FirstReservePeriod,
                                  InventoryReservation =  dl.InventoryReservation,
                                  LatinName =  dl.LatinName,
                                  Logo =  dl.Logo,
                                  ManagerName =  dl.ManagerName,
                                  Name =  dl.Name,
                                  NationalCode =  dl.NationalCode,
                                  NumberOfChildren =  dl.NumberOfChildren,
                                  OpeningDate =  dl.OpeningDate,
                                  Phone1 =  dl.Phone1,
                                  Phone2 =  dl.Phone2,
                                  Phone3 =  dl.Phone3,
                                  PhoneManager =  dl.PhoneManager,
                                  PoseId =  dl.PoseId,
                                  PostalCode =  dl.PostalCode,
                                  Province =  dl.Province,
                                  RegistrationNumber =  dl.RegistrationNumber,
                                  Sexuality =  dl.Sexuality,
                                  ShabaNumber =  dl.ShabaNumber,
                                  Town =  dl.Town,
                                  WebSite =  dl.WebSite,
                                  RelatedPeople =  dl.RelatedPeople
                                                             }).ToList();


                    report.RegBusinessObject("DLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\dlReport.mrt");
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
                var data = radGridView.SelectedItems.Cast<DL>()
                       .Select(dl => new
                       {

                           DLCode = dl.DLCode,
                           Title = dl.Title,
                           Title2 = dl.Title2,
                           Status = dl.Status == true ? "فعال" : "غیرفعال",
                           DLType = dl.DLType == DLTypeEnum.BankAccount ? "حساب بانکی" : dl.DLType == DLTypeEnum.Company ? "شرکت" : dl.DLType == DLTypeEnum.Cost ? "مرکز هزینه" : dl.DLType == DLTypeEnum.Others ? "سایر" : dl.DLType == DLTypeEnum.People ? "اشخاص" : dl.DLType == DLTypeEnum.Project ? "پروژه" : "",
                           dl.AccountType?.AccountTypeId,
                           Address = dl.Address,
                           Address2 = dl.Address2,
                           BankName = dl.Bank?.BankName,
                           BirthDate = dl.BirthDate,
                           BirthPlace = dl.BirthPlace,
                           Branch = dl.Branch,
                           CardNumber = dl.CardNumber,
                           CertificateNumber = dl.CertificateNumber,
                           CertificatePlace = dl.CertificatePlace,
                           CertificateSeries = dl.CertificateSeries,
                           City = dl.City,
                           CurrencyTitle = dl.CurrencyType?.CurrencyTitle,
                           DateRegistration = dl.DateRegistration,
                           DLAccountsNature = dl.DLAccountsNature,
                           EconomicalNumber = dl.EconomicalNumber,
                           ExchangeRate = dl.ExchangeRate,
                           Family = dl.Family,
                           FatherName = dl.FatherName,
                           Fax = dl.Fax,
                           FirstAccountBalance = dl.FirstAccountBalance,
                           FirstReservePeriod = dl.FirstReservePeriod,
                           InventoryReservation = dl.InventoryReservation,
                           LatinName = dl.LatinName,
                           Logo = dl.Logo,
                           ManagerName = dl.ManagerName,
                           Name = dl.Name,
                           NationalCode = dl.NationalCode,
                           NumberOfChildren = dl.NumberOfChildren,
                           OpeningDate = dl.OpeningDate,
                           Phone1 = dl.Phone1,
                           Phone2 = dl.Phone2,
                           Phone3 = dl.Phone3,
                           PhoneManager = dl.PhoneManager,
                           PoseId = dl.PoseId,
                           PostalCode = dl.PostalCode,
                           Province = dl.Province,
                           RegistrationNumber = dl.RegistrationNumber,
                           Sexuality = dl.Sexuality,
                           ShabaNumber = dl.ShabaNumber,
                           Town = dl.Town,
                           WebSite = dl.WebSite,
                           RelatedPeople = dl.RelatedPeople,
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

                var path = $"{Environment.CurrentDirectory}\\Report\\dlReport.mrt";
                report.RegBusinessObject("DLReport", data);
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
                var data = radGridView.SelectedItems.Cast<DL>()
                       .Select(dl => new
                       {

                           DLCode = dl.DLCode,
                           Title = dl.Title,
                           Title2 = dl.Title2,
                           Status = dl.Status == true ? "فعال" : "غیرفعال",
                           DLType = dl.DLType == DLTypeEnum.BankAccount ? "حساب بانکی" : dl.DLType == DLTypeEnum.Company ? "شرکت" : dl.DLType == DLTypeEnum.Cost ? "مرکز هزینه" : dl.DLType == DLTypeEnum.Others ? "سایر" : dl.DLType == DLTypeEnum.People ? "اشخاص" : dl.DLType == DLTypeEnum.Project ? "پروژه" : "",
                       });
                report.RegBusinessObject("DLReport", data);
                report.Load($"{Environment.CurrentDirectory}\\Report\\dlReport.mrt");
                report.Print();
            }
        }
        private void PrintReportAll()
        {
            if (radGridView.ItemsSource != null)
            {
                StiReport report = new StiReport();
                using (var uow = new SainaDbContext())
                {
                    dynamic data;

                    data = _viewModel.DLs.Cast<DL>()
                          .Select(dl => new
                          {

                              DLCode = dl.DLCode,
                              Title = dl.Title,
                              Title2 = dl.Title2,
                              Status = dl.Status == true ? "فعال" : "غیرفعال",
                              DLType = dl.DLType == DLTypeEnum.BankAccount ? "حساب بانکی" : dl.DLType == DLTypeEnum.Company ? "شرکت" : dl.DLType == DLTypeEnum.Cost ? "مرکز هزینه" : dl.DLType == DLTypeEnum.Others ? "سایر" : dl.DLType == DLTypeEnum.People ? "اشخاص" : dl.DLType == DLTypeEnum.Project ? "پروژه" : "",
                          }).ToList();


                    report.RegBusinessObject("DLReport", data);
                    report.Load($"{Environment.CurrentDirectory}\\Report\\dlReport.mrt");
                    report.Print();
                }
            }
            
        }
        private GridViewRow ClickedRow
        {
            get
            {
                return GridContextMenu.GetClickedElement<GridViewRow>();
            }
        }
        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
            beginEditCommand.Execute(null, headerDataForm);
            //var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            //addNewCommand.Execute(null, headerDataForm);
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var cancelCommand = RadDataFormCommands.CancelEdit as RoutedUICommand;
            cancelCommand.Execute(null, headerDataForm);
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(null, headerDataForm);
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        //private void BankAccountAddEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    var addEditBankAccountWindowViewModel = SmObjectFactory.Container.GetInstance<AddEditBankAccountWindowViewModel>();
        //    addEditBankAccountWindowViewModel.DL = _viewModel.DLss.CurrentItem as DL;

        //    AddEditBankAccountWindow addEditBankAccountWindow = new AddEditBankAccountWindow();
        //    addEditBankAccountWindow.DataContext = addEditBankAccountWindowViewModel;
        //    // addEditBankAccountWindow.DataContext = this.DataContext;
        //    addEditBankAccountWindow.OkClicked += () =>
        //    {
        //        EditableBankAccount editableBankAccount = ((AddEditBankAccountWindowViewModel)addEditBankAccountWindow.DataContext).BankAccount;
        //        BankAccount bankAccount = AutoMapper.Mapper.Map<BankAccount>(editableBankAccount);
        //        //  bankAccount.OpeningDate = DateTime.Now;
        //        //addEditBankAccountWindowViewModel.DL.BankAccounts.Add(bankAccount);
        //    };
        //    addEditBankAccountWindow.ShowDialog();
        //}

        private void RelatedButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedPerson = ((RadButton)sender).DataContext as Person;
            RelatedPersonListWindow relatedPersonListWindow = new RelatedPersonListWindow();
            relatedPersonListWindow.DataContext = DataContext;
            relatedPersonListWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            relatedPersonListWindow.Width = 1024;
            relatedPersonListWindow.Height = 768;
            relatedPersonListWindow.CanClose = true;
            relatedPersonListWindow.Header = "اشخاص مرتبط";
            relatedPersonListWindow.Owner = Window.GetWindow(this);
            relatedPersonListWindow.Show();
            //  relatedPersonListWindow.ShowDialog();
        }

        private void PersonAddEdit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CompanyAddEdit_Click(object sender, RoutedEventArgs e)
        {
            var addEditCompanyViewModel = SmObjectFactory.Container.GetInstance<AddEditCompanyViewModel>();
            addEditCompanyViewModel.DL = _viewModel.DLss.CurrentItem as DL;
            AddEditCompanyWindow addEditCompanyWindow = new AddEditCompanyWindow();
            addEditCompanyWindow.DataContext = addEditCompanyViewModel;
            // addEditCompanyWindow.DataContext = this.DataContext;
            addEditCompanyWindow.OkClicked += () =>
            {
                var editableCompany = ((AddEditCompanyViewModel)addEditCompanyWindow.DataContext).Company;
                Company company = AutoMapper.Mapper.Map<Company>(editableCompany);
                //addEditCompanyViewModel.DL.Companies.Add(company);
            };
            addEditCompanyWindow.Width = 1024;
            addEditCompanyWindow.Height = 768;
            addEditCompanyWindow.CanClose = true;
            addEditCompanyWindow.Owner = Window.GetWindow(this);
            addEditCompanyWindow.Owner = Window.GetWindow(this);
            addEditCompanyWindow.Show();
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // TextBox titleTextBox = null ;
            //var titleTextBox = headerDataForm.ChildrenOfType<TextBox>().FirstOrDefault(x => x.Name == "titleText");
            if (_viewModel.DLss.CurrentItem is DL dl && (dl.Title == null || dl.Title == ""))
            {
                //if (titleTextBox==null)
                //{

                //}
                //if (dl.DLType == 0)
                //    ((RadioButton)sender).IsChecked = true;
                switch (dl.DLType)
                {
                    case DLTypeEnum.BankAccount:
                        //   titleTextBox.IsEnabled = false;

                        _viewModel.TitleEnable = false;
                        EditBankAccountWindow editBankAccountWindow = new EditBankAccountWindow("radioButton") { DataContext = DataContext };
                        _viewModel.LoadBanks();
                        //  _viewModel.BankAccount = ((Control)sender).DataContext as BankAccount;
                        editBankAccountWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        editBankAccountWindow.Width = 768;
                        editBankAccountWindow.Height = 500;
                        editBankAccountWindow.CanClose = true;
                        editBankAccountWindow.Owner = Window.GetWindow(this);
                        editBankAccountWindow.Show();

                        // _viewModel.RaiseTestRequested(DLTypeEnum.BankAccount,dl.DLCode);

                        break;
                    case DLTypeEnum.People:
                        //  titleTextBox.IsEnabled = false;

                        _viewModel.TitleEnable = false;

                        EditPersonWindow editPersonWindow = new EditPersonWindow() { DataContext = DataContext };
                        //   _viewModel.Person = ((Control)sender).DataContext as Person;// _viewModel.Person = new Person();
                        editPersonWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        editPersonWindow.Width = 768;
                        editPersonWindow.Height = 500;
                        editPersonWindow.CanClose = true;
                        editPersonWindow.Owner = Window.GetWindow(this);
                        editPersonWindow.Show();
                        // _viewModel.RaiseTestRequested(DLTypeEnum.People, dl.DLCode);
                        break;

                    case DLTypeEnum.Company:
                        //  titleTextBox.IsEnabled = false;

                        _viewModel.TitleEnable = false;
                        EditCompanyWindow editCompanyWindow = new EditCompanyWindow() { DataContext = DataContext };

                        // _viewModel.Company = ((Control)sender).DataContext as Company;
                        editCompanyWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        editCompanyWindow.Width = 768;
                        editCompanyWindow.Height = 500;
                        editCompanyWindow.CanClose = true;
                        editCompanyWindow.Owner = Window.GetWindow(this);
                        editCompanyWindow.Show();
                        break;

                    case DLTypeEnum.Cost:
                        //  titleTextBox.IsEnabled = true;

                        _viewModel.TitleEnable = true;

                        break;
                    case DLTypeEnum.Project:
                        //titleTextBox.IsEnabled = true;

                        _viewModel.TitleEnable = true;

                        break;
                    case DLTypeEnum.Others:
                        //  titleTextBox.IsEnabled = true;

                        _viewModel.TitleEnable = true;

                        break;
                    default:
                        break;
                }
            }
            // PopulateComboBox();

        }

        private void headerDataForm_ValidatingItem(object sender, CancelEventArgs e)
        {
            // _viewModel.Active = true;

            // var dlType = DLTypeEnum.Others;
            //if (headerDataForm.CurrentItem is DL dl)
            //{
            //    if (dl.Title == null)
            //    {
            //        _viewModel.Active = true;
            //    }
            //}

            //    dlType = dl.DLType;
            //    if (dlType == DLTypeEnum.People && dl.Title != null)
            //    {
            //        var dLCode = dl.DLCode;
            //        var name = dl.Title;
            //        bool hasPerson = _viewModel.HasPerson(dLCode, name);
            //        if (hasPerson == false)
            //        {
            //            _viewModel.InsertPerson(dLCode, name);

            //        }
            //    }
            //    else if (dlType == DLTypeEnum.Company && dl.Title != null)
            //    {
            //        var dLCode = dl.DLCode;
            //        var name = dl.Title;
            //        bool hasCompany = _viewModel.HasCompany(dLCode, name);
            //        if (hasCompany == false)
            //        {
            //            _viewModel.InsertCompany(dLCode, name);

            //        }
            //    }
            //    else if (dlType == DLTypeEnum.BankAccount && dl.Title != null)
            //    {
            //        var dLCode = dl.DLCode;
            //        var name = dl.Title;
            //        bool hasCompany = _viewModel.HasCompany(dLCode, name);
            //        if (hasCompany == false)
            //        {
            //            _viewModel.InsertBankAccount(dLCode, name);

            //        }
            //    }
            //};

            if (ValidateDL())
            {

                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            AddEditPersonWindow addEditPersonWindow = new AddEditPersonWindow();
            addEditPersonWindow.DataContext = DataContext;

            addEditPersonWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            addEditPersonWindow.Width = 1024;
            addEditPersonWindow.Height = 768;
            addEditPersonWindow.CanClose = true;
            addEditPersonWindow.Owner = Window.GetWindow(this);
            addEditPersonWindow.Show();
            //  addEditPersonWindow.ShowDialog();
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

        private void titleRadComboBox_DropDownOpened(object sender, EventArgs e)
        {
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            RadComboBox radComboBox = headerDataForm.ChildrenOfType<RadComboBox>().FirstOrDefault();
            if (radComboBox != null && headerDataForm.CurrentItem is DL dl)
            {

                radComboBox.IsReadOnly = false;
                radComboBox.ItemsSource = null;
                switch (dl.DLType)
                {
                    case DLTypeEnum.BankAccount:
                        radComboBox.ItemsSource = _viewModel.GetBanks();
                        radComboBox.ItemTemplate = (DataTemplate)Resources["bankAccountDataTamplate"];
                        radComboBox.IsReadOnly = true;
                        Telerik.Windows.Controls.TextSearch.SetTextPath(radComboBox, "AccountNumber");
                        //radComboBox.DisplayMemberPath = "Bank.BankName";
                        break;
                    //case DLTypeEnum.People:
                    //    radComboBox.ItemsSource = _viewModel.GetPeople();
                    //    radComboBox.ItemTemplate = (DataTemplate)Resources["personDataTamplate"];
                    //    radComboBox.IsReadOnly = true;
                    //    Telerik.Windows.Controls.TextSearch.SetTextPath(radComboBox, "NationalCode");
                    //    //radComboBox.DisplayMemberPath = "NationalCode";
                    //    break;
                    case DLTypeEnum.Company:
                        radComboBox.ItemsSource = _viewModel.GetCompanies();
                        radComboBox.ItemTemplate = (DataTemplate)Resources["companyDataTamplate"];
                        radComboBox.IsReadOnly = false;
                        Telerik.Windows.Controls.TextSearch.SetTextPath(radComboBox, "RegistrationNumber");
                        //radComboBox.DisplayMemberPath = "RegistrationNumber";
                        break;
                    case DLTypeEnum.People:
                        // radComboBox.ItemsSource = _viewModel.GetCompanies();
                        radComboBox.ItemTemplate = (DataTemplate)Resources["PersonDataTamplate"];
                        radComboBox.IsReadOnly = true;
                        Telerik.Windows.Controls.TextSearch.SetTextPath(radComboBox, "RegistrationNumber");
                        // radComboBox.DisplayMemberPath = "RegistrationNumber";
                        break;
                    case DLTypeEnum.Cost:
                        break;
                    case DLTypeEnum.Project:
                        break;
                    case DLTypeEnum.Others:
                        break;
                    default:
                        break;
                }
            }
        }

        private void addButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(15))
            {
                radGridView.SelectedItem = ((Button)sender).DataContext;
            if (headerDataForm.CurrentItem is DL dl)
            {
                switch (dl.DLType)
                {
                    case DLTypeEnum.BankAccount:
                        EditBankAccountWindow editBankAccountWindow = new EditBankAccountWindow() { DataContext = DataContext };
                        _viewModel.LoadBanks();
                        // _viewModel.BankAccount = ((Control)sender).DataContext as BankAccount;
                        editBankAccountWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        editBankAccountWindow.Width = 768;
                        editBankAccountWindow.Height = 500;
                        editBankAccountWindow.CanClose = true;
                        editBankAccountWindow.Owner = Window.GetWindow(this);
                        editBankAccountWindow.Show();
                        // _viewModel.RaiseTestRequested(DLTypeEnum.BankAccount,dl.DLCode);

                        break;
                    case DLTypeEnum.People:
                        EditPersonWindow editPersonWindow = new EditPersonWindow() { DataContext = DataContext };
                        //   _viewModel.Person = ((Control)sender).DataContext as Person;// _viewModel.Person = new Person();
                        editPersonWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        editPersonWindow.Width = 768;
                        editPersonWindow.Height = 500;
                        editPersonWindow.CanClose = true;
                        editPersonWindow.Owner = Window.GetWindow(this);
                        editPersonWindow.Show();
                        // _viewModel.RaiseTestRequested(DLTypeEnum.People, dl.DLCode);
                        break;
                    case DLTypeEnum.Company:
                        EditCompanyWindow editCompanyWindow = new EditCompanyWindow() { DataContext = DataContext };

                        // _viewModel.Company = ((Control)sender).DataContext as Company;
                        editCompanyWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        editCompanyWindow.Width = 768;
                        editCompanyWindow.Height = 500;
                        editCompanyWindow.CanClose = true;
                        editCompanyWindow.Owner = Window.GetWindow(this);
                        editCompanyWindow.Show();
                        // _viewModel.RaiseTestRequested(DLTypeEnum.Company, dl.DLCode);
                        break;
                    case DLTypeEnum.Cost:
                        DialogParameters parameters = new DialogParameters();
                        parameters.OkButtonContent = "بستن";
                        parameters.Header = "اطلاعات";
                        parameters.Content = "نوع تفصیل مرکز هزینه اطلاعات بیشتر ندارد";
                        RadWindow.Alert(parameters);
                        break;
                    case DLTypeEnum.Project:
                        DialogParameters parameters1 = new DialogParameters();
                        parameters1.OkButtonContent = "بستن";
                        parameters1.Header = "اطلاعات";
                        parameters1.Content = "نوع تفصیل پروژه اطلاعات بیشتر ندارد";
                        RadWindow.Alert(parameters1);
                        break;
                    case DLTypeEnum.Others:
                        DialogParameters parameters2 = new DialogParameters();
                        parameters2.OkButtonContent = "بستن";
                        parameters2.Header = "اطلاعات";
                        parameters2.Content = "نوع تفصیل سایر اطلاعات بیشتر ندارد";
                        RadWindow.Alert(parameters2);
                        break;
                    default:
                        break;
                }
            }
                }

        }
        DL DL => headerDataForm.CurrentItem as DL;
        private bool ValidateDL()
        {
            var result = true;
            var errorMessage = "";
            if (DL != null)
            {


                if (_dLsService.HasTitle(DL.Title, DL.DLId))
                {
                    errorMessage += $"عنوان نباید تکراری باشد {Environment.NewLine}";

                }
                if (_dLsService.Hasduplicate(DL.DLCode, DL.DLId))
                {
                    errorMessage += $"کد حساب نباید تکراری باشد {Environment.NewLine}";

                }

                if (DL.DLCode == 0)
                {
                    errorMessage += $"کد حساب نباید 0 باشد {Environment.NewLine}";

                }
                if (DL.Title == null || DL.Title == "")
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

        private void headerDataForm_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            //if (headerDataForm.CurrentItem is DL dl)
            //{
            //    if (dl.Title == null)
            //    {
            //        _viewModel.Active = true;
            //    }
            //}
        }

        private void relatedButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (_accessUtility.HasAccess(16))
            {
                radGridView.SelectedItem = ((Button)sender).DataContext;

                if (headerDataForm.CurrentItem is DL dl)
                {
                    if (dl.DLType == DLTypeEnum.BankAccount || dl.DLType == DLTypeEnum.Company || dl.DLType == DLTypeEnum.People)
                    {

                        Saina.WPF.BasicInformation.Information.Related.RelatedPersonListWindow relatedPersonListWindow = new Saina.WPF.BasicInformation.Information.Related.RelatedPersonListWindow();
                        relatedPersonListWindow.DataContext = DataContext;
                        // _viewModel.BankAccount = ((Control)sender).DataContext as BankAccount;
                        relatedPersonListWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        relatedPersonListWindow.Width = 768;
                        relatedPersonListWindow.Height = 500;
                        relatedPersonListWindow.CanClose = true;
                        relatedPersonListWindow.Owner = Window.GetWindow(this);
                        relatedPersonListWindow.Show();
                    }
                    else
                    {

                        DialogParameters parameters = new DialogParameters();
                        parameters.OkButtonContent = "بستن";
                        parameters.Header = "اطلاعات";
                        parameters.Content = "این نوع تفصیل اطلاعات بیشتر ندارد";
                        RadWindow.Alert(parameters);

                    }
                }
            }
        }

        private void titleText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var dlType = headerDataForm.ChildrenOfType<System.Windows.Controls.GroupBox>().FirstOrDefault(x => x.Name == "dlTypeGroupBox");
            var titleText = headerDataForm.ChildrenOfType<System.Windows.Controls.TextBox>().FirstOrDefault(x => x.Name == "titleText");
            if (titleText.Text != "")
            {
                dlType.IsEnabled = false;
            }
            else
            {
                dlType.IsEnabled = true;
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

        private void headerDataForm_BeginningEdit(object sender, CancelEventArgs e)
        {
            if (!_accessUtility.HasAccess(13))
            {
                e.Cancel = true;
            }
            }
    }
}
