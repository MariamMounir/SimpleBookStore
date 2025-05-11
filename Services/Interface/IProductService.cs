using SimpleVookStore.Models;

namespace SimpleVookStore.Services.Interface
{
    public interface IProductService
    {
        List<Products> GetAllProducts();
    }
}
