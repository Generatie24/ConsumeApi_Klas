using ConsumeApi_Klas.Models;
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

        private async void GetReservations_Click(object sender, RoutedEventArgs e)
        {
            var reservations = await _apiService.GetReservationsAsync();
            ReservationsDataGrid.ItemsSource = reservations;
        }

        private void EditReservation_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsDataGrid.SelectedItem is ReservationDetailsDto selectedReservation)
            {
                EditReservationWindow editReservationWindow = new EditReservationWindow(selectedReservation.ReservationId);
                editReservationWindow.ShowDialog();
                GetReservations_Click(null,null);
            }   
        }

        private void DeleteReservation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}