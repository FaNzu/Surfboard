﻿using System.ComponentModel.DataAnnotations;

namespace SurfboardApi.Models.ViewModels
{
    public class BookingRequestVM
    {
        public BookingRequestVM() { }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? UserId { get; set; }

        public int BoardId { get; set; }

    }
}
