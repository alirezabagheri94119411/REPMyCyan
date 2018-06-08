using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
   public enum ProductTypeEnum
    {
        [Description("کالا")]
        Product =1,
        [Description("خدمات")]
        Service =2
    }
  public  enum TaxExemptEnum
    {
        [Description("خرید")]

        Buy = 1,
        [Description("فروش")]

        Sale = 2
    }
    /// <summary>
    /// جدول کالا
    /// </summary>
    public class Product: BaseEntity
    {
        public Product()
        {
            Stocks = new ObservableCollection<Stock>();
            ImageProducts = new ObservableCollection<ImageProduct>();
            SimilarProducts = new ObservableCollection<SimilarProduct>();
            ProductTypeEnum = ProductTypeEnum.Product;
        }
        private int _productId;
        [Key]
        /// <summary>
        /// آی دی کالا
        /// </summary>
        
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }
        private long _productCode;
        /// <summary>
        /// کد کالا
        /// </summary>
        public long ProductCode
        {
            get { return _productCode; }
            set { SetProperty(ref _productCode, value); }
        }

        private string _productTitle;
        /// <summary>
        /// عنوان کالا
        /// </summary>

        public string ProductTitle
        {
            get { return _productTitle; }
            set { _productTitle = value; }
        }
        private string _productTitle2;
        /// <summary>
        /// عنوان کالا
        /// </summary>

        public string ProductTitle2
        {
            get { return _productTitle2; }
            set { _productTitle2 = value; }
        }
        private ProductTypeEnum _productTypeEnum;
        /// <summary>
        /// نوع کالا
        /// </summary>
        public ProductTypeEnum ProductTypeEnum
        {
            get { return _productTypeEnum; }
            set { SetProperty(ref _productTypeEnum, value); }
        }
        private bool _salable;
        /// <summary>
        /// قابل فروش
        /// </summary>
        public bool Salable
        {
            get { return _salable; }
            set { SetProperty(ref _salable, value); }
        } 
        /// <summary>
        /// معاف از مالیات
        /// </summary>
        private TaxExemptEnum _TaxExemptEnum;
        public TaxExemptEnum TaxExemptEnum
        {
            get { return _TaxExemptEnum; }
            set { SetProperty(ref _TaxExemptEnum, value); }
        }
        
        private long _productBarcode;
        /// <summary>
        /// بارکد کالا
        /// </summary>
        public long ProductBarcode
        {
            get { return _productBarcode; }
            set { SetProperty(ref _productBarcode, value); }
        }
        private long _iranCode;
        /// <summary>
        /// ایران کد
        /// </summary>
        public long IranCode
        {
            get { return _iranCode; }
            set { SetProperty(ref _iranCode, value); }
        }
        private double _minInventory;
        /// <summary>
        /// حداقل موجودی
        /// </summary>
        public double MinInventory
        {
            get { return _minInventory; }
            set { SetProperty(ref _minInventory, value); }
        }
        private double _maxInventory;
        /// <summary>
        /// حداکثر موجودی
        /// </summary>
        public double MaxInventory
        {
            get { return _maxInventory; }
            set { SetProperty(ref _maxInventory, value); }
        }
        private bool _agent;
        /// <summary>
        /// عامل ردیابی
        /// </summary>
        public bool Agent
        {
            get { return _agent; }
            set { SetProperty(ref _agent, value); }
        }

        ////private InventoryControl _inventoryControl;
        /////// <summary>
        /////// کنترل موجودی
        /////// </summary>
        ////public virtual InventoryControl InventoryControl
        ////{
        ////    get { return _inventoryControl; }
        ////    set { SetProperty(ref _inventoryControl, value); }
        ////}
        //private int? _inventoryControlId;
        ///// <summary>
        /////آی دی کنترل موجودی
        ///// </summary>
        //public int? InventoryControlId
        //{
        //    get { return _inventoryControlId; }
        //    set { SetProperty(ref _inventoryControlId, value); }
        //}
        /// <summary>
        /// واحد اصلی
        /// </summary>
        private MeasurementUnit _mainUnit;
        [ForeignKey("MainUnitId")]

        public MeasurementUnit MainUnit
        {
            get { return _mainUnit; }
            set { SetProperty(ref _mainUnit, value); }
        }
        private int? _mainUnitId;
        /// <summary>
        /// آی دی واحد اصلی
        /// </summary>
        public int? MainUnitId
        {
            get { return _mainUnitId; }
            set { SetProperty(ref _mainUnitId, value); }
        }
        /// <summary>
        /// واحد فرعی
        /// </summary>
        private MeasurementUnit _subUnit;
        [ForeignKey("SubUnitId")]

        public MeasurementUnit SubUnit
        {
            get { return _subUnit; }
            set { SetProperty(ref _subUnit, value); }
        }
        private int? _subUnitId;
        /// <summary>
        /// آی دی واحد فرعی
        /// </summary>
        public int? SubUnitId
        {
            get { return _subUnitId; }
            set { SetProperty(ref _subUnitId, value); }
        }
        private int _equivalentUnit;
        /// <summary>
        /// معادل واحد
        /// </summary>
        public int EquivalentUnit
        {
            get { return _equivalentUnit; }
            set { SetProperty(ref _equivalentUnit, value); }
        }
        private double _salePrice;
        /// <summary>
        /// قیمت فروش
        /// </summary>
        public double SalePrice
        {
            get { return _salePrice; }
            set { SetProperty(ref _salePrice, value); }
        }
        private ProductRack _productRack;
        /// <summary>
        /// قفسه کالا
        /// </summary>
        public ProductRack ProductRack
        {
            get { return _productRack; }
            set { SetProperty(ref _productRack, value); }
        }
        private int? _productRackId;
        /// <summary>
        /// آی دی قفسه کالا
        /// </summary>
        public int? ProductRackId
        {
            get { return _productRackId; }
            set { SetProperty(ref _productRackId, value); }
        }
        private string _ProductCodeJoin;
        public string ProductCodeJoin
        {
            get { return _ProductCodeJoin; }
            set { SetProperty(ref _ProductCodeJoin, value); }
        }
        
        /// <summary>
        /// تصویر کالا
        /// </summary>
        public virtual ICollection<ImageProduct> ImageProducts { get; set; }
        /// <summary>
        /// کالای مشابه
        /// </summary>
        public virtual ICollection<SimilarProduct> SimilarProducts { get; set; }

        /// <summary>
        /// لیست انبار
        /// </summary>
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
