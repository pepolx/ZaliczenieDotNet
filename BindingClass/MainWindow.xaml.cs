using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace BindingKlasCSharp
{

    public partial class MainWindow : Window
    {
        public ObservableCollection<Movie> movies { get; set; } = new ObservableCollection<Movie>();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void AddMovie(object sender, RoutedEventArgs e)
        {
            Movie movie = new Movie();
            MovieWindow movieWindow = new MovieWindow(this, movie, true);
            movieWindow.Show();
        }

        private void EditMovie(object sender, RoutedEventArgs e)
        {
            Movie movie = MoviesList.SelectedItem as Movie;
            MovieWindow movieWindow = new MovieWindow(this, movie, false);
            movieWindow.Show();
        }

        private void DeleteMovie(object sender, RoutedEventArgs e)
        {
            Movie movie = MoviesList.SelectedItem as Movie;
            movies.Remove(movie);
        }

        private void ImportMovies(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Movie>));
            OpenFileDialog openFileDialog = new OpenFileDialog { DefaultExt = ".xml", Filter = "XML files (*.xml)|*.xml" };

            if (openFileDialog.ShowDialog() == true)
            {
                FileStream file = new FileStream(openFileDialog.FileName, FileMode.Open);
                ObservableCollection<Movie> movies_ = xmlSerializer.Deserialize(file) as ObservableCollection<Movie>;
                foreach (Movie movie_ in movies_)
                {
                    movies.Add(movie_);
                }
                file.Close();
            }
        }

        private void ExportMovies(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Movie>));
            SaveFileDialog saveFileDialog = new SaveFileDialog { DefaultExt = ".xml", Filter = "XML files (*.xml)|*.xml" };

            if (saveFileDialog.ShowDialog() == true)
            {
                TextWriter file = new StreamWriter(saveFileDialog.FileName);
                xmlSerializer.Serialize(file, movies);
                file.Close();
            }
        }
    }
}
