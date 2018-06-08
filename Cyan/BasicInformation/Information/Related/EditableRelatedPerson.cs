using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.Related
{
   public class EditableRelatedPerson:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی افراد مرتبط
        /// </summary>

        private int _relatedPersonId;

        public int RelatedPersonId
        {
            get { return _relatedPersonId; }
            set
            {
                SetProperty(ref _relatedPersonId, value);
            }
        }

        /// <summary>
        /// نام افراد مرتبط
        /// </summary>
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        private string _family;

        public string Family
        {
            get { return _family; }
            set
            {
                SetProperty(ref _family, value);
            }
        }

        /// <summary>
        /// تلفن
        /// </summary>
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                SetProperty(ref _phone, value);
            }
        }
        /// <summary>
        /// آی دی شرکت
        /// </summary>
        private int? _companyId;

        public int? CompanyId
        {
            get { return _companyId; }
            set { SetProperty(ref _companyId, value); }
        }

        
        /// <summary>
        /// آی دی پرسنل
        /// </summary>
        private int? _personId;

        public int? PersonId
        {
            get { return _personId; }
            set { SetProperty(ref _personId, value); }
        }

       
        /// <summary>
        /// آس دس حساب بانکی
        /// </summary>
        private int? _bankAccountId;

        public int? BankAccountId
        {
            get { return _bankAccountId; }
            set { SetProperty(ref _bankAccountId, value); }
        }
    }
}
