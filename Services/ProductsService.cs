using System;
using System.Collections.Generic;
using System.Linq;
using dvcsharp_core_api.Abstractions;
using dvcsharp_core_api.Data;
using dvcsharp_core_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dvcsharp_core_api.Services;

public class ProductsService : IProductsService
{
    private readonly GenericDataContext _context;
    
    public ProductsService(GenericDataContext context)
    {
        _context = context;
    }

    public void AddProduct(Product product)
    {
        var existingProduct = _context.Products.
            FirstOrDefault(b => (b.name == product.name) || (b.skuId == product.skuId));
         
        if(existingProduct != null) {
            throw new Exception("Product name or skuId is already taken");
        }

        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public Product[] GetProducts()
    {
        return _context.Products.ToArray();
    }
    
    public List<Product> GetProducts(string keyword)
    {
        
        var query = $"SELECT * From Products WHERE name LIKE '%{keyword}%' OR description LIKE '%{keyword}%'";
        var products = _context.Products
            .FromSqlRaw(query)
            .ToList();
        return products;
    }
    
    public List<Product> GetProducts2(string keyword)
    {
        var products = _context.Products
            .FromSql($"SELECT * From Products WHERE name LIKE '%{keyword}%' OR description LIKE '%{keyword}%'")
            .ToList();
        return products;
    }
}