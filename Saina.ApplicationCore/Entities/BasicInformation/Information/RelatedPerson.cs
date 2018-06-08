using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{
    /// <summary>
    /// افراد مرتبط
    /// </summary>
    [Table("RelatedPersons", Schema = "Info")]
    public class RelatedPerson:BaseEntity
    {
        
        /// <summary>
        /// آی دی افراد مرتبط
        /// </summary>
      
        private int _relatedPersonId;

        public int RelatedPersonId
        {
            get { return _relatedPersonId; }
            set { SetProperty(ref _relatedPersonId, value); }
           
        }

        /// <summary>
        /// نام افراد مرتبط
        /// </summary>
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }

           
        }
       

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        private string _family;

        public string Family
        {
            get { return _family; }
            set { SetProperty(ref _family, value); }

        }

        /// <summary>
        /// تلفن
        /// </summary>
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }

        }  ///// <summary>
        ///// لیستی از شرکت ها
        ///// </summary>
        private Company _company;

        public virtual Company Company
        {
            get { return _company; }
            set { SetProperty(ref _company, value); }

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

        ///// <summary>
        ///// پرسنل
        ///// </summary>
        private Person _person;

        public virtual Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
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

        ///// <summary>
        ///// حساب بانکی
        ///// </summary>
        private BankAccount _bankAccont;

        public virtual BankAccount BankAccont
        {
            get { return _bankAccont; }
            set { SetProperty(ref _bankAccont, value); }

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
        private DL dL;

        public DL DL
        {
            get { return dL; }
            set { SetProperty(ref dL, value); }
        }
        private int? dLId;

        public int? DLId
        {
            get { return dLId; }
            set { SetProperty(ref dLId, value); }
        }



    }
}
