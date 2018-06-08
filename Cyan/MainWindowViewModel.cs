using Saina.WPF.OrderPrep;
using Saina.WPF.Orders;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Resources;
using System.Reflection;
using Saina.WPF.BasicInformation.Admin;
//using Saina.WPF.Customers;
using Saina.WPF.BasicInformation.Financial;
using Saina.WPF.Login;
using Saina.WPF.BasicInformation.Accounts.GLAccount;
using Saina.WPF.BasicInformation.Accounts.TLAccount;
using Saina.WPF.BasicInformation.Accounts.SLAccount;
using Saina.WPF.BasicInformation.Accounts.DLAccount;
using Saina.WPF.BasicInformation.Information.BankInfo;
using Telerik.Windows.Controls;
using Saina.WPF.BasicInformation.Information.BankAccounts;
using Saina.WPF.Accounting.DocumentAccounting.CurrencyDocument;
using Saina.WPF.Accounting.DocumentAccounting.ExchangeRateDocument;
//using Saina.WPF.BasicInformation.Information.PersonInfo;
using Saina.WPF.BasicInformation.Information.CompanyInfo;
using Saina.WPF.Accounting.DocumentAccounting.DocumentInfo;
using Saina.WPF.Accounting.DocumentAccounting.DocumentNumberinginfo;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using Saina.WPF.BasicInformation.Settings.GeneralSetting;
using Saina.WPF.BasicInformation.Settings.ShoppingSetting;
using Saina.WPF.BasicInformation.Settings.SaleSetting;
using Saina.WPF.BasicInformation.Settings.ReceiveSetting;
using Saina.WPF.BasicInformation.Settings.RetailSetting;
using Saina.WPF.BasicInformation.Settings.SalarySetting;
using System;
using AutoMapper;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.WPF.BasicInformation.Settings.CompanyInformation;
using Saina.WPF.BasicInformation.Admin.GroupAccess;
using Saina.WPF.BasicInformation.Admin.UserAccess;
using Saina.WPF.Commerce.CommStock;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.WPF.Commerce.CommProduct;
using Saina.WPF.Accounting.DocumentAccounting.AccTypeDocument;
using Saina.WPF.BasicInformation.Information.Related;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using Saina.WPF.Accounting.DocumentAccounting.ReviewAcc;
using Saina.WPF.Accounting.DocumentAccounting.ConvertDoc;
using Saina.WPF.Accounting.DocumentAccounting.CloseProfitLossAcc;
using Saina.WPF.Accounting.DocumentAccounting.OpenClose;
using Saina.WPF.BasicInformation.SelectFinancial;
using Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo;
using Saina.WPF.Accounting.DocumentAccounting.TLDocumentInfo;
using Saina.WPF.Accounting.DocumentAccounting.TreeACC;
using Saina.WPF.Accounting.DocumentAccounting.DocumentSystem;
using Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.CurrencyExchangeRateDoc;
using Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.CloseProfitLossDoc;
using Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.OpenCloseDoc;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.WPF.Commerce.ProductModels;
using Saina.WPF.Commerce.ProductTypes;
using Saina.WPF.Commerce.OtherProducts;
using Saina.WPF.Commerce.ProductBrands;
using Saina.WPF.Commerce.MeasurementUnits;
using Saina.WPF.Commerce.WarehouseDocuments.IncomingDocuments;
using Saina.WPF.Commerce.WarehouseDocuments.OutputDocuments;
using Saina.WPF.Commerce.WarehouseDocuments.ReturnInput;
using Saina.WPF.Commerce.WarehouseDocuments.ReturnOutput;
using Saina.WPF.Commerce.WarehouseDocuments.TransferWarehouse;
using Saina.WPF.Commerce.ProductRacks;
using Saina.Data.Context;
using Telerik.Windows.Data;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using System.Windows;
using Saina.WPF.BasicInformation.Information.PersonInfo;
using static Saina.WPF.Accounting.DocumentAccounting.DocumentHeader.AccDocumentHeaderListView;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.DTOs;
using static Saina.WPF.BasicInformation.Accounts.DLAccount.EditBankAccountWindow;
using Saina.WPF.Commerce.test;
using Saina.WPF.Behaviors;

