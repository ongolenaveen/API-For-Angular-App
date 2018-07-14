using SwordTestApi.Contracts;
using SwordTestApi.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using SwordTestApi.Contracts.Enumerations;
using System;

namespace SwordTestApi.Services
{
    public class RiskRepository : IRiskRepository
    {
        private readonly IRiskDataSource _riskDataSource;
        private readonly List<Risk> _risks;
        public RiskRepository(IRiskDataSource riskDataSource)
        {
            _riskDataSource = riskDataSource;
            _risks = _riskDataSource.GetRisks();
        }

        /// <summary>
        /// Get Risks
        /// </summary>
        /// <param name="sortBy">Sort Condition</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageNumber">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>List Of Risk</returns>
        public List<Risk> GetRisks(string sortBy, Sortorder sortOrder, int pageNumber, int pageSize)
        {
            List<Risk> risks = null;
            var sortingOrder = (sortOrder == Sortorder.Ascending) ? "ASC" : "DESC";
            if (pageNumber <= 0)
                throw new ArgumentException($"Page Number:{pageNumber} received is invalid.");
            if (_risks !=null && _risks.Any())
            {
                risks = (from risk in _risks.AsQueryable()
                         orderby($"{sortBy} {sortingOrder}")
                         select risk).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return risks;
        }
    }
}
