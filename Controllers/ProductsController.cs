using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using dvcsharp_core_api.Abstractions;
using dvcsharp_core_api.Models;
using dvcsharp_core_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dvcsharp_core_api
{
   [Route("api/[controller]")]
   public class ProductsController : Controller
   {
      private readonly IProductsService _productsService;

      public ProductsController(IProductsService productsService)
      {
         _productsService = productsService;
      }

      [HttpGet]
      public IEnumerable<Product> Get()
      {
         return _productsService.GetProducts();
      }

      [HttpPost]
      public IActionResult Post([FromBody] Product product)
      {
         if(!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         try
         {
            _productsService.AddProduct(product);
         }
         catch (Exception e)
         {
            ModelState.AddModelError("name", e.Message);
            return BadRequest(ModelState);            
         }

         return Ok(product);
      }

      [HttpGet("export")]
      public void Export()
      {
         XmlRootAttribute root = new XmlRootAttribute("Entities");
         XmlSerializer serializer = new XmlSerializer(typeof(Product[]), root);

         Response.ContentType = "application/xml";
         serializer.Serialize(HttpContext.Response.Body, _productsService.GetProducts());
      }

      [HttpGet("search")]
      public IActionResult Search(string keyword)
      {
         if (String.IsNullOrEmpty(keyword)) {
            return Ok("Cannot search without a keyword");
         }

         var products = _productsService.GetProducts(keyword);
         return Ok(products);
      }
      
      [HttpGet("search2")]
      public IActionResult Search2(string keyword)
      {
         if (String.IsNullOrEmpty(keyword)) {
            return Ok("Cannot search without a keyword");
         }

         var products = _productsService.GetProducts2(keyword);

         return Ok(products);
      }

      [HttpPost("import")]
      public IActionResult Import()
      {
         XmlReader reader = XmlReader.Create(HttpContext.Request.Body);
         XmlRootAttribute root = new XmlRootAttribute("Entities");
         XmlSerializer serializer = new XmlSerializer(typeof(Product[]), root);

         var entities = (Product[]) serializer.Deserialize(reader);
         reader.Close();

         return Ok(entities);
      }
   }
}