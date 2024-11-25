using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTest.Models
{
    public class Ticket
    {
        public int ticketId { get; set; }
        public string train { get; set; } // add new attribute
        public string departure { get; set; }
        public string destination { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
        public string category { get; set; }
        public string seat { get; set; }
        public decimal price { get; set; }

        public Ticket(int ticketId, string train, string departure, string destination, DateTime departureTime, DateTime arrivalTime, string category, string seat, decimal price)
        {
            this.ticketId = ticketId;
            this.train = train;
            this.departure = departure;
            this.destination = destination;
            this.departureTime = departureTime;
            this.arrivalTime = arrivalTime;
            this.category = category;
            this.seat = seat;
            this.price = price;
        }
    }
}
