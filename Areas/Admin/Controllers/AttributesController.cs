using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Coza_Ecommerce_Shop.Data;
using Coza_Ecommerce_Shop.Models.Entities;
using Newtonsoft.Json.Linq;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttributesController : Controller
    {
        private readonly AppDbContext _context;

        public AttributesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Attributes
        public async Task<IActionResult> Index()
        {
            var attributes = await _context.Attributes.Include(x => x.AttributeValues).ToListAsync();
            return View(attributes);
        }


        // GET: Admin/Attributes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Attributes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AttributeName")] Attributes attributes, List<AttributeValue> attributeValues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attributes);
                await _context.SaveChangesAsync();

                foreach (var item in attributeValues)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                    {
                        item.AttributeId = attributes.Id; // Gán ID thuộc tính cho giá trị thuộc tính
                        _context.AttributeValues.Add(item);
                    };

                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(attributes);
        }

        // GET: Admin/Attributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributes = await _context.Attributes.Include(x => x.AttributeValues).FirstOrDefaultAsync(x => x.Id == id);
            if (attributes == null)
            {
                return NotFound();
            }
            return View(attributes);
        }

        // POST: Admin/Attributes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AttributeName")] Attributes attributes, List<AttributeValue> attributeValues)
        {
            if (id != attributes.Id)
            {
                return NotFound(); // đang làm ở đây
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributes);
                    await _context.SaveChangesAsync();

                    foreach (var item in attributeValues)
                    {
                        if (item.Id == 0)
                        {
                            if (!string.IsNullOrWhiteSpace(item.Value))
                            {
                                item.AttributeId = attributes.Id;
                                _context.AttributeValues.Add(item);
                            }
                        }
                        else
                        {
                            var atvl = _context.AttributeValues.FirstOrDefault(x => x.Id == item.Id);
                            if (atvl == null)
                            {
                                if (!string.IsNullOrWhiteSpace(item.Value))
                                {
                                    item.AttributeId = attributes.Id; // Gán ID thuộc tính cho giá trị thuộc tính
                                    _context.AttributeValues.Add(item);
                                };
                            }
                            else
                            {
                                if (item.IsDeleted) // Kiểm tra xem có đánh dấu xóa không
                                {
                                    _context.AttributeValues.Remove(atvl); // Xóa thuộc tính
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(item.Value))
                                    {
                                        // Cập nhật giá trị thuộc tính
                                        atvl.Value = item.Value; // Cập nhật giá trị
                                        _context.Update(atvl); // Cập nhật đối tượng trong ngữ cảnh
                                    }
                                }
                            }
                        }

                    }
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributesExists(attributes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attributes);
        }

        // GET: Admin/Attributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributes = await _context.Attributes.Include(x => x.AttributeValues)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attributes == null)
            {
                return NotFound();
            }

            return View(attributes);
        }

        // POST: Admin/Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attributes = await _context.Attributes.FindAsync(id);
            if (attributes != null)
            {
                var attributesvalues = attributes.AttributeValues;
                if (attributesvalues != null && attributesvalues.Any())
                {
                    foreach (var item in attributesvalues)
                    {
                        _context.AttributeValues.Remove(item);
                    }
                    await _context.SaveChangesAsync();
                }
                _context.Attributes.Remove(attributes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributesExists(int id)
        {
            return _context.Attributes.Any(e => e.Id == id);
        }
    }
}
