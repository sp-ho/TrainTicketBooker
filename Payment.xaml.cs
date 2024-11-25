using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        public Ticket selectedTicket;
        public Payment(Ticket ticket)
        {
            InitializeComponent();
            // Store the selected ticket received from the Search window
            string trainId = "Ticket ID: " + ticket.ticketId;
            string trainName = "\nTrain: " + ticket.train;
            string departure = "From: " + ticket.departure;
            string destination = "\nTo: " + ticket.destination;
            string departureTime = "\n"+ ticket.departureTime;
            string arrivalTime = "\n" + ticket.arrivalTime;
            string category = "\n" + ticket.category;
            string seat = "\nSeat: " + ticket.seat; 
            string price = "$" + ticket.price;

            lblIdTrainName.Content = trainId + trainName + category + seat;
            lblToFrom.Content = departure + departureTime + destination + arrivalTime;
            lbTotalPrice.Content = price;

            selectedTicket = new Ticket(ticket.ticketId, ticket.train, ticket.departure, ticket.destination, ticket.departureTime,
                                        ticket.arrivalTime, ticket.category, ticket.seat, ticket.price);
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            
            ETicket eticket = new ETicket(selectedTicket);
            eticket.Show();
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
