using System.Collections.Generic;
using SimpleVookStore.Models;
namespace SimpleVookStore.Models
{
    public class ProductListViewModel
    {
        public List<Products> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
