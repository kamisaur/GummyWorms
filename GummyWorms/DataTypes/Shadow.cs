using System;
using Xamarin.Forms;

namespace GummyWorms
{
    public class Shadow : BindableObject
    {
        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(
            propertyName: nameof(Thickness)
            , returnType: typeof(float)
            , declaringType: typeof(Shadow)
            , defaultValue: default(float));

        public float Thickness
        {
            get { return (float)GetValue(ThicknessProperty); }
            set
            {
                if (value < 0)
                    throw new ArgumentException(
                        $"{nameof(Thickness)} must be greater than or equal to zero."
                        , nameof(Thickness));

                SetValue(ThicknessProperty, value);
            }
        }



        public static readonly BindableProperty OffsetXProperty = BindableProperty.Create(
            propertyName: nameof(OffsetX)
            , returnType: typeof(float)
            , declaringType: typeof(Shadow)
            , defaultValue: default(float));

        public float OffsetX
        {
            get { return (float)GetValue(OffsetXProperty); }
            set
            {
                SetValue(OffsetXProperty, value);
            }
        }



        public static readonly BindableProperty OffsetYProperty = BindableProperty.Create(
            propertyName: nameof(OffsetY)
            , returnType: typeof(float)
            , declaringType: typeof(Shadow)
            , defaultValue: default(float));

        public float OffsetY
        {
            get { return (float)GetValue(OffsetYProperty); }
            set
            {
                SetValue(OffsetYProperty, value);
            }
        }



        public static readonly BindableProperty BlurXProperty = BindableProperty.Create(
            propertyName: nameof(BlurX)
            , returnType: typeof(float)
            , declaringType: typeof(Shadow)
            , defaultValue: default(float));

        public float BlurX
        {
            get { return (float)GetValue(BlurXProperty); }
            set
            {
                SetValue(BlurXProperty, value);
            }
        }



        public static readonly BindableProperty BlurYProperty = BindableProperty.Create(
            propertyName: nameof(BlurY)
            , returnType: typeof(float)
            , declaringType: typeof(Shadow)
            , defaultValue: default(float));

        public float BlurY
        {
            get { return (float)GetValue(BlurYProperty); }
            set
            {
                SetValue(BlurYProperty, value);
            }
        }



        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: nameof(Color)
            , returnType: typeof(Color)
            , declaringType: typeof(Shadow)
            , defaultValue: Color.Black);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set
            {
                SetValue(ColorProperty, value);
            }
        }




    }
}
