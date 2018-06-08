using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Settings.ShoppingSetting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Data;
using Saina.ApplicationCore.DTOs;

namespace Saina.WPF.Commerce.ProductTypes
{
    class ProductTypeListViewModel : BindableBase
    {

        #region Constructors
        public ProductTypeListViewModel(IShoppingSystemSettingsService shoppingSystemSettingsService, SainaDbContext uow)
        {
            _shoppingSystemSettingsService = shoppingSystemSettingsService;
            AddBrandCommand = new RelayCommand(OnAddProduct);
            ProductType = new ProductType();
            _uow = uow;

        }

        private void OnAddProduct()
        {
            var lastCode = 0;
            var ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());
            ShoppingSystemSettingProductTypeLenght = int.Parse(ShoppingSystemSettingModel.ProductTypeLenght ?? "0");
            if (_uow.ProductTypes.Any())
                lastCode = _uow.ProductTypes.Select(x => x.ProductTypeCode).Max();
            var len = (lastCode.ToString()).Length;

            if (ShoppingSystemSettingProductTypeLenght < 0)
            {
                Error(".تنظیمات بازرگانی خرید انجام نشده است");

            }
            else if (len > ShoppingSystemSettingProductTypeLenght)

            {
                Error("شماره گذاری به پایان رسیده است");

            }
            else
            {
                string s = "0," + (ShoppingSystemSettingProductTypeLenght).ToString();
             
                if (lastCode == 0)
                {
                    var stringLastbrandCode = "1";
                    var lastBrandCode = stringLastbrandCode.ToString().PadRight(ShoppingSystemSettingProductTypeLenght, '0');
                    ProductType.ProductTypeCode = int.Parse(lastBrandCode);

                    Regex = $"^[0-9]{{{s}}}$";
                    //SL.SLCodeEmptyValue = SelectedTL.TLCode;

                }
                else
                {

                    lastCode++;
                    var lastBrandCode = lastCode.ToString().PadLeft(ShoppingSystemSettingProductTypeLenght, '0');

                    ProductType.ProductTypeCode = int.Parse($"{lastBrandCode}");

                    Regex = $"^[0-9]{{{s}}}$";
                }


            }
        }
        #endregion
        #region Fields
        private SainaDbContext _uow;
        private IEditableCollectionViewAddNewItem _productTypes;
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        public event Action<string> Error;

        #endregion
        #region Commands
        public ICommand AddBrandCommand { get; private set; }


        #endregion
        #region Properties
        private ProductType _productType;
        public ProductType ProductType
        {
            get { return _productType; }
            set { SetProperty(ref _productType, value); }
        }

        public IEditableCollectionViewAddNewItem ProductTypes
        {
            get { return _productTypes; }
            set
            {
                SetProperty(ref _productTypes, value);
            }
        }
        private string _regex;


        public string Regex
        {
            get { return _regex; }
            set { SetProperty(ref _regex, value); }
        }

        public int ShoppingSystemSettingProductTypeLenght { get; private set; }


        #endregion
        #region Methods
        public void Loaded()
        {
            _uow = new SainaDbContext();
            ProductTypes = new QueryableCollectionView(_uow.ProductTypes.ToList());
        }
        public override void UnLoaded()
        {
            _uow.Dispose();
        }
        public void AddProductType(ProductType productType)
        {
            _uow.ProductTypes.Add(productType);
        }
        public void Save()
        {
            _uow.SaveChanges();
        }
        public int DeleteProductType(ProductType productType)
        {
            _uow.ProductTypes.Remove(productType);
            return _uow.SaveChanges();
        }

        #endregion

    }
}
