using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using MyStage2.Data;
using MyStage2.ViewModels;

namespace MyStage2.Controllers
{
    public class AnnounsmentsController : Controller
    {
        private readonly Context _context;

        public AnnounsmentsController(Context context)
        {
            _context = context;

            if (!_context.Announsment.Any())
            {
                for (int i = 0; i < 400; i++)
                {
                    var announsment = Data.TestDataGenerator.GenerateAnnounsment();
                    _context.Announsment.Add(announsment);
                }

                _context.SaveChanges();
            }

        }

        // GET: Announsments
        public async Task<IActionResult> Index()
        {
            var viewModel = new AnnounsmentsVM
            {
                Users = await _context.Users
                    .Select(user => new SelectListItem(user.FirstName + " " + user.LastName, user.Id.ToString()))
                    .ToListAsync()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> GetAnnounsmentsJson(string searchString, int selectedUserId, DateTime? fromDate, DateTime? toDate)
        {
            var announsments = _context.Announsment.Include(u => u.User).AsQueryable();

            if (fromDate.HasValue) announsments = announsments.Where(a => a.CreateDate >= fromDate);

            if (toDate.HasValue) announsments = announsments.Where(a => a.CreateDate <= toDate);


            if (!string.IsNullOrEmpty(searchString))
                announsments = announsments
                    .Where(u => u.Id.ToString()
                                    .Contains(searchString) ||
                                u.TextAnnounsment.Contains(searchString) ||
                                u.User.FirstName.Contains(searchString) ||
                                u.User.LastName.Contains(searchString) ||
                                u.Rating.ToString().Contains(searchString)
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
            if (ids.Length == 0) return RedirectToAction(nameof(Index));

            var entities = _context.Announsment.Where(a => ids.Contains(a.Id));

            if (!entities.Any()) return RedirectToAction(nameof(Index));

            _context.Announsment.RemoveRange(entities);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            await _context.Users.LoadAsync();

            var announsment = await _context.Announsment.FindAsync(id);


            if (announsment == null) return NotFound();

            var announsmentVm = new EditAnnounsmentVm
            {
                Announsment = announsment,
                Users = await _context.Users
                    .Select(user => new SelectListItem(user.FirstName + " " + user.LastName, user.Id.ToString()))
                    .ToListAsync(),
                SelectedUserId = announsment.User.Id
            };


            return PartialView("_EditAnnounsmentModalPartial", announsmentVm);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAnnounsment(EditAnnounsmentVm announsmentVm)
        {
            try
            {
                announsmentVm.Announsment.User = await _context.Users.FindAsync(announsmentVm.SelectedUserId);
                _context.Update(announsmentVm.Announsment);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}