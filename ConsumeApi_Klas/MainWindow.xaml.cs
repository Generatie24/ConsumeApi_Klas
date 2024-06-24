using ConsumeApi_Klas.Services;
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

namespace ConsumeApi_Klas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService;
        public MainWindow()
        {

            InitializeComponent();
            _apiService = new ApiService();
            LoadReservations();
        }

        private async void LoadReservations()
        {
            var reservations = await _apiService.GetReservationsAsync();   
            ReservationsDataGrid.ItemsSource = reservations;
        }

        private void CreateReservation_Click(object sender, RoutedEventArgs e)
        {
            CreateReservationWindow createReservationWindow = new CreateReservationWindow();
            createReservationWindow.ShowDialog();
            LoadReservations();
        }

        private void GetReservations_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditReservation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteReservation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}