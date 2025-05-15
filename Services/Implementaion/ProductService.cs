using SimpleVookStore.Models;
using SimpleVookStore.Repo;
using SimpleVookStore.Services.Interface;

public class ProductService : IProductService
{
    private readonly GenericRepo<Products> _productRepo;

    public ProductService(GenericRepo<Products> productRepo)
    {
        _productRepo = productRepo;
    }

    public List<Products> GetAllProducts()
    {
        return _productRepo.GetAll();
    }

    public List<Products> GetPaginatedProducts(int pageNumber, int pageSize)
    {
        return _productRepo.GetAll()
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
    }

    public int GetTotalProductCount()
    {
        return _productRepo.GetAll().Count;
    }
}
