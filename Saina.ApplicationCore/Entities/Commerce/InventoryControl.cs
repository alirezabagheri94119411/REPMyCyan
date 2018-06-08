using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول کنترل موجودی
    /// </summary>
   public class InventoryControl:BaseEntity
    {
        private int _inventoryControlId;
        /// <summary>
        /// آی دی کنترل موجودی
        /// </summary>
        public int InventoryControlId
        {
            get { return _inventoryControlId; }
            set { SetProperty(ref _inventoryControlId, value); }
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
        private Product _product;
        /// <summary>
        /// نمونه کالا
        /// </summary>
        public virtual Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }
        private int _productId;
        [Key, ForeignKey("Product")]
        public int ProductId
        {
            get { return _productId; }
            set { SetProperty(ref _productId, value); }
        }

    }
}
