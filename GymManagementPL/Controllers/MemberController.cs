using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public ActionResult Index()
        {
            return View(_memberService.GetAllMembers());
        }

        public ActionResult MemberDetails(int id)
        {
            var member = _memberService.GetMemberDetails(id);

            if (member is null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            var member = _memberService.GetMemberHealthDetails(id);

            if (member is null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel memberViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data");
                return View(nameof(Create), memberViewModel);
            }

            bool result = _memberService.CreateMember(memberViewModel);

            if (result)
                TempData["SuccessMessage"] = "Member Created Successfully";
            else
                TempData["ErrorMessage"] = "Member Failed To Create, Phone Number Or Email Already Exist";

            return RedirectToAction(nameof(Index));
        }


        public ActionResult MemberEdit(int id)
        {
            var member = _memberService.GetMemberToUpdate(id);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id, UpdateMemberViewModel memberViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data");
                return View(nameof(MemberEdit), memberViewModel);
            }

            bool result = _memberService.UpdateMember(id, memberViewModel);

            if (result)
                TempData["SuccessMessage"] = "Member Updated Successfully";
            else
                TempData["ErrorMessage"] = "Member Failed To Update";

            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Member Id Must Be Positive Integer";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(id);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MemberId = id;

            return View();
        }

        [HttpPost]
        public ActionResult DeleteMemberConfirmed([FromForm] int id)
        {
            bool result = _memberService.RemoveMember(id);

            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Member Can Not Be Deleted";

            return RedirectToAction(nameof(Index));
        }
    }
}
