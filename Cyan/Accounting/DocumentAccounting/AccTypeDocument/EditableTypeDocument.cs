using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.AccTypeDocument
{
    /// <summary>
    /// نوع سند حسابداری
    /// </summary>
   public class EditableTypeDocument:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی انواع سند حسابداری
        /// </summary>
        private int _typeDocumentId;

        public int TypeDocumentId
        {
            get { return _typeDocumentId; }
            set { SetProperty(ref _typeDocumentId, value); }
        }
        /// <summary>
        /// عنوان انواع سند حسابداری
        /// </summary>
        private string _typeDocumentTitle;
        [Required(ErrorMessage = "عنوان را وارد نمایید.")]

        public string TypeDocumentTitle
        {
            get { return _typeDocumentTitle; }
            set { SetProperty(ref _typeDocumentTitle, value); }
        }
    }
}