namespace Saina.WPF
{
    class MainWindowViewModel : BindableBase
    {
        #region Constructors
       // private ICompanyInformationsService _companyInformationsService;
        public MainWindowViewModel(IAppContextService appContextService, IShoppingSystemSettingsService shoppingSystemSettingsService)
        {
            //_companyInformationsService = companyInformationsService;
         //   CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            #region GetInstances


            _unauthorizedViewModel = SmObjectFactory.Container.GetInstance<UnauthorizedViewModel>();
            _orderViewModel = SmObjectFactory.Container.GetInstance<OrderViewModel>();
            _orderPrepViewModel = SmObjectFactory.Container.GetInstance<OrderPrepViewModel>();
            //_customerListViewModel = SmObjectFactory.Container.GetInstance<CustomerListViewModel>();
            //_addEditViewModel = SmObjectFactory.Container.GetInstance<AddEditCustomerViewModel>();
            _addEditUserViewModel = SmObjectFactory.Container.GetInstance<AddEditUserViewModel>();
            _userListViewModel = SmObjectFactory.Container.GetInstance<UserListViewModel>();
            _documentnumberingInformationViewModel = SmObjectFactory.Container.GetInstance<CompanyInformationViewModel>();

            _groupListViewModel = SmObjectFactory.Container.GetInstance<GroupListViewModel>();
            _addEditGroupViewModel = SmObjectFactory.Container.GetInstance<AddEditGroupViewModel>();
            _addEditFinancialYearViewModel = SmObjectFactory.Container.GetInstance<AddEditFinancialYearViewModel>();
            _financialYearListViewModel = SmObjectFactory.Container.GetInstance<FinancialYearListViewModel>();
            _loginPageViewModel = SmObjectFactory.Container.GetInstance<LoginPageViewModel>();
            _gLListViewModel = SmObjectFactory.Container.GetInstance<GLListViewModel>();
            //_addEditGLViewModel = SmObjectFactory.Container.GetInstance<AddEditGLViewModel>();
            _tLListViewModel = SmObjectFactory.Container.GetInstance<TLListViewModel>();
          //  _addEditTLViewModel = SmObjectFactory.Container.GetInstance<AddEditTLViewModel>();
            _sLListViewModel = SmObjectFactory.Container.GetInstance<SLListViewModel>();
            //_addEditSLViewModel = SmObjectFactory.Container.GetInstance<AddEditSLViewModel>();
            _dLListViewModel = SmObjectFactory.Container.GetInstance<DLListViewModel>();
            _addEditDLViewModel = SmObjectFactory.Container.GetInstance<AddEditDLViewModel>();
            _bankListViewModel = SmObjectFactory.Container.GetInstance<BankListViewModel>();
            _addEditBankViewModel = SmObjectFactory.Container.GetInstance<AddEditBankViewModel>();
            _bankAccountListViewModel = SmObjectFactory.Container.GetInstance<BankAccountListViewModel>();
           // _addEditBankAccountViewModel = SmObjectFactory.Container.GetInstance<AddEditBankAccountViewModel>();

            _currencyListViewModel = SmObjectFactory.Container.GetInstance<CurrencyListViewModel>();
            _addEditCurrencyViewModel = SmObjectFactory.Container.GetInstance<AddEditCurrencyViewModel>();
            _exchangeRateListViewModel = SmObjectFactory.Container.GetInstance<ExchangeRateListViewModel>();
            _addEditExchangeRateViewModel = SmObjectFactory.Container.GetInstance<AddEditExchangeRateViewModel>();
            _personListViewModel = SmObjectFactory.Container.GetInstance<PersonListViewModel>();
            //_addEditPersonViewModel = SmObjectFactory.Container.GetInstance<AddEditPersonViewModel>();
            _companyListViewModel = SmObjectFactory.Container.GetInstance<CompanyListViewModel>();
            //_addEditCompanyViewModel = SmObjectFactory.Container.GetInstance<AddEditCompanyViewModel>();

            _accountDocumentListViewModel = SmObjectFactory.Container.GetInstance<AccountDocumentListViewModel>();
            _addEditAccountDocumentViewModel = SmObjectFactory.Container.GetInstance<AddEditAccountDocumentViewModel>();
            _documentNumberingListViewModel = SmObjectFactory.Container.GetInstance<DocumentNumberingListViewModel>();
            _addEditDocumentNumberingViewModel = SmObjectFactory.Container.GetInstance<AddEditDocumentNumberingViewModel>();
            _companyInformationViewModel = SmObjectFactory.Container.GetInstance<CompanyInformationViewModel>();
            _systemAccountingSettingViewModel = SmObjectFactory.Container.GetInstance<SystemAccountingSettingViewModel>();
            _generalSystemSettingViewModel = SmObjectFactory.Container.GetInstance<GeneralSystemSettingViewModel>();
            _shoppingSystemSettingViewModel = SmObjectFactory.Container.GetInstance<ShoppingSystemSettingViewModel>();
            _systemSettingSaleViewModel = SmObjectFactory.Container.GetInstance<SystemSettingSaleViewModel>();
            _systemReceivePaymentSettingViewModel = SmObjectFactory.Container.GetInstance<SystemReceivePaymentSettingViewModel>();
            _systemSettingRetailViewModel = SmObjectFactory.Container.GetInstance<SystemSettingRetailViewModel>();
            _salarySystemSettingViewModel = SmObjectFactory.Container.GetInstance<SalarySystemSettingViewModel>();
          //  _sLStandardDescriptionListViewModel = SmObjectFactory.Container.GetInstance<SLStandardDescriptionListViewModel>();
          //  _addEditSLStandardDescriptionViewModel = SmObjectFactory.Container.GetInstance<AddEditSLStandardDescriptionViewModel>();
            _stockListViewModel = SmObjectFactory.Container.GetInstance<StockListViewModel>();
            _addEditStockViewModel = SmObjectFactory.Container.GetInstance<AddEditStockViewModel>();
            _productListViewModel = SmObjectFactory.Container.GetInstance<ProductListViewModel>();
            _addEditProductViewModel = SmObjectFactory.Container.GetInstance<AddEditProductViewModel>();
            _typeDocumentListViewModel = SmObjectFactory.Container.GetInstance<TypeDocumentListViewModel>();
            _addEditTypeDocumentViewModel = SmObjectFactory.Container.GetInstance<AddEditTypeDocumentViewModel>();
          //  _relatedPersonListViewModel = SmObjectFactory.Container.GetInstance<RelatedPersonListViewModel>();
            //_addEditRelatedPersonViewModel = SmObjectFactory.Container.GetInstance<AddEditRelatedPersonViewModel>();
            //_addEditAccDocumentHeaderViewModel = SmObjectFactory.Container.GetInstance<AddEditAccDocumentHeaderViewModel>();
            _accDocumentHeaderListViewModel = SmObjectFactory.Container.GetInstance<AccDocumentHeaderListViewModel>();
            _accDocumentItemListViewModel = SmObjectFactory.Container.GetInstance<AccDocumentItemListViewModel>();
            //_addEditAccDocumentItemViewModel = SmObjectFactory.Container.GetInstance<AddEditAccDocumentItemViewModel>();
            _reviewAccountListViewModel = SmObjectFactory.Container.GetInstance<ReviewAccountListViewModel>();
            _convertDocumentListViewModel = SmObjectFactory.Container.GetInstance<ConvertDocumentListViewModel>();
            _closeProfitLossAccountListViewModel = SmObjectFactory.Container.GetInstance<CloseProfitLossAccountListViewModel>();
            _openingClosingListViewModel = SmObjectFactory.Container.GetInstance<OpeningClosingListViewModel>();
            _selectFinancialYearListViewModel = SmObjectFactory.Container.GetInstance<SelectFinancialYearListViewModel>();
            _currencyExchangeListViewModel = SmObjectFactory.Container.GetInstance<CurrencyExchangeListViewModel>();
            _tLDocumentHeaderListViewModel = SmObjectFactory.Container.GetInstance<TLDocumentHeaderListViewModel>();
            _tLDocumentItemListViewModel = SmObjectFactory.Container.GetInstance<TLDocumentItemListViewModel>();
            _treeAccountListViewModel = SmObjectFactory.Container.GetInstance<TreeAccountListViewModel>();
         
            _currencyExchangeRateDocHeaderListViewModel = SmObjectFactory.Container.GetInstance<CurrencyExchangeRateDocHeaderListViewModel>();
            _closeProfitLossDocListViewModel = SmObjectFactory.Container.GetInstance<CloseProfitLossDocListViewModel>();
            _openingClosingDocHeaderListViewModel = SmObjectFactory.Container.GetInstance<OpeningClosingDocHeaderListViewModel>();
            _currencyDocumentItemListViewModel = SmObjectFactory.Container.GetInstance<CurrencyDocumentItemListViewModel>();
            _closeProfitDocumentItemListViewModel = SmObjectFactory.Container.GetInstance<CloseProfitDocumentItemListViewModel>();
            _openingClosingDocItemListViewModel = SmObjectFactory.Container.GetInstance<OpeningClosingDocItemListViewModel>();
            _productModelListViewModel= SmObjectFactory.Container.GetInstance<ProductModelListViewModel>();
            _productTypeListViewModel= SmObjectFactory.Container.GetInstance<ProductTypeListViewModel>();
            _otherProductListViewModel = SmObjectFactory.Container.GetInstance<OtherProductListViewModel>();
            _productBrandListViewModel = SmObjectFactory.Container.GetInstance<ProductBrandListViewModel>();
            _measurementUnitListViewModel = SmObjectFactory.Container.GetInstance<MeasurementUnitListViewModel>();
            _incomingDocumentListViewModel = SmObjectFactory.Container.GetInstance<IncomingDocumentListViewModel>();
            _outputDocumentListViewModel = SmObjectFactory.Container.GetInstance<OutputDocumentListViewModel>();
            _returnInputListViewModel = SmObjectFactory.Container.GetInstance<ReturnInputListViewModel>();
            _returnOutputListViewModel = SmObjectFactory.Container.GetInstance<ReturnOutputListViewModel>();
            _transferWarehouseListViewModel = SmObjectFactory.Container.GetInstance<TransferWarehouseListViewModel>();
            _productRackListViewModel = SmObjectFactory.Container.GetInstance<ProductRackListViewModel>();
            // _productCodeWindowViewModel = SmObjectFactory.Container.GetInstance<ProductCodeWindowViewModel>();
            _documntsFlowViewModel = SmObjectFactory.Container.GetInstance<DocumntsFlowViewModel>();
            _testViewModel = SmObjectFactory.Container.GetInstance<TestViewModel>();
            _accessUtility = SmObjectFactory.Container.GetInstance<Behaviors.AccessUtility>();
            #endregion

            #region Subscribe Events
            _loginPageViewModel.Logedin += _loginPageViewModel_Logedin;
            //_customerListViewModel.PlaceOrderRequested += NavToOrder;
            //_customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            //_customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            //_addEditViewModel.Done += NavToCustomerList;
            _userListViewModel.AddUserRequested += NavToAddUser;
            _userListViewModel.EditUserRequested += NavToEditUser;
            _addEditUserViewModel.Done += NavToUserList;
            _groupListViewModel.AddGroupRequested += NavToAddGroup;
            _groupListViewModel.EditGroupRequested += NavToEditGroup;
            _addEditGroupViewModel.Done += NavToGroupList;
            _financialYearListViewModel.AddFinancialYearRequested += NavToAddFinancialYear;
            _financialYearListViewModel.EditFinancialYearRequested += NavToEditFinancialYear;
            _addEditFinancialYearViewModel.Done += NavToFinancialYearList;

            //_gLListViewModel.AddGLRequested += NavToAddGL;
            //_gLListViewModel.EditGLRequested += NavToEditGL;
            //_addEditGLViewModel.Done += NavToGLList;

            //_tLListViewModel.AddTLRequested += NavToAddTL;
          //  _tLListViewModel.EditTLRequested += NavToEditTL;
           // _addEditTLViewModel.Done += NavToTLList;

            //_sLListViewModel.AddSLRequested += NavToAddSL;
            //_sLListViewModel.EditSLRequested += NavToEditSL;
           // _sLListViewModel.AddSLStandardDescriptionRequested += NavToSLStandardDescriptionList;
           // _sLListViewModel.AddCompanyRequested += NavToSLCompanyList;
            //_sLStandardDescriptionListViewModel.AddSLStandardDescriptionRequested += NavToAddSLStandardDescription ;
            //_addEditSLViewModel.Done += NavToSLList;

            _dLListViewModel.AddDLRequested += NavToAddDL;
            _dLListViewModel.EditDLRequested += NavToEditDL;
            _addEditDLViewModel.Done += NavToDLList;

            _bankListViewModel.AddBankRequested += NavToAddBank;
            _bankListViewModel.EditBankRequested += NavToEditBank;
            _addEditBankViewModel.Done += NavToBankList;



            _currencyListViewModel.AddCurrencyRequested += NavToAddCurrency;
            _currencyListViewModel.EditCurrencyRequested += NavToEditCurrency;
            _addEditCurrencyViewModel.Done += NavToCurrencyList;

            _exchangeRateListViewModel.AddExchangeRateRequested += NavToAddExchangeRate;
            _exchangeRateListViewModel.EditExchangeRateRequested += NavToEditExchangeRate;
            _addEditExchangeRateViewModel.Done += NavToExchangeRateList;

            //_personListViewModel.AddPersonRequested += NavToAddPerson;
            //_personListViewModel.EditPersonRequested += NavToEditPerson;
            //_personListViewModel.AddRelatedPersonRequested += NavToRelatedPersonPersonList;
            //// _relatedPersonListViewModel.AddRelatedPersonRequested += NavToAddRelatedPerson;
            //_addEditPersonViewModel.Done += NavToPersonList;

            //_companyListViewModel.AddCompanyRequested += NavToAddCompany;
            //_companyListViewModel.EditCompanyRequested += NavToEditCompany;
            //_companyListViewModel.AddRelatedPersonRequested += NavToRelatedPersonCompanyList;
            //  _relatedPersonListViewModel.AddRelatedPersonRequested += NavToAddRelatedPerson;
           // _addEditCompanyViewModel.Done += NavToCompanyList;

          //  _bankAccountListViewModel.AddBankAccountRequested += NavToAddBankAccount;
          //  _bankAccountListViewModel.EditBankAccountRequested += NavToEditBankAccount;
          //  //_bankAccountListViewModel.AddRelatedPersonRequested += NavToRelatedPersonBankAccountList;
          ////  _relatedPersonListViewModel.AddRelatedPersonRequested += NavToAddRelatedPerson;
          //  _addEditBankAccountViewModel.Done += NavToBankAccountList;

            _documentNumberingListViewModel.AddDocumentNumberingRequested += NavToAddDocumentNumbering;
            _documentNumberingListViewModel.EditDocumentNumberingRequested += NavToEditDocumentNumbering;
            _addEditDocumentNumberingViewModel.Done += NavToDocumentNumberingList;

            _accountDocumentListViewModel.AddAccountDocumentRequested += NavToAddAccountDocument;
            _accountDocumentListViewModel.EditAccountDocumentRequested += NavToEditAccountDocument;
            _addEditAccountDocumentViewModel.Done += NavToAccountDocumentList;

            _stockListViewModel.AddStockRequested += NavToAddStock;
            _stockListViewModel.EditStockRequested += NavToEditStock;
            _addEditStockViewModel.Done += NavToStockList;

         //   _productListViewModel.AddProductRequested += NavToAddProduct;
           // _productListViewModel.EditProductRequested += NavToEditProduct;
            _addEditProductViewModel.Done += NavToProductList;

            _typeDocumentListViewModel.AddTypeDocumentRequested += NavToAddTypeDocument;
            _typeDocumentListViewModel.EditTypeDocumentRequested += NavToEditTypeDocument;
            _addEditTypeDocumentViewModel.Done += NavToTypeDocumentList;

            //_accDocumentHeaderListViewModel.AddAccDocumentHeaderRequested += NavToAddAccDocumentHeader;
            //_accDocumentHeaderListViewModel.EditAccDocumentHeaderRequested += NavToEditAccDocumentHeader;
            //_accDocumentHeaderListViewModel.AddAccDocumentItemRequested += NavToAccDocumentItemList;
            //_addEditAccDocumentHeaderViewModel.Done += NavToAccDocumentHeaderList;



            //  _reviewAccountListViewModel.AddReviewAccountRequested += NavToAccDocumentItemList;
            _reviewAccountListViewModel.Done += NavToReviewAccountList;
            _convertDocumentListViewModel.Done += NavToReviewAccountList;

            _closeProfitLossAccountListViewModel.Done += NavToCloseProfitLossAccountList;
            _closeProfitLossAccountListViewModel.ViewSystemDocumentHeaderRequested += NavToViewCloseProfitLossDocument;
            _closeProfitLossDocListViewModel.ReturnRequested += NavToRequestCloseProfitLossDocument;
            _closeProfitLossDocListViewModel.EditAccDocumentHeaderRequested += NavToEditCloseProfitDocumentHeader;
            _closeProfitDocumentItemListViewModel.Done += NavToViewCloseProfitLossDocument;

            _openingClosingListViewModel.Done += NavToOpeningClosingList;
            _openingClosingListViewModel.ViewSystemDocumentHeaderRequested += NavToViewOpeningClosingDocument;
            _openingClosingDocHeaderListViewModel.ReturnRequested += NavToRequestOpeningClosingDocument;
            _openingClosingDocHeaderListViewModel.EditAccDocumentHeaderRequested += NavToEditOpeningClosingDocumentHeader;
            _openingClosingDocItemListViewModel.Done += NavToViewOpeningClosingDocument;
            // OpeningClosingDocHeaderListViewModel
            _selectFinancialYearListViewModel.Done += NavToSelectFinancialYearList;

            _currencyExchangeListViewModel.Done += NavToCurrencyExchangeList;
            _currencyExchangeListViewModel.ViewSystemDocumentHeaderRequested += NavToViewCurrencyExchangeDocument;
            _currencyExchangeRateDocHeaderListViewModel.ReturnRequested += NavToRequestCurrencyExchangeDocument;
            _currencyExchangeRateDocHeaderListViewModel.EditAccDocumentHeaderRequested += NavToEditDocumentHeader;
            _currencyDocumentItemListViewModel.Done += NavToViewCurrencyExchangeDocument;
           // _editCurrencyExchangeRateDocHeaderViewModel.Done += NavToViewCurrencyExchangeDocument;
            // _systemDocumentHeaderListViewModel.ReturnRequested += NavToRequestDocument;
            //  _tLDocumentListViewModel.Done += NavToTLDocumentList;
            //_treeAccountListViewModel.Done += NavToTreeAccountList;

            //_accountDocumentListViewModel.AddAccountDocumentRequested += NavToAddAccountDocument;
            //_accountDocumentListViewModel.EditAccountDocumentRequested += NavToEditAccountDocument;
            //_addEditAccountDocumentViewModel.Done += NavToAccountDocumentList;


            _tLDocumentHeaderListViewModel.ViewTLDocumentItemRequested += NavToViewTLDocumentItem;
            _tLDocumentItemListViewModel.Done += NavToTLDocumentHeaderList;

            
            //ReviewAccountListView
            _reviewAccountListViewModel.TestRequested += _reviewAccountListViewModel_TestRequested;
            _documntsFlowViewModel.TestRequested += _reviewAccountListViewModel_TestRequested;
            _reviewAccountListViewModel.DetailRequested += _reviewAccountListViewModel_DetailRequested;
            //step8
            _currencyExchangeListViewModel.AccRequested += _currencyExchangeListViewModel_AccRequested;
            _accDocumentHeaderListViewModel.SLRequested += _accDocumentHeaderListViewModel_SLRequested;
            _openingClosingListViewModel.AccRequested +=         _openingClosingListViewModel_AccRequested;
            _closeProfitLossAccountListViewModel.AccRequested += _closeProfitLossAccountListViewModel_AccRequested;
            _dLListViewModel.DLTypeRequested += _dLListViewModel_DLTypeRequested;
            _dLListViewModel.BankCurrencyRequested += _dLListViewModel_BankCurrencyRequested;
            #endregion

            IsAuthenticated = false;

            NavCommand = new RelayCommand<string>(OnNav);
            PreviewCloseTabCommand = new RelayCommand<string>(OnPreviewCloseTab);
            CloseTabCommand = new RelayCommand(OnCloseTab);

            ActivePane = new RadPane { Name = "login", Header = "", Tag = "Bottom", Content = _loginPageViewModel ,Focusable=true,IsActive=true,IsTabStop=true};
            Tabs = new ObservableCollection<RadPane>() { ActivePane };
            _navs = new Dictionary<string, BindableBase>
            {
                {"orderPrep",_orderPrepViewModel },
                //{"CustomerListView",_customerListViewModel },
                {"CustomerListViewOrder",_orderPrepViewModel },
                {"UserListView",_userListViewModel },
                {"GroupListView",_groupListViewModel },
                {"FinancialYearListView",_financialYearListViewModel },
                {"DocumentnumberingInformationView",_documentnumberingInformationViewModel },
                {"GLListView",_gLListViewModel },
                {"TLListView",_tLListViewModel },
                {"SLListView",_sLListViewModel },
                {"DLListView",_dLListViewModel },
                {"BankListView",_bankListViewModel },
                {"BankAccountListView",_bankAccountListViewModel },
                {"CurrencyListView",_currencyListViewModel },
                {"ExchangeRateListView",_exchangeRateListViewModel },
                {"PersonListView",_personListViewModel },
                {"CompanyListView",_companyListViewModel },
                {"AccountDocumentListView",_accountDocumentListViewModel },
                {"DocumentNumberingListView",_documentNumberingListViewModel },
                {"CompanyInformationView",_companyInformationViewModel },
                {"SystemAccountingSettingView",_systemAccountingSettingViewModel},
                {"GeneralSystemSettingView",_generalSystemSettingViewModel},
                {"ShoppingSystemSettingView",_shoppingSystemSettingViewModel},
                {"SystemSettingSaleView",_systemSettingSaleViewModel},
                {"SystemReceivePaymentSettingView",_systemReceivePaymentSettingViewModel},
                {"SystemSettingRetailView",_systemSettingRetailViewModel},
                {"SalarySystemSettingView",_salarySystemSettingViewModel},
              //  {"SLStandardDescriptionListView",_sLStandardDescriptionListViewModel},
                {"StockListView",_stockListViewModel},
                {"ProductListView",_productListViewModel},
                {"TypeDocumentListView",_typeDocumentListViewModel},
                {"RelatedPersonListView",_relatedPersonListViewModel},
                {"AccDocumentHeaderListView",_accDocumentHeaderListViewModel},
                {"AccDocumentItemListView",_accDocumentItemListViewModel},
                {"ReviewAccountListView",_reviewAccountListViewModel},
                {"ConvertDocumentListView",_convertDocumentListViewModel},
                {"CloseProfitLossAccountListView",_closeProfitLossAccountListViewModel},
                {"OpeningClosingListView",_openingClosingListViewModel},
                {"SelectFinancialYearListView",_selectFinancialYearListViewModel},
                {"CurrencyExchangeListView",_currencyExchangeListViewModel},
                {"TreeAccountListView",_treeAccountListViewModel},
                {"TLDocumentHeaderListView",_tLDocumentHeaderListViewModel},
                {"CurrencyDocumentItemListView",_currencyDocumentItemListViewModel},
                {"CloseProfitDocumentItemListView",_closeProfitDocumentItemListViewModel},
                {"ProductModelListView",_productModelListViewModel},
                {"ProductTypeListView",_productTypeListViewModel},
                {"ProductBrandListView",_productBrandListViewModel},
                {"OtherProductListView",_otherProductListViewModel},
                {"MeasurementUnitListView",_measurementUnitListViewModel},
                {"IncomingDocumentListView",_incomingDocumentListViewModel},
                {"OutputDocumentListView",_outputDocumentListViewModel},
                {"ReturnInputListView",_returnInputListViewModel},
                {"ReturnOutputListView",_returnOutputListViewModel},
                {"TransferWarehouseListView",_transferWarehouseListViewModel},
                {"ProductRackListView",_productRackListViewModel},
                {"DocumntsFlowView",_documntsFlowViewModel},
                {"TestView",_testViewModel},




            };

            _ResourceManager = new ResourceManager("Saina.WPF.Properties.Resources", Assembly.GetExecutingAssembly());
            IDynamicPagesService _dynamicPagesService = SmObjectFactory.Container.GetInstance<IDynamicPagesService>();
            _appContextService = appContextService; //SmObjectFactory.Container.GetInstance<AppContextService>();
            _shoppingSystemSettingsService = shoppingSystemSettingsService;
            _appContextService.PropertyChanged += async (se, ea) =>
            {
                if (ea.PropertyName == "CurrentUser")
                {
                    try
                    {
                        _allDynamicPages = (await _dynamicPagesService.GetDynamicPagesAsync(true)).ToList();

                        //var newList = _allDynamicPages.ConvertAll(Mapper.Map<DynamicPage, DynamicPage>);//clone
                        DynamicPages = new ObservableCollection<DynamicPage>(await _dynamicPagesService.GetDynamicPagesAsync(false));
                        for (int i = 0; i < DynamicPages.Count; i++)
                        {
                            var xx = Filter(DynamicPages[i]);
                        }
                        for (int i = 0; i < _allDynamicPages.Count; i++)
                        {
                            var xx = Filter(_allDynamicPages[i]);
                        }
                    }
                    catch (Exception)
                    { }

                }
            };
        }

