using System.Net;
using System.Web.Http;
using SwordTestApi.Contracts;
using System.Linq;
using System.Web.Http.Cors;
using System.Net.Http;
using SwordTestApi.Contracts.Enumerations;

namespace SwordTestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RisksController : ApiController
    {
        private readonly IRiskRepository _riskRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public RisksController(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        /// <summary>
        /// Get Risks
        /// </summary>
        /// <param name="pageNumber">Sort Condition</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageNumber">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Risks</returns>
        [HttpGet]
        public HttpResponseMessage Get(string sortBy = "Id", Sortorder sortOrder = Sortorder.Ascending,int pageNumber = 1, int pageSize = 10)
        {
            var risks = _riskRepository.GetRisks(sortBy, sortOrder,pageNumber, pageSize);
            if (risks !=null && risks.Any())
            {
                var response = (from risk in risks
                                select new{ risk.Id,risk.Title,Score = risk.RiskScore,Status = risk.Status.ToString(),risk.Owner
                                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

        }
    }
}
