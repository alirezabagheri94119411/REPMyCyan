using System.ComponentModel.DataAnnotations;

namespace Saina.WPF.BasicInformation.Information.BankInfo
{
   public class EditableBank:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی بانک
        /// </summary>
        private int _bankId;
        [Required(ErrorMessage = "لطفا نوع بانک راانتخاب نمایید.")]

        public int BankId
        {
            get { return _bankId; }
            set
            {
                SetProperty(ref _bankId, value);
            }
        }


        /// <summary>
        /// نام بانک
        /// </summary>
        private string _bankName;
        [Required(ErrorMessage = "لطفا نام بانک را وارد نمایید.")]
        public string BankName
        {
            get { return _bankName; }
            set
            {
                SetProperty(ref _bankName, value);
            }
        }

        /// <summary>
        /// نام بانک 2
        /// </summary>
        ///  private string _bankName;
        private string _bankName2;
        public string BankName2
        {
            get { return _bankName2; }
            set
            {
                SetProperty(ref _bankName2, value);
            }
        }

        /// <summary>
        /// نوع بانک
        /// </summary>
        private int? _bankTypeId;
        [Required(ErrorMessage = "لطفا نوع بانک را وارد نمایید.")]
        public int? BankTypeId
        {
            get { return _bankTypeId; }
            set
            {
                SetProperty(ref _bankTypeId, value);
            }
        }
    }
}
