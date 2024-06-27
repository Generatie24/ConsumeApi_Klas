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

        private async void DeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsDataGrid.SelectedItem is ReservationDetailsDto selectedReservation)
            {
                var result = MessageBox.Show("Are you sure you want to delete this reservation?", 
                    "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.DeleteReservationAsync(selectedReservation.ReservationId);
                        MessageBox.Show("Reservation deleted successfully.");
                        LoadReservations(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting reservation: {ex.Message}");
                        Console.WriteLine(ex.ToString()); 
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to delete.");
            }
        }
    }
}