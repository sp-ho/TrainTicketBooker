using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicketTest.Models;
using Newtonsoft.Json;
/*using System.Text.Json.Serialization;*/

namespace TicketTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
        }

        private void tbFrom_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        private void tbTo_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            // API endpoint for searching tickets based on date
            string apiUrl = "https://localhost:7203/api/Ticket/";


            using (HttpClient client = new HttpClient())
            {
                // Set the request headers (if required)
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Capture the input values
                string from = tbFrom.Text;
                string to = tbTo.Text;
                string category = ((ComboBoxItem)cbCategory.SelectedItem).Content.ToString();
                DateTime date = dpDate.SelectedDate ?? DateTime.Now;

                DateSearchCriteria searchCriteria = new DateSearchCriteria
                {
                    Date = date
                };

                try
                {
                    string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(searchCriteria);
                    var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("SearchTicketsByDate", httpContent);
                    response.EnsureSuccessStatusCode(); // test

                    string responseContent = await response.Content.ReadAsStringAsync();

                    Response res = JsonConvert.DeserializeObject<Response>(responseContent);
                    Result.ItemsSource = res.TicketList; // Display the results in the Result data grid
                    
                    // Close the current Search window
                    /*this.Close();*/
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"An error occurred while sending the request: {ex.Message}");
                }
            }

        }

        private void btnBookTicket_Click(object sender, RoutedEventArgs e)
        {
            // Check if a ticket is selected
            if (Result.SelectedItem is Ticket selectedTicket)
            {
                // Create a new instance of the Payment window
                Payment paymentWindow = new Payment(selectedTicket);

                // Show the Payment window as a dialog (blocking)
                paymentWindow.ShowDialog();

            }
            else
            {
                // Inform the user to select a ticket before booking
                MessageBox.Show("Please select a ticket from the list before booking.", "Ticket Booking", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
