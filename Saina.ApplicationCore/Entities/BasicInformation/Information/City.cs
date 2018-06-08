using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{
    /// <summary>
    /// نام استان
    /// </summary>
   public class City:BaseEntity
    {
        /// <summary>
        /// آی دی استان
        /// </summary>
        [Key]
        public int CityId { get; set; }
        /// <summary>
        ///عنوان استان
        /// </summary>
        public string CityTitle { get; set; }
        /// <summary>
        /// لیستی از شرکت ها
        /// </summary>
     //   public virtual ICollection<City> Companies { get; protected set; }
    }
}
