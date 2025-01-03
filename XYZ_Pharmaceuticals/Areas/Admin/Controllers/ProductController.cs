﻿using Microsoft.AspNetCore.Mvc;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly string _imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // Index - List all products
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(c => c.Category)
                .ToList();
            ViewBag.Pro = _context.Products
                .Include(c => c.Category)
                .Count(c => c.Category.CategoryName == "Tablet");
            ViewBag.Pro1 = _context.Products
                .Include(c => c.Category)
                .Count(c => c.Category.CategoryName == "Liquid Filling");
            ViewBag.Pro2 = _context.Products
                .Include(c => c.Category)
                .Count(c => c.Category.CategoryName == "Capsule/Encapsulation");
            return View(products);
        }

        // Create - Show create product form (GET)
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "ID", "CategoryName");
            return View();
        }

        // Create (POST) - Create a new product
        [HttpPost]
        public IActionResult Create(Product product, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Product lacks information !!!" });
            }

            // Handle file upload (image)
            if (file != null && file.Length > 0)
            {
                if (!Directory.Exists(_imageUploadPath))
                {
                    Directory.CreateDirectory(_imageUploadPath);
                }

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(_imageUploadPath, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                product.Image = $"/uploads/{newFileName}";
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return Json(new { success = true, message = "Product created successfully !!!" });
        }

        // Edit - Show edit form for an existing product (GET)
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "ID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // Edit (POST) - Update the product information
        [HttpPost]
        public IActionResult Edit(int id, Product updatedProduct, IFormFile? file)
        {
            if (id != updatedProduct.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Product lacks information !!!" });
            }

            var product = _context.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            // Handle image update if a new image is uploaded
            if (file != null && file.Length > 0)
            {
                if (!Directory.Exists(_imageUploadPath))
                {
                    Directory.CreateDirectory(_imageUploadPath);
                }

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(_imageUploadPath, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                product.Image = $"/uploads/{newFileName}";
            }

            // Update product properties
            product.ProductName = updatedProduct.ProductName;
            product.Output = updatedProduct.Output;
            product.CapsuleSize = updatedProduct.CapsuleSize;
            product.MachineDimension = updatedProduct.MachineDimension;
            product.ShippingWeight = updatedProduct.ShippingWeight;
            product.ModelNumber = updatedProduct.ModelNumber;
            product.Dies = updatedProduct.Dies;
            product.MaxPressure = updatedProduct.MaxPressure;
            product.MaxDiameter = updatedProduct.MaxDiameter;
            product.MaxDepth = updatedProduct.MaxDepth;
            product.CategoryID = updatedProduct.CategoryID;
            product.ProductionCapacity = updatedProduct.ProductionCapacity;
            product.AirPressure = updatedProduct.AirPressure;
            product.AirVolume = updatedProduct.AirVolume;
            product.FillingSpeed = updatedProduct.FillingSpeed;
            product.FillingRange = updatedProduct.FillingRange;
            product.FillingType = updatedProduct.FillingType;

            _context.SaveChanges();

            return Json(new { success = true, message = "Product updated successfully !!!" });
        }

        // Delete - Delete a product
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Json(new { success = true, message = "Product deleted successfully !!!" });
        }

        // Details - Show details of a product
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
