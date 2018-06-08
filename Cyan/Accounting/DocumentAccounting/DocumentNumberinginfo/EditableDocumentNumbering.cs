using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentNumberinginfo
{
   public class EditableDocumentNumbering: ValidatableBindableBase
    {
        private int _documentNumberingId;

        public int DocumentNumberingId
        {
            get { return _documentNumberingId; }
            set
            {
                SetProperty(ref _documentNumberingId, value);
            }
        }

        /// <summary>
        /// آی دی سند
        /// </summary>
        private int? _accountDocumentId;
        [Required(ErrorMessage = "لطفا نوع سند را انتخاب نمایید.")]

        public int? AccountDocumentId
        {
            get { return _accountDocumentId; }
            set
            {
                SetProperty(ref _accountDocumentId, value);
            }
        }

        /// <summary>
        /// روش شمار گذاری
        /// </summary>
        private int? _countingWayId;
        [Required(ErrorMessage = "لطفاروش شماره گذاری را انتخاب نمایید.")]

        public int? CountingWayId
        {
            get { return _countingWayId; }
            set { SetProperty(ref _countingWayId, value); }
        }

        /// <summary>
        /// شروع شماره
        /// </summary>
        private long _startNumber;
        [Required(ErrorMessage = "لطفاشماره شروع را وارد نمایید.")]

        public long StartNumber
        {
            get { return _startNumber; }
            set
            {
                SetProperty(ref _startNumber, value);
            }
        }

        /// <summary>
        /// پایان شماره
        /// </summary>
        private long _endNumber;
        [Required(ErrorMessage = "لطفا شماره پایان را وارد نمایید.")]

        public long EndNumber
        {
            get { return _endNumber; }
            set
            {
                SetProperty(ref _endNumber, value);
            }
        }

        /// <summary>
        /// روش 
        /// </summary>
       
        private int? _styleId;
        [Required(ErrorMessage = "لطفاروش شماره گذاری را انتخاب نمایید.")]

        public int? StyleId
        {
            get { return _styleId; }
            set { SetProperty(ref _styleId, value); }
        }
        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }

    }
}