        private void _reviewAccountListViewModel_TestRequested(int headerId, int date,bool hasFlow)
        {
            _accDocumentHeaderListViewModel.HeaderId = headerId;
            _accDocumentHeaderListViewModel.HasFlow = hasFlow;
            _accDocumentHeaderListViewModel.DateFlow = date;
            OnNav("AccDocumentHeaderListView");
           
        }
        private void _reviewAccountListViewModel_DetailRequested(AccDocumentItemDTO accDocumentItemDTO, string s)
        {
            _documntsFlowViewModel.AccDocumentItemDTO = accDocumentItemDTO;
            _documntsFlowViewModel.State = s;
            var people = Tabs.FirstOrDefault(x => x.Name == "DocumntsFlowView");
            if (people == null)
            {
                ActivePane = new RadPane { Name = "DocumntsFlowView", Header = "گردش مرور", Tag = "Bottom", Content = _navs["DocumntsFlowView"], DataContext = _navs["DocumntsFlowView"] };
                Tabs.Add(ActivePane);
            }
            ActivePane.Header = "گردش مرور";
            OnNav("DocumntsFlowView");
            
        }
        
        //step6
        private void _currencyExchangeListViewModel_AccRequested(TypeEnum typeEnum)
        {
            _accDocumentHeaderListViewModel.TypeEnum = (int)(typeEnum);

            //  _currencyExchangeListViewModel.TypeEnum = typeEnum;
            OnNav("AccDocumentHeaderListView");
        }
        private void _accDocumentHeaderListViewModel_SLRequested(SLDLEnum slDLEnum)
        {
            if (slDLEnum==SLDLEnum.SL)
            {
            OnNav("SLListView");

            }
            else if (slDLEnum == SLDLEnum.DL)
            {
                OnNav("DLListView");

            }
        }
        
        private void _openingClosingListViewModel_AccRequested(TypeEnum typeEnum)
        {
            _accDocumentHeaderListViewModel.TypeEnum = (int)(typeEnum);
          //  _openingClosingListViewModel.TypeEnum = typeEnum;
            OnNav("AccDocumentHeaderListView");
        }
        private void _closeProfitLossAccountListViewModel_AccRequested(TypeEnum typeEnum)
        {
            _accDocumentHeaderListViewModel.TypeEnum = (int)(typeEnum);

            // _closeProfitLossAccountListViewModel.TypeEnum = typeEnum;
            OnNav("AccDocumentHeaderListView");
          
        }
        
        private void _dLListViewModel_DLTypeRequested(DLTypeEnum dLTypeEnum,long? dCode)
        {
            _dLListViewModel.DLTypeEnum = dLTypeEnum;
            if (dLTypeEnum== DLTypeEnum.BankAccount)
            {
                IsBusy = true;
               
                var bankAccounts = Tabs.FirstOrDefault(x => x.Name == "BankAccountListView");
                if (bankAccounts == null)
                {
                    ActivePane = new RadPane { Name = "BankAccountListView", Header = "فهرست حساب بانکی", Tag = "Bottom", Content = _navs["BankAccountListView"], DataContext = _navs["BankAccountListView"] };
                    Tabs.Add(ActivePane);
                }
                ActivePane.Header = "فهرست حساب بانکی";
                OnNav("BankAccountListView");
                IsBusy = false;
            }
            else if (dLTypeEnum == DLTypeEnum.People)
            {
                IsBusy = true;
                _personListViewModel.DCode = dCode;
                var people = Tabs.FirstOrDefault(x => x.Name == "PersonListView");
                if (people == null)
                {
                    ActivePane = new RadPane { Name = "PersonListView", Header = "فهرست اشخاص", Tag = "Bottom", Content = _navs["PersonListView"], DataContext = _navs["PersonListView"] };
                    Tabs.Add(ActivePane);
                }
                ActivePane.Header = "فهرست اشخاص";
                //
                //people.Header = "فهرست اشخاص";
                OnNav("PersonListView");
                IsBusy = false;
            }
            else if (dLTypeEnum == DLTypeEnum.Company)
            {
                IsBusy = true;
                var company = Tabs.FirstOrDefault(x => x.Name == "CompanyListView");
                if (company == null)
                {
                    ActivePane = new RadPane { Name = "CompanyListView", Header = "فهرست شرکت ها", Tag = "Bottom", Content = _navs["CompanyListView"], DataContext = _navs["CompanyListView"] };
                    Tabs.Add(ActivePane);
                }
                ActivePane.Header = "فهرست شرکت ها";
                OnNav("CompanyListView");
                IsBusy = false;
            }

        }
        private void _dLListViewModel_BankCurrencyRequested(BankCurrencyEnum bankCurrencyEnum)
        {
            _dLListViewModel.BankCurrencyEnum = bankCurrencyEnum;
            if (bankCurrencyEnum == BankCurrencyEnum.Bank)
            {
                IsBusy = true;

                OnNav("BankListView");
                IsBusy = false;
            }
            else if (bankCurrencyEnum == BankCurrencyEnum.Currency)
            {
                IsBusy = true;
               
                OnNav("CurrencyListView");
                IsBusy = false;
            }
            

        }



