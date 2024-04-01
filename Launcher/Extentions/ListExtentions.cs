using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinRpLauncher.Extentions
{
    public static class ListExtentions
    {
        public static void PushFront<T>(this List<T> list, T item)
        {
            list.Insert(0, item);
        }
    }
}
