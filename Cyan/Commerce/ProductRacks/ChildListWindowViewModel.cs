using Saina.ApplicationCore.Entities.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Commerce.ProductRacks
{
    class ChildListWindowViewModel:BindableBase
    {
        #region Constructors
        public ChildListWindowViewModel()
        {
           
            SaveCommand = new RelayCommand(OnSave, CanSave);

            ProductRack = new ProductRack();
        }
        #endregion
        #region Fields
      
        #endregion
        #region Commands
       
        public RelayCommand SaveCommand { get; private set; }
        #endregion
        #region Properties
        private ProductRack _productRack;
        public ProductRack ProductRack
        {
            get { return _productRack; }
            set { SetProperty(ref _productRack, value); }
        }


        public event Action<Exception> Failed;
        public event Action<ProductRack> SaveClicked;
        public ProductRack DataItem { get; set; }
        #endregion
        #region Methode
      
 
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        //private void OnCancel()
        //{
        //    Done?.Invoke();
        //}
        private async void OnSave()
        {
            using (var uow = new SainaDbContext())
            {
                uow.ProductRacks.Add(_productRack);
            await uow.SaveChangesAsync().ConfigureAwait(false);
            }
           
        }

        private bool CanSave()
        {
            return true;
        }
   
        #endregion
    }
}
