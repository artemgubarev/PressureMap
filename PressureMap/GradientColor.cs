using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressureMap
{
    internal class GradientColor
    {
        private static Color InterpolateColor(Color startColor, Color endColor, float ratio)
        {
            int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
            int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
            int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);
            return Color.FromArgb(r, g, b);
        }

        internal static Color[] GetColors()
        {
            Color[] rainbowColors = new Color[]
            {
                Color.Red,    // Красный
                Color.Orange, // Оранжевый
                Color.Yellow, // Желтый
                Color.Green,  // Зеленый
                Color.Blue,   // Синий
                Color.DarkMagenta  // Фиолетовый
            };
            
            int totalColors = 24;
            Color[] gradientColors = new Color[totalColors];
            int colorsBetween = totalColors / (rainbowColors.Length - 1);
            
            int index = 0;
            for (int i = 0; i < rainbowColors.Length - 1; i++)
            {
                Color startColor = rainbowColors[i];
                Color endColor = rainbowColors[i + 1];

                for (int j = 0; j < colorsBetween; j++, index++)
                {
                    float ratio = (float)j / colorsBetween;
                    gradientColors[index] = InterpolateColor(startColor, endColor, ratio);
                }
            }
            gradientColors[totalColors - 1] = rainbowColors[rainbowColors.Length - 1];

            return gradientColors;
        }
    }
}
