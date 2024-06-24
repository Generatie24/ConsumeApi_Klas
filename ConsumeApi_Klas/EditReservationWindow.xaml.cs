using ConsumeApi_Klas.Models;
using ConsumeApi_Klas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ConsumeApi_Klas
{
    /// <summary>
    /// Interaction logic for EditReservationWindow.xaml
    /// </summary>
    public partial class EditReservationWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly int _reservationId;
        public EditReservationWindow(int reservationId)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _reservationId = reservationId;
            LoadReservationDetails();
            LoadCustomers();
        }

        private async void LoadReservationDetails()
        {
            var reservation = await _apiService.GetReservationAsync(_reservationId);
            ReservationIdTextBox.Text = reservation.ReservationId.ToString();
            TableIdTextBox.Text = reservation.TableId.ToString();
            ReservationDatePicker.SelectedDate = reservation.DateTime;
            SpecialRequestsComboBox.SelectedItem = SpecialRequestsComboBox.Items.OfType<ComboBoxItem>()
                .FirstOrDefault(x => x.Content.ToString() == reservation.SpecialRequests.ToString());
        }

        private async void LoadCustomers()
        {
            var customers = await _apiService.GetCustomersAsync();
            CustomerComboBox.ItemsSource = customers;
            CustomerComboBox.DisplayMemberPath = "Name";
            CustomerComboBox.SelectedValuePath = "CustomerId";
            var reservation = await _apiService.GetReservationAsync(_reservationId);
            CustomerComboBox.SelectedValue = reservation.CustomerId;
        }
        private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerComboBox.SelectedValue is int customerId &&
                int.TryParse(TableIdTextBox.Text, out int tableId) &&
                ReservationDatePicker.SelectedDate is DateTime selectedDate &&
                SpecialRequestsComboBox.SelectedItem is ComboBoxItem selectedItem &&
                Enum.TryParse(selectedItem.Content.ToString(), out SpecialRequests selectedSpecialRequest))
            {
                var reservation = new UpdateReservationDto
                {
                    CustomerId = customerId,
                    TableId = tableId,
                    DateTime = selectedDate,
                    SpecialRequests = selectedSpecialRequest
                };
                try
                {
                    await _apiService.UpdateReservationAsync(_reservationId, reservation);
                    MessageBox.Show($"Reservation with id  Updated Successfully");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
