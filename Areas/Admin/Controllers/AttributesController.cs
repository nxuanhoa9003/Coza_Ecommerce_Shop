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
using AspNetCoreHero.ToastNotification.Abstractions;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.Repositories.Implementations;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttributesController : Controller
    {
        private readonly IAttributesRepository _attributesRepository;
        private readonly IAttributesValuesRepository _attributesValuesRepository;
        public INotyfService _notifyService { get; }
        public AttributesController(IAttributesRepository attributesRepository, IAttributesValuesRepository attributesValuesRepository, INotyfService notifyService)
        {
            _attributesRepository = attributesRepository;
            _attributesValuesRepository = attributesValuesRepository;
            _notifyService = notifyService;
        }

        // GET: Admin/Attributes
        public async Task<IActionResult> Index()
        {
            var attributes = await _attributesRepository.GetAllAsync();
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
                await _attributesRepository.AddAsync(attributes);
                var validAttributeValues = attributeValues.Where(item => !string.IsNullOrWhiteSpace(item.Value)).ToList();

                foreach (var item in validAttributeValues)
                {
                    item.AttributeId = attributes.Id;
                }
                if (validAttributeValues.Any())
                {
                    await _attributesValuesRepository.AddRangeAsync(validAttributeValues);
                }
                _notifyService.Success("Thêm thuộc tính thành công");
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

            var attributes = await _attributesRepository.GetByIdAsync(id);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _attributesRepository.UpdateAsync(attributes);

                    var newAttributeValues = attributeValues.Where(item => item.Id == 0 && !string.IsNullOrWhiteSpace(item.Value)).ToList();
                    var existingAttributeValues = attributeValues.Where(item => item.Id != 0).ToList();

                    if (newAttributeValues.Any())
                    {
                        foreach (var item in newAttributeValues)
                        {
                            item.AttributeId = attributes.Id;
                        }
                        await _attributesValuesRepository.AddRangeAsync(newAttributeValues);
                    }

                    var existingAttributeValueIds = existingAttributeValues.Select(item => item.Id).ToList();
                    var attributeValuesToUpdate = await _attributesValuesRepository.GetListByIdsAsync(existingAttributeValueIds);

                    var attributeValuesToUpdateDict = attributeValuesToUpdate.ToDictionary(x => x.Id);

                    foreach (var item in existingAttributeValues)
                    {
                        var atvl = attributeValuesToUpdateDict.ContainsKey(item.Id) ? attributeValuesToUpdateDict[item.Id] : null;

                        if (atvl == null)
                        {
                            if (!string.IsNullOrWhiteSpace(item.Value))
                            {
                                item.AttributeId = attributes.Id;
                                await _attributesValuesRepository.AddAsync(item);
                            }
                        }
                        else
                        {
                            if (item.IsDeleted)
                            {
                                await _attributesValuesRepository.RemoveAsync(atvl);
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(item.Value))
                                {
                                    atvl.Value = item.Value;
                                    await _attributesValuesRepository.UpdateAsync(atvl);
                                }
                            }
                        }
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    var AttributesExists = await _attributesRepository.GetByIdAsync(attributes.Id);
                    if (AttributesExists is null)
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

            var attributes = await _attributesRepository.GetByIdAsync(id);
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
            
            var attributes = await _attributesRepository.GetByIdAsync(id);
            if (attributes != null)
            {
                var attributesvalues = attributes.AttributeValues;
                if (attributesvalues != null && attributesvalues.Any())
                { 
                   await _attributesValuesRepository.RemoveRangeAsync(attributesvalues);
                }
                await _attributesRepository.RemoveAsync(attributes);
            }
            return RedirectToAction(nameof(Index));
        }

     
    }
}
