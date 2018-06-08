using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.ApplicationCore.IServices.Commerce;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Settings.ShoppingSetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Data;
using Saina.ApplicationCore.DTOs;
using System.Data.Entity;
using Telerik.Windows.Controls;

namespace Saina.WPF.Commerce.CommProduct
{
    /// <summary>
    /// لیست کالا
    /// </summary>
    class ProductListViewModel : BindableBase
    {

        #region Constructors
        public ProductListViewModel(IShoppingSystemSettingsService shoppingSystemSettingsService, SainaDbContext uow)
        {
            _shoppingSystemSettingsService = shoppingSystemSettingsService;
            AddBrandCommand = new RelayCommand(OnAddProduct);
            
            ApplyCommand = new RelayCommand(OnApply);
            ProductDropDownOpenedCommand = new RelayCommand(OnProductModelsDropDownOpened, () => ProductBrands != null && ProductBrands.Any());
            _uow = uow;
            ProductBrands = new ObservableCollection<ProductBrand>();
            OtherProducts = new ObservableCollection<OtherProduct>();
            ProductTypes = new ObservableCollection<ProductType>();
            ProductModels = new ObservableCollection<ProductModel>();
            MeasurementUnits = new ObservableCollection<MeasurementUnit>();
            // Stocks = new ObservableCollection<Stock>();
            //InventoryControl = new InventoryControl();
        }

       




        #endregion
        #region Fields
        private SainaDbContext _uow;
        private ICollectionView _products;
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        public event Action<string> Error;
        public event Action Done;
        private List<OtherProduct> _allOtherProduct;


        #endregion
        #region Commands
        public ICommand AddBrandCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }
        public ICommand ApplyCommand { get; }
        public ICommand ProductDropDownOpenedCommand { get; }

        #endregion
        #region Properties
        public Product Product=> (Product)Products.CurrentItem;

        public ICollectionView Products
        {
            get { return _products; }
            set
            {
                SetProperty(ref _products, value);
            }
        }
        //}
        private string _regex;


        public string Regex
        {
            get { return _regex; }
            set { SetProperty(ref _regex, value); }
        }
        private ProductBrand _productBrand;
        public ProductBrand ProductBrand
        {
            get { return _productBrand; }
            set { if (value == null) return; SetProperty(ref _productBrand, value); }
        }
        private ProductModel _productModel;
        public ProductModel ProductModel
        {
            get { return _productModel; }
            set { if (value == null) return; SetProperty(ref _productModel, value); }
        }
        private ProductType _productType;
        public ProductType ProductType
        {
            get { return _productType; }
            set { if (value == null) return; SetProperty(ref _productType, value); }
        }
        private OtherProduct _otherProduct;
        public OtherProduct OtherProduct
        {
            get { return _otherProduct; }
            set { if (value == null) return; SetProperty(ref _otherProduct, value); }
        }

        private ObservableCollection<ProductBrand> _productBrands;
        private List<ProductBrand> _allProductBrand;

        public ObservableCollection<ProductBrand> ProductBrands
        {
            get { return _productBrands; }
            set { SetProperty(ref _productBrands, value); }
        }
        private ObservableCollection<MeasurementUnit> _MeasurementUnits;
        public ObservableCollection<MeasurementUnit> MeasurementUnits
        {
            get { return _MeasurementUnits; }
            set { SetProperty(ref _MeasurementUnits, value); }
        }

        private List<Stock> _allStocks;
        private List<MeasurementUnit> _allMeasurementUnits;
        private MeasurementUnit _MeasurementUnit;
        public MeasurementUnit MeasurementUnit
        {
            get { return _MeasurementUnit; }
            set { SetProperty(ref _MeasurementUnit, value); }
        }
        private MeasurementUnit _MeasurementUnit1;
        public MeasurementUnit MeasurementUnit1
        {
            get { return _MeasurementUnit1; }
            set { SetProperty(ref _MeasurementUnit1, value); }
        }


        private ObservableCollection<OtherProduct> _otherProducts;
        public ObservableCollection<OtherProduct> OtherProducts
        {
            get { return _otherProducts; }
            set { SetProperty(ref _otherProducts, value); }
        }
        private ObservableCollection<ProductModel> _productModels;
        private List<ProductModel> _allProductModel;

        public ObservableCollection<ProductModel> ProductModels
        {
            get { return _productModels; }
            set { SetProperty(ref _productModels, value); }
        }
        private ObservableCollection<ProductType> _productTypes;
        private List<ProductType> _allProductType;

        public ObservableCollection<ProductType> ProductTypes
        {
            get { return _productTypes; }
            set { SetProperty(ref _productTypes, value); }
        }

