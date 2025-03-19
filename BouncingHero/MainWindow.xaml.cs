using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using NLog;

namespace BouncingHero
{
    public partial class MainWindow : Window
    {
        private Image hero;
        private double xSpeed = 5, ySpeed = 5;
        private double xPos = 0, yPos = 0;
        private Random random = new Random();
        private double screenWidth, screenHeight;
        private RotateTransform aTransform;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                InitializeFullScreen();
                CreateHero();
                //InitializeSpeed();
                CompositionTarget.Rendering += UpdateHero;
            }
            catch (Exception ex)
            {
                // Log the exception with the error level
                logger.Error(ex, "An error occurred while performing an operation.");
                throw;
            }
        }

        private void InitializeFullScreen()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            Topmost = true;
            AllowsTransparency = true;
            Background = Brushes.Transparent;

            screenWidth = SystemParameters.PrimaryScreenWidth;
            screenHeight = SystemParameters.PrimaryScreenHeight;

            aTransform = new RotateTransform(90, 0, 0);
        }

        private void CreateHero()
        {
            hero = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resources/hero.png")),
                RenderTransformOrigin = new Point(0.5, 0.5),
                //Opacity = 1.0,
            };
            hero.Width = 200;
            hero.Height = 200;

            aTransform = new RotateTransform(0);

            hero.RenderTransform = aTransform;
            MainCanvas.Children.Add(hero);
        }

        private void InitializeSpeed()
        {
            xSpeed = random.NextDouble() * 4 + 2;
            ySpeed = random.NextDouble() * 4 + 2;
            if (random.Next(2) == 0) xSpeed *= -1;
            if (random.Next(2) == 0) ySpeed *= -1;
        }

        private void UpdateHero(object sender, EventArgs e)
        {
            xPos += xSpeed;
            yPos += ySpeed;

            if (xPos <= 0 - hero.Width || xPos >= screenWidth)
            {
                xSpeed *= -1;
                xPos = Math.Max(0 - hero.Width, Math.Min(xPos, screenWidth));
            }

            if (yPos <= 0 - hero.Height || yPos >= screenHeight)
            {
                ySpeed *= -1;
                yPos = Math.Max(0 - hero.Width, Math.Min(yPos, screenHeight));
            }

            if(xSpeed < 0)
            {
                if (ySpeed < 0)
                    Rotate(180);
                else
                    Rotate(90);
            }
            else
            {
                if(ySpeed < 0)
                    Rotate(270);
                else
                    Rotate(0);
            }
            Canvas.SetLeft(hero, xPos);
            Canvas.SetTop(hero, yPos);
        }

        private void Rotate(int degree)
        {
            int xscale = (int)(xSpeed / Math.Abs(xSpeed));
            int yscale = (int)(ySpeed / Math.Abs(ySpeed));
            int rdegree = ( xscale * yscale ) * xscale == -1 ? 270 : 0;
            aTransform = new RotateTransform(rdegree);
            //if (xscale * yscale == -1)
            //{
            //    int temp = xscale;
            //    xscale = yscale;
            //    yscale = temp;
            //}
            ScaleTransform scaleTransform = new ScaleTransform(xscale, 1);
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(aTransform);
            transformGroup.Children.Add(scaleTransform);
            hero.RenderTransform = transformGroup;
        }

        private void RotateX()
        {
            DoubleAnimation rotationAnimation = new DoubleAnimation
            {
                By = -270, // Rotate by 180 degrees
                Duration = TimeSpan.FromSeconds(0),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            aTransform.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
        }

        private void RotateY()
        {
            DoubleAnimation rotationAnimation = new DoubleAnimation
            {
                By = -90, // Rotate by 180 degrees
                Duration = TimeSpan.FromSeconds(0),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            aTransform.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
        }

        private void LaunchAnimation()
        {
            ScaleTransform scale = new ScaleTransform();
            hero.RenderTransform = scale;

            DoubleAnimation scaleAnimation = new DoubleAnimation(1, 1.5, TimeSpan.FromMilliseconds(200))
            {
                AutoReverse = true
            };
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }

        private void FlyAnimation()
        {
            RotateTransform rotate = new RotateTransform();
            hero.RenderTransform = rotate;

            DoubleAnimation rotateAnimation = new DoubleAnimation(0, 360, TimeSpan.FromMilliseconds(500));
            rotate.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }
    }
}