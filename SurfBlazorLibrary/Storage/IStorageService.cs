using SurfBlazorLibrary.Product.Models;
using SurfBlazorLibrary.ShoppingCart.Models;

namespace SurfBlazorLibrary.Storage
{
    /// <summary>
    /// Stores the data used for the application.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Stores a list of products.
        /// </summary>
        IList<ProductModel> Products { get; }

        /// <summary>
        /// Stores the shopping cart.
        /// </summary>
        ShoppingCartModel ShoppingCart { get; }

    }
}
