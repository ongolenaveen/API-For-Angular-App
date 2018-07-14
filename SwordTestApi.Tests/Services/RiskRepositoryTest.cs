using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SwordTestApi.Services;
using SwordTestApi.Contracts.Enumerations;
using SwordTestApi.Contracts.Models;
using SwordTestApi.Contracts;
using System.Collections.Generic;
using System;

namespace SwordTestApi.Tests.Services
{
    [TestClass]
    public class RiskRepositoryTest
    {
        List<Risk> _validRiskResponse = new List<Risk>{
            new Risk(){ Id = 1,Title="Loss of key staff", RiskScore=10, Owner= new Resource {Id=1,Name="Matt Sharpe" } }
        };
        List<Risk> _nilRisks = null;

        [TestMethod]
        public void GetRisks_WithPagination_Returns_Success()
        {
            //Arrange
            var riskDataSourceMock = new Mock<IRiskDataSource>();
            riskDataSourceMock.Setup(x => x.GetRisks()).Returns(() => _validRiskResponse);
            var riskRepository = new RiskRepository(riskDataSourceMock.Object);
            //Act
            var response = riskRepository.GetRisks("Id", Sortorder.Ascending, 1, 5);
            //Assert
            Assert.AreEqual(response.Count, _validRiskResponse.Count);
        }

        [TestMethod]
        public void GetRisks_When_NoRiskData_Returns_Null()
        {
            //Arrange
            var riskDataSourceMock = new Mock<IRiskDataSource>();
            riskDataSourceMock.Setup(x => x.GetRisks()).Returns(() => _nilRisks);
            var riskRepository = new RiskRepository(riskDataSourceMock.Object);
            //Act
            var response = riskRepository.GetRisks("Id", Sortorder.Ascending, 1, 5);
            //Assert
            Assert.AreEqual(response, _nilRisks);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRisks_RaiseException_Returns_Error()
        {
            //Arrange
            var riskDataSourceMock = new Mock<IRiskDataSource>();
            riskDataSourceMock.Setup(x => x.GetRisks()).Throws<Exception>();
            var riskRepository = new RiskRepository(riskDataSourceMock.Object);
            //Act
            var response = riskRepository.GetRisks("Id", Sortorder.Ascending, 1, 5);
            //Assert
            Assert.AreEqual(response, _nilRisks);
        }

        [TestMethod]
        public void GetRisks_InvalidSortCondition_Raise_NoException()
        {
            //Arrange
            var riskDataSourceMock = new Mock<IRiskDataSource>();
            riskDataSourceMock.Setup(x => x.GetRisks()).Returns(() => _validRiskResponse);
            var riskRepository = new RiskRepository(riskDataSourceMock.Object);
            //Act
            var response = riskRepository.GetRisks("xx", Sortorder.Ascending, 1, 5);
            //Assert
            Assert.AreEqual(response.Count, _validRiskResponse.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRisks_InvalidPageNumber_Raise_Exception()
        {
            //Arrange
            var riskDataSourceMock = new Mock<IRiskDataSource>();
            riskDataSourceMock.Setup(x => x.GetRisks()).Returns(() => _validRiskResponse);
            var riskRepository = new RiskRepository(riskDataSourceMock.Object);
            //Act
            var response = riskRepository.GetRisks("Id", Sortorder.Ascending, -1, 5);
            //Assert
            Assert.AreEqual(response.Count, _validRiskResponse.Count);
        }
    }
}
