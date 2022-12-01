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
    public class InvoiceNewViewModelTests
    {
        private InvoiceNewViewModel _vm;
        private Mock<INavService> _navMock;

        [SetUp]
        public void Setup()
        {
            _navMock = new Mock<INavService>();

            _vm = new InvoiceNewViewModel(_navMock.Object);
        }

        [Test]
        public void SaveCommand_InvoiceTotalIsZero_CanExecuteReturnsFalse()
        {
            //Arrange
            _vm.InvoiceTotal = 0;

            //Act
            var canSave = _vm.SaveCommand.CanExecute(null);

            //Assert
            Assert.IsFalse(canSave);
        }

        [Test]
        public void SaveCommand_InvoiceTotalIsNotZero_CanExecuteReturnsTrue()
        {
            //Arrange
            _vm.InvoiceTotal = 0.01m;

            //Act
            var canSave = _vm.SaveCommand.CanExecute(null);

            //Assert
            Assert.IsTrue(canSave);
        }
    }
}
