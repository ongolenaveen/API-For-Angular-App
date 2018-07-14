
using SwordTestApi.Contracts.Models;
using System.Collections.Generic;
using SwordTestApi.Contracts;
using SwordTestApi.Contracts.Enumerations;

namespace SwordTestApi.Services
{
    public class RiskDatasource: IRiskDataSource
    {
        private readonly Resource _matt = new Resource { Id = 1, Name = "Matt Sharpe" };
        private readonly Resource _john = new Resource { Id = 2, Name = "John Hillhouse" };
        private readonly Resource _julian = new Resource { Id = 3, Name = "Julian Jelfs" };
        private readonly Resource _darren = new Resource { Id = 4, Name = "Darren Thorpe" };
        private readonly Resource _jonm = new Resource { Id = 5, Name = "Jon Moore" };

        public List<Risk> GetRisks()
        {
            var risks = new List<Risk>();
            risks.Add(new Risk { Id = 1, Owner = _matt, RiskScore = 5, Status = RiskStatus.Approved, Title = "Lack Build Capacity In Dockyard" });
            risks.Add(new Risk { Id = 2, Owner = _john, RiskScore = 10, Status = RiskStatus.Unapproved, Title = "Small scale fire in warehouse" });
            risks.Add(new Risk { Id = 3, Owner = _matt, RiskScore = 17, Status = RiskStatus.Mitigated, Title = "Contract delays" });
            risks.Add(new Risk { Id = 4, Owner = _julian, RiskScore = 23, Status = RiskStatus.Open, Title = "Supplier insolvency" });
            risks.Add(new Risk { Id = 5, Owner = _darren, RiskScore = 13, Status = RiskStatus.Closed, Title = "Loss of key staff" });
            risks.Add(new Risk { Id = 6, Owner = _jonm, RiskScore = 97, Status = RiskStatus.Open, Title = "Fire in plant" });
            risks.Add(new Risk { Id = 7, Owner = _jonm, RiskScore = 97, Status = RiskStatus.Open, Title = "Fire in backup plant" });
            risks.Add(new Risk { Id = 8, Owner = _jonm, RiskScore = 45, Status = RiskStatus.Unapproved, Title = "Disaster recovery doesn't cover fire" });
            risks.Add(new Risk { Id = 9, Owner = _darren, RiskScore = 36, Status = RiskStatus.Approved, Title = "Component fails to meet performance" });
            risks.Add(new Risk { Id = 10, Owner = _john, RiskScore = 36, Status = RiskStatus.Approved, Title = "Component fails to meet performance" });
            return risks;
        }
    }
}
