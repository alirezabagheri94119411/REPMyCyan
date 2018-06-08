using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{
    /// <summary>
    /// نام شهر
    /// </summary>
   public class Province:BaseEntity
    {
        /// <summary>
        /// آی دی شهرستان
        /// </summary>
        [Key]
        public int ProvinceId { get; set; }
        /// <summary>
        ///عنوان شهرستان
        /// </summary>
        public string ProvinceTitle { get; set; }
        /// <summary>
        /// لیستی از شرکت ها
        /// </summary>
       // public virtual ICollection<Company> Companies { get; set; }
    }
}
