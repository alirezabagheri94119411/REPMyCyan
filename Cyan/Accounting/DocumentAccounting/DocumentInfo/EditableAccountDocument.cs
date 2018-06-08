using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentInfo
{
  public  class EditableAccountDocument: ValidatableBindableBase
    {
        /// <summary>
        /// آی دی سند
        /// </summary>
        private int _accountDocumentId;

        public int AccountDocumentId
        {
            get { return _accountDocumentId; }
            set
            {
                SetProperty(ref _accountDocumentId, value);
            }

        }
        /// <summary>
        /// عنوان سند
        /// </summary>
        private string _accountDocumentTitle;
        [Required(ErrorMessage = "عنوان را وارد نمایید.")]

        public string AccountDocumentTitle
        {
            get { return _accountDocumentTitle; }
            set
            {
                SetProperty(ref _accountDocumentTitle, value);
            }
        }
    }
}
