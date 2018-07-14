using SwordTestApi.Contracts.Enumerations;
namespace SwordTestApi.Contracts.Models
{
    public class Risk
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Resource Owner { get; set; }
        public RiskStatus Status { get; set; }
        public int RiskScore { get; set; }
    }
}
