using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Convey.Persistence.MongoDB;
using GrandParade.Registration.Controllers;
using GrandParade.Registration.DTO;
using GrandParade.Registration.Interface;
using GrandParade.Registration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using GrandParade.API.Tests.MoqData;
namespace GrandParade.API.Tests
{
    public class RegistrationControllerTest
    {
        RegistratonController _controller;
        Mock<IRegistration> _service;
        private Mock<IMongoRepository<RegistrationBaseDTO, Guid>> _repo;
        private Mock<ILogger<RegistrationControllerTest>> _logger;

        public RegistrationControllerTest()
        {
            Setup();
        }

        private void Setup()
        {
            _repo = new Mock<IMongoRepository<RegistrationBaseDTO, Guid>>();
            _logger = new Mock<ILogger<RegistrationControllerTest>>();
            _service = new Mock<IRegistration>();
            _service.Setup(x => x.GetAll(It.IsAny<SearchPaginationQuery>())).ReturnsAsync(TestData.GetList());
            _service.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(TestData.GetList().Items.First(x => x.Id != Guid.Empty));
            _controller = new RegistratonController(_service.Object);


        }

        [Fact]
        public async void ShouldGetAll_Data_ByPages()
        {
            // act
            var result = await _controller.Get(new SearchPaginationQuery());
            Assert.Equal(10,result.Value.Items.Count());
        }

        [Fact]
        public async void ShouldGetData_By_Id()
        {
            // act
            var result = await _controller.Get(Guid.Parse("4ba2df9c-21ec-934d-a50e-335f4c55bbef"));
            Assert.Equal("4ba2df9c-21ec-934d-a50e-335f4c55bbef", result.Value.Id.ToString());
        }


    }
}
