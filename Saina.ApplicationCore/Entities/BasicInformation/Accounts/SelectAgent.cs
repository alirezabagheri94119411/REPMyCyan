using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
    /// <summary>
    /// جدول انتخاب عامل
    /// </summary>
    [Table("SelectAgents", Schema = "Info")]
    public class SelectAgent:BaseEntity
    {
        /// <summary>
        /// ای دی انتخاب عامل
        /// </summary>
    
        private int _selectAgentId;

        public int SelectAgentId
        {
            get { return _selectAgentId; }
            set
            {
                _selectAgentId= value;
            }
        }

        /// <summary>
        /// عنوان انتخاب عامل
        /// </summary>
        private string _selectAgentTitle;

        public string SelectAgentTitle
        {
            get { return _selectAgentTitle; }
            set
            {
                _selectAgentTitle= value;
            }
        }

    }
}