        //    public int ProductCode { get; private set; }
        private int _productCode;
        public int ProductCode
        {
            get { return _productCode; }
            set { SetProperty(ref _productCode, value); }
        }
        private int _ShoppingSystemSettingProductLenght;
        public int ShoppingSystemSettingProductLenght
        {
            get { return _ShoppingSystemSettingProductLenght; }
            set { SetProperty(ref _ShoppingSystemSettingProductLenght, value); }
        }
        private bool _EnableCodeText;
        public bool EnableCodeText
        {
            get { return _EnableCodeText; }
            set { SetProperty(ref _EnableCodeText, value); }
        }
        private bool _EnableCodeButton;
        public bool EnableCodeButton
        {
            get { return _EnableCodeButton; }
            set { SetProperty(ref _EnableCodeButton, value); }
        }
        private string _ProductCommunication;
        public string ProductCommunication
        {
            get { return _ProductCommunication; }
            set { SetProperty(ref _ProductCommunication, value); }
        }
        private int _ProductCodeLenght;
        public int ProductCodeLenght
        {
            get { return _ProductCodeLenght; }
            set { SetProperty(ref _ProductCodeLenght, value); }
        }
        private int _ProductTypeLenght;
        public int ProductTypeLenght
        {
            get { return _ProductTypeLenght; }
            set { SetProperty(ref _ProductTypeLenght, value); }
        }
        private int _ProductBrandLenght;
        public int ProductBrandLenght
        {
            get { return _ProductBrandLenght; }
            set { SetProperty(ref _ProductBrandLenght, value); }
        }
        private int _ProductModelLenght;
        public int ProductModelLenght
        {
            get { return _ProductModelLenght; }
            set { SetProperty(ref _ProductModelLenght, value); }
        }
        private int _OtherProductLenght;
        public int OtherProductLenght
        {
            get { return _OtherProductLenght; }
            set { SetProperty(ref _OtherProductLenght, value); }
        }
        private int _IranCodeProduct;
        public int IranCodeProductLenght
        {
            get { return _IranCodeProduct; }
            set { SetProperty(ref _IranCodeProduct, value); }
        }
        private int _NumberLevel;
        public int NumberLevel
        {
            get { return _NumberLevel; }
            set { SetProperty(ref _NumberLevel, value); }
        }

        private bool _EnableTab;
        public bool EnableTab
        {
            get { return _EnableTab; }
            set { SetProperty(ref _EnableTab, value); }
        }
        private int _Barcode;
        public int BarcodeLenght
        {
            get { return _Barcode; }
            set { SetProperty(ref _Barcode, value); }
        }
        private string _RegexBarcode;
        public string RegexBarcode
        {
            get { return _RegexBarcode; }
            set { SetProperty(ref _RegexBarcode, value); }
        }
        private string _RegexIranCode;
        public string RegexIranCode
        {
            get { return _RegexIranCode; }
            set { SetProperty(ref _RegexIranCode, value); }
        }
        private ObservableCollection<Stock> _Stocks;
        public ObservableCollection<Stock> Stocks
        {
            get { return _Stocks; }
            set { SetProperty(ref _Stocks, value); }
        }
        private ObservableCollection<ImageProduct> _ImageProducts;
        public ObservableCollection<ImageProduct> ImageProducts
        {
            get { return _ImageProducts; }
            set { SetProperty(ref _ImageProducts, value); }
        }

        private ObservableCollection<SimilarProduct> _SimilarProducts;
        public ObservableCollection<SimilarProduct> SimilarProducts
        {
            get { return _SimilarProducts; }
            set { SetProperty(ref _SimilarProducts, value); }
        }
        private SimilarProduct _SimilarProduct;
        public SimilarProduct SimilarProduct
        {
            get { return _SimilarProduct; }
            set { SetProperty(ref _SimilarProduct, value); }
        }

        //private InventoryControl _InventoryControl;
        //public InventoryControl InventoryControl
        //{
        //    get { return _InventoryControl; }
        //    set { SetProperty(ref _InventoryControl, value); }
        //}


        #endregion
        #region Methods
        public async void Loaded()
        {
            _uow = new SainaDbContext();
            Products = new QueryableCollectionView(_uow.Products.ToList());
            _allMeasurementUnits = await _uow.MeasurementUnits.AsNoTracking().ToListAsync().ConfigureAwait(false);
            MeasurementUnits = new ObservableCollection<MeasurementUnit>(_allMeasurementUnits);
            _allStocks = _uow.Stocks.AsNoTracking().ToList();
            Stocks = new ObservableCollection<Stock>(_allStocks);
            var ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());
            int.TryParse(ShoppingSystemSettingModel.ProductCodeLenght, out int productCodeLenght);

