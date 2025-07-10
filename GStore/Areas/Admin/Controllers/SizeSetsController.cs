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
using System.Linq;
using System.Linq.Expressions;
using GStore.Repositories.Interfaces;
using GStore.Utils.Constants;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
//using GStore.Models.ExtensionMappers;

namespace GStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeSetsController : Controller
    {
        private readonly ISizeSetRepo sizeSetRepository;

        public SizeSetsController(ISizeSetRepo sizeSetRepository)
        {
            this.sizeSetRepository = sizeSetRepository;
        }

        // GET: Admin/SizeSets
        public async Task<IActionResult> Index()
        {
            IEnumerable<SizeSetVM> sizeSetsVM = await sizeSetRepository.GetAllVmsNoTracking(SizeSet.SizeSetToVmSelector);

            return View(sizeSetsVM);
        }

        // GET: Admin/SizeSets/Create
        public IActionResult Create()
        {
            SizeSetVM sizeSetVM = new SizeSetVM();

            return View(sizeSetVM);
        }

        // POST: Admin/SizeSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] SizeSetVM sizeSetVM)
        {
            if (ModelState.IsValid == false) return View(sizeSetVM);

            SizeSet sizeSet = SizeSet.MapVmToEntity(sizeSetVM);

            bool resultFromCreate = await sizeSetRepository
                .SetSizeAndAllSizeRelatedTables(sizeSet);

            if (resultFromCreate == true)

                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("",
                 VarietyValues.ErrorOnCreation);
            return View(sizeSetVM);
        }

        // GET: Admin/SizeSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            SizeSet sizeSet = await sizeSetRepository.GetEntityByIdAsNoTraking(id.Value, x => x.Id== id);

            if (sizeSet == null) return BadRequest();

            SizeSetVM sizeSetVM = SizeSet.MapEntityToVm(sizeSet);

            return View(sizeSetVM);
        }

        // POST: Admin/SizeSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] SizeSetVM sizeSetVM)
        {
            if ((id == null) || (id.Value != sizeSetVM.Id)) return NotFound();

            if (ModelState.IsValid == false) return View(sizeSetVM);

            SizeSet sizeSet = await sizeSetRepository.GetEntityByIdAsync(id.Value);

            if (sizeSet == null) return BadRequest();

            SizeSet.MapEntityForEdit(sizeSet, sizeSetVM);

            bool resultFromEdit = await sizeSetRepository.UpdateAsync(sizeSet);

            if (resultFromEdit == true)

                return RedirectToAction(nameof(Index));

            return View(sizeSetVM);
        }

        // GET: Admin/SizeSets/Delete/5
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null) return NotFound();

            SizeSet sizeSet = await sizeSetRepository.GetEntityByIdAsNoTraking(id.Value, x => x.Id == id);

            if (sizeSet == null) return NotFound();

            SizeSetVM sizeSetVM = SizeSet.MapEntityToVm(sizeSet);

            SizeStutusChangeVM statusChangeVM =
                SizeSet.SetStatusChange(sizeSetVM);

            return View(statusChangeVM);
        }

        // POST: Admin/SizeSets/Delete/5
        [HttpPost, ActionName("ChangeStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int? id, SizeStutusChangeVM statusChangeVM)
        {
            if ((id == null) || (id != statusChangeVM.sizeSetVM.Id)) return NotFound();

            SizeSet sizeSet = await sizeSetRepository.GetEntityByIdAsync(id.Value);

            if (sizeSet == null) return BadRequest();

            SizeSet.ToggleActivityStatus(sizeSet);

            bool resultFromChangeStatus = await sizeSetRepository
                .UpdateSizeAndShirtAvailabilitySizes(sizeSet);

            if (resultFromChangeStatus == true)

                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("",
                 VarietyValues.ErrorOnUpdate);
            return View(sizeSet);
        }
    }
}
