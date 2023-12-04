using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartAdSignage.Core.Extra
{
    public class PageInfo
    {
        public PageInfo()
        {
        }
        public int Size { get; set; }
        public int Number { get; set; }
        public PageInfo(int size, int number)
        {
            Size = size;
            Number = number;
        }
    }
}
