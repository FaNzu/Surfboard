using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SurfBoardWeb.Models
{
    public class Bookings
    {
        public int BookingsId { get; set; }

        [Display(Name = "Bookings starting date")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Bookings ending date")]
        public DateTime EndDate { get; set; }
        
        public string? UserId { get; set; }
        
        public int BoardId { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
