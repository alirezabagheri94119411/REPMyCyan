using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace Saina.WPF.Commerce.WarehouseDocuments.IncomingDocuments
{
    class IncomingDocumentListViewModel:BindableBase
    {
     
            #region Constructors
            public IncomingDocumentListViewModel(IAppContextService appContextService)
            {
                _appContextService = appContextService;
            }
            #endregion
            #region Fields
            private SainaDbContext _uow;
            private ICollectionView _StcDocumentHeaders;
            #endregion
            #region Commands
            private IAppContextService _appContextService;
            #endregion
            #region Properties
            public ICollectionView StcDocumentHeaders
            {
                get { return _StcDocumentHeaders; }
                set
                {
                    SetProperty(ref _StcDocumentHeaders, value);
                }
            }
            private StcDocumentItem _StcDocumentItem;
            public StcDocumentItem StcDocumentItem
            {
                get { return _StcDocumentItem; }
                set
                {
                    SetProperty(ref _StcDocumentItem, value);
                    if (_StcDocumentItem != null)
                    {
                        _StcDocumentItem.PropertyChanged -= _StcDocumentItem_PropertyChanged;
                        _StcDocumentItem.PropertyChanged += _StcDocumentItem_PropertyChanged;
                    }
                }
            }
            private void _StcDocumentItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                //if (e.PropertyName == "CurrencyId")
                //{
                //    if (StcDocumentItem?.CurrencyId != null)
                //    {
                //        var exchangeRate = _uow.ExchangeRates.OrderBy(x => x.EffectiveDate).Select(x => new { x.CurrencyId, x.ParityRate }).FirstOrDefault(x => x.CurrencyId == StcDocumentItem.CurrencyId);
                //        StcDocumentItem.ExchangeRate = exchangeRate != null ? exchangeRate.ParityRate : 1;
                //    }
                //}
                //else if (e.PropertyName == "CurrencyAmount")
                //{
                //    if (StcDocumentItem?.CurrencyId != null)
                //    {
                //        if (StcDocumentItem.Debit > 0)
                //        {
                //            StcDocumentItem.Debit = (StcDocumentItem.ExchangeRate * StcDocumentItem.CurrencyAmount);
                //        }
                //        else
                //        {
                //            StcDocumentItem.Credit = (StcDocumentItem.ExchangeRate * StcDocumentItem.CurrencyAmount);
                //        }
                //    }
                //}
            }
            private IEnumerable<SL> _sLs;
            public IEnumerable<SL> SLs
            {
                get { return _sLs; }
                set { SetProperty(ref _sLs, value); }
            }
            private IEnumerable<DL> _DLs;
            public IEnumerable<DL> DLs
            {
                get { return _DLs; }
                set { SetProperty(ref _DLs, value); }
            }
           
            private List<SLStandardDescription> _SLStandardDescriptions;
            public List<SLStandardDescription> SLStandardDescriptions
            {
                get { return _SLStandardDescriptions; }
                set { SetProperty(ref _SLStandardDescriptions, value); }
            }
            #endregion
            #region Methods
            public void Loaded()
            {
                _uow = new SainaDbContext();
                var ids = new List<int> { 1, 3, 4, 5, 2 };
                SLs = _uow.SLs.Where(x => x.Status == true).ToList();
                DLs = _uow.DLs.AsNoTracking().ToList();
                SLStandardDescriptions = _uow.SLStandardDescriptions.AsNoTracking().ToList();
              //  StcDocumentHeaders = new QueryableCollectionView(_uow.StcDocumentHeaders.Include(x => x.StcDocumentItems).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()).ToList());

                _appContextService.PropertyChanged += async (sender, eventArgs) =>
                {
                    if (eventArgs.PropertyName == "CurrentFinancialYear")
                    {
                        var dateDocument = _appContextService.CurrentFinancialYear;
                    //   StcDocumentHeaders = new QueryableCollectionView(await _uow.StcDocumentHeaders.Include(x => x.StcDocumentItems).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()).ToListAsync());

                    }
                };

            }
            public override void UnLoaded()
            {
                ////this.stream.Position = 0L;
                ////PersistenceManager manager = new PersistenceManager();
                ////manager.Load(this.reviewRadGridView, this.stream);
                ////this.EnsureLoadState();
                _uow.Dispose();
            }
            public void AddStcDocumentHeader(StcDocumentHeader stcDocumentHeader)
            {
                _uow.StcDocumentHeaders.Add(stcDocumentHeader);
            }
            public void Save()
            {
                var stcDocumentHeader = StcDocumentHeaders.CurrentItem as StcDocumentHeader;
                if (stcDocumentHeader != null)
                {
                    //stop Entity Framework from trying to save/insert child objects?
                    //var items = stcDocumentHeader.StcDocumentItems.ToList();
                    //for (int i = 0; i < items.Count; i++)
                    //{
                    //    if (items[i].DL1 != null)
                    //        _uow.Entry(items[i].DL1).State = EntityState.Detached;
                    //    if (items[i].DL2 != null)
                    //        _uow.Entry(items[i].DL2).State = EntityState.Detached;
                    //    if (items[i].Currency != null)
                    //        _uow.Entry(items[i].Currency).State = EntityState.Detached;
                    //}
                }
                _uow.SaveChanges();
            }
            public int DeleteDocumentHeader(StcDocumentHeader stcDocumentHeader)
            {
                _uow.StcDocumentHeaders.Remove(stcDocumentHeader);
                return _uow.SaveChanges();
            }
            #endregion
        }
    }
