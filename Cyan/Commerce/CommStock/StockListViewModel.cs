using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Commerce;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Saina.WPF.Commerce.CommStock
{
    /// <summary>
    /// لیست انبار
    /// </summary>
   public class StockListViewModel:BindableBase
    {
        #region Constructors
        public StockListViewModel(IStocksService stocksService, ISLsService sLsService, IProductsService productsService)
        {
            _sLsService = sLsService;
            _productsService = productsService;
            _stocksService = stocksService;
            AddStockCommand = new RelayCommand(OnAddStock);
            EditStockCommand = new RelayCommand<Stock>(OnEditStock);
            DeleteCommand = new RelayCommand<Stock>(OnDeleteStock);
        }
        #endregion
        #region Fields
        private IProductsService _productsService;
        private List<Product> _allProducts;
        private IStocksService _stocksService;
        private ISLsService _sLsService;
        private List<Stock> _allStocks;
        private List<SL> _allSLs;
        #endregion
        #region Commands

        public ICommand AddStockCommand { get; private set; }
        public ICommand EditStockCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        #endregion
        #region Properties
        private ObservableCollection<Stock> _stocks;
        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks; }
            set { SetProperty(ref _stocks, value); }
        }
        private ObservableCollection<SL> _sLs;

        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        public event Action<Stock> AddStockRequested;
        public event Action<Stock> EditStockRequested ;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public event Action<string> Error;

        #endregion
        #region Methode

        public async void LoadStocks()
        {
            _allStocks = await _stocksService.GetStocksAsync();
            Stocks = new ObservableCollection<Stock>(_allStocks);
        }
        private async void OnAddStock()
        {
            _allSLs = await _sLsService.GetSLsActiveAsync();
            SLs = new ObservableCollection<SL>(_allSLs);
            if (Products==null)
            {
                _allProducts = await _productsService.GetProductsAsync();
              //  SLs = new ObservableCollection<SL>(_allSLs);
              //  Products = new ObservableCollection<Product>(_allProducts);
            _allProducts = await _productsService.GetProductsAsync();

            }

            if (SLs?.Any() != true)
            {
                Error(".سند معین ثبت نشده است");
               
            }
            //else if (Products?.Any() != true)
            //{
            //    DialogParameters parameters = new DialogParameters();
            //    parameters.OkButtonContent = "بله";
            //    parameters.Header = "اخطار";
            //    parameters.Content = "کالا ثبت نشده است";
            //    // parameters.Closed = OnClosed;
            //    RadWindow.Alert(parameters);
            //}
            else
            {
                AddStockRequested(new Stock());
            }
        }
        private void OnEditStock(Stock stock)
        {
            EditStockRequested(stock);
        }
        private async void OnDeleteStock(Stock stock)
        {
            if (Deleting())
            {
                try
                {
                    await _stocksService.DeleteStockAsync(stock.StockId);
                    Stocks.Remove(stock);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }
            }
        }

        #endregion
    }
}
