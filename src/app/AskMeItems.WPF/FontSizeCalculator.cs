using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace AskMeItems.WPF
{
    public class FontSizeCalculator
    {
        public static double GetFontWidth(string text, CultureInfo cultureInfo, FontFamily fontFamily, double fontSize,
                                   FlowDirection leftToRight)
        {
            return
                new FormattedText(text,
                                  cultureInfo,
                                  leftToRight,
                                  new Typeface(fontFamily.ToString()),
                                  fontSize,
                                  Brushes.Black)
                    .Width;
        }

        public static double GetFontWidth(string text, FontFamily fontFamily, double fontSize)
        {
            return GetFontWidth(text, CultureInfo.CurrentUICulture, fontFamily, fontSize, FlowDirection.LeftToRight);
        }
    }
}