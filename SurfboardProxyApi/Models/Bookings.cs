using Microsoft.AspNetCore.Identity;
using SurfboardApi.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SurfboardApi.Models
{
    public class Bookings
    {
        public Bookings() { }
        public Bookings(DateTime _startDate, DateTime _endDate, string _userId, int _boardId)
        {
            StartDate = _startDate;
            EndDate = _endDate;
            UserId = _userId;
            BoardId = _boardId;
            BookingsId = 0;
        }
        public Bookings(DateTime _startDate, DateTime _endDate, int _boardId)
        {
            StartDate = _startDate;
            EndDate = _endDate;
            BoardId = _boardId;
            BookingsId = 0;
        }

        public int BookingsId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? UserId { get; set; }
        public int BoardId { get; set; }

    }
}
