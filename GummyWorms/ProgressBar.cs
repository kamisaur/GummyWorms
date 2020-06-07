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

        public static BindableProperty BarBackgroundColorProperty = BindableProperty.Create(nameof(BarBackgroundColor), typeof(Color),
            typeof(ProgressBar), Color.White, BindingMode.OneWay,
            validateValue: (_, value) => value != null, propertyChanged: OnPropertyChangedInvalidate);

        public Color BarBackgroundColor
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

            canvas.Clear();


            DrawProgressBackground(canvas, cornerRadius);
            DrawProgressBar(canvas, cornerRadius, percentageWidth);

            if (HasText)
            {
                DrawProgressText(canvas, percentageWidth, scale);
            }
        }


        private void DrawProgressBackground(SKCanvas canvas, float cornerRadius)
        {
            var backgroundBar = new SKRoundRect(
                //new SKRect(10, 10, CanvasSize.Width - 10, CanvasSize.Height - 10)
                new SKRect(0, 0, CanvasSize.Width, CanvasSize.Height)
                , cornerRadius
                , cornerRadius);


            using (SKPaint background = new SKPaint())
            {
                background.Color = BarBackgroundColor.ToSKColor();
                background.IsAntialias = true;

                canvas.DrawRoundRect(backgroundBar, background);
            }

            //background.ImageFilter = SKImageFilter.CreateDropShadow(
            //        0,
            //        0,
            //        10,
            //        10,
            //        SKColors.Red,
            //        SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);



            //var RectangleStyleFillShadow = SKImageFilter.CreateDropShadow(0f, 0f, 20f, 20f, Color.Black.ToSKColor(), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground, null, null);

            //var RectangleStyleFillPaint = new SKPaint()
            //{
            //    Style = SKPaintStyle.Fill,
            //    Color = Color.White.ToSKColor(),
            //    BlendMode = SKBlendMode.SrcOver,
            //    IsAntialias = true,
            //    ImageFilter = RectangleStyleFillShadow
            //};

            //canvas.DrawRect(new SKRect(20f, 20f, CanvasSize.Width - 20, CanvasSize.Height - 20), RectangleStyleFillPaint);


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


        private void DrawProgressText(SKCanvas canvas, float width, float scale)
        {
            var str = Progress.ToString("0%");

            var textPaint = new SKPaint
            {
                Color = TextColor.ToSKColor(),
                TextSize = FontSize * scale
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
