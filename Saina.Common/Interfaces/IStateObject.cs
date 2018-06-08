using Saina.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Common.Interfaces
{
    public interface IStateObject
    {
        ObjectState State { get; set; }
    }
}
