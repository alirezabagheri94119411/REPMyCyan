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
namespace Saina.WPF.Commerce.OtherProducts
{
    class OtherProductListViewModel : BindableBase
    {

        #region Constructors
        public OtherProductListViewModel(IShoppingSystemSettingsService shoppingSystemSettingsService, SainaDbContext uow)
        {
            _shoppingSystemSettingsService = shoppingSystemSettingsService;
            AddBrandCommand = new RelayCommand(OnAddProduct);
            OtherProduct = new OtherProduct();
            _uow = uow;

        }

        private void OnAddProduct()
        {
            var lastCode = 0;
            var ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());
            ShoppingSystemSettingOtherProductLenght = int.Parse(ShoppingSystemSettingModel.OtherProductLenght ?? "0");
            if (_uow.OtherProducts.Any())
                lastCode = _uow.OtherProducts.Select(x => x.OtherProductCode).Max();
            var len = (lastCode.ToString()).Length;
            if (ShoppingSystemSettingOtherProductLenght < 0)
            {
                Error(".تنظیمات بازرگانی خرید انجام نشده است");

            }
           else if (len>ShoppingSystemSettingOtherProductLenght )
            
            {
                Error("شماره گذاری به پایان رسیده است");

            }
            else
            {
                string s = "0," + (ShoppingSystemSettingOtherProductLenght).ToString();
                
                if (lastCode == 0)
                {
                    var stringLastbrandCode = "1";
                    var lastBrandCode = stringLastbrandCode.ToString().PadRight(ShoppingSystemSettingOtherProductLenght, '0');
                    OtherProduct.OtherProductCode = int.Parse(lastBrandCode);

                    Regex = $"^[0-9]{{{s}}}$";
                    //SL.SLCodeEmptyValue = SelectedTL.TLCode;

                }
                else
                {

                    lastCode++;
                    var lastBrandCode = lastCode.ToString().PadLeft(ShoppingSystemSettingOtherProductLenght, '0');

                    OtherProduct.OtherProductCode = int.Parse($"{lastBrandCode}");

                    Regex = $"^[0-9]{{{s}}}$";
                }


            }
        }
        #endregion
        #region Fields
        private SainaDbContext _uow;
        private IEditableCollectionViewAddNewItem _otherProducts;
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        public event Action<string> Error;

        #endregion
        #region Commands
        public ICommand AddBrandCommand { get; private set; }


        #endregion
        #region Properties
        private OtherProduct _otherProduct;
        public OtherProduct OtherProduct
        {
            get { return _otherProduct; }
            set { SetProperty(ref _otherProduct, value); }
        }

        public IEditableCollectionViewAddNewItem OtherProducts
        {
            get { return _otherProducts; }
            set
            {
                SetProperty(ref _otherProducts, value);
            }
        }
        private string _regex;


        public string Regex
        {
            get { return _regex; }
            set { SetProperty(ref _regex, value); }
        }

        public int ShoppingSystemSettingOtherProductLenght { get; private set; }


        #endregion
        #region Methods
        public void Loaded()
        {
            _uow = new SainaDbContext();
            OtherProducts = new QueryableCollectionView(_uow.OtherProducts.ToList());
        }
        public override void UnLoaded()
        {
            _uow.Dispose();
        }
        public void AddOtherProduct(OtherProduct otherProduct)
        {
            _uow.OtherProducts.Add(otherProduct);
        }
        public void Save()
        {
            _uow.SaveChanges();
        }
        public int DeleteOtherProduct(OtherProduct otherProduct)
        {
            _uow.OtherProducts.Remove(otherProduct);
            return _uow.SaveChanges();
        }

        #endregion

    }
}

