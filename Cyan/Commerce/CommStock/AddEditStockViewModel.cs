using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Commerce;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.Commerce.CommStock
{
    public class AddEditStockViewModel : BindableBase
    {
        #region Constructors

        public AddEditStockViewModel(IStocksService stocksService, IProductsService productsService, IUsersService usersService, ISLsService sLsService)
        {
            _stocksService = stocksService;
            _productsService = productsService;
            _sLsService = sLsService;
            _usersService = usersService;

            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            ProductsDropDownOpenedCommand = new RelayCommand(OnProductsDropDownOpened, () => Products != null && Products.Any());
            SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened, () => SLs != null && SLs.Any());
            DLs1DropDownOpenedCommand = new RelayCommand<string>(OnDLs1DropDownOpened);
            DLs2DropDownOpenedCommand = new RelayCommand<string>(OnDLs2DropDownOpened);
            UsersDropDownOpenedCommand = new RelayCommand(OnUsersDropDownOpened, () => Users != null && Users.Any());
        }
        
        #endregion
        #region Fields

        private IStocksService _stocksService;
        private List<Product> _allProducts;
        private IProductsService _productsService;
        private List<User> _allUsers;
        private IUsersService _usersService;
        private List<SL> _allSLs;
        private List<DL> _allDLs;
        private ISLsService _sLsService;
        //private Stock _editingStock = null;

        #endregion
        #region Commands

        public ICommand CancelCommand { get; private set; }
        public ICommand ProductsDropDownOpenedCommand { get; private set; }
        public ICommand UsersDropDownOpenedCommand { get; private set; }
        public ICommand SLsDropDownOpenedCommand { get; private set; }
        public ICommand DLs1DropDownOpenedCommand { get; private set; }
        public ICommand DLs2DropDownOpenedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        #endregion
        #region Properties

        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableStock _Stock;
        public EditableStock Stock
        {
            get { return _Stock; }
            set { SetProperty(ref _Stock, value); }
        }
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }
        private ObservableCollection<SL> _sLs;
        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }

        private SL _sL;
        public SL SL
        {
            get { return _sL; }
            set { SetProperty(ref _sL, value); }
        }

        private ObservableCollection<DL> _dLs1;
        public ObservableCollection<DL> DLs1
        {
            get { return _dLs1; }
            set { SetProperty(ref _dLs1, value); }
        }
        private ObservableCollection<DL> _dLs2;
        public ObservableCollection<DL> DLs2
        {
            get { return _dLs2; }
            set { SetProperty(ref _dLs2, value); }
        }
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }
        public event Action<Exception> Failed;
        public event Action Done;

        #endregion
        #region Methode

        private async void OnProductsDropDownOpened()
        {
            _allProducts = await _productsService.GetProductsAsync();
            Products = new ObservableCollection<Product>(_allProducts);
        }
        private async void OnUsersDropDownOpened()
        {
            _allUsers = await _usersService.GetUsersAsync();
            Users = new ObservableCollection<User>(_allUsers);
        }
        private async void OnSLsDropDownOpened()
        {
            _allSLs = await _sLsService.GetSLsActiveAsync();
            SLs = new ObservableCollection<SL>(_allSLs);
        }
        private async void OnDLs1DropDownOpened(string dlType)
        {
                _allDLs = await _sLsService.GetDLsAsync(Stock.SLId,dlType);
                DLs1 = new ObservableCollection<DL>(_allDLs);
        }
        private async void OnDLs2DropDownOpened(string dlType)
        {
                _allDLs = await _sLsService.GetDLsAsync(Stock.SLId, dlType);
                DLs2 = new ObservableCollection<DL>(_allDLs);
        }
        public async void LoadProducts()
        {
            if (Products == null)
            {
                _allProducts = await _productsService.GetProductsAsync();
                Products = new ObservableCollection<Product>(_allProducts);
            }
            if (SLs == null)
            {

                _allSLs = await _sLsService.GetSLsActiveAsync();
                SLs = new ObservableCollection<SL>(_allSLs);
            }
            if (Users == null)
            {
                _allUsers = await _usersService.GetUsersActiveAsync();
                Users = new ObservableCollection<User>(_allUsers);
            }
        }
        public void SetStock(Stock stock)
        {

            Stock = Mapper.Map<Stock, EditableStock>(stock);
            Stock.ValidationDelegate += Stock_ValidationDelegate;

            Stock.ErrorsChanged += RaiseCanExecuteChanged;
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            Done?.Invoke();
        }
        public async void OnSave()
        {
            var editingStock = Mapper.Map<EditableStock, Stock>(Stock);

            try
            {
                if (EditMode)
                    await _stocksService.UpdateStockAsync(editingStock);

                else
                    await _stocksService.AddStockAsync(editingStock);

                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed?.Invoke(ex);
            }
            finally
            {
                Stock = null;
            }
        }
        private bool CanSave()
        {
            return !Stock.HasErrors;
        }
        private async Task<List<string>> Stock_ValidationDelegate(object sender, string propertyName)
        {
            var stock = (EditableStock)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(Stock.StockTitle1):

                    if (await _stocksService.HasTitle(stock.StockTitle1))
                        errors.Add("عنوان نباید تکراری باشد");
                    return errors;
                case nameof(Stock.StockCode):
                    if (await _stocksService.Hasduplicate(stock.StockCode))
                        errors.Add("کد  انبار نباید تکراری باشد");
                    return errors;
                default:
                    return null;
            }
        }

        #endregion
    }
}
