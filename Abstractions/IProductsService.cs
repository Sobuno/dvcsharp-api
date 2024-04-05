using System.Collections.Generic;
using dvcsharp_core_api.Models;

namespace dvcsharp_core_api.Abstractions;

public interface IProductsService
{
    void AddProduct(Product product);
    Product[] GetProducts();
    List<Product> GetProducts(string keyword);
    List<Product> GetProducts2(string keyword);
}