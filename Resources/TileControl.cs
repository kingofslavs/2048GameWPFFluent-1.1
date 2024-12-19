using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FluentUIGame.Resources
{
    public class TileControl : UserControl
    {
        public int Value { get; set; }
        private TextBlock textBlock;

        public TileControl(int value)
        {
            Value = value;
            Width = 90;
            Height = 90;

            // Фон плитки
            var rectangle = new Rectangle
            {
                Width = 90,
                Height = 90,
                Fill = new SolidColorBrush(Colors.IndianRed),
                RadiusX = 10,
                RadiusY = 10
            };

            // Текст на плитке
            textBlock = new TextBlock
            {
                Text = Value.ToString(),
                FontSize = 24,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.Black)
            };

            // Контейнер для прямоугольника и текста
            var grid = new Grid();
            grid.Children.Add(rectangle);
            grid.Children.Add(textBlock);

            Content = grid;
        }

        public void UpdateValue(int newValue)
        {
            Value = newValue;
            textBlock.Text = Value.ToString();
        }

        public void MoveTo(double newX, double newY)
        {
            var leftAnimation = new DoubleAnimation
            {
                To = newX,
                Duration = TimeSpan.FromSeconds(0.25)
            };

            var topAnimation = new DoubleAnimation
            {
                To = newY,
                Duration = TimeSpan.FromSeconds(0.25)
            };

            BeginAnimation(Canvas.LeftProperty, leftAnimation);
            BeginAnimation(Canvas.TopProperty, topAnimation);
        }

        public void AnimateMerge()
        {
            var scaleTransform = new ScaleTransform(1.0, 1.0);
            RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            RenderTransform = scaleTransform;

            var scaleAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.2,
                Duration = TimeSpan.FromSeconds(0.3),
                AutoReverse = true
            };

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }
    }
}
