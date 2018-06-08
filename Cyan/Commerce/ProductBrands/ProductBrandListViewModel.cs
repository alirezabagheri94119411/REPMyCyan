using Saina.ApplicationCore.DTOs;
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

namespace Saina.WPF.Commerce.ProductBrands
{
    class ProductBrandListViewModel : BindableBase
    {

        #region Constructors
        public ProductBrandListViewModel(IShoppingSystemSettingsService shoppingSystemSettingsService, SainaDbContext uow)
        {
            _shoppingSystemSettingsService = shoppingSystemSettingsService;
            AddBrandCommand = new RelayCommand(OnAddProduct);
            ProductBrand = new ProductBrand();
            _uow = uow;
        }

        private void OnAddProduct()
        {
            var lastCode = 0;
            var ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());
            ShoppingSystemSettingProductBrandLenght = int.Parse(ShoppingSystemSettingModel.ProductBrandLenght ?? "0");
            if (_uow.ProductBrands.Any())
                lastCode = _uow.ProductBrands.Select(x => x.ProductBrandCode).Max();
            var len = (lastCode.ToString()).Length;

            if (ShoppingSystemSettingProductBrandLenght < 0)
            {
                Error(".تنظیمات بازرگانی خرید انجام نشده است");

            }

            else if (len > ShoppingSystemSettingProductBrandLenght)

            {
                Error("شماره گذاری به پایان رسیده است");

            }
            else
            {
                string s = "0," + (ShoppingSystemSettingProductBrandLenght).ToString();

                if (lastCode == 0)
                {
                    var stringLastbrandCode = "1";
                    var lastBrandCode = stringLastbrandCode.ToString().PadRight(ShoppingSystemSettingProductBrandLenght, '0');
                    ProductBrand.ProductBrandCode = int.Parse(lastBrandCode);

                    Regex = $"^[0-9]{{{s}}}$";
                    //SL.SLCodeEmptyValue = SelectedTL.TLCode;

                }
                else
                {

                    lastCode++;
                    var lastBrandCode = lastCode.ToString().PadLeft(ShoppingSystemSettingProductBrandLenght, '0');

                    ProductBrand.ProductBrandCode = int.Parse($"{lastBrandCode}");

                    Regex = $"^[0-9]{{{s}}}$";
                }


            }
        }
        #endregion
        #region Fields
        private SainaDbContext _uow;
        private IEditableCollectionViewAddNewItem _productBrands;
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        public event Action<string> Error;

        #endregion
        #region Commands
        public ICommand AddBrandCommand { get; private set; }


        #endregion
        #region Properties
        private ProductBrand _productBrand;
        public ProductBrand ProductBrand
        {
            get { return _productBrand; }
            set { SetProperty(ref _productBrand, value); }
        }

        public IEditableCollectionViewAddNewItem ProductBrands
        {
            get { return _productBrands; }
            set
            {
                SetProperty(ref _productBrands, value);
            }
        }
        private string _regex;


        public string Regex
        {
            get { return _regex; }
            set { SetProperty(ref _regex, value); }
        }

        public int ShoppingSystemSettingProductBrandLenght { get; private set; }


        #endregion
        #region Methods
        public void Loaded()
        {
            _uow = new SainaDbContext();
            ProductBrands = new QueryableCollectionView(_uow.ProductBrands.ToList());
        }
        public override void UnLoaded()
        {
            _uow.Dispose();
        }
        public void AddProductBrand(ProductBrand productBrand)
        {
            _uow.ProductBrands.Add(productBrand);
        }
        public void Save()
        {
            _uow.SaveChanges();
        }
        public int DeleteProductBrand(ProductBrand productBrand)
        {
            _uow.ProductBrands.Remove(productBrand);
            return _uow.SaveChanges();
        }

        #endregion

    }
}