        bool Filter(DynamicPage node)
        {
            //remove children
            foreach (DynamicPage child in node.Children.Where(c => !Filter(c)).ToArray())
                node.Children.Remove(child);
            //return if should be retained
            return _appContextService.CurrentUser.HasAccess(node.Name)
                && (_navs.ContainsKey(node.Name) || node.Children.Count > 0);
        }
        #endregion
        #region Fields
        private readonly IAppContextService _appContextService;
        private readonly IShoppingSystemSettingsService _shoppingSystemSettingsService;
        
        private readonly IDictionary<string, BindableBase> _navs;
        private readonly ResourceManager _ResourceManager;
      //  public CompanyInformationModel CompanyInformationModel;
        private UnauthorizedViewModel _unauthorizedViewModel;
        private OrderViewModel _orderViewModel;
        private OrderPrepViewModel _orderPrepViewModel;
        //private CustomerListViewModel _customerListViewModel;
        //private AddEditCustomerViewModel _addEditViewModel;
        private UserListViewModel _userListViewModel;
        private AddEditUserViewModel _addEditUserViewModel;
        private GroupListViewModel _groupListViewModel;
        private AddEditGroupViewModel _addEditGroupViewModel;
        private FinancialYearListViewModel _financialYearListViewModel;
        private CompanyInformationViewModel _documentnumberingInformationViewModel;
        private LoginPageViewModel _loginPageViewModel;
        private AddEditFinancialYearViewModel _addEditFinancialYearViewModel;
        private GLListViewModel _gLListViewModel;
      //  private AddEditGLViewModel _addEditGLViewModel;
        private TLListViewModel _tLListViewModel;
       // private AddEditTLViewModel _addEditTLViewModel;
        private SLListViewModel _sLListViewModel;
       // private AddEditSLViewModel _addEditSLViewModel;
        private DLListViewModel _dLListViewModel;
        private AddEditDLViewModel _addEditDLViewModel;
        private BankListViewModel _bankListViewModel;
        private AddEditBankViewModel _addEditBankViewModel;
        private BankAccountListViewModel _bankAccountListViewModel;
      // private AddEditBankAccountViewModel _addEditBankAccountViewModel;
        private CurrencyListViewModel _currencyListViewModel;
        private AddEditCurrencyViewModel _addEditCurrencyViewModel;
        private ExchangeRateListViewModel _exchangeRateListViewModel;
        private AddEditExchangeRateViewModel _addEditExchangeRateViewModel;
        private PersonListViewModel _personListViewModel;
     //   private AddEditPersonViewModel _addEditPersonViewModel;
        private CompanyListViewModel _companyListViewModel;
        private AddEditCompanyViewModel _addEditCompanyViewModel;
        private AccountDocumentListViewModel _accountDocumentListViewModel;
        private AddEditAccountDocumentViewModel _addEditAccountDocumentViewModel;
        private DocumentNumberingListViewModel _documentNumberingListViewModel;
        private AddEditDocumentNumberingViewModel _addEditDocumentNumberingViewModel;
        private CompanyInformationViewModel _companyInformationViewModel;
        private SystemAccountingSettingViewModel _systemAccountingSettingViewModel;
        private GeneralSystemSettingViewModel _generalSystemSettingViewModel;
        private ShoppingSystemSettingViewModel _shoppingSystemSettingViewModel;
        private SystemSettingSaleViewModel _systemSettingSaleViewModel;
        private SalarySystemSettingViewModel _salarySystemSettingViewModel;
        private SystemSettingRetailViewModel _systemSettingRetailViewModel;
        private SystemReceivePaymentSettingViewModel _systemReceivePaymentSettingViewModel;
    //    private SLStandardDescriptionListViewModel _sLStandardDescriptionListViewModel;
    //    private AddEditSLStandardDescriptionViewModel _addEditSLStandardDescriptionViewModel;
        private StockListViewModel _stockListViewModel;
        private AddEditStockViewModel _addEditStockViewModel;
        private ProductListViewModel _productListViewModel;
        private AddEditProductViewModel _addEditProductViewModel;
        private TypeDocumentListViewModel _typeDocumentListViewModel;
        private AddEditTypeDocumentViewModel _addEditTypeDocumentViewModel;
        private RelatedPersonListViewModel _relatedPersonListViewModel;
        private AddEditRelatedPersonViewModel _addEditRelatedPersonViewModel;
       // private AddEditAccDocumentHeaderViewModel _addEditAccDocumentHeaderViewModel;
        private AccDocumentHeaderListViewModel _accDocumentHeaderListViewModel;
        private AccDocumentItemListViewModel _accDocumentItemListViewModel;
        //  private AddEditAccDocumentItemViewModel _addEditAccDocumentItemViewModel;
        private ReviewAccountListViewModel _reviewAccountListViewModel;
        private ConvertDocumentListViewModel _convertDocumentListViewModel;
        private CloseProfitLossAccountListViewModel _closeProfitLossAccountListViewModel;
        private OpeningClosingListViewModel _openingClosingListViewModel;
        private SelectFinancialYearListViewModel _selectFinancialYearListViewModel;
        private CurrencyExchangeListViewModel _currencyExchangeListViewModel;
        private TLDocumentHeaderListViewModel _tLDocumentHeaderListViewModel;
        private TLDocumentItemListViewModel _tLDocumentItemListViewModel;
        private TreeAccountListViewModel _treeAccountListViewModel;
      
        private CurrencyExchangeRateDocHeaderListViewModel _currencyExchangeRateDocHeaderListViewModel;
        private CloseProfitLossDocListViewModel _closeProfitLossDocListViewModel;
        private OpeningClosingDocHeaderListViewModel _openingClosingDocHeaderListViewModel;
        private CurrencyDocumentItemListViewModel _currencyDocumentItemListViewModel;
        private CloseProfitDocumentItemListViewModel _closeProfitDocumentItemListViewModel;
        private OpeningClosingDocItemListViewModel _openingClosingDocItemListViewModel;
        private ProductModelListViewModel _productModelListViewModel;
        private ProductTypeListViewModel _productTypeListViewModel;
        private OtherProductListViewModel _otherProductListViewModel;
        private ProductBrandListViewModel _productBrandListViewModel;
        private MeasurementUnitListViewModel _measurementUnitListViewModel;
        private IncomingDocumentListViewModel _incomingDocumentListViewModel;
        private OutputDocumentListViewModel _outputDocumentListViewModel;
        private ReturnInputListViewModel _returnInputListViewModel;
        private ReturnOutputListViewModel _returnOutputListViewModel;
        private TransferWarehouseListViewModel _transferWarehouseListViewModel;
        private ProductRackListViewModel _productRackListViewModel;
        private DocumntsFlowViewModel _documntsFlowViewModel;
        private TestViewModel _testViewModel;
        private Saina.WPF.Behaviors.AccessUtility _accessUtility;
        // private ProductCodeWindowViewModel _productCodeWindowViewModel;


        //  private EditCurrencyExchangeRateDocHeaderViewModel _editCurrencyExchangeRateDocHeaderViewModel;

        // private EditCurrencyExchangeRateDocHeaderViewModel _editCurrencyExchangeRateDocHeaderViewModel;
        #endregion
        #region Commands
        public ICommand NavCommand { get; private set; }
        public ICommand CloseTabCommand { get; private set; }
        public event Action UnauthorizedViewModel;
        #endregion
        #region Properties
        private ObservableCollection<RadPane> _tabs;

        public ObservableCollection<RadPane> Tabs
        {
            get { return _tabs; }
            set { SetProperty(ref _tabs, value); }
        }

        public int NavToCompanyInformationList { get; }
        private RadPane _activePane;
        private RadPane _closePane;
        public RadPane ActivePane
        {
            get { return _activePane; }
            set { if (value == null || value.Tag as string == "Bottom") SetProperty(ref _activePane, value);if(value!= null) value.CanFloat = false; }
        }

        public RelayCommand<string> PreviewCloseTabCommand { get; private set; }
        private bool _isAuthenticated;

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { SetProperty(ref _isAuthenticated, value); }
        }

        private ObservableCollection<DynamicPage> _dynamicPages;
        private List<DynamicPage> _allDynamicPages;
        private SainaDbContext _uow;

        public ObservableCollection<DynamicPage> DynamicPages
        {
            get { return _dynamicPages; }
            set { SetProperty(ref _dynamicPages, value); }
        }
        public event Action<string> Failed;
        #endregion
        #region Methods
        private void OnPreviewCloseTab(string header)
        {
            _closePane = Tabs.First(x => x.Name.ToString().StartsWith(header));
        }
        private void _loginPageViewModel_Logedin(string message)
        {
            if (message == "OK" && ActivePane!=null)
            {
                IsAuthenticated = true;
                ((BindableBase)ActivePane.Content).UnLoaded();
                Tabs.Remove(ActivePane);
            }
        }
        private void OnNav(string destination)
        {
            IsBusy = true;
            
            var hasAccess = _appContextService.HasAccess(destination, Permission.Select);
            var year = _appContextService.CurrentFinancialYear;
            if ((year == 0 && destination != "FinancialYearListView"))
            {
                Failed?.Invoke("سال مالی انتخاب نشده است");
                //  destination = "FinancialYearListView";
            }

            //else if ((ProductCodeLenght == 0 ||
            //     ProductTypeLenght == 0 ||
            //     ProductBrandLenght == 0 ||
            //     ProductModelLenght == 0 ||
            //     OtherProductLenght == 0 ||
            //     IranCodeProduct == 0 ||
            //     NumberLevel == 0) && destination != "ShoppingSystemSettingView")
            //{
            //    Failed?.Invoke("تنظیمات بازرگانی خرید انجام نشده است");
            //}
            if ((hasAccess || true && (year != 0 || destination == "FinancialYearListView")))
            {
              //  if ((ProductCodeLenght != 0 &&
              //ProductTypeLenght != 0 &&
              //ProductBrandLenght != 0 &&
              //ProductModelLenght != 0 &&
              //OtherProductLenght != 0 &&
              //IranCodeProduct != 0 &&
              //NumberLevel != 0) || destination == "ShoppingSystemSettingView")
              //  {
                    var children = _allDynamicPages.FirstOrDefault(x => x.Name == destination)?.Children;
                    var notRoot = children == null || children?.Count == 0;
                    if (!string.IsNullOrWhiteSpace(destination) && notRoot)
                    {
                        ActivePane = Tabs.FirstOrDefault(x => x.Name.ToString().StartsWith(destination));
                        if (ActivePane == null && notRoot)
                        {
                            string header;
                            try
                            {
                                header = _allDynamicPages.FirstOrDefault(x => x.Name == destination).Title;//_ResourceManager.GetString(destination);
                            }
                            catch
                            {
                                header = destination;
                            }
                            ActivePane = new RadPane { Name = destination, Header = header, Tag = "Bottom", Content = _navs[destination], DataContext = _navs[destination] };
                            Tabs.Add(ActivePane);
                        }
                    };
                //}
           }

            //else
            //{
            //    ActivePane = new RadPane { Name = destination, Header = "عدم دسترسی", Tag = "Bottom", Content = _unauthorizedViewModel, DataContext = _unauthorizedViewModel };
            //    Tabs.Add(ActivePane);
            //    UnauthorizedViewModel?.Invoke();
            //}
            IsBusy = false;
        }
        private void OnCloseTab()
        {
            ((BindableBase)_closePane.Content).UnLoaded();
            Tabs.Remove(_closePane);
        }

