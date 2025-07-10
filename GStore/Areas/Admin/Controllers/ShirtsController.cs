using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GStore.Data;
using GStore.Models;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using GStore.Utils.Constants;
using GStore.Utils.ImagesValues;
using Microsoft.Extensions.Hosting;
using GStore.Models.OtherModels;
using GStore.Utils.ImageDataHelper.Interface;
using System.Xml.Linq;
using GStore.Repositories;
using GStore.Models.DTOs;
using GStore.ModelsHelper;

namespace GStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShirtsController : Controller
    {
        private readonly IShirtRepo shirtRepository;
        private readonly ApplicationDbContext dbContext;

        public ShirtsController(IShirtRepo shirtRepo
            , ApplicationDbContext context)
        {
            this.shirtRepository = shirtRepo;
            dbContext = context;
            //this.hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public JsonResult ChangeActiveStatus(int? id)
        {
            ResponseDTO responseDTO = new ResponseDTO() { ResponseStatus = false, ResultValue = false };

            if (id == null) return Json(responseDTO);

            Shirt shirt = dbContext.Shirts.Where(shirt => shirt.Id == id).FirstOrDefault();

            shirt.IsActive = !shirt.IsActive;

            dbContext.SaveChanges();

            responseDTO.ResponseStatus = true;
            responseDTO.ResultValue = shirt.IsActive;

            return Json(responseDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShirtAvailability(int? id
            , ShirtAvailabilityComboVM shirtAvailabilityIncomming
            , [FromServices] IShirtAvailabalityRepo shirtAvailabalityRepo)
        {
            if ((id == null) || (id != shirtAvailabilityIncomming.ProductId)) return NotFound();   // BadRequest()

            ShirtAvailabilityComboVM shirtAvailabilityLocal = await shirtRepository
                    .GetShirtShortPrev_AvailabilityPerShirt(id.Value);

            if (ModelState.IsValid == false)
            {
                return View(shirtAvailabilityLocal);
            }

            bool boolResult = await shirtRepository.CheckSetAvailabilityInDb(id.Value
                , shirtAvailabilityIncomming.ListShirtAvailability
                , shirtAvailabalityRepo);

            if (boolResult == false)
            {
                ModelState.AddModelError("", VarietyValues.ErrorOnUpdate);
                return View(shirtAvailabilityLocal);
            }

            shirtAvailabilityLocal.SuccessOnUpdate = VarietyValues.SuccessOnUpdate;

            return View(shirtAvailabilityLocal);
        }

        public async Task<IActionResult> EditShirtAvailability(int? id)
        {
            if (id == null) return NotFound();

            ShirtAvailabilityComboVM shirtAvailabilityComboVM = await shirtRepository
                    .GetShirtShortPrev_AvailabilityPerShirt(id.Value);

            return View(shirtAvailabilityComboVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSizesPerShirt(int? id
            , SizesPerShirtComboVM sizesPerShirtComboIncomming
            , [FromServices] IShirtSizeSetRepo shirtSizeSetRepo)
        {
            if ((id == null) || (id != sizesPerShirtComboIncomming.ProductId))  return NotFound();

            SizesPerShirtComboVM sizesPerShirtComboLocal = await shirtRepository
                    .GetShirtShortPrev_SizesPerShirt(id.Value);

            if (ModelState.IsValid == false)
            {
                return View(sizesPerShirtComboLocal);
            }

            bool boolResult = await shirtRepository.CheckSetSizesInDb(id.Value
                , sizesPerShirtComboIncomming.ListSizesPerShirt
                , shirtSizeSetRepo);

            if (boolResult == false)
            {
                ModelState.AddModelError("", VarietyValues.ErrorOnUpdate);
                return View(sizesPerShirtComboLocal);
            }

            sizesPerShirtComboLocal.SuccessOnUpdate = VarietyValues.SuccessOnUpdate;

            return View(sizesPerShirtComboLocal);
        }

        public async Task<IActionResult> EditSizesPerShirt(int? id)
        {
            if (id == null) return NotFound();

            SizesPerShirtComboVM sizesPerShirtComboVM = await shirtRepository
                    .GetShirtShortPrev_SizesPerShirt(id.Value);

            return View(sizesPerShirtComboVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditColorsPerShirt(int? id
            , ColorsPerShirtComboVM colorsPerShirtComboIncomming
            , [FromServices] IShirtColorSetRepo shirtColorSetRepo)
        {
            if ((id == null) || (id != colorsPerShirtComboIncomming.ProductId)) return NotFound();

            ColorsPerShirtComboVM colorsPerShirtComboLocal = await shirtRepository
                            .GetShirtShortPrev_ColorsPerShirt(id.Value);

            if (ModelState.IsValid == false)
            {
                return View(colorsPerShirtComboLocal);
            }

            bool boolResult = await shirtRepository.CheckSetColorsInDb(id.Value
                , colorsPerShirtComboIncomming.ListColorsPerShirt
                , shirtColorSetRepo);

            if (boolResult == false) 
            { 
                ModelState.AddModelError("", VarietyValues.ErrorOnUpdate);
                return View(colorsPerShirtComboLocal);
            }

            colorsPerShirtComboLocal.SuccessOnUpdate = VarietyValues.SuccessOnUpdate;

            return View(colorsPerShirtComboLocal);
        }

        // GET: Admin/Shirts/EditColorsPerShirt(5)
        public async Task<IActionResult> EditColorsPerShirt(int? id)
        {
            if (id == null) return NotFound();

            ColorsPerShirtComboVM colorsPerShirtComboVM = await shirtRepository
                    .GetShirtShortPrev_ColorsPerShirt(id.Value);

            return View(colorsPerShirtComboVM);
        }


        // GET: Admin/Shirts/AddColorImages(5, imageManager)
        public async Task<IActionResult> AddColorImages(int? id, [FromServices] IImageManager imageManager)
        {
            if (id == null) return NotFound();

            ImagePerColorUploadComboVM imagePerColorUploadComboVM
                = new ImagePerColorUploadComboVM();

            imagePerColorUploadComboVM.shirtShortById = await shirtRepository
                    .GetShirtForPreviewById(id.Value);

            imagePerColorUploadComboVM.listImagePerColorUploadVM =
                     await shirtRepository.GetListShirtsColorsAndImagesForUpload(id.Value);

            imageManager.CheckSetColorImagesForPreview(imagePerColorUploadComboVM.listImagePerColorUploadVM);

            return View(imagePerColorUploadComboVM);
        }


        [HttpGet]
        //[Route("{id:int?}/{imageType:int?}")]
        public IActionResult UploadImage(int? id, int? imageType
            , int? colorId, [FromServices] IImageManager imageManager)
        {
            if ((id == null) || (imageType == null)) return NotFound();

            if (colorId == null)

                colorId = 0;

            UploadImageVM uploadImageObj = null;

            uploadImageObj = imageManager
                .GetSetImagePath(id.Value, imageType.Value, colorId.Value);

            return View("UploadImage", uploadImageObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadImage(int? id, int? imageType, int? colorId
            , UploadImageVM uploadImageVM
            , [FromServices] IShirtColorSetRepo shirtColorSetRepo
            , [FromServices] IImageManager imageManager
            , [FromServices] IWebHostEnvironment hostEnvironment)
        {

            if (ModelState.IsValid == false) return View("UploadImage", uploadImageVM);

            if (((id == null) || (id != uploadImageVM.ProductId)) ||
                ((imageType == null) || (imageType != uploadImageVM.ImageType)))
            {
                return NotFound();
            }

            InitialImgAssist initialImgAssist = imageManager
                .GetInitialBmpValidate(uploadImageVM.ImageToUpload);

            if (initialImgAssist.IsImageOk == false)
            {
                ModelState.AddModelError("",
                             initialImgAssist.ErrorMessage);

                uploadImageVM = imageManager
                    .GetSetImagePath(id.Value, imageType.Value, null);

                return View("UploadImage", uploadImageVM);
            }

            ImageAssistMain imageAssistMain = imageManager
                .SetAllForFolderAndDbSave(id.Value, imageType.Value, colorId.Value
                , initialImgAssist.ImageExtension);

            bool boolResult= false;

            if (imageAssistMain.IsNewImage == true)
            {

                boolResult = imageManager
                    .CheckForTableAndUpdateDb(
                    id.Value
                    , imageType.Value
                    , colorId.Value
                    , imageAssistMain.ImageDbName
                    , shirtColorSetRepo);

                if (boolResult == false)
                {
                    ModelState.AddModelError("",
                        ImageValues.ErrorImageNotAdded);

                    uploadImageVM = imageManager
                        .GetSetImagePath(id.Value, imageType.Value, null);

                    return View("UploadImage", uploadImageVM);
                }
            }

            foreach (var imageAssist in imageAssistMain.ImageAssistList)
            {
                if (imageAssistMain.IsNewImage == false)
                {
                    boolResult = imageManager
                        .DeleteOldImage(imageAssist
                        , imageAssistMain.ImageDbName
                        , hostEnvironment);
                }

                if (boolResult == false)
                {
                    break;
                }

                boolResult = imageManager
                .ImageResizeAndSave(initialImgAssist.InitialBmp
                , imageAssist
                , imageAssistMain.ImageDbName
                , hostEnvironment);

                if (boolResult == false)
                {
                    break;
                }
            }

            if (boolResult == false)
            {
                ModelState.AddModelError("",
                             ImageValues.ErrorOnImageSave);

                uploadImageVM = imageManager
                    .GetSetImagePath(id.Value, imageType.Value, null);

                return View("UploadImage", uploadImageVM);
            }

            return RedirectToRoute(new { controller = "Shirts", action = "AddColorImages", id = id.Value });

            //return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Shirts/AddMainImage(int? id)
        public async Task<IActionResult> AddMainImages(int? id)
        {
            if (id == null) return NotFound();

            ShirtShortWithCategoryNameVM shirtShortById = await shirtRepository
                    .GetShirtForPreviewById(id.Value);

            return View(shirtShortById);
        }

        // GET: Admin/Shirts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || dbContext.Shirts == null) return NotFound();

            var product = await dbContext.Shirts
                .Include(p => p.Level2Set)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Admin/Shirts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || dbContext.Shirts == null)
            {
                return NotFound();
            }

            var product = await dbContext.Shirts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(dbContext.Level2Sets, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Shirts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Name,Description,Price,Discount,Quantity,IsAvailable,IsActive,IsPromo,CategoryId")] Shirt product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(product);
                    await dbContext.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(dbContext.Level2Sets, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Shirts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || dbContext.Shirts == null)
            {
                return NotFound();
            }

            var product = await dbContext.Shirts
                .Include(p => p.Level2Set)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Shirts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (dbContext.Shirts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Shirts'  is null.");
            }
            var product = await dbContext.Shirts.FindAsync(id);
            if (product != null)
            {
                dbContext.Shirts.Remove(product);
            }
            
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (dbContext.Shirts?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // GET: Admin/Shirts
        public async Task<IActionResult> Index()
        {
            List<ShirtShortWithCategoryNameVM> shirtShort =
                await shirtRepository.GetAllShirtShortVersion(0, 22);

            return View(shirtShort);
        }

        // GET: Admin/Shirts/Create
        public async Task<IActionResult> Create()
        {
            ShirtVM shirtVM = new ShirtVM();

            shirtVM.L2Sets = await shirtRepository.GetAllLevel2Categories();

            shirtVM.SelectedCategoryValue = null;

            return View(shirtVM);
        }

        // POST: Admin/Shirts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,SelectedCategoryValue")] ShirtVM shirtVM)
        {
            if (ModelState.IsValid == false)
            {
                shirtVM.L2Sets = await shirtRepository.GetAllLevel2Categories();
                return View(shirtVM);
            }

            Shirt shirt = ShirtHelper.MapVmToEntityForCreate(shirtVM);

            bool boolResult = await shirtRepository
                .SetShirtAndAllShirtRelatedTables(shirt);

            if (boolResult == true)

                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("",
                             VarietyValues.ErrorOnCreation);

            shirtVM.L2Sets = await shirtRepository.GetAllLevel2Categories();
            return View(shirtVM);
        }
    }
}
