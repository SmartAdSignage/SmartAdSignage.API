using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Extra
{
    [Serializable]
    public class LightMeterException : Exception
    {

        public LightMeterException() { }

        public LightMeterException(string message) : base(message) { }
    }
}
