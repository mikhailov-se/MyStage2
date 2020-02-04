using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStage2.Data;
using MyStage2.Models;
using MyStage2.ViewModels;

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
        public async Task<IActionResult> Index()
        {
            var viewModel = new AnnounsmentsVM
            {
                Users = new SelectList(_context.Users.AsEnumerable(), "Id", "FirstName")
            };
            return View(viewModel);
        }

        public JsonResult GetAnnounsmentsJson(string searchString, int selectedUserId, string fromDate, string toDate)
        {
            if (_context.Announsment.Count()==0)
            {
                _context.Users.Add(new User
                {
                    FirstName="firstname",
                    LastName="Lastname"
                });
                _context.SaveChanges();

                for (int i = 0; i < 100; i++)
                {
                    _context.Announsment.Add(new Announsment
                    {
                        Number = 1,
                        Rating = 1,
                        TextAnnounsment = "text",
                        CreateDate = DateTime.Now,
                        User = _context.Users.First()

                    });
                }
                _context.SaveChanges();

            }


            var announsments = _context.Announsment.Include(u => u.User).AsQueryable();

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime _fromDate = Convert.ToDateTime(fromDate);

                announsments = announsments.Where(a => a.CreateDate >= _fromDate);

            }

            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime _toDate = Convert.ToDateTime(toDate);

                announsments = announsments.Where(a => a.CreateDate <= _toDate);

            }



            if (!string.IsNullOrEmpty(searchString))
            {
                announsments = announsments
                                                   .Where(u => u.Id.ToString()
                                                  .Contains(searchString) ||
                                                  u.TextAnnounsment.Contains(searchString) ||
                                                  u.User.FirstName.Contains(searchString)  ||
                                                  u.User.LastName.Contains(searchString)

                                                  );
            }

            if (selectedUserId != 0)
            {
                announsments = announsments.Where(a => a.User.Id == selectedUserId);
            }

                return new JsonResult(announsments.ToList());

        }




    }
}
