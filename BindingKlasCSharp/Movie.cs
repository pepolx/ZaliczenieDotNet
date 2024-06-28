using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingKlasCSharp
{
    public class Movie : INotifyPropertyChanged
    {

        private string title = "";
        private string director = "";
        private string studio = "";
        private string media ="";
        private TimeSpan duration = TimeSpan.Parse("00:00:00");
        private DateTime released = DateTime.Today;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string Director
        {
            get
            {
                return director;
            }
            set
            {
                director = value;
            }
        }
        public string Studio
        {
            get
            {
                return studio;
            }
            set
            {
                studio = value;
            }
        }
        public string Media
        {
            get
            {
                return media;
            }
            set
            {
                media = value;
            }
        }
        public TimeSpan Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        public DateTime Released
        {
            get
            {
                return released;
            }
            set
            {
                released = value;
            }
        }

        public string PrintAttributes()
            {
            return $"{this.director} {this.title}";
            }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new("Title"));
            PropertyChanged?.Invoke(this, new("Director"));
            PropertyChanged?.Invoke(this, new("Studio"));
            PropertyChanged?.Invoke(this, new("Duration"));
            PropertyChanged?.Invoke(this, new("Released"));
        }
    }
}
