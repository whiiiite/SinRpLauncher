using System;

namespace SinRpLauncher.Extentions
{
    public static class ConvertExtentions
    {
        public static double BytesToKB(double bytes)
        {
            return bytes / 1024d;
        }

        public static double BytesToMB(double bytes)
        {
            return bytes / 1024d / 1024d;
        }

        public static double BytesToGB(double bytes)
        {
            return bytes / 1024d / 1024d / 1024d;
        }
    }
}
