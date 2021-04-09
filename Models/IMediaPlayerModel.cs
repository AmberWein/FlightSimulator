using System.ComponentModel;

namespace FlightSimulator.Models
{
    public interface IMediaPlayerModel : INotifyPropertyChanged
    {
        bool IsPlay { set; get; }
        double Speed { set; get; }
        double Timer { set; get; }
        bool UserIsDraggingSlider { set; get; }
    }
}
