using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStage2.Data;
using MyStage2.Models;
using MyStage2.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using MyStage2.Interfaces;

namespace MyStage2.Controllers
{
    public class AnnounsmentsController : Controller
    {
        private readonly IAnnounsmentRepository _announsmentRepository;
        private readonly IUserRepository _userRepository;

        public AnnounsmentsController(IAnnounsmentRepository announsmentRepository, IUserRepository userRepository)
        {
            _announsmentRepository = announsmentRepository;
            _userRepository = userRepository;

            if (!_announsmentRepository.Announsments.Any()) // todo Remove
                for (var i = 0; i < 400; i++)
                {
                    var announsment = TestDataGenerator.GenerateAnnounsment();
                    _announsmentRepository.CreateAnnounsment(announsment);
                }
        }

        // GET: Announsments
        public async Task<IActionResult> Index()
        {
            var viewModel = new AnnounsmentsVm
            {
                Users = await _userRepository.Users
                    .Select(user => new SelectListItem(user.FirstName + " " + user.LastName, user.Id.ToString()))
                    .ToListAsync()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> GetAnnounsmentsJson(string searchString, int selectedUserId,
            DateTime? fromDate, DateTime? toDate)
        {
            var announsments = _announsmentRepository.Announsments.AsQueryable();

            if (fromDate.HasValue) announsments = announsments.Where(a => a.CreateDate >= fromDate);

            if (toDate.HasValue) announsments = announsments.Where(a => a.CreateDate <= toDate);


            if (!string.IsNullOrEmpty(searchString))
                announsments = announsments
                    .Where(u =>
                        u.Number.ToString().ToLower().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        u.TextAnnounsment.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        u.User.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        u.User.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        u.Rating.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    );

            if (selectedUserId != 0) announsments = announsments.Where(a => a.User.Id == selectedUserId);

            var tableRows = await announsments.Select(announsment => new
            {
                id = announsment.Id,
                number = announsment.Number,
                createDate = announsment.CreateDate,
                textAnnounsment = announsment.TextAnnounsment,
                rating = announsment.Rating,
                userName = announsment.User.FirstName + " " + announsment.User.LastName
            }).ToListAsync();


            return new JsonResult(tableRows);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int[] ids)
        {
            if (ids.Length == 0) return NotFound();

            var entities = _announsmentRepository.Announsments.Where(a => ids.Contains(a.Id));

            if (!entities.Any()) return NotFound();

            _announsmentRepository.RemoveRange(entities);
            await _announsmentRepository.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> GetModalEditAnnounsment(int? id)
        {
            if (id == null) return NotFound();

            await _userRepository.Users.LoadAsync();

            var announsment = await _announsmentRepository.Announsments.FindAsync(id);


            if (announsment == null) return NotFound();

            var announsmentVm = new AnnounsmentsVm
            {
                Announsment = announsment,
                Users = await _userRepository.Users
                    .Select(user => new SelectListItem(user.FirstName + " " + user.LastName, user.Id.ToString()))
                    .ToListAsync(),
                SelectedUserId = announsment.User.Id
            };


            return PartialView("_EditAnnounsmentModalPartial", announsmentVm);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAnnounsment(AnnounsmentsVm announsmentVm)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                announsmentVm.Announsment.User = await _userRepository.Users.FindAsync(announsmentVm.SelectedUserId);
                _announsmentRepository.UpdateAnnounsment(announsmentVm.Announsment);
                await _announsmentRepository.SaveChangesAsync();
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> GetModalAddAnnounsment()
        {
            var maxNumber = await _announsmentRepository.Announsments.MaxAsync(a => a.Number);

            var announsment = new Announsment
            {
                CreateDate = DateTime.Now,
                Number = maxNumber + 1
            };

            var users = await _userRepository.Users
                .Select(user => new SelectListItem(user.FirstName + " " + user.LastName, user.Id.ToString()))
                .ToListAsync();

            var announsmentVm = new AnnounsmentsVm
            {
                Announsment = announsment,
                Users = users
            };
            return PartialView("AddAnnounsmentModalPartial", announsmentVm);
        }


        [HttpPost]
        public async Task<IActionResult> AddAnnounsment(AnnounsmentsVm announsmentVm)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                announsmentVm.Announsment.User = await _userRepository.Users.FindAsync(announsmentVm.SelectedUserId);

                await _announsmentRepository.Announsments.AddAsync(announsmentVm.Announsment);
                await _announsmentRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return RedirectToAction("Index");
        }

    }
}