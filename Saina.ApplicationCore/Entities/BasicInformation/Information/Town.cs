using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Information
{
    /// <summary>
    /// نام شهرستان
    /// </summary>
   public class Town:BaseEntity
    {
        /// <summary>
        /// آی دی شهر
        /// </summary>
        [Key]
        public int TownId { get; set; }
        /// <summary>
        ///عنوان شهر
        /// </summary>
        public string TownTitle { get; set; }
        /// <summary>
        /// لیستی از شرکت ها
        /// </summary>
     //   public virtual ICollection<Company> Companies { get; protected set; }
    }
}