        private void NavToOrder(int customerId)
        {
            _orderViewModel.CustomerId = customerId;
            OnNav("CustomerListViewOrder");
        }

        private void NavToAddFinancialYear(FinancialYear financialYear)
        {
            IsBusy = true;
            _addEditFinancialYearViewModel.EditMode = false;
            _addEditFinancialYearViewModel.SetFinancialYear(financialYear);
            var financialYears = Tabs.First(x => x.Name == "FinancialYearListView");
            financialYears.Header = "افزودن سال مالی";
            financialYears.Name = "FinancialYearListViewAddEditFinancialYear";
            financialYears.Content = _addEditFinancialYearViewModel;
            IsBusy = false;
        }
        private void NavToEditFinancialYear(FinancialYear financialYear)
        {
            IsBusy = true;
            _addEditFinancialYearViewModel.EditMode = true;
            _addEditFinancialYearViewModel.SetFinancialYear(financialYear);

            var financialYears = Tabs.First(x => x.Name == "FinancialYearListView");
            financialYears.Header = "ویرایش سال مالی";
            financialYears.Name = "FinancialYearListViewAddEditFinancialYear";
            financialYears.Content = _addEditFinancialYearViewModel;
            IsBusy = false;
        }
        private void NavToFinancialYearList()
        {
            IsBusy = true;
            var financialYears = Tabs.First(x => x.Name == "FinancialYearListViewAddEditFinancialYear");
            financialYears.Header = "سال مالی";
            financialYears.Name = "FinancialYearListView";
            financialYears.Content = _financialYearListViewModel;
            IsBusy = false;
        }

        private void NavToAddGroup(ApplicationCore.Entities.BasicInformation.UserAndGroup.Group group)
        {
            IsBusy = true;
            _addEditGroupViewModel.EditMode = false;
            _addEditGroupViewModel.SetGroup(group);

            var groups = Tabs.First(x => x.Name == "GroupListView");
            groups.Header = "افزودن گروه";
            groups.Name = "GroupListViewAddEditGroup";
            groups.Content = _addEditGroupViewModel;
            IsBusy = false;
        }
        private void NavToEditGroup(ApplicationCore.Entities.BasicInformation.UserAndGroup.Group group)
        {
            IsBusy = true;
            _addEditGroupViewModel.EditMode = true;
            _addEditGroupViewModel.SetGroup(group);

            var groups = Tabs.First(x => x.Name == "GroupListView");
            groups.Header = "ویرایش گروه";
            groups.Name = "GroupListViewAddEditGroup";
            groups.Content = _addEditGroupViewModel;
            IsBusy = false;
        }
        private void NavToGroupList()
        {
            IsBusy = true;
            var groups = Tabs.First(x => x.Name == "GroupListViewAddEditGroup");
            groups.Header = "دسترسی گروه";
            groups.Name = "GroupListView";
            groups.Content = _groupListViewModel;
            IsBusy = false;
        }
        private void NavToAddUser(User user)
        {
            IsBusy = true;
            _addEditUserViewModel.EditMode = false;
            _addEditUserViewModel.SetUser(user);

            var users = Tabs.First(x => x.Name == "UserListView");
            users.Header = "افزودن کاربر";
            users.Name = "UserListViewAddEditUser";
            users.Content = _addEditUserViewModel;
            IsBusy = false;
        }
        private void NavToEditUser(User user)
        {
            IsBusy = true;
            _addEditUserViewModel.EditMode = true;
            _addEditUserViewModel.SetUser(user);

            var users = Tabs.First(x => x.Name == "UserListView");
            users.Header = "ویرایش کاربر";
            users.Name = "UserListViewAddEditUser";
            users.Content = _addEditUserViewModel;
            IsBusy = false;
        }
        private void NavToUserList()
        {
            IsBusy = true;
            var users = Tabs.First(x => x.Name == "UserListViewAddEditUser");
            users.Header = "دسترسی کابران";
            users.Name = "UserListView";
            users.Content = _userListViewModel;
            IsBusy = false;
        }

        //private void NavToAddGL(GL gL)
        //{
        //    IsBusy = true;
        //    _addEditGLViewModel.EditMode = false;
        //   _addEditGLViewModel.SetGL(gL);

        //    var gLs = Tabs.First(x => x.Name == "GLListView");
        //    gLs.Header = "افزودن حساب گروه";
        //    gLs.Name = "GLListViewAddEditGL";
        //    gLs.Content = _addEditGLViewModel;
        //    IsBusy = false;
        //}
        //private void NavToEditGL(GL gL)
        //{
        //    IsBusy = false;
        //    _addEditGLViewModel.EditMode = true;
        //    _addEditGLViewModel.SetGL(gL);

        //    var gLs = Tabs.First(x => x.Name == "GLListView");
        //    gLs.Header = "ویرایش حساب";
        //    gLs.Name = "GLListViewAddEditGL";
        //    gLs.Content = _addEditGLViewModel;
        //    IsBusy = false;
        //}
        private void NavToGLList()
        {
            IsBusy = true;
            var gLs = Tabs.First(x => x.Name == "GLListViewAddEditGL");
            gLs.Header = "حساب گروه";
            gLs.Name = "GLListView";
            gLs.Content = _gLListViewModel;
            IsBusy = false;
        }

        //private void NavToAddTL(TL tL)
        //{
        //    IsBusy = true;
        //    _addEditTLViewModel.EditMode = false;
        //    _addEditTLViewModel.SetTL(tL);

        //    var tLs = Tabs.First(x => x.Name == "TLListView");
        //    tLs.Header = "افزودن حساب کل";
        //    tLs.Name = "TLListViewAddEditTL";
        //    tLs.Content = _addEditTLViewModel;
        //    IsBusy = false;
        //}
        //private void NavToEditTL(TL tL)
        //{
        //    IsBusy = true;
        //    _addEditTLViewModel.EditMode = true;
        //    _addEditTLViewModel.SetTL(tL);

        //    var tLs = Tabs.First(x => x.Name == "TLListView");
        //    tLs.Header = "ویرایش حساب";
        //    tLs.Name = "TLListViewAddEditTL";
        //    tLs.Content = _addEditTLViewModel;
        //    IsBusy = false;
        //}
        private void NavToTLList()
        {
            IsBusy = true;
            var tLs = Tabs.First(x => x.Name == "TLListViewAddEditTL");
            tLs.Header = "حساب کل";
            tLs.Name = "TLListView";
            tLs.Content = _tLListViewModel;
            IsBusy = false;
        }

        //private void NavToAddSLStandardDescription(int sLid)
        //{
        //    IsBusy = true;
        //    _sLStandardDescriptionListViewModel.SLId = sLid;
        //    _addEditSLStandardDescriptionViewModel.EditMode = false;
        //    _addEditSLStandardDescriptionViewModel.SetSLStandardDescription(new SLStandardDescription { SLId = sLid });

        //    var sLStandardDescriptions = Tabs.First(x => x.Name == "SLListViewSLStandardDescriptionListView");
        //    sLStandardDescriptions.Header = "افزودن شرح استاندارد";
        //    sLStandardDescriptions.Name = "SLListViewAddEditSLStandardDescriptionView";
        //    sLStandardDescriptions.Content = _addEditSLStandardDescriptionViewModel;
        //    IsBusy = false;
        //}
        //private void NavToEditSLStandardDescription(SLStandardDescription sLStandardDescription)
        //{
        //    IsBusy = true;
        //    _addEditSLStandardDescriptionViewModel.EditMode = true;
        //    _addEditSLStandardDescriptionViewModel.SetSLStandardDescription(sLStandardDescription);

        //    var sLStandardDescriptions = Tabs.First(x => x.Name == "SLStandardDescriptionListView");
        //    sLStandardDescriptions.Header = "ویرایش شرح استاندارد";
        //    sLStandardDescriptions.Name = "SLStandardDescriptionListViewAddEditSLStandardDescription";
        //    sLStandardDescriptions.Content = _addEditSLStandardDescriptionViewModel;
        //    IsBusy = true;
        //}
        //private void NavToSLStandardDescriptionList(SL sl)
        //{
        //    IsBusy = true;
        //    _sLStandardDescriptionListViewModel.SLId = sl.SLId;
        //    var sLStandardDescriptions = Tabs.First(x => x.Name == "SLListView");
        //    sLStandardDescriptions.Header = "شرح استاندارد";
        //    sLStandardDescriptions.Name = "SLListViewSLStandardDescriptionListView";
        //    sLStandardDescriptions.Content = _sLStandardDescriptionListViewModel;
        //    IsBusy = false;
        //}
        private void NavToSLCompanyList(SL sl)
        {
            IsBusy = true;
            _companyListViewModel.SLId = sl.SLId;
            var sLCompanys = Tabs.First(x => x.Name == "SLListView");
            sLCompanys.Header = "شرح استاندارد";
            sLCompanys.Name = "SLListViewCompanyListView";
            sLCompanys.Content = _companyListViewModel;
            IsBusy = false;
        }
        //private void NavToAddSL(SL sL)
        //{
        //    IsBusy = true;
        //    _addEditSLViewModel.EditMode = false;
        //    _addEditSLViewModel.SetSL(sL);

        //    var sLs = Tabs.First(x => x.Name == "SLListView");
        //    sLs.Header = "افزودن حساب معین";
        //    sLs.Name = "SLListViewAddEditSL";
        //    sLs.Content = _addEditSLViewModel;
        //    IsBusy = false;
        //}
        //private void NavToEditSL(SL sL)
        //{
        //    IsBusy = true;
        //    _addEditSLViewModel.EditMode = true;
        //    _addEditSLViewModel.SetSL(sL);
        //    var temp = sL.SLStandardDescriptions.ToList(); //_addEditSLViewModel.SLStandardDescriptionListViewModel.SLId = sL.SLId;
        //    _addEditSLViewModel.SLStandardDescriptionListViewModel.SLStandardDescriptions = new ObservableCollection<SLStandardDescription>(temp);
        //    var sLs = Tabs.First(x => x.Name == "SLListView");
        //    sLs.Header = "ویرایش حساب";
        //    sLs.Name = "SLListViewAddEditSL";
        //    sLs.Content = _addEditSLViewModel;
        //    IsBusy = false;
        //}
        private void NavToSLList()
        {
            IsBusy = true;
            var sLs = Tabs.First(x => x.Name == "SLListViewAddEditSL");
            sLs.Header = "حساب معین";
            sLs.Name = "SLListView";
            sLs.Content = _sLListViewModel;
            IsBusy = false;
        }

        private void NavToAddDL(DL dL)
        {
            IsBusy = true;
            _addEditDLViewModel.EditMode = false;
            _addEditDLViewModel.SetDL(dL);

            var dLs = Tabs.First(x => x.Name == "DLListView");
            dLs.Header = "افزودن حساب تفصیل";
            dLs.Name = "DLListViewAddEditDL";
            dLs.Content = _addEditDLViewModel;
            IsBusy = false;
        }
        private void NavToEditDL(DL dL)
        {
            IsBusy = true;
            _addEditDLViewModel.EditMode = true;
            _addEditDLViewModel.SetDL(dL);

            var dLs = Tabs.First(x => x.Name == "DLListView");
            dLs.Header = " ویرایش حساب";
            dLs.Name = "DLListViewAddEditDL";
            dLs.Content = _addEditDLViewModel;
            IsBusy = false;
        }
        private void NavToDLList()
        {
            IsBusy = true;
            var dLs = Tabs.First(x => x.Name == "DLListViewAddEditDL");
            dLs.Header = "حساب تفصیل";
            dLs.Name = "DLListView";
            dLs.Content = _dLListViewModel;
            IsBusy = false;
        }

