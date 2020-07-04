using System;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace GummyWorms.Helpers
{
    public static class Utilities
    {

        public static SKColor[] GetGradinetStops(GradientStopCollection gradientStops)
        {
            return gradientStops.Select(x => x.Color.ToSKColor()).ToArray();
        }


        public static float[] GetOffsets(GradientStopCollection gradientStops)
        {
            return gradientStops.Select(x => x.Offset).ToArray();
        }

    }
}
