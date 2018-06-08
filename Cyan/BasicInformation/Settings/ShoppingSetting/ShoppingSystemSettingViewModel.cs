using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Settings.ShoppingSetting
{
    /// <summary>
    /// تنظیمات سیستم بازرگانی خرید
    /// </summary>
    public class ShoppingSystemSettingViewModel : BindableBase
    {
        #region Fields
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        private IAppContextService _appContextService;
        private List<SLSetting> _allSLs;
        private List<SL> _allSL;
        private ISLsService _sLsService;
        #endregion
        #region Constructors

        public ShoppingSystemSettingViewModel(IAppContextService appContextService, IShoppingSystemSettingsService shoppingSystemSettingsService, ISLsService sLsService)
        {
            _sLsService = sLsService;
            _appContextService = appContextService;
            _shoppingSystemSettingsService = shoppingSystemSettingsService;
           // ShoppingSystemSettingModel = _shoppingSystemSettingsService.GetShoppingSystemSettingModel();
            SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened);
            SaveCommand = new RelayCommand(onSave);
            ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());


           ShoppingSystemSettingModel.ValidationDelegate += ShoppingSystemSettingModel_ValidationDelegate;

        }

        #endregion
        #region Commands
        public RelayCommand SLsDropDownOpenedCommand { get; private set; }

        public ICommand SaveCommand { get; set; }
        #endregion
        #region Properties
        public EditableShoppingSystemSettingViewModel ShoppingSystemSettingModel { get; set; }
        private ObservableCollection<SLSetting> _sLs;
        public ObservableCollection<SLSetting> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        private ObservableCollection<AutomaticAccountingDocument> _automaticAccountingDocument;

        public ObservableCollection<AutomaticAccountingDocument> AutomaticAccountingDocument
        {
            get { return _automaticAccountingDocument; }
            set { SetProperty(ref _automaticAccountingDocument, value); }
        }
        public event Action<Exception> Failed;
        public event Action<string> Error;
        public event Action Done;
        #endregion
        #region Methods
        private async void OnSLsDropDownOpened()
        {
            _allSL = await _sLsService.GetSLsActiveAsync();
            SLs = new ObservableCollection<SLSetting>(_allSL.Select(x => new SLSetting { SLId = x.SLId.ToString(), Title = x.Title }));
        }
        public async void LoadSLs()
        {
            if (SLs == null)
            {
                _allSLs = (await _sLsService.GetSLsActiveAsync()).Select(x => new SLSetting { SLId = x.SLId.ToString(), Title = x.Title }).ToList();
                SLs = new ObservableCollection<SLSetting>(_allSLs);
            }
            if (AutomaticAccountingDocument == null)
            {
                AutomaticAccountingDocument = new ObservableCollection<AutomaticAccountingDocument>
            {
                new AutomaticAccountingDocument {AutomaticAccountingDocumentId = "1", AutomaticAccountingDocumentTitle = "رسید از انبار" },
                new AutomaticAccountingDocument {AutomaticAccountingDocumentId = "2", AutomaticAccountingDocumentTitle = "برگشت از انبار" },
                new AutomaticAccountingDocument {AutomaticAccountingDocumentId = "3", AutomaticAccountingDocumentTitle = "هر دو" },

            };
                

            }
        }
        //public async void LoadSystemAccountingSettingModel()
        //{
        //    _allGLs = await _gLsService.GetGLsAsync();
        //    GLs = new ObservableCollection<GL>(_allGLs);
        //    _allTLs = await _tLsService.GetTLsAsync();
        //    TLs = new ObservableCollection<TL>(_allTLs);
        //    _allSLs = await _sLsService.GetSLsAsync();
        //    SLs = new ObservableCollection<SL>(_allSLs);
        //    _allDLs = await _dLsService.GetDLsAsync();
        //    DLs = new ObservableCollection<DL>(_allDLs);
        //    SystemAccountingSettingModel.GLActive = true;
        //    SystemAccountingSettingModel.DLActive = true;
        //    SystemAccountingSettingModel.TLActive = true;
        //    SystemAccountingSettingModel.SLActive = true;
        //    //if (GLs?.Any() != true)
        //    //{
        //    //    SystemAccountingSettingModel.GLActive = true;
        //    //}
        //    //else
        //    //{
        //    //    SystemAccountingSettingModel.GLActive = false;
        //    //}
        //    //if (TLs?.Any() != true)
        //    //{
        //    //    SystemAccountingSettingModel.TLActive = true;
        //    //}
        //    //else
        //    //{
        //    //    SystemAccountingSettingModel.TLActive = false;
        //    //}
        //    //if (SLs?.Any() != true)
        //    //{
        //    //    SystemAccountingSettingModel.SLActive = true;
        //    //}
        //    //else
        //    //{
        //    //    SystemAccountingSettingModel.SLActive = false;
        //    //}
        //    //if (DLs?.Any() != true)
        //    //{
        //    //    SystemAccountingSettingModel.DLActive = true;
        //    //}
        //    //else
        //    //{
        //    //    SystemAccountingSettingModel.DLActive = false;
        //    //}
        //}
        private async Task<List<string>> ShoppingSystemSettingModel_ValidationDelegate(object sender, string propertyName)
        {

            var editableShoppingSystemSettingModel = (EditableShoppingSystemSettingViewModel)sender;
            List<string> errors = new List<string>();
            int.TryParse(editableShoppingSystemSettingModel.ProductCodeLenght, out int productCodeLenght);
            int.TryParse(editableShoppingSystemSettingModel.ProductBrandLenght, out int productBrandLenght);
            int.TryParse(editableShoppingSystemSettingModel.ProductTypeLenght, out int productTypeLenght);
            int.TryParse(editableShoppingSystemSettingModel.ProductModelLenght, out int productModelLenght);
            int.TryParse(editableShoppingSystemSettingModel.OtherProductLenght, out int otherProductLenght);
            var len = 25;
            var allow = productCodeLenght - 2;
            switch (propertyName)
            {
                case nameof(EditableShoppingSystemSettingViewModel.ProductCodeLenght):
                    if (productCodeLenght == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (productCodeLenght >= len)
                    {
                        errors.Add("طول کد کالا بزرگتر از حد مجاز است");
                    }
          //          editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductCodeLenght), editableShoppingSystemSettingModel.ProductCodeLenght);

                    break;
                case nameof(EditableShoppingSystemSettingViewModel.ProductBrandLenght):

                    if (productBrandLenght == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (productBrandLenght >= productCodeLenght)
                    {
                        errors.Add("طول کد برند کالا نباید از طول کد کالا بزرگتر باشد");
                    }
                    if ((productBrandLenght) > allow)
                    {
                        errors.Add("طول برند کالا از حد مجاز بزرگتر است");
                    }
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductCodeLenght), editableShoppingSystemSettingModel.ProductCodeLenght);
                 //   editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductBrandLenght), editableShoppingSystemSettingModel.ProductBrandLenght);

                    break;
                case nameof(EditableShoppingSystemSettingViewModel.ProductTypeLenght):

                    if (productTypeLenght == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (productTypeLenght >= productCodeLenght)
                    {
                        errors.Add("طول کد نوع کالا نباید از طول کد کالا بزرگتر باشد");
                    }
                    if ((productBrandLenght+ productTypeLenght) > allow)
                    {
                        errors.Add("طول نوع کالا از حد مجاز بزرگتر است");
                    }
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductCodeLenght), editableShoppingSystemSettingModel.ProductCodeLenght);
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductBrandLenght), editableShoppingSystemSettingModel.ProductBrandLenght);
                  //  editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductTypeLenght), editableShoppingSystemSettingModel.ProductTypeLenght);

                    break;
                case nameof(EditableShoppingSystemSettingViewModel.ProductModelLenght):

                    if (productModelLenght == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (productModelLenght >= productCodeLenght)
                    {
                        errors.Add("طول کد مدل کالا نباید از طول کد کالا بزرگتر باشد");
                    }
                    if ((productBrandLenght + productTypeLenght+ productModelLenght) > allow)
                    {
                        errors.Add("طول مدل کالا از حد مجاز بزرگتر است");
                    }
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductCodeLenght), editableShoppingSystemSettingModel.ProductCodeLenght);
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductBrandLenght), editableShoppingSystemSettingModel.ProductBrandLenght);
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductTypeLenght), editableShoppingSystemSettingModel.ProductTypeLenght);
                    //editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductModelLenght), editableShoppingSystemSettingModel.ProductModelLenght);

                    break;
                case nameof(EditableShoppingSystemSettingViewModel.OtherProductLenght):

                    if (otherProductLenght == 0)
                    {
                        errors.Add("عدد وارد نمایید");
                    }
                    if (otherProductLenght >= productCodeLenght)
                    {
                        errors.Add("طول کد سایر کالا نباید از طول کد کالا بزرگتر باشد");
                    }
                    if ((productBrandLenght + productTypeLenght + productModelLenght+ otherProductLenght) > allow)
                    {
                        errors.Add("طول سایر کالا از حد مجاز بزرگتر است");
                    }
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductCodeLenght), editableShoppingSystemSettingModel.ProductCodeLenght);
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductBrandLenght), editableShoppingSystemSettingModel.ProductBrandLenght);
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductTypeLenght), editableShoppingSystemSettingModel.ProductTypeLenght);
                    editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.ProductModelLenght), editableShoppingSystemSettingModel.ProductModelLenght);
                  //  editableShoppingSystemSettingModel.ValidateProperty(nameof(EditableShoppingSystemSettingViewModel.OtherProductLenght), editableShoppingSystemSettingModel.OtherProductLenght);

                    break;

            

                default:
                    return null;
            }
            //editableSystemAccountingSettingModel.NotifyAllPropertiesHaveChanged();
            return errors;

        }
        private void onSave()

        {
            try
            {
              //  ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());

                var editableShoppingSystemSettingModel = AutoMapper.Mapper.Map<EditableShoppingSystemSettingViewModel, ShoppingSystemSettingModel>(ShoppingSystemSettingModel);
                _shoppingSystemSettingsService.SaveShoppingSystemSettingModel(editableShoppingSystemSettingModel);
                Error?.Invoke(".تغییرات انجام شد");

            }
            catch (Exception ex)
            {

                Failed(ex);
            }
        }


        #endregion
    }
}
