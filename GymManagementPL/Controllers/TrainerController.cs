using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public ActionResult Index()
        {
            return View(_trainerService.GetAllTrainers());
        }

        public ActionResult TrainerDetails(int id)
        {
            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer is null)
                return RedirectToAction(nameof(Index));

            return View(trainer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel trainerViewModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data");
                return View(nameof(Create), trainerViewModel);
            }

            bool result = _trainerService.CreateTrainer(trainerViewModel);

            if (result)
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Create, Phone Number Or Email Already Exist";

            return RedirectToAction(nameof(Index));
        }

        public ActionResult TrainerEdit(int id)
        {
            var trainer = _trainerService.GetTrainerToUpdate(id);

            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }

        [HttpPost]
        public ActionResult TrainerEdit([FromRoute] int id, UpdateTrainerViewModel trainerViewModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data");
                return View(nameof(TrainerEdit), trainerViewModel);
            }

            bool result = _trainerService.UpdateTrainer(id, trainerViewModel);

            if (result)
                TempData["SuccessMessage"] = "Trainer Updated Successfully";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Update";

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Trainer Id Must Be Positive Integer";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            @ViewBag.TrainerId = trainer.Id;

            return View();
        }

        [HttpPost]
        public ActionResult TrainerDeleteConfirmed([FromForm] int id)
        {
            var result = _trainerService.RemoveTrainer(id);

            if (result)
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Trainer Can Not Be Deleted";

            return RedirectToAction(nameof(Index));
        }
    }
}