        private void NavToAddBank(Bank bank)
        {
            IsBusy = true;
            _addEditBankViewModel.EditMode = false;
            _addEditBankViewModel.SetBank(bank);

            var banks = Tabs.First(x => x.Name == "BankListView");
            banks.Header = "افزودن بانک";
            banks.Name = "BankListViewAddEditBank";
            banks.Content = _addEditBankViewModel;
            IsBusy = false;
        }
        private void NavToEditBank(Bank bank)
        {
            IsBusy = true;
            _addEditBankViewModel.EditMode = true;
            _addEditBankViewModel.SetBank(bank);

            var banks = Tabs.First(x => x.Name == "BankListView");
            banks.Header = "ویرایش بانک";
            banks.Name = "BankListViewAddEditBank";
            banks.Content = _addEditBankViewModel;
            IsBusy = false;
        }

        private void NavToBankList()
        {
            IsBusy = true;
            var banks = Tabs.First(x => x.Name == "BankListViewAddEditBank");
            banks.Header = "لیست بانک";
            banks.Name = "BankListView";
            banks.Content = _bankListViewModel;
            IsBusy = false;
        }

        //private void NavToAddBankAccount(BankAccount bankAccount)
        //{
        //    IsBusy = true;
        //    _addEditBankAccountViewModel.EditMode = false;
        //    _addEditBankAccountViewModel.SetBankAccount(bankAccount);

        //    var bankAccounts = Tabs.First(x => x.Name == "BankAccountListView");
        //    bankAccounts.Header = "افزودن حساب های بانکی";
        //    bankAccounts.Name = "BankAccountListViewAddEditBankAccount";
        //    bankAccounts.Content = _addEditBankAccountViewModel;
        //    IsBusy = false;
        //}
        //private void NavToEditBankAccount(BankAccount bankAccount)
        //{
        //    IsBusy = true;
        //    _addEditBankAccountViewModel.EditMode = true;
        //    _addEditBankAccountViewModel.SetBankAccount(bankAccount);
        //    var temp = bankAccount.RelatedPeople.ToList(); //_addEditSLViewModel.SLStandardDescriptionListViewModel.SLId = sL.SLId;
        //    _addEditBankAccountViewModel.RelatedPersonListViewModel.RelatedPeople = new ObservableCollection<RelatedPerson>(temp);
        //    var bankAccounts = Tabs.First(x => x.Name == "BankAccountListView");
        //    bankAccounts.Header = "ویرایش حساب های بانکی";
        //    bankAccounts.Name = "BankAccountListViewAddEditBankAccount";
        //    bankAccounts.Content = _addEditBankAccountViewModel;
        //    IsBusy = false;
        //}
        private void NavToBankAccountList()
        {
            IsBusy = true;
            var bankAccounts = Tabs.First(x => x.Name == "BankAccountListViewAddEditBankAccount");
            bankAccounts.Header = "لیست حساب های بانکی";
            bankAccounts.Name = "BankAccountListView";
            bankAccounts.Content = _bankAccountListViewModel;
            IsBusy = false;
        }

        private void NavToAddCurrency(Currency currency)
        {
            IsBusy = true;
            _addEditCurrencyViewModel.EditMode = false;
            _addEditCurrencyViewModel.SetCurrency(currency);

            var Currencies = Tabs.First(x => x.Name == "CurrencyListView");
            Currencies.Header = "افزودن ارز";
            Currencies.Name = "CurrencyListViewAddEditCurrency";
            Currencies.Content = _addEditCurrencyViewModel;
            IsBusy = false;
        }
        private void NavToEditCurrency(Currency currency)
        {
            IsBusy = true;
            _addEditCurrencyViewModel.EditMode = true;
            _addEditCurrencyViewModel.SetCurrency(currency);

            var Currencies = Tabs.First(x => x.Name == "CurrencyListView");
            Currencies.Header = "ویرایش ارز";
            Currencies.Name = "CurrencyListViewAddEditCurrency";
            Currencies.Content = _addEditCurrencyViewModel;
            IsBusy = false;
        }
        private void NavToCurrencyList()
        {
            IsBusy = true;
            var Currencies = Tabs.First(x => x.Name == "CurrencyListViewAddEditCurrency");
            Currencies.Header = "لیست ارز";
            Currencies.Name = "CurrencyListView";
            Currencies.Content = _currencyListViewModel;
            IsBusy = false;
        }

        private void NavToAddExchangeRate(ExchangeRate exchangeRate)
        {
            IsBusy = true;
            _addEditExchangeRateViewModel.EditMode = false;
            _addEditExchangeRateViewModel.SetExchangeRate(exchangeRate);

            var exchangeRates = Tabs.First(x => x.Name == "ExchangeRateListView");
            exchangeRates.Header = "افزودن نرخ ارز";
            exchangeRates.Name = "ExchangeRateListViewAddEditExchangeRate";
            exchangeRates.Content = _addEditExchangeRateViewModel;
            IsBusy = false;
        }
        private void NavToEditExchangeRate(ExchangeRate exchangeRate)
        {
            IsBusy = true;
            _addEditExchangeRateViewModel.EditMode = true;
            _addEditExchangeRateViewModel.SetExchangeRate(exchangeRate);

            var exchangeRates = Tabs.First(x => x.Name == "ExchangeRateListView");
            exchangeRates.Header = "ویرایش نرخ ارز";
            exchangeRates.Name = "ExchangeRateListViewAddEditExchangeRate";
            exchangeRates.Content = _addEditExchangeRateViewModel;
            IsBusy = false;
        }
        private void NavToExchangeRateList()
        {
            IsBusy = true;
            var exchangeRates = Tabs.First(x => x.Name == "ExchangeRateListViewAddEditExchangeRate");
            exchangeRates.Header = "لیست نرخ ارز";
            exchangeRates.Name = "ExchangeRateListView";
            exchangeRates.Content = _exchangeRateListViewModel;
            IsBusy = false;
        }

        //private void NavToAddPerson(Person person)
        //{
        //    IsBusy = true;
        //    _addEditPersonViewModel.EditMode = false;
        //    _addEditPersonViewModel.SetPerson(person);

        //    var people = Tabs.First(x => x.Name == "PersonListView");
        //    people.Header = "افزودن پرسنل";
        //    people.Name = "PersonListViewAddEditPerson";
        //    people.Content = _addEditPersonViewModel;
        //    IsBusy = false;
        //}
        //private void NavToEditPerson(Person person)
        //{
        //    IsBusy = true;
        //    _addEditPersonViewModel.EditMode = true;
        //    _addEditPersonViewModel.SetPerson(person);
        //    var temp = person.RelatedPeople.ToList(); //_addEditSLViewModel.SLStandardDescriptionListViewModel.SLId = sL.SLId;
        //    _addEditPersonViewModel.RelatedPersonListViewModel.RelatedPeople = new ObservableCollection<RelatedPerson>(temp);
        //    var people = Tabs.First(x => x.Name == "PersonListView");
        //    people.Header = "ویرایش پرسنل";
        //    people.Name = "PersonListViewAddEditPerson";
        //    people.Content = _addEditPersonViewModel;
        //    IsBusy = false;
        //}
        //private void NavToPersonList()
        //{
        //    IsBusy = true;
        //    var people = Tabs.First(x => x.Name == "PersonListViewAddEditPerson");
        //    people.Header = " لیست پرسنل";
        //    people.Name = "PersonListView";
        //    people.Content = _personListViewModel;
        //    IsBusy = false;
        //}

        private void NavToAddCompany(Company company)
        {
            IsBusy = true;
            _addEditCompanyViewModel.EditMode = false;
            _addEditCompanyViewModel.SetCompany(company);

            var companies = Tabs.First(x => x.Name == "CompanyListView");
            companies.Header = "افزودن شرکت";
            companies.Name = "CompanyListViewAddEditCompany";
            companies.Content = _addEditCompanyViewModel;
            IsBusy = false;
        }
        private void NavToEditCompany(Company company)
        {
            IsBusy = true;
            _addEditCompanyViewModel.EditMode = true;
            _addEditCompanyViewModel.SetCompany(company);
            var temp = company.RelatedPeople.ToList(); //_addEditSLViewModel.SLStandardDescriptionListViewModel.SLId = sL.SLId;
            _addEditCompanyViewModel.RelatedPersonListViewModel.RelatedPeople = new ObservableCollection<RelatedPerson>(temp);
            var companies = Tabs.First(x => x.Name == "CompanyListView");
            companies.Header = "ویرایش شرکت";
            companies.Name = "CompanyListViewAddEditCompany";
            companies.Content = _addEditCompanyViewModel;
            IsBusy = false;
        }
        private void NavToCompanyList()
        {
            IsBusy = true;
            var companies = Tabs.First(x => x.Name == "CompanyListViewAddEditCompany");
            companies.Header = " لیست شرکت";
            companies.Name = "CompanyListView";
            companies.Content = _companyListViewModel;
            IsBusy = false;
        }

        private void NavToAddDocumentNumbering(DocumentNumbering documentnumbering)
        {
            IsBusy = true;
            _addEditDocumentNumberingViewModel.EditMode = false;
            _addEditDocumentNumberingViewModel.SetDocumentNumbering(documentnumbering);

            var documentnumberings = Tabs.First(x => x.Name == "DocumentNumberingListView");
            documentnumberings.Header = "افزودن شماره گذاری اسناد";
            documentnumberings.Name = "DocumentNumberingListViewAddEditDocumentNumbering";
            documentnumberings.Content = _addEditDocumentNumberingViewModel;
            IsBusy = false;
        }
        private void NavToEditDocumentNumbering(DocumentNumbering documentnumbering)
        {
            IsBusy = true;
            _addEditDocumentNumberingViewModel.EditMode = true;
            _addEditDocumentNumberingViewModel.SetDocumentNumbering(documentnumbering);

            var documentnumberings = Tabs.First(x => x.Name == "DocumentNumberingListView");
            documentnumberings.Header = "ویرایش شماره گذاری اسناد";
            documentnumberings.Name = "DocumentNumberingListViewAddEditDocumentNumbering";
            documentnumberings.Content = _addEditDocumentNumberingViewModel;
            IsBusy = false;
        }
        private void NavToDocumentNumberingList()
        {
            IsBusy = true;
            var documentnumberings = Tabs.First(x => x.Name == "DocumentNumberingListViewAddEditDocumentNumbering");
            documentnumberings.Header = " شماره گذاری اسناد";
            documentnumberings.Name = "DocumentNumberingListView";
            documentnumberings.Content = _documentNumberingListViewModel;
            IsBusy = false;
        }

