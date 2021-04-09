using System.ComponentModel;

namespace FlightSimulator.Models
{
    class MediaPlayerModel : IMediaPlayerModel
    {
        private bool isPlay;
        private double speed;
        private double timer;
        private bool userIsDraggingSlide;

        public MediaPlayerModel()
        {
            this.isPlay = false;
            this.speed = 1;
            this.timer = 0;
            this.userIsDraggingSlide = false;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool IsPlay
        {
            get { return isPlay; }
            set
            {
                isPlay = value;
                NotifyPropertyChanged("IsPlay");
            }
        }

        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                NotifyPropertyChanged("Speed");
            }
        }

        public double Timer
        {
            get { return timer; }
            set
            {
                timer = value;
                NotifyPropertyChanged("Timer");
            }
        }

        public bool UserIsDraggingSlider
        {
            get { return userIsDraggingSlide; }
            set
            {
                userIsDraggingSlide = value;
                NotifyPropertyChanged("UserIsDraggingSlider");
            }

        }
    }
}
