using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VirtualNvhAnalyzer.Core.Models
{
    public class AbstractBaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
