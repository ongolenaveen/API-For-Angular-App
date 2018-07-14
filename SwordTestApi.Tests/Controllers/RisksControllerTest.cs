using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwordTestApi.Contracts;
using SwordTestApi.Contracts.Enumerations;
using SwordTestApi.Controllers;
using Moq;
using SwordTestApi.Contracts.Models;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace SwordTestApi.Tests.Controllers
{
    /// <summary>
    /// Summary description for RisksControllerTest
    /// </summary>
    [TestClass]
    public class RisksControllerTest
    {
        List<Risk> _validRiskResponse = new List<Risk>{
            new Risk(){ Id = 1,Title="Loss of key staff", RiskScore=10, Owner= new Resource {Id=1,Name="Matt Sharpe" } }
        };
        List<Risk> _nilRisks = null;

        [TestMethod]
        public void GetRisks_WithPagination_Returns_Success()
        {
            //Arrange
            var riskRepositoryMock = new Mock<IRiskRepository>();
            riskRepositoryMock.Setup(x => x.GetRisks(It.IsAny<string>(), It.IsAny<Sortorder>(),It.IsAny<int>(), It.IsAny<int>())).Returns(_validRiskResponse);
            var risksController = new RisksController(riskRepositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration
                {
                    IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly
                }
            };
            //Act
            var response = risksController.Get("Id", Sortorder.Ascending, 1, 5);
            var risks = response.Content.ReadAsAsync(typeof(List<Risk>)).Result;
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetRisks_WithoutPagination_Returns_Success()
        {
            //Arrange
            var riskRepositoryMock = new Mock<IRiskRepository>();
            riskRepositoryMock.Setup(x => x.GetRisks(It.IsAny<string>(), It.IsAny<Sortorder>(),It.IsAny<int>(), It.IsAny<int>())).Returns(_validRiskResponse);
            var risksController = new RisksController(riskRepositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration
                {
                    IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly
                }
            };
            //Act
            var response = risksController.Get();
            var risks = response.Content.ReadAsAsync(typeof(List<Risk>)).Result;
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetRisks_When_NoData_Returns_NoContent()
        {
            //Arrange
            var riskRepositoryMock = new Mock<IRiskRepository>();
            riskRepositoryMock.Setup(x => x.GetRisks(It.IsAny<string>(), It.IsAny<Sortorder>(),It.IsAny<int>(), It.IsAny<int>())).Returns(_nilRisks);
            var risksController = new RisksController(riskRepositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration
                {
                    IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly
                }
            };
            //Act
            var response = risksController.Get();
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRisks_RaiseException_Returns_HttpError()
        {
            //Arrange
            var riskRepositoryMock = new Mock<IRiskRepository>();
            riskRepositoryMock.Setup(x => x.GetRisks(It.IsAny<string>(), It.IsAny<Sortorder>(),It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();
            var risksController = new RisksController(riskRepositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration
                {
                    IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly
                }
            };
            //Act
            var response = risksController.Get();
        }

        [TestMethod]
        public void GetRisks_WithInValidPageNumber_Returns_NoContent()
        {
            //Arrange
            var riskRepositoryMock = new Mock<IRiskRepository>();
            riskRepositoryMock.Setup(x => x.GetRisks(It.IsAny<string>(), It.IsAny<Sortorder>() ,- 1, It.IsAny<int>())).Returns(_validRiskResponse);
            var risksController = new RisksController(riskRepositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration
                {
                    IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly
                }
            };
            //Act
            var response = risksController.Get();
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void GetRisks_WithPageSize_Returns_CorrectCount()
        {
            //Arrange
            var riskRepositoryMock = new Mock<IRiskRepository>();
            riskRepositoryMock.Setup(x => x.GetRisks(It.IsAny<string>(), It.IsAny<Sortorder>(),It.IsAny<int>(), It.IsAny<int>())).Returns(_validRiskResponse);
            var risksController = new RisksController(riskRepositoryMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration
                {
                    IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly
                }
            };
            //Act
            var response = risksController.Get("Id", Sortorder.Ascending, 1,1);
            var risks = response.Content.ReadAsAsync(typeof(List<Risk>)).Result as List<Risk>;
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(risks.Count, _validRiskResponse.Count);
        }
    }
}