        private void NavToAddAccountDocument(AccountDocument accountdocument)
        {
            IsBusy = true;
            _addEditAccountDocumentViewModel.EditMode = false;
            _addEditAccountDocumentViewModel.SetAccountDocument(accountdocument);

            var accountdocuments = Tabs.First(x => x.Name == "AccountDocumentListView");
            accountdocuments.Header = "افزودن سند";
            accountdocuments.Name = "AccountDocumentListViewAddEditAccountDocument";
            accountdocuments.Content = _addEditAccountDocumentViewModel;
            IsBusy = false;
        }
        private void NavToEditAccountDocument(AccountDocument accountdocument)
        {
            IsBusy = true;
            _addEditAccountDocumentViewModel.EditMode = true;
            _addEditAccountDocumentViewModel.SetAccountDocument(accountdocument);

            var accountdocuments = Tabs.First(x => x.Name == "AccountDocumentListView");
            accountdocuments.Header = "ویرایش اسناد";
            accountdocuments.Name = "AccountDocumentListViewAddEditAccountDocument";
            accountdocuments.Content = _addEditAccountDocumentViewModel;
            IsBusy = false;
        }
        private void NavToAccountDocumentList()
        {
            IsBusy = true;
            var accountdocuments = Tabs.First(x => x.Name == "AccountDocumentListViewAddEditAccountDocument");
            accountdocuments.Header = "اسناد";
            accountdocuments.Name = "AccountDocumentListView";
            accountdocuments.Content = _accountDocumentListViewModel;
            IsBusy = false;
        }
        private void NavToSystemAccountingSettingList()
        {
            IsBusy = true;
            var systemAccountingSetting = Tabs.First(x => x.Name == "SystemAccountingSettingView");
            systemAccountingSetting.Header = "سیستم اسناد حسابداری";
            systemAccountingSetting.Name = "SystemAccountingSettingView";
            systemAccountingSetting.Content = _systemAccountingSettingViewModel;
            IsBusy = false;
        }
        private void NavToSalarySystemSettingList()
        {
            IsBusy = true;
            var salarySystemSetting = Tabs.First(x => x.Name == "SalarySystemSettingView");
            salarySystemSetting.Header = "سیستم حقوق و دستمزد";
            salarySystemSetting.Name = "SalarySystemSettingView";
            salarySystemSetting.Content = _salarySystemSettingViewModel;
            IsBusy = false;
        }
        private void NavToSystemSettingRetailList()
        {
            IsBusy = true;
            var systemSettingRetail = Tabs.First(x => x.Name == "SystemSettingRetailView");
            systemSettingRetail.Header = "سیستم خرده فروشی";
            systemSettingRetail.Name = "SystemSettingRetailView";
            systemSettingRetail.Content = _systemSettingRetailViewModel;
            IsBusy = false;
        }
        private void NavToSystemReceivePaymentSettingList()
        {
            IsBusy = true;
            var systemReceivePaymentSetting = Tabs.First(x => x.Name == "SystemReceivePaymentSettingView");
            systemReceivePaymentSetting.Header = "سیستم دریافت و پرداخت";
            systemReceivePaymentSetting.Name = "SystemReceivePaymentSettingView";
            systemReceivePaymentSetting.Content = _systemReceivePaymentSettingViewModel;
            IsBusy = false;
        }
        private void NavToSystemSettingSaleList()
        {
            IsBusy = true;
            var systemSettingSale = Tabs.First(x => x.Name == "SystemSettingSaleView");
            systemSettingSale.Header = "سیستم فروش";
            systemSettingSale.Name = "SystemSettingSaleView";
            systemSettingSale.Content = _systemSettingSaleViewModel;
            IsBusy = false;
        }
        private void NavToShoppingSystemSettingList()
        {
            IsBusy = true;
            var shoppingSystemSetting = Tabs.First(x => x.Name == "ShoppingSystemSettingView");
            shoppingSystemSetting.Header = "سیستم بازرگانی خرید";
            shoppingSystemSetting.Name = "ShoppingSystemSettingView";
            shoppingSystemSetting.Content = _shoppingSystemSettingViewModel;
            IsBusy = false;
        }
        private void NavToGeneralSystemSettingList()
        {
            IsBusy = true;
            var generalSystemSetting = Tabs.First(x => x.Name == "GeneralSystemSettingView");
            generalSystemSetting.Header = "سیستم عمومی";
            generalSystemSetting.Name = "GeneralSystemSettingView";
            generalSystemSetting.Content = _generalSystemSettingViewModel;
            IsBusy = false;
        }
        private void NavToCompanyInformationgList()
        {
            IsBusy = true;
            var companyInformation = Tabs.First(x => x.Name == "CompanyInformationView");
            companyInformation.Header = "اطلاعات شرکت";
            companyInformation.Name = "CompanyInformationView";
            companyInformation.Content = _companyInformationViewModel;
            IsBusy = false;
        }
        private void NavToAddStock(Stock stock)
        {
            IsBusy = true;
            _addEditStockViewModel.EditMode = false;
            _addEditStockViewModel.SetStock(stock);

            var stocks = Tabs.First(x => x.Name == "StockListView");
            stocks.Header = "افزودن انبار";
            stocks.Name = "StockListViewAddEditStock";
            stocks.Content = _addEditStockViewModel;
            IsBusy = false;
        }
        private void NavToEditStock(Stock stock)
        {
            IsBusy = true;
            _addEditStockViewModel.EditMode = true;
            _addEditStockViewModel.SetStock(stock);

            var stocks = Tabs.First(x => x.Name == "StockListView");
            stocks.Header = "ویرایش انبار";
            stocks.Name = "StockListViewAddEditStock";
            stocks.Content = _addEditStockViewModel;
            IsBusy = false;
        }
        private void NavToStockList()
        {
            IsBusy = true;
            var stocks = Tabs.First(x => x.Name == "StockListViewAddEditStock");
            stocks.Header = "فهرست انبار";
            stocks.Name = "StockListView";
            stocks.Content = _stockListViewModel;
            IsBusy = false;
        }
        private void NavToAddProduct(Product product)
        {
            IsBusy = true;
            _addEditProductViewModel.EditMode = false;
            _addEditProductViewModel.SetProduct(product);

            var products = Tabs.First(x => x.Name == "ProductListView");
            products.Header = "افزودن کالا";
            products.Name = "ProductListViewAddEditProduct";
            products.Content = _addEditProductViewModel;
            IsBusy = false;
        }
        private void NavToEditProduct(Product product)
        {
            IsBusy = true;
            _addEditProductViewModel.EditMode = true;
            _addEditProductViewModel.SetProduct(product);

            var products = Tabs.First(x => x.Name == "ProductListView");
            products.Header = "ویرایش کالا";
            products.Name = "ProductListViewAddEditProduct";
            products.Content = _addEditProductViewModel;
            IsBusy = false;
        }
        private void NavToProductList()
        {
            IsBusy = true;
            var products = Tabs.First(x => x.Name == "ProductListViewAddEditProduct");
            products.Header = "فهرست کالا";
            products.Name = "ProductListView";
            products.Content = _productListViewModel;
            IsBusy = false;
        }

        private void NavToAddTypeDocument(TypeDocument typeDocument)
        {
            IsBusy = true;
            _addEditTypeDocumentViewModel.EditMode = false;
            _addEditTypeDocumentViewModel.SetTypeDocument(typeDocument);

            var typeDocuments = Tabs.First(x => x.Name == "TypeDocumentListView");
            typeDocuments.Header = "افزودن نوع سند";
            typeDocuments.Name = "TypeDocumentListViewAddEditTypeDocument";
            typeDocuments.Content = _addEditTypeDocumentViewModel;
            IsBusy = false;
        }
        private void NavToEditTypeDocument(TypeDocument typeDocument)
        {
            IsBusy = true;
            _addEditTypeDocumentViewModel.EditMode = true;
            _addEditTypeDocumentViewModel.SetTypeDocument(typeDocument);

            var typeDocuments = Tabs.First(x => x.Name == "TypeDocumentListView");
            typeDocuments.Header = "ویرایش نوع سند";
            typeDocuments.Name = "TypeDocumentListViewAddEditTypeDocument";
            typeDocuments.Content = _addEditTypeDocumentViewModel;
            IsBusy = false;
        }
        private void NavToTypeDocumentList()
        {
            IsBusy = true;
            if (Tabs.Count > 0)
            {
                var typeDocuments = Tabs.First(x => x.Name == "TypeDocumentListViewAddEditTypeDocument");
                typeDocuments.Header = "فهرست نوع اسناد";
                typeDocuments.Name = "TypeDocumentListView";
                typeDocuments.Content = _typeDocumentListViewModel;
            }
            IsBusy = false;
        }
        private void NavToAddRelatedPerson(int relatedid)
        {
            IsBusy = true;
            _relatedPersonListViewModel.RelatedId = relatedid;
            _addEditRelatedPersonViewModel.EditMode = false;
            _addEditRelatedPersonViewModel.SetRelatedPerson(new RelatedPerson { RelatedPersonId = relatedid });

            var relatedPersons = Tabs.First(x => x.Name == "SLListViewRelatedPersonListView");
            relatedPersons.Header = "افزودن افراد مرتبط";
            relatedPersons.Name = "SLListViewAddEditRelatedPersonView";
            relatedPersons.Content = _addEditRelatedPersonViewModel;
            IsBusy = false;
        }
        private void NavToEditRelatedPerson(RelatedPerson relatedPerson)
        {
            IsBusy = true;
            _addEditRelatedPersonViewModel.EditMode = true;
            _addEditRelatedPersonViewModel.SetRelatedPerson(relatedPerson);

            var relatedPersons = Tabs.First(x => x.Name == "RelatedPersonListView");
            relatedPersons.Header = "ویرایش افراد مرتبط";
            relatedPersons.Name = "RelatedPersonListViewAddEditRelatedPerson";
            relatedPersons.Content = _addEditRelatedPersonViewModel;
            IsBusy = false;
        }
        private void NavToRelatedPersonCompanyList(Company company)
        {
            IsBusy = true;
            _relatedPersonListViewModel.RelatedId = company.CompanyId;
            var relatedPersons = Tabs.First(x => x.Name == "CompanyListView");
            relatedPersons.Header = "  لیست افراد مرتبط";
            relatedPersons.Name = "CompanyListViewRelatedPersonListView";
            relatedPersons.Content = _relatedPersonListViewModel;
            IsBusy = false;
        }
        private void NavToRelatedPersonPersonList(Person person)
        {
            IsBusy = true;
            _relatedPersonListViewModel.RelatedId = person.PersonId;
            var relatedPersons = Tabs.First(x => x.Name == "PersonListView");
            relatedPersons.Header = "لیست افراد مرتبط";
            relatedPersons.Name = "PersonListViewRelatedPersonListView";
            relatedPersons.Content = _relatedPersonListViewModel;
            IsBusy = false;
        }
        private void NavToRelatedPersonBankAccountList(BankAccount bankAccount)
        {
            IsBusy = true;
            _relatedPersonListViewModel.RelatedId = bankAccount.BankAccountId;
            var relatedPersons = Tabs.First(x => x.Name == "BankAccountListView");
            relatedPersons.Header = "لیست افراد مرتبط";
            relatedPersons.Name = "BankAccountListViewRelatedPersonListView";
            relatedPersons.Content = _relatedPersonListViewModel;
            IsBusy = false;
        }
        //private void NavToAddAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        //{
        //    IsBusy = true;
        //    _addEditAccDocumentHeaderViewModel.EditMode = false;
        //    _addEditAccDocumentHeaderViewModel.SetAccDocumentHeader(accDocumentHeader);

        //    var accDocumentHeaders = Tabs.First(x => x.Name.StartsWith("AccDocumentHeaderListView"));
        //    accDocumentHeaders.Header = "افزودن هدر سند حسابداری";
        //    accDocumentHeaders.Name = "AccDocumentHeaderListViewAddEditAccDocumentHeader";
        //    accDocumentHeaders.Content = _addEditAccDocumentHeaderViewModel;
        //    IsBusy = false;
        //}
        //private void NavToEditAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        //{
        //    IsBusy = true;
        //    //_addEditAccDocumentHeaderViewModel.EditMode = true;
        //  //  _addEditAccDocumentHeaderViewModel.SetAccDocumentHeader(accDocumentHeader);

