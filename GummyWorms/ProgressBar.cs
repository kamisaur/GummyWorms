using System;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;


namespace GummyWorms
{
    public class ProgressBar : SKCanvasView
    {
        public static BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(float),
            typeof(ProgressBar), 0f, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public float Progress
        {
            get => (float)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }


        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float),
            typeof(ProgressBar), 0f, BindingMode.OneWay,
            validateValue: (_, value) => value != null && (float)value >= 0,
            propertyChanged: OnPropertyChangedInvalidate);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static BindableProperty BarBackgroundColorProperty = BindableProperty.Create(nameof(BackgroundBarColor), typeof(Color),
            typeof(ProgressBar), Color.White, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color BackgroundBarColor
        {
            get => (Color)GetValue(BarBackgroundColorProperty);
            set => SetValue(BarBackgroundColorProperty, value);
        }

        public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float),
            typeof(ProgressBar), 12f, BindingMode.OneWay,
            validateValue: (_, value) => value != null && (float)value >= 0,
            propertyChanged: OnPropertyChangedInvalidate);

        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static BindableProperty GradientStartColorProperty = BindableProperty.Create(nameof(GradientStartColor), typeof(Color),
            typeof(ProgressBar), Color.Purple, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color GradientStartColor
        {
            get => (Color)GetValue(GradientStartColorProperty);
            set => SetValue(GradientStartColorProperty, value);
        }

        public static BindableProperty GradientEndColorProperty = BindableProperty.Create(nameof(GradientEndColor), typeof(Color),
            typeof(ProgressBar), Color.Blue, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color GradientEndColor
        {
            get => (Color)GetValue(GradientEndColorProperty);
            set => SetValue(GradientEndColorProperty, value);
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color),
            typeof(ProgressBar), Color.Blue, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static BindableProperty AlternativeTextColorProperty = BindableProperty.Create(nameof(AlternativeTextColor), typeof(Color),
            typeof(ProgressBar), Color.Blue, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color AlternativeTextColor
        {
            get => (Color)GetValue(AlternativeTextColorProperty);
            set => SetValue(AlternativeTextColorProperty, value);
        }

        public static BindableProperty HasTextProperty = BindableProperty.Create(nameof(HasText), typeof(bool),
            typeof(ProgressBar), false, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public bool HasText
        {
            get => (bool)GetValue(HasTextProperty);
            set => SetValue(HasTextProperty, value);
        }



        public static BindableProperty BorderProperty = BindableProperty.Create(
            propertyName: nameof(InnerShadow)
            , returnType: typeof(Border)
            , declaringType: typeof(ProgressBar)
            , defaultValue: new Border()
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);

        public Border Border
        {
            get { return (Border)GetValue(BorderProperty); }
            set { SetValue(BorderProperty, value); }
        }


        public static BindableProperty InnerShadowProperty = BindableProperty.Create(
            propertyName: nameof(InnerShadow)
            , returnType: typeof(Shadow)
            , declaringType: typeof(ProgressBar)
            , defaultValue: new Shadow()
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);

        public Shadow InnerShadow
        {
            get => (Shadow)GetValue(InnerShadowProperty);
            set => SetValue(InnerShadowProperty, value);
        }


        public static BindableProperty ProgressInnerShadowProperty = BindableProperty.Create(
            propertyName: nameof(ProgressInnerShadow)
            , returnType: typeof(Shadow)
            , declaringType: typeof(ProgressBar)
            , defaultValue: new Shadow()
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);

        public Shadow ProgressInnerShadow
        {
            get => (Shadow)GetValue(ProgressInnerShadowProperty);
            set => SetValue(ProgressInnerShadowProperty, value);
        }


        public static BindableProperty ShadowProperty = BindableProperty.Create(
            propertyName: nameof(Shadow)
            , returnType: typeof(Shadow)
            , declaringType: typeof(ProgressBar)
            , defaultValue: new Shadow()
            , defaultBindingMode: BindingMode.OneWay
            , validateValue: (_, value) => value != null
            , propertyChanged: OnPropertyChangedInvalidate);

        public Shadow Shadow
        {
            get => (Shadow)GetValue(ShadowProperty);
            set => SetValue(ShadowProperty, value);
        }



        static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ProgressBar)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }


        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            // Skia gets pixel count when xf adjusts width value to screen density
            var scale = CanvasSize.Width / (float)Width;


            canvas.Clear();

            var cornerRadius = CornerRadius * scale;
            var backgroundColors = new SKColor[] { SKColors.LightGray, SKColors.LightGray, SKColors.LightGray };
            var backgroundColorOffsets = new float[] { 0, 0.5f, 1 };


            // Border
            float borderThickness = Border.Thickness * scale;

            SKColor[] borderColors = null;
            if (Border.GradientStops != null)
            {
                borderColors = Border.GradientStops.Select(x => x.Color.ToSKColor()).ToArray();
            }
            else
            {
                borderColors = new SKColor[] { Border.Color.ToSKColor(), Border.Color.ToSKColor() };
            }


            float[] borderColorOffsets = null;
            if (Border.GradientStops != null)
            {
                borderColorOffsets = Border.GradientStops.Select(x => x.Offset).ToArray();
            }
            else
            {
                borderColorOffsets = new float[] { 0, 1f };
            }


            //var borderColors = new SKColor[] { SKColors.Blue, SKColors.Blue, SKColors.Blue };
            //var borderColorOffsets = new float[] { 0, 0.5f, 1 };


            // Outter shadow
            float shadowThickness = Shadow.Thickness * scale;
            float shadowOffsetX = Shadow.OffsetX * scale;
            float shadowOffsetY = Shadow.OffsetY * scale;
            float shadowBlurX   = Shadow.BlurX * scale;
            float shadowBlurY   = Shadow.BlurY * scale;
            var shadowColor = Shadow.Color.ToSKColor();

            // Inner Shadow
            float innerBlurShadowThickness  = InnerShadow.Thickness * scale;
            float innerBlurShadowOffsetX    = InnerShadow.OffsetX * scale;
            float innerBlurShadowOffsetY    = InnerShadow.OffsetY * scale;
            float innerBlurShadowBlurX      = InnerShadow.BlurX * scale;
            float innerBlurShadowBlurY      = InnerShadow.BlurY * scale;
            SKColor innerBlurShadowColor    = InnerShadow.Color.ToSKColor();


            var rect = DrawRectangle(
                canvas
                , CanvasSize.Width
                , CanvasSize.Height
                , 0
                , 0
                , cornerRadius
                , backgroundColors
                , backgroundColorOffsets
                , borderThickness
                , borderColors
                , borderColorOffsets
                , shadowOffsetX
                , shadowOffsetY
                , shadowBlurX
                , shadowBlurY
                , shadowColor
                , innerBlurShadowThickness
                , innerBlurShadowOffsetX
                , innerBlurShadowOffsetY
                , innerBlurShadowBlurX
                , innerBlurShadowBlurY
                , innerBlurShadowColor);


            var percentageWidth = rect.Width * Progress;

            var cornerRadiusProgress = cornerRadius;
            var backgroundColorsProgress = new SKColor[] { SKColors.Orange, SKColors.Violet };
            var backgroundColorOffsetsProgress = new float[] { 0, 1f };


            // Border
            float borderThicknessProgress = 0;
            var borderColorsProgress = new SKColor[] { SKColors.Blue, SKColors.Blue, SKColors.Blue };
            var borderColorOffsetsProgress = new float[] { 0, 0.5f, 1 };

            // Inner Shadow
            float innerBlurShadowThicknessProgress = ProgressInnerShadow.Thickness * scale;
            float innerBlurShadowOffsetXProgress = ProgressInnerShadow.OffsetX * scale;
            float innerBlurShadowOffsetYProgress = ProgressInnerShadow.OffsetY * scale;
            float innerBlurShadowBlurXProgress = ProgressInnerShadow.BlurX * scale;
            float innerBlurShadowBlurYProgress = ProgressInnerShadow.BlurY * scale;
            SKColor innerBlurShadowColorProgress = ProgressInnerShadow.Color.ToSKColor();


            DrawRectangle(
                canvas
                , percentageWidth
                , rect.Height
                , rect.Left
                , rect.Top
                , cornerRadiusProgress
                , backgroundColorsProgress
                , backgroundColorOffsetsProgress
                , borderThicknessProgress
                , borderColorsProgress
                , borderColorOffsetsProgress
                , 0
                , 0
                , 0
                , 0
                , 0
                , innerBlurShadowThicknessProgress
                , innerBlurShadowOffsetXProgress
                , innerBlurShadowOffsetYProgress
                , innerBlurShadowBlurXProgress
                , innerBlurShadowBlurYProgress
                , innerBlurShadowColorProgress);


            //DrawProgressBar(canvas, cornerRadius, percentageWidth);


            //var fontSize = FontSize * scale;
            //if (HasText)
            //{
            //    DrawProgressText(canvas, percentageWidth, fontSize);
            //}
        }

        /// <summary>
        /// Draw rectangle with shadows and border
        /// </summary>
        SKRect DrawRectangle(
            SKCanvas canvas
            , float width
            , float height
            , float offsetX 
            , float offsetY 
            , float cornerRadius
            , SKColor[] backgroundColors
            , float[] backgroundColorOffsets
            , float borderThickness
            , SKColor[] borderColors
            , float[] borderColorOffsets

            , float shadowOffsetX
            , float shadowOffsetY
            , float shadowBlurX
            , float shadowBlurY
            , SKColor shadowColor

            , float innerBlurShadowThickness
            , float innerBlurShadowOffsetX
            , float innerBlurShadowOffsetY
            , float innerBlurShadowBlurX
            , float innerBlurShadowBlurY
            , SKColor innerBlurShadowColor

            )
        {
            // give some padding for shadow so it's not cut out by boundaries
            // todo improve logic for x and y
            var additonalPadding = shadowBlurX + shadowBlurX;

            // so that the rect is not kissing the border
            // todo improve logic for x and y
            var initialPadding = shadowBlurX;

            // border is drawn in rect in the way that half of the border is out of rect and half is in
            var borderPadding = borderThickness / 2;


            // paddings are added based on shadow blur, so the shadow wouldn't get out of the boudnaries
            var rect = new SKRect
            {
                Left = shadowOffsetX >= 0
                    ? initialPadding + shadowBlurX + offsetX
                    : Math.Abs(shadowOffsetX) + shadowBlurX + additonalPadding + offsetX,

                Right = shadowOffsetX <= 0
                    ? width - initialPadding - shadowBlurX
                    : width - shadowOffsetX - shadowBlurX - additonalPadding,

                Top = shadowOffsetY >= 0
                    ? initialPadding + shadowBlurY + offsetY
                    : Math.Abs(shadowOffsetY) + shadowBlurY + additonalPadding + offsetY,

                Bottom = shadowOffsetY <= 0
                    ? height - initialPadding - shadowBlurY + offsetY
                    : height - shadowOffsetY - shadowBlurY - additonalPadding + offsetY
            };

            var borderRect = new SKRect
            {
                //Left = rect.Left + borderPadding - initialPadding,
                //Right = rect.Right - borderPadding + initialPadding,
                //Top = rect.Top + borderPadding - initialPadding,
                //Bottom = rect.Bottom - borderPadding + initialPadding
                Left = rect.Left + borderPadding,
                Right = rect.Right - borderPadding,
                Top = rect.Top + borderPadding,
                Bottom = rect.Bottom - borderPadding
            };

            var innerShadowRect = new SKRect
            {
                Left = innerBlurShadowOffsetX <= 0
                    ? borderRect.Left + innerBlurShadowOffsetX + borderPadding
                    : borderRect.Left + borderPadding - 0,

                Right = innerBlurShadowOffsetX >= 0
                    ? borderRect.Right + innerBlurShadowOffsetX - borderPadding
                    : borderRect.Right - borderPadding,

                Top = innerBlurShadowOffsetY <= 0
                    ? borderRect.Top + innerBlurShadowOffsetY + borderPadding
                    : borderRect.Top + borderPadding,

                Bottom = innerBlurShadowOffsetY >= 0
                    ? borderRect.Bottom + innerBlurShadowOffsetY - borderPadding
                    : borderRect.Bottom - borderPadding
            };


            using (var backgroundBar = new SKRoundRect(
                rect
                , cornerRadius
                , cornerRadius))
            {
                using (SKPaint paint = new SKPaint())
                {
                    paint.IsAntialias = true;
                    paint.Shader = SKShader.CreateLinearGradient(
                        new SKPoint(rect.Left, rect.Top),
                        new SKPoint(rect.Right, rect.Bottom),
                        backgroundColors,
                        backgroundColorOffsets,
                        SKShaderTileMode.Clamp);

                    // paint background
                    canvas.DrawRoundRect(backgroundBar, paint);

                    // shadow
                    if (shadowBlurX > 0 && shadowBlurY > 0)
                    {
                        paint.ImageFilter = SKImageFilter.CreateDropShadow(
                            shadowOffsetX,
                            shadowOffsetY,
                            shadowBlurX,
                            shadowBlurY,
                            shadowColor,
                            SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);
                        canvas.DrawRoundRect(backgroundBar, paint);
                    }

                    // inner shadow
                    if (innerBlurShadowThickness > 0)
                    {
                        canvas.ClipRoundRect(backgroundBar, SKClipOperation.Intersect, true);

                        using (var innerShadowBorder = new SKRoundRect(
                            innerShadowRect
                            , cornerRadius - borderThickness
                            , cornerRadius - borderThickness))
                        {
                            using (SKPaint paint1 = new SKPaint
                            {
                                Color = innerBlurShadowColor,
                                Style = SKPaintStyle.Stroke,
                                StrokeWidth = innerBlurShadowThickness,
                                ImageFilter = SKImageFilter.CreateBlur(innerBlurShadowBlurX, innerBlurShadowBlurY)
                            })
                            {
                                canvas.DrawRoundRect(innerShadowBorder, paint1);
                            }
                        }
                    }

                    // border
                    if (borderThickness > 0)
                    {
                        using (var border = new SKRoundRect(
                            borderRect
                            , cornerRadius - borderPadding
                            , cornerRadius - borderPadding))
                        {

                            paint.Style = SKPaintStyle.Stroke;
                            paint.StrokeWidth = borderThickness;
                            paint.ImageFilter = null;
                            paint.Shader = SKShader.CreateLinearGradient(
                                new SKPoint(rect.Left, rect.Top),
                                new SKPoint(rect.Right, rect.Bottom),
                                borderColors,
                                borderColorOffsets,
                                SKShaderTileMode.Clamp);

                            canvas.DrawRoundRect(border, paint);
                        }
                    }
                }
            }

            return rect;
        }




        void DrawProgressBar(SKCanvas canvas, float cornerRadius, float width)
        {
            var progressBar = new SKRoundRect(
                new SKRect(0, 0, width, CanvasSize.Height)
                , cornerRadius
                , cornerRadius);

            using (var paint = new SKPaint() { IsAntialias = true })
            {
                var rect = new SKRect(0, 0, width, CanvasSize.Height);

                paint.Shader = SKShader.CreateLinearGradient(
                    start: new SKPoint(rect.Left, rect.Top),
                    end: new SKPoint(rect.Right, rect.Top),
                    colors: new[]
                        {
                            GradientStartColor.ToSKColor(),
                            GradientEndColor.ToSKColor()
                        },
                    colorPos: new float[] { 0, 1 },
                    mode: SKShaderTileMode.Clamp);

                canvas.DrawRoundRect(progressBar, paint);
            }
        }


        void DrawProgressText(SKCanvas canvas, float width, float fontSize)
        {
            var str = Progress.ToString("0%");

            var textPaint = new SKPaint
            {
                Color = TextColor.ToSKColor(),
                TextSize = fontSize
            };

            var textBounds = new SKRect();
            textPaint.MeasureText(str, ref textBounds);

            var xText = width / 2 - textBounds.MidX;
            if (xText < 0)
            {
                xText = CanvasSize.Width / 2 - textBounds.MidX;
                textPaint.Color = AlternativeTextColor.ToSKColor();
            }

            var yText = CanvasSize.Height / 2 - textBounds.MidY;

            canvas.DrawText(str, xText, yText, textPaint);
        }
    }
}
