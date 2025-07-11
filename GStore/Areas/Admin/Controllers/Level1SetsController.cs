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
using GStore.Repositories.Interfaces;
using GStore.Repositories;
using GStore.ModelsHelper;


namespace GStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Level1SetsController : Controller
    {
        private readonly IGenericRepository<Level1Set> l1SetRepository;

        public Level1SetsController(IGenericRepository<Level1Set> l1SetRepository)
        {
            this.l1SetRepository = l1SetRepository;
        }

        // GET: Admin/Level1Set
        public async Task<IActionResult> Index()
        {
            IEnumerable<L1SetVM> l1SetsVM = await l1SetRepository.GetAllVmsNoTracking(Level1SetHelper.LevelSetToVmSelector); ;

            return View(l1SetsVM);
        }

        // GET: Admin/Level1Set/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Level1Set/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive")] L1SetVM l1SetVM)
        {
            if (ModelState.IsValid == false) return View();

            Level1Set l1Set = Level1SetHelper.MapVmToEntity(l1SetVM);

            bool resultFromCreate = await l1SetRepository.AddAsync(l1Set);

            if (resultFromCreate == true)

                return RedirectToAction(nameof(Index));

            return View(l1SetVM);
        }

        // GET: Admin/Level1Set/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Level1Set l1Set = await l1SetRepository.GetEntityByIdAsNoTraking(id.Value, x => x.Id == id); ;

            if (l1Set == null) return NotFound();

            L1SetVM l1SetVM = Level1SetHelper.MapEntityToVm(l1Set);

            return View(l1SetVM);
        }

        // POST: Admin/Level1Set/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,IsActive")] L1SetVM l1SetVM)
        {
            if ((id == null) || (id.Value != l1SetVM.Id)) return NotFound();

            if (ModelState.IsValid == false) return View(l1SetVM);

            Level1Set l1Set = await l1SetRepository.GetEntityByIdAsync(id.Value);

            if (l1Set == null) return NotFound();

            Level1SetHelper.MapEntityForEdit(l1Set, l1SetVM);

            bool resultFromEdit = await l1SetRepository.UpdateAsync(l1Set);

            if (resultFromEdit == true)
                return RedirectToAction(nameof(Index));

            return View(l1SetVM);
        }

        // GET: Admin/Level1Set/Delete/5
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null) return NotFound();

            Level1Set l1Set = await l1SetRepository
                .GetEntityByIdAsNoTraking(id.Value, x => x.Id == id);

            if (l1Set == null) return NotFound();

            L1SetVM l1SetVM = Level1SetHelper.MapEntityToVm(l1Set);

            L1SetStutusChangeVM statusChangeVM =
                Level1SetHelper.SetStatusChange(l1SetVM);

            return View(statusChangeVM);
        }

        // POST: Admin/Level1Set/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int? id, L1SetStutusChangeVM statusChangeVM)
        {
            if ((id == null) || (id != statusChangeVM.l1SetVM.Id)) return NotFound();

            Level1Set l1Set = await l1SetRepository.GetEntityByIdAsync(id.Value);

            Level1SetHelper.ToggleActivityStatus(l1Set);

            bool resultFromChangeStatus =
                await l1SetRepository.UpdateAsync(l1Set);

            if (resultFromChangeStatus == false) return View(statusChangeVM);

            return RedirectToAction(nameof(Index));
        }
    }
}
