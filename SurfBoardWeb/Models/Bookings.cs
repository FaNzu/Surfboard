using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SurfBoardWeb.Models
{
    public class Bookings
    {
        public int Id { get; set; }

        [Display(Name = "Booking starting date")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Booking ending date")]
        public DateTime EndDate { get; set; }
        
        public string? UserId { get; set; }

        public DefaultUser User { get; set; }

        public string UserName { get; set; }
        
        public int SurfboardId { get; set; }
    }
}
