using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SurfBoardWeb.Models
{
    public class Bookings
    {
        public int Id { get; set; }

        [Display(Name = "Bookings starting date")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Bookings ending date")]
        public DateTime EndDate { get; set; }
        
        public string? UserId { get; set; }

        public DefaultUser User { get; set; }

        public string UserName { get; set; }
        
        public int SurfboardId { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
