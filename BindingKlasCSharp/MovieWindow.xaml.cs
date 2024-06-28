using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BindingKlasCSharp
{
    public partial class MovieWindow : Window
    {
        private MainWindow _mainWindow;
        private Movie _movie;
        private bool _createNew;
        public MovieWindow(MainWindow mainWindow, Movie movie, bool createNew)
        {
            InitializeComponent();
            DataContext = movie;
            _mainWindow = mainWindow;
            _movie = movie;
            _createNew = createNew;
        }

        public void Submit(object sender, RoutedEventArgs e)
        {
            string title = Title.Text;
            string director = Director.Text;
            string studio = Studio.Text;
            string media = Media.Text;
            TimeSpan duration = TimeSpan.Parse(Duration.Text);
            DateTime? released = Released.SelectedDate;

            _movie.Title = title;
            _movie.Director = director;
            _movie.Studio = studio;
            _movie.Media = media;
            _movie.Duration = duration;
            _movie.Released = released.Value;
               
            if (_createNew)
            {
                _mainWindow.movies.Add(_movie);
            }
            _movie.OnPropertyChanged();
            Close();
        }
    }
}
