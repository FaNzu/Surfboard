﻿using System.ComponentModel.DataAnnotations;

namespace SurfBoardWeb.Models
{
    public class Board
    {
        
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 10)]
        [Required]
        public string Name { get; set; }

        [Range(100, 600)]
        [Required]
        public double Length { get; set; }

        [Range(100, 600)]
        [Required]
        public double Width { get; set; }

        [Range(100, 600)]
        [Required]
        public double Thickness { get; set; }

        [Range(100, 600)]
        [Required]
        public double Volume { get; set; }


        [Required]
        public string Type { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public double Price{ get; set; }




        public string Equipment { get; set; }
        public string PicturePath { get; set; }
    }
}
