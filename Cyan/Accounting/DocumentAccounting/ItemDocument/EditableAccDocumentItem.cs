using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.ItemDocument
{
    public class EditableAccDocumentItem : ValidatableBindableBase
    {
        public EditableAccDocumentItem()
        {
            //TrackingDate = DateTime.Now;
        }
        /// <summary>
        /// آی دی آیتم سند حسابداری
        /// </summary>
        private int _accDocumentItemId;

        public int AccDocumentItemId
        {
            get { return _accDocumentItemId; }
            set { SetProperty(ref _accDocumentItemId, value); }
        }
        /// <summary>
        /// ردیف
        /// </summary>
        private int _accRow;

        public int AccRow
        {
            get { return _accRow; }
            set
            {
                SetProperty(ref _accRow, value);


            }
        }

        /// <summary>
        /// آی دی کد معین
        /// </summary>
        private int _sLId;
        [Required(ErrorMessage = "حساب معین خود را انتخاب نمایید.")]

        public int SLId
        {
            get { return _sLId; }
            set { SetProperty(ref _sLId, value); }
        }
        //private SL _sLCode;
        //public SL SLCode
        //{
        //    get { return _sLCode; }
        //    set { SetProperty(ref _sLCode, value); }
        //}
        /// <summary>
        /// کد تفصیل 1 معین
        /// </summary>
        private int? _dL1Id;

        public int? DL1Id
        {
            get { return _dL1Id; }
            set
            {
                SetProperty(ref _dL1Id, value);
                //  SetProperty(ref _dL2Id, null);
            }
        }

        /// <summary>
        /// کد تفصیل 2 معین
        /// </summary>
        private int? _dL2Id;

        public int? DL2Id
        {
            get { return _dL2Id; }
            set
            {
                SetProperty(ref _dL2Id, value);
                //    SetProperty(ref _dL1Id, null);

            }

        }
        /// <summary>
        /// آرتیکل 1
        /// </summary>
        private string _description1;

        public string Description1
        {
            get { return _description1; }
            set { SetProperty(ref _description1, value); }
        }
        /// <summary>
        /// آرتیکل 2
        /// </summary>
        private string _description2;

        public string Description2
        {
            get { return _description2; }
            set { SetProperty(ref _description2, value); }
        }
        /// <summary>
        /// بدهکار
        /// </summary>
        private double _debit;

        public double Debit
        {
            get { return _debit; }
            set
            {
                SetProperty(ref _debit, value);
                if (value != 0)
                {
                    OnPropertyChanging(nameof(Credit));
                    _credit = 0;
                    OnPropertyChanged(nameof(Credit));
                }
            }
        }
        /// <summary>
        /// بستانکار
        /// </summary>
        private double _credit;

        public double Credit
        {
            get { return _credit; }
            set
            {
                SetProperty(ref _credit, value);
                if (value !=0)
                {
                    OnPropertyChanging(nameof(Debit));
                    _debit = 0;
                    OnPropertyChanged(nameof(Debit));
                }
            }
        }
        /// <summary>
        /// مبلغ ارز
        /// </summary>
        private double _currencyAmount;

        public double CurrencyAmount
        {
            get { return _currencyAmount; }
            set { SetProperty(ref _currencyAmount, value); }
        }
        /// <summary>
        /// نرخ ارز
        /// </summary>
        private int? _exchangeRateId;

        public int? ExchangeRateId
        {
            get { return _exchangeRateId; }
            set { SetProperty(ref _exchangeRateId, value); }
        }
        /// <summary>
        /// نام ارز
        /// </summary>

        private int? _currencyId;

        public int? CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }
        /// <summary>
        /// تاریخ پیگیری
        /// </summary>
        private DateTime? _trackingDate;

        public DateTime? TrackingDate
        {
            get { return _trackingDate; }
            set { SetProperty(ref _trackingDate, value); }
        }
        /// <summary>
        /// شماره پیگیری
        /// </summary>
        private long _trackingNumber;

        public long TrackingNumber
        {
            get { return _trackingNumber; }
            set { SetProperty(ref _trackingNumber, value); }
        }

        /// <summary>
        /// هدر سند حسابداری
        /// </summary>
        private int? _accDocumentHeaderId;


        public int? AccDocumentHeaderId
        {
            get { return _accDocumentHeaderId; }
            set { SetProperty(ref _accDocumentHeaderId, value); }
        }

        private DL _selectedDL1;

        public DL DL1
        {
            get { return _selectedDL1; }
            set { SetProperty(ref _selectedDL1, value); }
        }
        private DL _selectedDL2;

        public DL DL2
        {
            get { return _selectedDL2; }
            set { SetProperty(ref _selectedDL2, value); }
        }
        private SL _selectedSL;


        public SL SL
        {
            get { return _selectedSL; }
            set { SetProperty(ref _selectedSL, value); }
        }
        private CurrencyExchangesDTO _selectedCurrency;

        public CurrencyExchangesDTO SelectedCurrency
        {
            get { return _selectedCurrency; }
            set { SetProperty(ref _selectedCurrency, value); }
        }
        private double? _exchangeRate;

        public double? ExchangeRate
        {
            get { return _exchangeRate; }
            set { SetProperty(ref _exchangeRate, value); }
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
        private ObservableCollection<SL> _sLs;

        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        //private ObservableCollection<Currency> _currencies;

        //public ObservableCollection<Currency> Currencies
        //{
        //    get { return _currencies; }
        //    set { SetProperty(ref _currencies, value); }
        //}
        private ObservableCollection<ExchangeRate> _exchangeRates;

        public ObservableCollection<ExchangeRate> ExchangeRates
        {
            get { return _exchangeRates; }
            set { SetProperty(ref _exchangeRates, value); }
        }
        private ObservableCollection<SLStandardDescription> _sLStandardDescriptions;

        public ObservableCollection<SLStandardDescription> SLStandardDescriptions
        {
            get { return _sLStandardDescriptions; }
            set { SetProperty(ref _sLStandardDescriptions, value); }

        }

    }
}
