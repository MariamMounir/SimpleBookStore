using SimpleVookStore.Models;
using SimpleVookStore.Repo;
using SimpleVookStore.Services.Interface;

namespace SimpleVookStore.Services.Implementaion
{
    public class ProductService: IProductService
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
    }
}
