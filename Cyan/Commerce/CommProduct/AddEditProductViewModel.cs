using AutoMapper;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Commerce.CommProduct
{
   public class AddEditProductViewModel:BindableBase
    {
        #region Constructors
        public AddEditProductViewModel(IProductsService productsService)
        {
            _productsService = productsService;
         
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);

        }
        #endregion
        #region Fields
        private IProductsService _productsService;
       
        private Product _editingProduct = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableProduct _Product;
        public EditableProduct Product
        {
            get { return _Product; }
            set { SetProperty(ref _Product, value); }
        }
     
        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode
        
       
        public void SetProduct(Product product)
        {
            //_editingProduct = product;
            //if (Product != null) Product.ErrorsChanged -= RaiseCanExecuteChanged;
            //Product = new SimpleEditableProduct();
            //// CopyProduct(product, Product);
            // Product =product;
            Product = Mapper.Map<Product, EditableProduct>(product);
            Product.ErrorsChanged += RaiseCanExecuteChanged;
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            Done?.Invoke();
        }
        private async void OnSave()
        {
            var editingProduct = Mapper.Map<EditableProduct, Product>(Product);
            try
            {
                if (EditMode)
                    await _productsService.UpdateProductAsync(editingProduct);
                else
                    await _productsService.AddProductAsync(editingProduct);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                Product = null;
            }
        }

        private bool CanSave()
        {
            return !Product.HasErrors;
        }

        #endregion

    }
}
