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
namespace Saina.WPF.Commerce.ProductModels
{
    class ProductModelListViewModel : BindableBase
    {

        #region Constructors
        public ProductModelListViewModel(IShoppingSystemSettingsService shoppingSystemSettingsService, SainaDbContext uow)
        {
            _shoppingSystemSettingsService = shoppingSystemSettingsService;
            AddBrandCommand = new RelayCommand(OnAddProduct);
            ProductModel = new ProductModel();
            _uow = uow;

        }

        private void OnAddProduct()
        {
            var lastCode = 0;
            var ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());
            ShoppingSystemSettingProductModelLenght = int.Parse(ShoppingSystemSettingModel.ProductModelLenght ?? "0");
            if (_uow.ProductModels.Any())
                lastCode = _uow.ProductModels.Select(x => x.ProductModelCode).Max();
            var len = (lastCode.ToString()).Length;

            if (ShoppingSystemSettingProductModelLenght < 0)
            {
                Error(".تنظیمات بازرگانی خرید انجام نشده است");

            }
            else if (len > ShoppingSystemSettingProductModelLenght)

            {
                Error("شماره گذاری به پایان رسیده است");

            }
            else
            {
                string s = "0," + (ShoppingSystemSettingProductModelLenght).ToString();
              
                if (lastCode == 0)
                {
                    var stringLastbrandCode = "1";
                    var lastBrandCode = stringLastbrandCode.ToString().PadRight(ShoppingSystemSettingProductModelLenght, '0');
                    ProductModel.ProductModelCode = int.Parse(lastBrandCode);

                    Regex = $"^[0-9]{{{s}}}$";
                    //SL.SLCodeEmptyValue = SelectedTL.TLCode;

                }
                else
                {

                    lastCode++;
                    var lastBrandCode = lastCode.ToString().PadLeft(ShoppingSystemSettingProductModelLenght, '0');

                    ProductModel.ProductModelCode = int.Parse($"{lastBrandCode}");

                    Regex = $"^[0-9]{{{s}}}$";
                }


            }
        }
        #endregion
        #region Fields
        private SainaDbContext _uow;
        private IEditableCollectionViewAddNewItem _productModels;
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        public event Action<string> Error;

        #endregion
        #region Commands
        public ICommand AddBrandCommand { get; private set; }


        #endregion
        #region Properties
        private ProductModel _productModel;
        public ProductModel ProductModel
        {
            get { return _productModel; }
            set { SetProperty(ref _productModel, value); }
        }

        public IEditableCollectionViewAddNewItem ProductModels
        {
            get { return _productModels; }
            set
            {
                SetProperty(ref _productModels, value);
            }
        }
        private string _regex;


        public string Regex
        {
            get { return _regex; }
            set { SetProperty(ref _regex, value); }
        }

        public int ShoppingSystemSettingProductModelLenght { get; private set; }


        #endregion
        #region Methods
        public void Loaded()
        {
            _uow = new SainaDbContext();
            ProductModels = new QueryableCollectionView(_uow.ProductModels.ToList());
        }
        public override void UnLoaded()
        {
            _uow.Dispose();
        }
        public void AddProductModel(ProductModel productModel)
        {
            _uow.ProductModels.Add(productModel);
        }
        public void Save()
        {
            _uow.SaveChanges();
        }
        public int DeleteProductModel(ProductModel productModel)
        {
            _uow.ProductModels.Remove(productModel);
            return _uow.SaveChanges();
        }

        #endregion

    }
}
   