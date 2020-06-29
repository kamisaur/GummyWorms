using System;
using System.Collections.ObjectModel;
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
            , defaultValue: new Point(0,0));

        public Point GradientStartPoint
        {
            get => (Point)GetValue(GradientStartPointProperty);
            set => SetValue(GradientStartPointProperty, value);
        }


        public static readonly BindableProperty GradientEndPointProperty = BindableProperty.Create(
            propertyName: nameof(GradientEndPoint)
            , returnType: typeof(Point)
            , declaringType: typeof(Border)
            , defaultValue: new Point(0, 0));

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
            , returnType: typeof(ObservableCollection<GradientStop>)
            , declaringType: typeof(Border)
            , defaultValue: default(ObservableCollection<GradientStop>));


        public ObservableCollection<GradientStop> GradientStops
        {
            get { return (ObservableCollection<GradientStop>)GetValue(GradientStopsProperty); }
            set { SetValue(GradientStopsProperty, value); }
        }

    }
}
