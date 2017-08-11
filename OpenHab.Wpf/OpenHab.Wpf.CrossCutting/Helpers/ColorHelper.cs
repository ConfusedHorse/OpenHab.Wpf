using System;
using System.Windows.Media;

namespace OpenHab.Wpf.CrossCutting.Helpers
{
    public static class ColorHelper
    {
        public static Color HsbCsvToRgb(string hsbCsv)
        {
            var values = hsbCsv?.Split(',') ?? new[] { "0", "0", "0" };
            var h = Convert.ToInt32(values[0]);
            var s = Convert.ToInt32(values[1]);
            var b = Convert.ToInt32(values[2]);
            return HsbToRgb(h, s, b);
        }

        public static Color HsbToRgb(int hue, int saturation, int brightness)
        {
            return HsbToRgb(hue / 360f, saturation / 255f, brightness / 255f);
        }

        public static Color HsbToRgb(float hue, float saturation, float brightness)
        {
            int r = 0, g = 0, b = 0;
            if (Math.Abs(saturation) < 0.1)
            {
                r = g = b = (int) (brightness * 255.0f + 0.5f);
            }
            else
            {
                var h = (hue - (float) Math.Floor(hue)) * 6.0f;
                var f = h - (float) Math.Floor(h);
                var p = brightness * (1.0f - saturation);
                var q = brightness * (1.0f - saturation * f);
                var t = brightness * (1.0f - saturation * (1.0f - f));
                switch ((int) h)
                {
                    case 0:
                        r = (int) (brightness * 255.0f + 0.5f);
                        g = (int) (t * 255.0f + 0.5f);
                        b = (int) (p * 255.0f + 0.5f);
                        break;
                    case 1:
                        r = (int) (q * 255.0f + 0.5f);
                        g = (int) (brightness * 255.0f + 0.5f);
                        b = (int) (p * 255.0f + 0.5f);
                        break;
                    case 2:
                        r = (int) (p * 255.0f + 0.5f);
                        g = (int) (brightness * 255.0f + 0.5f);
                        b = (int) (t * 255.0f + 0.5f);
                        break;
                    case 3:
                        r = (int) (p * 255.0f + 0.5f);
                        g = (int) (q * 255.0f + 0.5f);
                        b = (int) (brightness * 255.0f + 0.5f);
                        break;
                    case 4:
                        r = (int) (t * 255.0f + 0.5f);
                        g = (int) (p * 255.0f + 0.5f);
                        b = (int) (brightness * 255.0f + 0.5f);
                        break;
                    case 5:
                        r = (int) (brightness * 255.0f + 0.5f);
                        g = (int) (p * 255.0f + 0.5f);
                        b = (int) (q * 255.0f + 0.5f);
                        break;
                }
            }
            return Color.FromArgb(Convert.ToByte(255), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
        }
    }
}