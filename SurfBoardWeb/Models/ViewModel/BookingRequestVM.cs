using System.ComponentModel.DataAnnotations;

namespace SurfBoardWeb.Models.ViewModels
{
    public class BookingRequestVM
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? UserId { get; set; }

        public int BoardId { get; set; }

        public string? email { get; set; }
        public string? phoneNumber { get; set; }
    }
}
