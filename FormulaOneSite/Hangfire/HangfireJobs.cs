using FormulaOneSite.Data;
using Hangfire;
using System.Linq;

namespace FormulaOneSite.Hangfire
{
    public class HangfireJobs : IHangfireJobs
    {
        private readonly ApplicationDbContext _context;

        public HangfireJobs(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ClearUsers()
        {
            var users = _context.Users.Where(u => u.EmailConfirmed == false).ToList();
            _context.Users.RemoveRange(users);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void ApplyCleaning()
        {
            this.ClearUsers();
            this.SaveChanges();
        }

        public void ClearNotConfirmedUsers()
        {
            RecurringJob.AddOrUpdate("DeleteNotConfirmed", () => this.ApplyCleaning(), Cron.Weekly);
        }
    }
}
