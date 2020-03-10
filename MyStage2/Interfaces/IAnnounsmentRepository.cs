using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStage2.Models;

namespace MyStage2.Interfaces
{
    public interface IAnnounsmentRepository
    {
        DbSet<Announsment> Announsments { get;}
        Announsment GetAnnounsment(int id);
        void CreateAnnounsment(Announsment announsment);
        void UpdateAnnounsment(Announsment announsment);
        void DeleteAnnounsment(Announsment announsment);

        void RemoveRange(IEnumerable<Announsment> announsments);

        void SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
