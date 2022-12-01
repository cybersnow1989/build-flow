using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Services;
using BuildFlow.ViewModel;
using Moq;

namespace BuildFlow.Tests.ViewModels
{
    public class JobNewViewModelTests
    {
        private JobNewViewModel _vm;
        public Mock<INavService> _navMock;

        [SetUp]
        public void Setup()
        {
            _navMock = new Mock<INavService>();

            _vm = new JobNewViewModel(_navMock.Object);
        }

        [Test]
        public void SaveCommand_TitleAndCustomerEmpty_CanExecuteReturnsFalse()
        {
            //Arrange
            _vm.JobTitle = "";
            _vm.JobCustomerName = "";

            //Act
            var canSave = _vm.SaveCommand.CanExecute(null);

            //Assert
            Assert.IsFalse(canSave);
        }

        [Test]
        public void SaveCommand_EntriesAreFilled_CanExecuteReturnsTrue()
        {
            //Arrange
            _vm.JobTitle = "Mock Title";
            _vm.JobCustomerName = "Mock Customer";

            //Act
            var canSave = _vm.SaveCommand.CanExecute(null);

            //Assert
            Assert.IsTrue(canSave);
        }
    }
}
