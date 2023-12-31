﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfBoardWeb.Models
{
    public class Board
    {
        
        public int BoardId { get; set; }

        [StringLength(60, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }

        [DisplayName("Length (feet)")]
        [Range(0, 100)]
        [Required]
        public double Length { get; set; }

        [DisplayName("Width (inchs)")]
        [Range(0, 100)]
        [Required]
        public double Width { get; set; }

        [DisplayName("Thickness (Inches)")]
        [Range(0, 100)]
        [Required]
        public double Thickness { get; set; }

        [DisplayName("Volume (L)")]
        [Range(0, 100)]
        [Required]
        public double Volume { get; set; }


        [Required]
        public string Type { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public double Price{ get; set; } //bør laves til decimal

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string? Equipment { get; set; }
        public string? PicturePath { get; set; }

        public bool IsBooked { get; set; }
    }
}
