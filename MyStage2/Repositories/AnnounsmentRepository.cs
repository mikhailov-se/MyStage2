using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStage2.Data;
using MyStage2.Interfaces;
using MyStage2.Models;

namespace MyStage2.Repositories
{
    public class AnnounsmentRepository : IAnnounsmentRepository
    {

        private readonly Context _context;

        public AnnounsmentRepository(Context context)
        {
            _context = context;
        }

        public DbSet<Announsment> Announsments => _context.Announsment;

        public Announsment GetAnnounsment(int id)
        {
            return _context.Announsment.Find(id);
        }

        public void CreateAnnounsment(Announsment announsment)
        {
            _context.Announsment.Add(announsment);
        }


        public void UpdateAnnounsment(Announsment announsment)
        {
            _context.Announsment.Update(announsment);
        }

        public void DeleteAnnounsment(Announsment announsment)
        {
            _context.Announsment.Remove(announsment);
        }

        public void RemoveRange(IEnumerable<Announsment> announsments)
        {
            _context.RemoveRange(announsments);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
