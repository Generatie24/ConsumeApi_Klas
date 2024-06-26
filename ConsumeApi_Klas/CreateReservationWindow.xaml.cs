﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ConsumeApi_Klas.Services;
using ConsumeApi_Klas.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics.Eventing.Reader;

namespace ConsumeApi_Klas
{
    /// <summary>
    /// Interaction logic for CreateReservationWindow.xaml
    /// </summary>
    public partial class CreateReservationWindow : Window
    {
        private readonly ApiService _apiService;
        public CreateReservationWindow()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadCustomers();

        }

        private async void LoadCustomers()
        {
            var customers = await _apiService.GetCustomersAsync();
            CustomerComboBox.ItemsSource = customers;
            CustomerComboBox.DisplayMemberPath = "Name";
            CustomerComboBox.SelectedValuePath = "CustomerId";
        }

        private async void CreateReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerComboBox.SelectedValue is int customerId &&
                int.TryParse(TableIdTextBox.Text, out int tableId) &&
                ReservationDatePicker.SelectedDate is DateTime selectedDate &&
                SpecialRequestsComboBox.SelectedItem is ComboBoxItem selectedItem &&
                Enum.TryParse(selectedItem.Content.ToString(), out SpecialRequests selectedSpecialRequest))
            {
                var reservation = new CreateReservationDto
                {
                    CustomerId = customerId,
                    TableId = tableId,
                    DateTime = selectedDate,
                    SpecialRequests = selectedSpecialRequest
                };
                try
                {
                    var createdReservation = await _apiService.CreateReservationAsync(reservation);
                    MessageBox.Show($"Reservation created with id {createdReservation.ReservationId}");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Please fill in all fields");
            }
        }
    }
}
