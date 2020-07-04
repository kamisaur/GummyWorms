using System;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace GummyWorms.Base
{
    public class BaseProgressBar : SKCanvasView
    {
        public static BindableProperty ProgressProperty = BindableProperty.Create(
           propertyName: nameof(Progress)
            , returnType: typeof(float)
            , declaringType: typeof(BaseProgressBar)
            , defaultValue: 0f
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);

        public float Progress
        {
            get => (float)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }


        #region Shadow

        public static BindableProperty InnerShadowProperty = BindableProperty.Create(
            propertyName: nameof(InnerShadow)
            , returnType: typeof(Shadow)
            , declaringType: typeof(BaseProgressBar)
            , defaultValue: new Shadow()
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);


        public static BindableProperty ProgressInnerShadowProperty = BindableProperty.Create(
            propertyName: nameof(ProgressInnerShadow)
            , returnType: typeof(Shadow)
            , declaringType: typeof(BaseProgressBar)
            , defaultValue: new Shadow()
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);


        public static BindableProperty ShadowProperty = BindableProperty.Create(
            propertyName: nameof(Shadow)
            , returnType: typeof(Shadow)
            , declaringType: typeof(BaseProgressBar)
            , defaultValue: new Shadow()
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);



        public Shadow InnerShadow
        {
            get => (Shadow)GetValue(InnerShadowProperty);
            set => SetValue(InnerShadowProperty, value);
        }

        public Shadow ProgressInnerShadow
        {
            get => (Shadow)GetValue(ProgressInnerShadowProperty);
            set => SetValue(ProgressInnerShadowProperty, value);
        }

        public Shadow Shadow
        {
            get => (Shadow)GetValue(ShadowProperty);
            set => SetValue(ShadowProperty, value);
        }

        #endregion


        protected static void OnPropertyChangedInvalidate(
            BindableObject bindable
            , object oldvalue
            , object newvalue)
        {
            var control = (ProgressBar)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }
    }
}
