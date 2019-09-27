using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Invia.News.ViewModels
{
    /// <summary>
    /// Base of other view model.
    /// Provides a heper to trigger property changed events.
    ///
    /// This class is inspired by others.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occures on the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
