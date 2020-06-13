using System;
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



        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
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
            var cornerRadius = CornerRadius * scale;

            var percentageWidth = (int)Math.Floor(CanvasSize.Width * Progress);
            var fontSize = FontSize * scale;

            canvas.Clear();


            DrawRectangle(
                canvas
                , CanvasSize.Width
                , CanvasSize.Height
                , cornerRadius
                , BackgroundBarColor.ToSKColor()
                , 2
                , SKColors.Black
                , 0
                , 0
                , 10
                , 10);

            DrawProgressBar(canvas, cornerRadius, percentageWidth);

            if (HasText)
            {
                DrawProgressText(canvas, percentageWidth, fontSize);
            }
        }


        /// <summary>
        /// Draw rectangle with shadows and border
        /// </summary>
        private void DrawRectangle(
            SKCanvas canvas
            , float width
            , float height
            , float cornerRadius
            , SKColor backgroundColor
            , float borderThickness
            , SKColor borderColor
            , float shadowOffsetX
            , float shadowOffsetY
            , float shadowBlurX
            , float shadowBlurY
            )
        {
            // give some padding for shadow so it's not cut out by boundaries
            var additonalPadding = 4;

            // so that the rect is not kissing the border
            var initialPadding = 1;

            // border is drawn in rect in the way that half of the border is out of rect and half is in
            var borderPadding = borderThickness / 2;


            // paddings are added based on shadow blur, so the shadow wouldn't get out of the boudnaries
            var rect = new SKRect
            {
                Left = shadowOffsetX > 0
                ? initialPadding
                : Math.Abs(shadowOffsetX) + shadowBlurX + additonalPadding,

                Right = shadowOffsetX < 0
                ? width - initialPadding
                : width - shadowOffsetX - shadowBlurX - additonalPadding,

                Top = shadowOffsetY > 0
                ? initialPadding + borderPadding
                : Math.Abs(shadowOffsetY) + shadowBlurY + additonalPadding,

                Bottom = shadowOffsetY < 0
                ? height - initialPadding
                : height - shadowOffsetY - shadowBlurY - additonalPadding
            };

            var backgroundBar = new SKRoundRect(
                rect
                , cornerRadius
                , cornerRadius);

            using (SKPaint paint = new SKPaint())
            {
                paint.Color = backgroundColor;
                paint.IsAntialias = true;

                // paint background
                canvas.DrawRoundRect(backgroundBar, paint);

                // shadow
                paint.ImageFilter = SKImageFilter.CreateDropShadow(
                    shadowOffsetX,
                    shadowOffsetY,
                    shadowBlurX,
                    shadowBlurY,
                    SKColors.Green,
                    SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);
                canvas.DrawRoundRect(backgroundBar, paint);



                // border
                var borderRect = new SKRect();
                borderRect.Left = rect.Left + borderPadding;
                borderRect.Right = rect.Right - borderPadding;
                borderRect.Top = rect.Top + borderPadding;
                borderRect.Bottom = rect.Bottom - borderPadding;

                var border = new SKRoundRect(
                    borderRect
                    , cornerRadius - borderPadding
                    , cornerRadius - borderPadding);

                paint.Style = SKPaintStyle.Stroke;
                paint.Color = borderColor;
                paint.StrokeWidth = borderThickness;
                paint.ImageFilter = null;
                canvas.DrawRoundRect(border, paint);
            }
        }


        private void DrawProgressBar(SKCanvas canvas, float cornerRadius, float width)
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


        private void DrawProgressText(SKCanvas canvas, float width, float fontSize)
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