        //    var accDocumentHeaders = Tabs.First(x => x.Name.StartsWith("AccDocumentHeaderListView"));
        //    accDocumentHeaders.Header = "ویرایش هدر سند حسابداری";
        //    accDocumentHeaders.Name = "AccDocumentHeaderListViewAddEditAccDocumentHeader";
        //    accDocumentHeaders.Content = _addEditAccDocumentHeaderViewModel;
        //    IsBusy = false;
        //}
        private void NavToAccDocumentHeaderList()
        {
            IsBusy = true;
            var accDocumentHeaders = Tabs.First(x => x.Name == "AccDocumentHeaderListViewAddEditAccDocumentHeader");
            accDocumentHeaders.Header = "فهرست هدر اسناد حسابدرای";
            accDocumentHeaders.Name = "AccDocumentHeaderListView";
            accDocumentHeaders.Content = _accDocumentHeaderListViewModel;
            IsBusy = false;
        }

        private void NavToAccDocumentItemList(AccDocumentHeader accDocumentHeader)
        {
            IsBusy = true;
            Mapper.Map(accDocumentHeader, _accDocumentItemListViewModel.AccDocumentHeader);
            var accDocumentItems = Tabs.First(x => x.Name == "AccDocumentHeaderListView");
            accDocumentItems.Header = "آیتم سند حسابداری";
            accDocumentItems.Name = "AccDocumentHeaderListViewAccDocumentItemListView";
            accDocumentItems.Content = _accDocumentItemListViewModel;
            IsBusy = false;
        }
      
        
        private void NavToReviewAccountList()
        {
            IsBusy = true;
            var reviewAccounts = Tabs.First(x => x.Name == "ReviewAccountListViewAddEditReviewAccount");
            reviewAccounts.Header = "مرور حساب ها";
            reviewAccounts.Name = "ReviewAccountListView";
            reviewAccounts.Content = _reviewAccountListViewModel;
            IsBusy = false;
        }
        private void NavToSelectFinancialYearList()
        {
            IsBusy = true;
            var selectFinancialYears = Tabs.First(x => x.Name == "SelectFinancialYearListViewAddEditSelectFinancialYear");
            selectFinancialYears.Header = "انتخاب سال مالی";
            selectFinancialYears.Name = "SelectFinancialYearListView";
            selectFinancialYears.Content = _selectFinancialYearListViewModel;
            IsBusy = false;
        }

        private void NavToCurrencyExchangeList()
        {
            IsBusy = true;
            var currencyExchanges = Tabs.First(x => x.Name == "CurrencyExchangeListViewCurrencyExchangeRateDocHeaderListView");
            currencyExchanges.Header = "تسعیر ارز";
            currencyExchanges.Name = "CurrencyExchangeListView";
            currencyExchanges.Content = _currencyExchangeListViewModel;
            IsBusy = false;
        }
        private void NavToCloseProfitLossAccountList()
        {
            IsBusy = true;
            var closeProfits = Tabs.First(x => x.Name == "CloseProfitLossAccountListViewCloseProfitLossDocListView");
            closeProfits.Header = "سود و زیانی";
            closeProfits.Name = "CloseProfitLossAccountListView";
            closeProfits.Content = _closeProfitLossAccountListViewModel;
            IsBusy = false;
        }
        private void NavToOpeningClosingList()
        {
            IsBusy = true;
            var openingClosings = Tabs.First(x => x.Name == "OpeningClosingListViewOpeningClosingDocHeaderListView");
            openingClosings.Header = "افتتاحیه و اختتامیه";
            openingClosings.Name = "OpeningClosingListView";
            openingClosings.Content = _openingClosingListViewModel;
            IsBusy = false;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="accDocumentHeader"></param>
        private void NavToEditDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            IsBusy = true;
            Mapper.Map(accDocumentHeader, _currencyDocumentItemListViewModel.AccDocumentHeader);
            var accDocumentItems = Tabs.First(x => x.Name == "CurrencyExchangeListViewCurrencyExchangeRateDocHeaderListView");
            accDocumentItems.Header = "آیتم سند حسابداری";
            accDocumentItems.Name = "CurrencyExchangeListViewCurrencyExchangeRateDocHeaderListViewCurrencyDocumentItemListView";
            accDocumentItems.Content = _currencyDocumentItemListViewModel;
            IsBusy = false;
        }
        private void NavToEditCloseProfitDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            IsBusy = true;
            Mapper.Map(accDocumentHeader, _closeProfitDocumentItemListViewModel.AccDocumentHeader);
            var accDocumentItems = Tabs.First(x => x.Name == "CloseProfitLossAccountListViewCloseProfitLossDocListView");
            accDocumentItems.Header = "آیتم سند حسابداری";
            accDocumentItems.Name = "CloseProfitLossAccountListViewCloseProfitLossDocListViewCloseProfitDocumentItemListView";
            accDocumentItems.Content = _closeProfitDocumentItemListViewModel;
            IsBusy = false;
        }
        private void NavToEditOpeningClosingDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            IsBusy = true;
            Mapper.Map(accDocumentHeader, _openingClosingDocItemListViewModel.AccDocumentHeader);
            var accDocumentItems = Tabs.First(x => x.Name == "OpeningClosingListViewOpeningClosingDocHeaderListView");
            accDocumentItems.Header = "آیتم سند حسابداری";
            accDocumentItems.Name = "OpeningClosingListViewOpeningClosingDocHeaderListViewOpeningClosingDocItemListView";
            accDocumentItems.Content = _openingClosingDocItemListViewModel;
            IsBusy = false;
        }
        
        private void NavToViewCurrencyExchangeDocument()
        {
                IsBusy = true;
            _currencyExchangeRateDocHeaderListViewModel.LoadAccDocumentHeaders();
                var currencyExchangeRateDocs = Tabs.First(x => x.Name.StartsWith("CurrencyExchangeListView"));
                currencyExchangeRateDocs.Header = "اسناد سیستمی";
                currencyExchangeRateDocs.Name = "CurrencyExchangeListViewCurrencyExchangeRateDocHeaderListView";
            currencyExchangeRateDocs.Content = _currencyExchangeRateDocHeaderListViewModel;
                IsBusy = false;
        }
        private void NavToViewOpeningClosingDocument()
        {
            IsBusy = true;
            _openingClosingDocHeaderListViewModel.LoadAccDocumentHeaders();
            var openingClosingDocs = Tabs.First(x => x.Name.StartsWith("OpeningClosingListView"));
            openingClosingDocs.Header = "اسناد سیستمی";
            openingClosingDocs.Name = "OpeningClosingListViewOpeningClosingDocHeaderListView";
            openingClosingDocs.Content = _openingClosingDocHeaderListViewModel;
            IsBusy = false;
        }
  
        private void NavToViewCloseProfitLossDocument()
        {
            IsBusy = true;
            _closeProfitLossDocListViewModel.LoadAccDocumentHeaders();
            var closeProfitLossDocs = Tabs.First(x => x.Name.StartsWith("CloseProfitLossAccountListView"));
            closeProfitLossDocs.Header = "اسناد سیستمی";
            closeProfitLossDocs.Name = "CloseProfitLossAccountListViewCloseProfitLossDocListView";
            closeProfitLossDocs.Content = _closeProfitLossDocListViewModel;
            IsBusy = false;
        }

        private void NavToRequestCloseProfitLossDocument()
        {
            IsBusy = true;
            var closeProfitLossDocs = Tabs.First(x => x.Name == "CloseProfitLossAccountListViewCloseProfitLossDocListView");
            closeProfitLossDocs.Header = "حساب های سود و زیانی";
            closeProfitLossDocs.Name = "CloseProfitLossAccountListView";
            closeProfitLossDocs.Content = _closeProfitLossAccountListViewModel;
            IsBusy = false;
        }
        private void NavToRequestCurrencyExchangeDocument()
        {
            IsBusy = true;
            var currencyExchanges = Tabs.First(x => x.Name == "CurrencyExchangeListViewCurrencyExchangeRateDocHeaderListView");
            currencyExchanges.Header = "تسعیر ارز";
            currencyExchanges.Name = "CurrencyExchangeListView";
            currencyExchanges.Content = _currencyExchangeListViewModel;
            IsBusy = false;
        }
        private void NavToRequestOpeningClosingDocument()
        {
            IsBusy = true;
            var openingClosingDocs = Tabs.First(x => x.Name == "OpeningClosingListViewOpeningClosingDocHeaderListView");
            openingClosingDocs.Header = "افتتاحیه و اختتامیه";
            openingClosingDocs.Name = "OpeningClosingListView";
            openingClosingDocs.Content = _openingClosingDocHeaderListViewModel;
            IsBusy = false;
        }
        private void NavToRequestDocument(int typeDocument)
        {
            if (typeDocument == 6)
            {
                IsBusy = true;

                var currencyExchanges = Tabs.First(x => x.Name == "CurrencyExchangeListViewSystemDocumentHeaderListView");
                currencyExchanges.Header = "تسعیر ارز";
                currencyExchanges.Name = "CurrencyExchangeListView";
                currencyExchanges.Content = _currencyExchangeListViewModel;
                IsBusy = false;
            }
            else if (typeDocument == 1)
            {
                IsBusy = true;

                var closeProfitLossAccounts = Tabs.First(x => x.Name == "CloseProfitLossAccountListViewSystemDocumentHeaderListView");
                closeProfitLossAccounts.Header = "سود و زیانی";
                closeProfitLossAccounts.Name = "CloseProfitLossAccountListView";
                closeProfitLossAccounts.Content = _closeProfitLossAccountListViewModel;
                IsBusy = false;
            }
            else if (typeDocument == 3 || typeDocument == 4)
            {
                IsBusy = true;

                var openingClosings = Tabs.First(x => x.Name == "OpeningClosingListViewSystemDocumentHeaderListView");
                openingClosings.Header = "افتتاحیه و اختتامیه";
                openingClosings.Name = "OpeningClosingListView";
                openingClosings.Content = _openingClosingListViewModel;
                IsBusy = false;
            }

        }

        private void NavToTreeAccountList()
        {
            IsBusy = true;
            var treeAccounts = Tabs.First(x => x.Name == "TreeAccountListViewAddEditTreeAccount");
            treeAccounts.Header = "درختواره حساب ها";
            treeAccounts.Name = "TreeAccountListView";
            treeAccounts.Content = _treeAccountListViewModel;
            IsBusy = false;
        }

        private void NavToViewTLDocumentItem(TLDocumentHeader tLDocumentHeader)
        {
            IsBusy = true;
            // _tLDocumentItemListViewModel.EditMode = true;
            _tLDocumentItemListViewModel.SetTLDocumentHeader(tLDocumentHeader);

            var tLDocumentItems = Tabs.First(x => x.Name.StartsWith("TLDocumentHeaderListView"));
            tLDocumentItems.Header = "آیتم  سند کل";
            tLDocumentItems.Name = "TLDocumentHeaderListViewTLDocumentItemListView";
            tLDocumentItems.Content = _tLDocumentItemListViewModel;
            IsBusy = false;
        }
        private void NavToTLDocumentHeaderList()
        {
            IsBusy = true;
            var tLDocumentHeaders = Tabs.First(x => x.Name == "TLDocumentHeaderListViewTLDocumentItemListView");
            tLDocumentHeaders.Header = "سند کل";
            tLDocumentHeaders.Name = "TLDocumentHeaderListView";
            tLDocumentHeaders.Content = _tLDocumentHeaderListViewModel;
            IsBusy = false;
        }

        #endregion
    }
}
