using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Saina.ApplicationCore.Entities
{
    public class ProductOption
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Factor { get; set; }
        public bool IsPizaaOption { get; set; }
        public bool IsSaladOption { get; set; }
    }
}
