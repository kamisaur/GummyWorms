using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace GummyWorms
{
    public class Border : BindableObject
    {
        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(
            propertyName: nameof(Thickness)
            , returnType: typeof(float)
            , declaringType: typeof(Border)
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


        public static readonly BindableProperty GradientStartPointProperty = BindableProperty.Create(
            propertyName: nameof(GradientStartPoint)
            , returnType: typeof(Point)
            , declaringType: typeof(Border)
            , defaultValue: new Point(0, 0));

        public Point GradientStartPoint
        {
            get => (Point)GetValue(GradientStartPointProperty);
            set => SetValue(GradientStartPointProperty, value);
        }


        public static readonly BindableProperty GradientEndPointProperty = BindableProperty.Create(
            propertyName: nameof(GradientEndPoint)
            , returnType: typeof(Point)
            , declaringType: typeof(Border)
            , defaultValue: new Point(1, 0));

        public Point GradientEndPoint
        {
            get => (Point)GetValue(GradientEndPointProperty);
            set => SetValue(GradientEndPointProperty, value);
        }


        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: nameof(Color)
            , returnType: typeof(Color)
            , declaringType: typeof(Border)
            , defaultValue: default(Color));

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty GradientStopsProperty = BindableProperty.Create(
            propertyName: nameof(GradientStops)
            , returnType: typeof(GradientStopCollection)
            , declaringType: typeof(Border)
            , defaultValue: default(GradientStopCollection)
            , defaultValueCreator: bindable =>
                {
                    return new GradientStopCollection();
                }
            , propertyChanging: (bindable, oldvalue, newvalue) =>
                {
                    if (oldvalue != null)
                    {
                        (bindable as Border).SetupInternalCollectionPropertyPropagation(true);
                    }
                }
            , propertyChanged: (bindable, oldvalue, newvalue) =>
                {
                    if (newvalue != null)
                    {
                        (bindable as Border).SetupInternalCollectionPropertyPropagation();
                    }
                });


        public GradientStopCollection GradientStops
        {
            get { return (GradientStopCollection)GetValue(GradientStopsProperty); }
            set { SetValue(GradientStopsProperty, value); }
        }


        void SetupInternalCollectionPropertyPropagation(bool teardown = false)
        {
            if (teardown && GradientStops != null)
                GradientStops.CollectionChanged -= InternalCollectionChanged;
            else if (GradientStops != null)
                GradientStops.CollectionChanged += InternalCollectionChanged;
        }

        void InternalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            => OnPropertyChanged(ProgressBar.BorderProperty.PropertyName);

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ColorProperty.PropertyName ||
                propertyName == GradientStartPointProperty.PropertyName ||
                propertyName == GradientEndPointProperty.PropertyName ||
                propertyName == ThicknessProperty.PropertyName ||
                propertyName == nameof(GradientStops))
            {
                OnPropertyChanged(ProgressBar.BorderProperty.PropertyName);
            }
        }



        public SKColor[] GetGradinetStops()
        {
            if (GradientStops != null)
                return GradientStops.Select(x => x.Color.ToSKColor()).ToArray();
            else
                return new SKColor[] { Color.ToSKColor(), Color.ToSKColor() };
        }


        public float[] GetOffsets()
        {
            if (GradientStops != null)
                return GradientStops.Select(x => x.Offset).ToArray();
            else
                return new float[] { 0, 1f };
        }

    }
}
