using System.Collections.Generic;
using SwordTestApi.Contracts.Models;

namespace SwordTestApi.Contracts
{
    public interface IRiskDataSource
    {
        List<Risk> GetRisks();
    }
}
