using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        public ActionResult Create()
        {
            LoadCategoriesDropDown();
            LoadTrainersDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult CreateSession(CreateSessionViewModel sessionViewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadCategoriesDropDown();
                LoadTrainersDropDown();
                return View(nameof(Create), sessionViewModel);
            }

            var result = _sessionService.CreateSession(sessionViewModel);

            if (result)
                TempData["SuccessMessage"] = "Session Created Successfully";
            else
                TempData["ErrorMessage"] = "Error Creating Session";

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Session Id Must Be Positive Integer";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionForUpdate(id);

            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }

            LoadCategoriesDropDown();
            LoadTrainersDropDown();
            return View(session);
        }
        
        [HttpPost]
        public ActionResult EditSession(int id, UpdateSessionViewModel sessionViewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadCategoriesDropDown();
                LoadTrainersDropDown();
                return View(sessionViewModel);
            }

            var result = _sessionService.UpdateSession(id, sessionViewModel);

            if (result)
                TempData["SuccessMessage"] = "Session Updated Successfully";
            else
                TempData["ErrorMessage"] = "Error Updating Session";

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(int id)
        {
            var session = _sessionService.GetSessionDetails(id);

            if (session is null)
                return RedirectToAction(nameof(Index));

            return View(session);
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Session Id Must Be Positive Integer";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionDetails(id);

            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionId = id;

            return View();
        }

        [HttpPost]
        public ActionResult DeleteSession(int id)
        {
            var result = _sessionService.RemoveSession(id);

            if (result)
                TempData["SuccessMessage"] = "Session Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Error Deleting Session";

            return RedirectToAction(nameof(Index));
        }



        #region Helper Methods
        public void LoadCategoriesDropDown()
        {
            var categories = _sessionService.GetCategoriesDropDown();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
        }

        public void LoadTrainersDropDown()
        {
            var trainers = _sessionService.GetTrainersDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        #endregion
    }
}
