﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GummyWorms.MarkupExtensions
{
    public class BorderMarkupExtension : IMarkupExtension<Border>
    {
        public int Thickness { set; get; } = (int)Border.ThicknessProperty.DefaultValue;

        public Color Color { set; get; } = (Color)Border.ColorProperty.DefaultValue;

        public Point GradientStartPoint { set; get; } = (Point)Border.GradientStartPointProperty.DefaultValue;

        public Point GradientEndPoint { set; get; } = (Point)Border.GradientEndPointProperty.DefaultValue;



        public Border ProvideValue(IServiceProvider serviceProvider)
        {
            return new Border
            {
                Color = Color,
                GradientStartPoint = GradientStartPoint,
                GradientEndPoint = GradientEndPoint,
                Thickness = Thickness
            };
        }


        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Border>).ProvideValue(serviceProvider);
        }
    }
}
