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
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using GStore.Repositories.Interfaces;
using NuGet.ContentModel;

namespace GStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Level2SetsController : Controller
    {
        private readonly ILevel2CategoryRepo l2SetRepository;

        public Level2SetsController(ILevel2CategoryRepo l2SetRepository)
        {
            this.l2SetRepository = l2SetRepository;
        }

        // GET: Admin/Level2Setasync Task<>
        public async Task<IActionResult> Index()
        {
            List<L1SetVMForFullDisplay> allCategoriesCombined=
                await l2SetRepository.GetAllCategoriesCombined();
                        
            return View(allCategoriesCombined);
        }

        // GET: Admin/Level2Set/Create
        public async Task<IActionResult> Create()
        {
            L2SetVM l2SetVM = new L2SetVM();

            l2SetVM.L1Sets = await l2SetRepository.GetAllLevel1Categories();

            l2SetVM.SelectedRadioValue = null;

            return View(l2SetVM);
        }

        // POST: Admin/Level2Set/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(L2SetVM l2SetVM)
        {
            if (ModelState.IsValid == false)
            {
                l2SetVM.L1Sets = await l2SetRepository.GetAllLevel1Categories();
                l2SetVM.SelectedRadioValue = null;
                return View(l2SetVM);
            }

            Level2Set l2Set = Level2Set.MapVmToEntity(l2SetVM);

            bool resultFromCreate = await l2SetRepository.AddAsync(l2Set);

            if (resultFromCreate == true)

                return RedirectToAction(nameof(Index));

            return View(l2SetVM);
        }

        // GET: Admin/Level2Set/Edit/5 p => p.Id == parentId
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Level2Set l2SetAndl1Set = await l2SetRepository.GetL1SetAndL2SetNoTracking(id.Value);

            if (l2SetAndl1Set == null) return BadRequest();

            L2SetVM l2SetVM = Level2Set.MapEntityToVm(l2SetAndl1Set);

            l2SetVM.L1Sets = await l2SetRepository.GetAllLevel1Categories();

            return View(l2SetVM);
        }

        // POST: Admin/Level2Set/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, L2SetVM l2SetVM)
        {
            if ((id == null) || (id != l2SetVM.Id)) return NotFound();

            if (ModelState.IsValid == false)
            {
                l2SetVM.L1Sets = await l2SetRepository.GetAllLevel1Categories();
                return View(l2SetVM);
            }

            Level2Set l2Set = await l2SetRepository.GetEntityByIdAsync(id.Value);

            if (l2Set == null) return BadRequest();

            Level2Set.MapVMToEntityForEditPost(l2Set, l2SetVM);

            bool resultFromEdit = await l2SetRepository.UpdateAsync(l2Set);

            if (resultFromEdit == true)

                return RedirectToAction(nameof(Index));

                //l2SetVM.L1Sets = await l2SetRepository.GetAllLevel1Categories();

            return View(l2SetVM);
        }

        // GET: Admin/SizeSets/Delete/5
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null) return NotFound();

            Level2Set l2Set = await l2SetRepository.
                GetL1SetAndL2SetNoTracking(id.Value);

            if (l2Set == null) return NotFound();

            L2SetStutusChangeVM statusChangeVM =
                Level2Set.SetStatusChange(l2Set);

            return View(statusChangeVM);
        }

        // POST: Admin/SizeSets/Delete/5
        [HttpPost, ActionName("ChangeStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int? id, L2SetStutusChangeVM statusChangeVM)
        {
            if ((id == null) || (id != statusChangeVM.Id)) return NotFound();

            Level2Set l2Set = await l2SetRepository.GetEntityByIdAsync(id.Value);

            if (l2Set == null) return BadRequest();

            Level2Set.ToggleActivityStatus(l2Set);

            bool resultFromChangeStatus = await l2SetRepository.UpdateAsync(l2Set);

            if (resultFromChangeStatus == false) return View(statusChangeVM);

            return RedirectToAction(nameof(Index));
        }
    }
}
