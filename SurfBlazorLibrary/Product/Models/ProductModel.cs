using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SurfBlazorLibrary.Product.Models
{
    /// <summary>
    /// Stores a product.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public string Sku { get { return BoardId.ToString();
            } }

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
        public decimal Price { get; set; } //bør laves til decimal

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string? Equipment { get; set; }
        public string? PicturePath { get; set; }

        public bool IsBooked { get; set; }
        public string Slug
        {
            get
            {
                return Sku.ToLower();
            }
        }

        /// <summary>
        /// The full URL of the product
        /// </summary>
        public string FullUrl
        {
            get
            {
                return string.Format("/product/{0}", Slug);
            }
        }

        /// <summary>
        /// Constructs a new product.
        /// </summary>
        /// <param name="sku">Unique identifier of the product.</param>
        /// <param name="name">Name of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="image">Image path of the product.</param>
        public ProductModel(int boardid, string name, double length, double width, double thickness, double volume, string type, string equipment, decimal price, string picturePath)
        {
            BoardId = boardid;
            Name = name;
            Length = length;
            Width = width;
            Thickness = thickness;
            Volume = volume;
            Type = type;
            Equipment = equipment;
            Price = price;
            PicturePath = picturePath;
        }
    }
}
