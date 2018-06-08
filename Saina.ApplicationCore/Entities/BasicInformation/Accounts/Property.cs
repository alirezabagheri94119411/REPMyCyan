using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus.Internal.Core.SchemaObjectModel;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    /// <summary>
    /// جدول ویژگی ها
    /// </summary>
    [Table("Properties", Schema = "Info")]
    public class Property : BaseEntity
    {
        public Property()
        {
        }
        /// <summary>
        /// آی دی ویژگی
        /// </summary>
        private int _propertyId;

        public int PropertyId
        {
            get { return _propertyId; }
            set { _propertyId = value; }
        }

        /// <summary>
        /// عنوان ویژگی
        /// </summary>
        private string _propertyTitle;

        public string PropertyTitle
        {
            get { return _propertyTitle; }
            set { _propertyTitle = value; }
        }

       // public virtual ICollection<SL> SLs { get; protected set; }
    }
}
