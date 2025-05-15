using SimpleVookStore.Models;

namespace SimpleVookStore.Services.Interface
{
    public interface IProductService
    {
        List<Products> GetAllProducts();
        List<Products> GetPaginatedProducts(int pageNumber, int pageSize);
        int GetTotalProductCount();
    }
}
