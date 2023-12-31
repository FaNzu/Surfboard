﻿using SurfBlazorLibrary.Product.Models;

namespace SurfBlazorLibrary.ShoppingCart.Models
{
    /// <summary>
    /// Stores a shopping cart.
    /// </summary>
    public class ShoppingCartModel
    {
        /// <summary>
        /// A list of all the items stored in the shopping cart.
        /// </summary>
        public IList<ShoppingCartItemModel> Items { get; }

        /// <summary>
        /// Constructs a new shopping cart.
        /// </summary>
        public ShoppingCartModel()
        {
            Items = new List<ShoppingCartItemModel>();
        }
    }
}
