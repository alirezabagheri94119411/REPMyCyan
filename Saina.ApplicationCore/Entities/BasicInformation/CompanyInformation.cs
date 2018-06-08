using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation
{

    /// <summary>
    /// تعریف فیلدهای اطلاعات شرکت
    /// </summary>
    public class CompanyInformation : BaseEntity
    {
        /// <summary>
        /// شماره ثبت
        /// </summary>
        private string _storeCode;

        public string StoreCode
        {
            get { return _storeCode; }
            set { _storeCode = value; }
        }
        /// <summary>
        /// نام فروشگاه
        /// </summary>
        private string _storeName;

        public string StoreName
        {
            get { return _storeName; }
            set { _storeName = value; }
        }
        /// <summary>
        /// کد اقتصادی
        /// </summary>
        private string _economicStoreCode;

        public string EconomicStoreCode
        {
            get { return _economicStoreCode; }
            set { _economicStoreCode = value; }
        }
        /// <summary>
        /// استان
        /// </summary>
        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        /// <summary>
        /// شهر
        /// </summary>
        private string _province;

        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }
        /// <summary>
        /// شهرستان
        /// </summary>
        private string _town;

        public string Town
        {
            get { return _town; }
            set { _town = value; }
        }
        /// <summary>
        /// کدپستی
        /// </summary>
        private string _postalCode;

        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }
        /// <summary>
        /// آدرس
        /// </summary>
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// تلفن
        /// </summary>
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        /// <summary>
        /// فکس
        /// </summary>
        private string _fax;

        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        private string _logo;
        /// <summary>
        /// لوگو
        /// </summary>
        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }



    }
}
