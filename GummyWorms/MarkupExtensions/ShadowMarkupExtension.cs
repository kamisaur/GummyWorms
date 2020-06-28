using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GummyWorms
{
    [AcceptEmptyServiceProvider]
    public class ShadowMarkupExtension : IMarkupExtension<Shadow>
    {
        public float Thickness { get; set; } = (float)Shadow.ThicknessProperty.DefaultValue;

        public float OffsetX { get; set; } = (float)Shadow.OffsetXProperty.DefaultValue;

        public float OffsetY { get; set; } = (float)Shadow.OffsetYProperty.DefaultValue;

        public float BlurX { get; set; } = (float)Shadow.BlurXProperty.DefaultValue;

        public float BlurY { get; set; } = (float)Shadow.BlurYProperty.DefaultValue;

        public Color Color { get; set; } = (Color)Shadow.ColorProperty.DefaultValue;



        public Shadow ProvideValue(IServiceProvider serviceProvider)
        {
            return new Shadow
            {
                Thickness = Thickness,
                OffsetX = OffsetX,
                OffsetY = OffsetY,
                BlurX = BlurX,
                BlurY = BlurY,
                Color = Color

            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Shadow>).ProvideValue(serviceProvider);
        }

    }
}
