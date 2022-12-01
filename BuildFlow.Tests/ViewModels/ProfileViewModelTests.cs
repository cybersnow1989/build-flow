using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.Services;
using BuildFlow.ViewModel;
using Moq;

namespace BuildFlow.Tests.ViewModels
{
    public class ProfileViewModelTests
    {
        public ProfileViewModel _vm;
        public Mock<INavService> _navMock;

        [SetUp]
        public void Setup()
        {
            _navMock = new Mock<INavService>();

            _vm = new ProfileViewModel(_navMock.Object);
        }

        [Test]
        public void SaveCommand_EntriesAreEmpty_CanExecuteReturnsFalse()
        {
            //Arrange
            _vm.FirstName = "";
            _vm.LastName = "";
            _vm.CompanyName = "";
            _vm.Address = "";
            _vm.City = "";
            _vm.State = "";
            _vm.ZipCode = "";

            //Act
            var canSave = _vm.SaveCommand.CanExecute(null);

            //Assert
            Assert.IsFalse(canSave);
        }

        [Test]
        public void SaveCommand_EntriesAreFilled_CanExecuteReturnsTrue()
        {
            //Arrange
            _vm.FirstName = "Mock FirstName";
            _vm.LastName = "Mock LastName";
            _vm.CompanyName = "Mock Company";
            _vm.Address = "Mock Address";
            _vm.City = "Mock City";
            _vm.State = "Mock State";
            _vm.ZipCode = "Mock ZipCode";

            //Act
            var canSave = _vm.SaveCommand.CanExecute(null);

            //Assert
            Assert.IsTrue(canSave);
        }
    }
}
