using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BouncingHero
{
    public class HeroFigure : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry.Parse("M151.6,97.2v-3.6l-16.9-25c-5.2-7.2-11.3-10.5-19.7-10.5H87.5c-8.5,0-14.5,3.6-19.7,10.5l-16.9,25v3.6 c-34.3,42.3-50,78.6-50,125.7l6,0.4c0-0.4,1.6-8,14.9-7.2c8.5,0.4,13.3,4,17.7,7.2c4,2.8,7.6,5.6,12.9,5.6c3.2,0,7.2-1.2,12.5-4.4 c4-2.4,8-4,11.7-5.2h0.2v18.5c0,6.3,5,11.3,11.3,11.3s11.3-5,11.3-11.3v-18.5h4.6v18.5c0,6.3,5,11.3,11.3,11.3s11.3-5,11.3-11.3 v-18.3c3.4,0.9,7.1,2.4,10.9,5c4.8,3.2,8.9,4.4,12.5,4.4c5.2,0,8.9-2.8,12.9-5.6c4.4-3.2,9.7-6.8,17.7-7.2 c13.3-0.8,14.9,6.8,14.9,7.2l6-0.4C201.5,175.8,185.5,139.5,151.6,97.2z");
            }
        }
    }
}