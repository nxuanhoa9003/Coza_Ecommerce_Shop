using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Models.Common;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        public INotyfService _notifyService { get; }

        public ProductsController(AppDbContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        [HttpPost]
        [Route("/GetValuesAttribute")]
        public IActionResult GetValuesAttribute([FromForm] int attributeId)
        {
            var listvalue = _context.AttributeValues.Where(x => x.AttributeId == attributeId)
                 .Select(x => new
                 {
                     x.Id,
                     x.Value
                 })
                .ToList();

            return Json(new { success = true, data = listvalue });

        }


        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(p => p.ProductCategory).OrderByDescending(x => x.Id);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Title");

            ViewData["Attributes"] = new SelectList(_context.Attributes, "Id", "AttributeName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductCode,Title,Slug,Description,Detail,Image,Files,Price,PriceSale,Quantity,IsSale,IsFeature,IsHot,ProductCategoryId,SeoTitile,SeoDescription,SeoKeywords,IsActive,CreateBy,CreateDate,ModifierDate,ModifiedBy")] Product product, List<ProductVariant> variants)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    product.IsActive = true;
                    product.CreateDate = DateTime.Now;
                    product.ModifierDate = DateTime.Now;
                    product.Slug = FilterChar.GenerateSlug(product.Title);
                    product.ProductCategory = _context.ProductCategories.Find(product.ProductCategoryId);
                    _context.Add(product);
                    await _context.SaveChangesAsync();

                    List<ProductVariant> addedVariants = new List<ProductVariant>();

                    if (variants != null && variants.Count > 0)
                    {
                        foreach (var vr in variants)
                        {
                            ProductVariant pv = new ProductVariant
                            {
                                ProductId = product.Id,
                                SKU = vr.SKU,
                                Quantity = vr.Quantity,
                                AdditionalPrice = vr.AdditionalPrice,
                                CreateDate = DateTime.Now,
                                ModifierDate = DateTime.Now,
                                IsActive = true,
                                ProductVariantAttributes = new List<ProductVariantAttribute>() // Khởi tạo danh sách
                                
                            };

                            if (vr.ProductVariantAttributes != null && vr.ProductVariantAttributes.Any())
                            {
                                foreach (var item in vr.ProductVariantAttributes)
                                {
                                    if (int.TryParse(item.Value, out int attributeValueId))
                                    {
                                        var attributeValue = _context.AttributeValues.FirstOrDefault(x => x.Id == attributeValueId);
                                        if (attributeValue != null)
                                        {
                                            // Thêm thuộc tính biến sản phẩm với giá trị thực tế
                                            var pva = new ProductVariantAttribute
                                            {
                                                ProductVariantId = pv.Id, // ID sẽ được cập nhật sau khi lưu pv
                                                Value = attributeValue.Value,
                                                AttributeId = item.AttributeId,
                                                CreateDate = DateTime.Now,
                                                ModifierDate = DateTime.Now
                                            };
                                            pv.ProductVariantAttributes.Add(pva); // Thêm vào danh sách
                                        }
                                    }
                                }
                            }

                            _context.Add(pv);
                        }

                        // Lưu tất cả biến sản phẩm và thuộc tính của chúng
                        await _context.SaveChangesAsync();
                    }


                    if (product.Files != null && product.Files.Count > 0)
                    {
                        foreach (var file in product.Files)
                        {
                            if (file.Length > 0)
                            {
                                ProductImage productImage = new ProductImage();
                                productImage.ProductId = product.Id;
                                productImage.Image = await Utilities.UploadFileAsync(file, "Products");

                                if (product.Image.Equals(file.FileName))
                                {
                                    product.Image = productImage.Image;
                                    productImage.IsDefault = true;
                                }
                                else
                                {
                                    productImage.IsDefault = false;
                                }
                                _context.Add(productImage);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Thêm sản phẩm thành công");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            else
            {
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        string propertyName = entry.Key; // Tên thuộc tính
                        var errors = entry.Value.Errors.Select(e => e.ErrorMessage).ToList(); // Danh sách lỗi

                        Console.WriteLine($"Thuộc tính {propertyName} có lỗi: {string.Join(", ", errors)}");
                    }
                }
            }


            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Title", product.ProductCategoryId);
            ViewData["Attributes"] = new SelectList(_context.Attributes, "Id", "AttributeName");

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products.FindAsync(id);
            var product = await _context.Products.Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Title", product.ProductCategoryId);
            ViewData["Attributes"] = new SelectList(_context.Attributes, "Id", "AttributeName");

            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]                                                 
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductCode,Title,Slug,Description,Detail,Image,Files,Price,PriceSale,Quantity,IsHome,IsSale,IsFeature,IsHot,ProductCategoryId,SeoTitile,SeoDescription,SeoKeywords,isActive,CreateBy,CreateDate,ModifierDate,ModifiedBy")] Product product, List<ProductVariant> variants)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(product);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Title", product.ProductCategoryId);
            ViewData["Attributes"] = new SelectList(_context.Attributes, "Id", "AttributeName");
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
