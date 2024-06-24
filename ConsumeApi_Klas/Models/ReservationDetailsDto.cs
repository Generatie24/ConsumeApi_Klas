using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeApi_Klas.Models
{
    public class ReservationDetailsDto
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int TableId { get; set; }
        public DateTime DateTime { get; set; }
        public SpecialRequests SpecialRequests { get; set; }
    }
}
