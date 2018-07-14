using System.Collections.Generic;
using SwordTestApi.Contracts.Models;
using SwordTestApi.Contracts.Enumerations;

namespace SwordTestApi.Contracts
{
    public interface IRiskRepository
    {
        List<Risk> GetRisks(string sort, Sortorder sortOrder,int pageNumber, int pageSize);
    }
}
