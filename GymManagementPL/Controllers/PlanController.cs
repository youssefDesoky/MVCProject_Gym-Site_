using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        public ActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        public ActionResult Details(int id)
        {
            var plan = _planService.GetPlanDetails(id);

            if (plan is null)
                return RedirectToAction(nameof(Index));

            return View(plan);
        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Plan Id Must Be Positive Integer";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanToEdit(id);

            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        [HttpPost]
        public ActionResult EditPlan([FromRoute] int id, UpdatePlanViewModel planViewModel)
        {
            if (!ModelState.IsValid)
                return View(planViewModel);

            var result = _planService.EditPlan(id, planViewModel);

            if (result)
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            else
                TempData["ErrorMessage"] = "Error Updating Plan";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult ActivateDeactivatePlan(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Plan Id Must Be Positive Integer";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanDetails(id);

            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }

            var result = _planService.ToggleActivePlanStatus(id);

            if (result)
                TempData["SuccessMessage"] = "Plan Status Changed Successfully";
            else
                TempData["ErrorMessage"] = "Error Changing Plan Status";

            return RedirectToAction(nameof(Index));
        }
    }
}