            ProductCommunication = ShoppingSystemSettingModel.ProductCommunication;
            ProductCodeLenght = productCodeLenght;
            ProductTypeLenght = int.Parse(ShoppingSystemSettingModel.ProductTypeLenght ?? "0");
            ProductBrandLenght = int.Parse(ShoppingSystemSettingModel.ProductBrandLenght ?? "0");
            ProductModelLenght = int.Parse(ShoppingSystemSettingModel.ProductModelLenght ?? "0");
            OtherProductLenght = int.Parse(ShoppingSystemSettingModel.OtherProductLenght ?? "0");
            IranCodeProductLenght = int.Parse(ShoppingSystemSettingModel.IranCodeProduct ?? "0");
            NumberLevel = int.Parse(ShoppingSystemSettingModel.NumberLevel ?? "0");
            BarcodeLenght = int.Parse(ShoppingSystemSettingModel.Barcode ?? "0");
            string s = "0," + (ProductCodeLenght).ToString();
            Regex = $"^[0-9]{{{s}}}$";
            if (ProductCodeLenght == 0 ||
               ProductTypeLenght == 0 ||
               ProductBrandLenght == 0 ||
               ProductModelLenght == 0 ||
               OtherProductLenght == 0 ||
               IranCodeProductLenght == 0 ||
               NumberLevel == 0 ||
               BarcodeLenght==0
              )
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "!اخطار";
                parameters.Content = " تنظیمات بازرگانی خرید انجام نشده است";
                RadWindow.Alert(parameters);
                Error(" تنظیمات بازرگانی خرید انجام نشده است");
                EnableTab = false;
            }
            else
            {
                EnableTab = true;
            }

            if (ProductCommunication == "Yes")
            {
                EnableCodeText = true;
                EnableCodeButton = true;
            }
            else
            {
                EnableCodeText = false;
                EnableCodeButton = false;
            }

            //ProductBrands = _uow.ProductBrands.ToList();
        }
        public override void UnLoaded()
        {
            _uow.Dispose();
        }
        public void AddProduct(Product product)
        {
            _uow.Products.Add(product);
        }
        public void Save()
        {
            var SelectedStocks = Stocks.Where(x => x.IsSelected == true);
            Product.Stocks.Clear();
            foreach (var stock in SelectedStocks)
            {
                //if (!Product.Stocks.Any(x => x.StockId == stock.StockId))
                    Product.Stocks.Add(stock);
                //else
                //    Product.Stocks.Add(stock);
            }
            var ss = _uow.Entry<Product>(Product).State;
            _uow.SaveChanges();
        }
        public int DeleteProduct(Product product)
        {
            _uow.Products.Remove(product);
            return _uow.SaveChanges();
        }

        private async void OnProductModelsDropDownOpened()
        {
            _allOtherProduct = await _uow.OtherProducts.AsNoTracking().ToListAsync().ConfigureAwait(false);

            OtherProducts = new ObservableCollection<OtherProduct>(_allOtherProduct);
            _allProductBrand = await _uow.ProductBrands.AsNoTracking().ToListAsync().ConfigureAwait(false);

            ProductBrands = new ObservableCollection<ProductBrand>(_allProductBrand);
            _allProductType = await _uow.ProductTypes.AsNoTracking().ToListAsync().ConfigureAwait(false);

            ProductTypes = new ObservableCollection<ProductType>(_allProductType);
            _allProductModel = await _uow.ProductModels.AsNoTracking().ToListAsync().ConfigureAwait(false);

            ProductModels = new ObservableCollection<ProductModel>(_allProductModel);

            if (!string.IsNullOrWhiteSpace(Product.ProductCodeJoin))
            {
                var t = Product.ProductCodeJoin.Split(',');
                ProductBrand = ProductBrands.FirstOrDefault(x => x.ProductBrandCode.ToString() == t[0]);
                ProductType = ProductTypes.FirstOrDefault(x => x.ProductTypeCode.ToString() == t[1]);
                ProductModel = ProductModels.FirstOrDefault(x => x.ProductModelCode.ToString() == t[2]);
                OtherProduct = OtherProducts.FirstOrDefault(x => x.OtherProductCode.ToString() == t[3]);
            }

        }

        // string join = "";
        private void OnApply()
        {
            var errorMessage = "";
            var codeString = "";
            var len = 0;
            var sub = 0;
            long lastCode = 0;
            if (ProductBrand == null)
            {
                errorMessage += $"عنوان برند خالی می باشد {Environment.NewLine}";
            }
            if (ProductType == null)
            {
                errorMessage += $"عنوان نوع خالی می باشد {Environment.NewLine}";
            }
            if (ProductModel == null)
            {
                errorMessage += $"عنوان مدل خالی می باشد {Environment.NewLine}";
            }
            if (OtherProduct == null)
            {
                errorMessage += $"عنوان سایر خالی می باشد {Environment.NewLine}";
            }
            if (errorMessage.Length > 0)
            {
                Error(errorMessage);
            }
            else
            {

                Product.ProductCodeJoin = $"{ ProductBrand.ProductBrandCode},{ProductType.ProductTypeCode},{ProductModel.ProductModelCode},{OtherProduct.OtherProductCode}";

                if (ProductBrand != null && ProductBrand.RequiredCode == true)
                {
                    codeString = (ProductBrand.ProductBrandCode).ToString();
                }

                if (ProductType != null && ProductType.RequiredCode == true)
                {
                    codeString += (ProductType.ProductTypeCode).ToString();
                }
                if (ProductModel != null && ProductModel.RequiredCode == true)
                {
                    codeString += (ProductModel.ProductModelCode).ToString();
                }
                if (OtherProduct != null && OtherProduct.RequiredCode == true)
                {
                    codeString += (OtherProduct.OtherProductCode).ToString();
                }
                if (codeString != "")
                {

                    len = codeString.Length;
                    sub = ProductCodeLenght - len;
                    string s = "0," + (ProductCodeLenght).ToString();
                    var productCode = codeString.ToString().PadRight(ProductCodeLenght, '0');
                    var productcodeLong = long.Parse($"{productCode}");
                    if (_uow.Products.Any(x => x.ProductCode == productcodeLong))
                        lastCode = _uow.ProductBrands.Select(x => x.ProductBrandCode).Max() + 1;
                    else
                    {
                        lastCode = productcodeLong;
                    }
                    if (Products.CurrentItem is Product ProductCode)
                    {
                        ProductCode.ProductCode = lastCode;
                        Done?.Invoke();
                    }

                }
                else
                {
                    Error("لطفا تیک اجباری در کد را در یکی از موارد فعال نمایید.");
                }
            }
        }
        private void OnAddProduct()
        {
            long lastIranCode = 0;
            long lastBarCode = 0;
            long lastProductCode = 0;
            if (_uow.Products.Any())
            {
                lastIranCode = _uow.Products.Select(x => x.IranCode).Max();
                lastBarCode = _uow.Products.Select(x => x.ProductBarcode).Max();
                lastProductCode = _uow.Products.Select(x => x.ProductCode).Max();
            }
            if (Products.CurrentItem is Product ProductCode)
            {
                if (EnableCodeButton==true)
                {
                    if (lastProductCode==0)
                    {
                        string s = "0," + (ProductCodeLenght).ToString();
                        var stringLastCode = "1";
                        var stringlastCode = stringLastCode.ToString().PadRight(ProductCodeLenght, '0');
                        Product.ProductCode = long.Parse(stringlastCode);
                        ProductCode.ProductCode = long.Parse(stringlastCode);

                        Regex = $"^[0-9]{{{s}}}$";
                    }
                    else
                    {
                        lastProductCode++;
                        string s = "0," + (ProductCodeLenght).ToString();
                        ProductCode.ProductCode = lastProductCode;
                        Regex = $"^[0-9]{{{s}}}$";
                    }

                }
                if (lastBarCode == 0)
                {
                    string s = "0," + (BarcodeLenght).ToString();
                    var stringLastbarCode = "1";
                    var stringlastBarCode = stringLastbarCode.ToString().PadRight(BarcodeLenght, '0');
                    Product.ProductBarcode = long.Parse(stringlastBarCode);
                    ProductCode.ProductBarcode = long.Parse(stringlastBarCode);

                    RegexBarcode = $"^[0-9]{{{s}}}$";
                }
                else
                {
                    lastBarCode++;
                    string s = "0," + (BarcodeLenght).ToString();
                    ProductCode.ProductBarcode = lastBarCode;
                    RegexBarcode = $"^[0-9]{{{s}}}$";

                }
                if (lastIranCode == 0)
                {
                    string s = "0," + (IranCodeProductLenght).ToString();
                    var stringLastIranCode = "1";
                    var stringlastIranCode = stringLastIranCode.ToString().PadRight(IranCodeProductLenght, '0');
                    ProductCode.IranCode = long.Parse(stringlastIranCode);

                    RegexIranCode = $"^[0-9]{{{s}}}$";
                }
                else
                {
                    lastIranCode++;
                    string s = "0," + (IranCodeProductLenght).ToString();
                    ProductCode.IranCode = lastIranCode;
                    RegexIranCode = $"^[0-9]{{{s}}}$";

                }

            }
        }
        #endregion
        // private SainaDbContext _uow;
        public void SaveImage()
        {

        }


    }
}
