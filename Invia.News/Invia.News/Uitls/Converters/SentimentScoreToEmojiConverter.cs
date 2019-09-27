using System;
using System.Globalization;
using Xamarin.Forms;

namespace Invia.News.Uitls.Converters
{
    /// <summary>
    /// Converts double Sentiment Scores to Emoji string values.
    /// No Convert-Back supported.
    /// </summary>
    public class SentimentScoreToEmojiConverter : IValueConverter
    {
        /// <summary>
        /// Converts double value to string value.
        /// </summary>
        /// <param name="value">Sentiment value as double</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Parameter (ignored)</param>
        /// <param name="culture">Culture (ignored)</param>
        /// <returns>Emoji string representation of the score value./returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Cast value to useable score.
            var score = (double)value;

            // Ensure score is between 0 and 1.
            if(score < 0 || score > 1)
            {
                return "";
            }

            // Return emoji according to the score value.
            if (score > 0.89)
            {
                return "😀";
            }
            if (score > 0.74)
            {
                return "🙂";
            }
            if (score > 0.29)
            {
                return "😐";
            }

            return "😟";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
