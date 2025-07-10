using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GStore.Data;
using GStore.Models;
using GStore.Models.ViewModels;
using System.Drawing.Imaging;
using GStore.Repositories;
using GStore.Repositories.Interfaces;
using GStore.Utils.Constants;

namespace GStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorSetsController : Controller
    {
        private readonly IColorSetRepo colorSetRepository;

        public ColorSetsController(IColorSetRepo colorSetRepository)
        {
            this.colorSetRepository = colorSetRepository;
        }

        // GET: Admin/ColorSets
        public async Task<IActionResult> Index()
        {

            IEnumerable<ColorSetVM> colorSetsVM = await colorSetRepository.GetAllVmsNoTracking(ColorSet.ColorSetToVmSelector);

            return View(colorSetsVM);
        }

        // GET: Admin/ColorSets/Create
        public IActionResult Create()
        {
            ColorSetVM colorSetVM = new ColorSetVM();
            
            return View(colorSetVM);
        }

        // POST: Admin/ColorSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorCode,Name")] ColorSetVM colorSetVM)
        {
            if (ModelState.IsValid == false) return View(colorSetVM);

            ColorSet colorSet = ColorSet.MapVmToEntity(colorSetVM);

            bool resultFromCreate = await colorSetRepository
                .SetColorAndAllColorRelatedTables(colorSet);

            if (resultFromCreate == true)

                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("",
                             VarietyValues.ErrorOnCreation);

                return View(colorSetVM);
        }

        // GET: Admin/ColorSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            ColorSet colorSet = await colorSetRepository.GetEntityByIdAsNoTraking(id.Value, x => x.Id == id);

            if (colorSet == null) return NotFound();

            ColorSetVM colorSetVM = ColorSet.MapEntityToVm(colorSet);

            return View(colorSetVM);
        }

        // POST: Admin/ColorSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ColorCode,Name,IsActive")] ColorSetVM colorSetVM)
        {
            if ((id == null) || (id != colorSetVM.Id)) return NotFound();

            if (ModelState.IsValid == false) return View(colorSetVM);

                ColorSet colorSet = await colorSetRepository.GetEntityByIdAsync(id.Value);

                if (colorSet == null) return NotFound();

                ColorSet.MapEntityForEdit(colorSet, colorSetVM);

                bool resultFromEdit = await colorSetRepository.UpdateAsync(colorSet);

                if (resultFromEdit == true)

                    return RedirectToAction(nameof(Index));

                return View(colorSetVM);
        }

        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null) return NotFound();

            ColorSet colorSet = await colorSetRepository
                .GetEntityByIdAsNoTraking(id.Value, x => x.Id == id);

            if (colorSet == null) return NotFound();

            ColorSetVM colorSetVM = ColorSet.MapEntityToVm(colorSet);

            ColorStutusChangeVM statusChangeVM =
                ColorSet.SetStatusChange(colorSetVM);

            return View(statusChangeVM);
        }

        // POST: Admin/SizeSets/Delete/5
        [HttpPost, ActionName("ChangeStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int? id, ColorStutusChangeVM statusChangeVM)
        {
            if ((id == null) || (id != statusChangeVM.colorSetVM.Id)) return NotFound();

            ColorSet colorSet = await colorSetRepository.GetEntityByIdAsync(id.Value);

            if (colorSet == null) return NotFound();

            ColorSet.ToggleActivityStatus(colorSet);

            bool resultFromChangeStatus = await colorSetRepository
                .UpdateColorAndShirtAvailabilityColors(colorSet);

            if (resultFromChangeStatus == true)

                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("",
                 VarietyValues.ErrorOnUpdate);
            return View(colorSet);
        }
    }
}
