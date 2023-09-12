using System.ComponentModel.DataAnnotations;

namespace SurfBoardWeb.Models
{
    public class BoardType
    {
        [Key]
        public string Name { get; set; }
    }
}
