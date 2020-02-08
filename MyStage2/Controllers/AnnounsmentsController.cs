using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStage2.Data;
using MyStage2.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyStage2.Controllers
{
    public class AnnounsmentsController : Controller
    {
        private readonly Context _context;

        public AnnounsmentsController(Context context)
        {
            _context = context;
        }

        // GET: Announsments
        public IActionResult Index()
        {
            var viewModel = new AnnounsmentsVM
            {
                Users = new SelectList(_context.Users.AsEnumerable(), "Id", "FirstName")
            };
            return View(viewModel);
        }

        public async Task<IActionResult> GetAnnounsmentsJson(string searchString, int selectedUserId, DateTime? fromDate, DateTime? toDate)
        {
            if (!await _context.Announsment.AnyAsync())
            {
                for (var i = 0; i < 100; i++)
                {
                    var ann = TestDataGenerator.GenerateAnnounsment();

                    _context.Announsment.Add(ann);
                }

                _context.SaveChanges();
            }


            var announsments = _context.Announsment.Include(u => u.User).AsQueryable();

            if (fromDate.HasValue) announsments = announsments.Where(a => a.CreateDate >= fromDate);

            if (toDate.HasValue) announsments = announsments.Where(a => a.CreateDate <= toDate);


            if (!string.IsNullOrEmpty(searchString))
            {
                announsments = announsments
                    .Where(u => u.Id.ToString()
                                    .Contains(searchString) ||
                                u.TextAnnounsment.Contains(searchString) ||
                                u.User.FirstName.Contains(searchString) ||
                                u.User.LastName.Contains(searchString) ||
                                u.Rating.ToString().Contains(searchString)
                    );
            }

            if (selectedUserId != 0) announsments = announsments.Where(a => a.User.Id == selectedUserId);

            return new JsonResult(await announsments.ToListAsync());
        }


        public async Task<IActionResult> Delete(int[] ids)
        {
            if (ids == null) return RedirectToAction(nameof(Index));

            var entities = _context.Announsment.Where(a => ids.Contains(a.Id));

            if (!entities.Any()) return RedirectToAction(nameof(Index));

            _context.Announsment.RemoveRange(entities);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
