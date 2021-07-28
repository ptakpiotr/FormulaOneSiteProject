using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Hangfire
{
    public interface IHangfireJobs
    {
        void ClearNotConfirmedUsers();
    }
}
