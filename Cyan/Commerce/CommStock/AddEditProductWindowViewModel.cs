using AutoMapper;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.Data.Context;
using Saina.WPF.Commerce.CommProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Data;

namespace Saina.WPF.Commerce.CommStock
{
    class AddEditProductWindowViewModel:BindableBase
    {
        #region Constructors

        AddEditProductWindowViewModel()
        {
            SelectProductCommand = new RelayCommand(OnSelectProductCommand);
        }
        
        #endregion
        #region Fields

        private SainaDbContext _uow;
        private IEditableCollectionViewAddNewItem _Products;

        #endregion
        #region Commands

        public ICommand SelectProductCommand { get; private set; }

        #endregion
        #region Properties

        public IEditableCollectionViewAddNewItem Products
        {
            get { return _Products; }
            set
            {
                SetProperty(ref _Products, value);
            }
        }
        
        #endregion
        #region Methods

        public void Loaded()
        {
            _uow = new SainaDbContext();

            Products = new QueryableCollectionView(_uow.Products.ToList());
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
            _uow.SaveChanges();
        }
        public int DeleteProduct(Product product)
        {
            _uow.Products.Remove(product);
            return _uow.SaveChanges();
        }
        private void OnSelectProductCommand()
        {
            
        }

        #endregion
    }
}