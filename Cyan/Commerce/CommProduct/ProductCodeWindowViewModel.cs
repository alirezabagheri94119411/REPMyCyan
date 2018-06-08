using Saina.ApplicationCore.Entities.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Data;

namespace Saina.WPF.Commerce.CommProduct
{
    class ProductCodeWindowViewModel : BindableBase
    {
        #region Constructors
        public ProductCodeWindowViewModel(SainaDbContext uow)
        {
            ProductBrandsDropDownOpenedCommand = new RelayCommand(OnProductBrandsDropDownOpened, () => ProductBrands != null && ProductBrands.Any());
            ProductTypesDropDownOpenedCommand = new RelayCommand(OnProductTypesDropDownOpened, () => ProductTypes != null && ProductTypes.Any());
            ProductModelsDropDownOpenedCommand = new RelayCommand(OnProductModelsDropDownOpened, () => ProductModels != null && ProductModels.Any());
            OtherProductsDropDownOpenedCommand = new RelayCommand(OnOtherProductsDropDownOpened, () => OtherProducts != null && OtherProducts.Any());
            _uow =uow;
            ProductBrands = new ObservableCollection<ProductBrand>();
        }


        #endregion
        private SainaDbContext _uow;
        private List<OtherProduct> _allOtherProduct;
        #region Fields

        #endregion
        #region Commands
        public ICommand ProductBrandsDropDownOpenedCommand { get; private set; }
        public ICommand ProductTypesDropDownOpenedCommand { get; }
        public ICommand ProductModelsDropDownOpenedCommand { get; }
        public ICommand OtherProductsDropDownOpenedCommand { get; }

        #endregion
        #region Metode
        //public void Loaded()
        //{
        //    _uow = new SainaDbContext();
        //  //  Products = new QueryableCollectionView(_uow.Products.ToList());
        //    ProductBrands = _uow.ProductBrands.ToList();
        //}
        private async void OnOtherProductsDropDownOpened()
        {
            _allOtherProduct= await _uow.OtherProducts.AsNoTracking().ToListAsync().ConfigureAwait(false);

            OtherProducts = new ObservableCollection<OtherProduct>(_allOtherProduct);
        }
        private async void OnProductBrandsDropDownOpened()
        {
            _allProductBrand = await _uow.ProductBrands.AsNoTracking().ToListAsync().ConfigureAwait(false);

            ProductBrands = new ObservableCollection<ProductBrand>(_allProductBrand);
        }

        private async void OnProductTypesDropDownOpened()
        {
            _allProductType = await _uow.ProductTypes.AsNoTracking().ToListAsync().ConfigureAwait(false);

            ProductTypes = new ObservableCollection<ProductType>(_allProductType);
        }

        private async void OnProductModelsDropDownOpened()
        {
            _allProductModel = await _uow.ProductModels.AsNoTracking().ToListAsync().ConfigureAwait(false);

            ProductModels = new ObservableCollection<ProductModel>(_allProductModel);
        }
        #endregion
        #region Properties
        private ProductBrand _productBrand;
        public ProductBrand ProductBrand
        {
            get { return _productBrand; }
            set { SetProperty(ref _productBrand, value); }
        }
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }
        private ObservableCollection<ProductBrand> _productBrands   ;
        private List<ProductBrand> _allProductBrand;

        public ObservableCollection<ProductBrand> ProductBrands
        {
            get { return _productBrands; }
            set { SetProperty(ref _productBrands, value); }
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


        #endregion

    }
}
