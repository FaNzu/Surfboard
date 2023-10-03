using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SurfboardApi.Models
{
    public class Bookings
    {
        public int BookingsId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? UserId { get; set; }
        public int BoardId { get; set; }

    }
}
