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

        public string Name { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        public double Volume { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; } //bør laves til decimal

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
