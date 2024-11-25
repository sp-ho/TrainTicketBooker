using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TicketTest.Models;

namespace TicketTest
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
            showData();
        }

        public async void showData()
        {
            // API endpoint for searching tickets based on date
            string apiUrl = "https://localhost:7203/api/Ticket/";


            using (HttpClient client = new HttpClient())
            {
                // Set the request headers (if required)
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = await client.GetAsync("GetAllTickets");
                    response.EnsureSuccessStatusCode(); // test

                    string responseContent = await response.Content.ReadAsStringAsync();

                    Response res = JsonConvert.DeserializeObject<Response>(responseContent);
                    TicketList.ItemsSource = res.TicketList; // Display the results in the Result data grid

                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"An error occurred while sending the request: {ex.Message}");
                }
            }

        }
    }
}
