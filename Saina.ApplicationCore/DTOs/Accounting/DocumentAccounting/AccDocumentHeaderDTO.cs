using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting
{
   public class AccDocumentHeaderDTO
    {
        public int AccDocumentHeaderId { get; set; }
        public long DailyNumber { get; set; }
        public long SystemFixNumber { get; set; }
        public long DocumentNumber { get; set; }
        public string Exporter { get; set; }
        public string Seconder { get; set; }
        public StatusEnum Status { get; set; }
        public string SystemName { get; set; }
        public long ManualDocumentNumber { get; set; }
        public string HeaderDescription { get; set; }
        //public DateTime DocumentDate { get; set; }
        private DateTime _DocumentDate;

        public DateTime DocumentDate
        {
            get { return _DocumentDate; }
            set { _DocumentDate = value; }
        }

        [NotMapped]
        public string DocumentDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string result = string.Format($"{pc.GetYear(_DocumentDate)}/{pc.GetMonth(_DocumentDate)}/{pc.GetDayOfMonth(_DocumentDate)}");
                return result;
            }
        }
        public double? AmountDocument { get; set; }
        public string TypeDocumentTitle { get; set; }
    }
}
