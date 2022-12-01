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
    public class InvoiceDetailsViewModelTests
    {
        private InvoiceDetailsViewModel _vm;
        private Mock<INavService> _navMock;

        [SetUp]
        public void Setup()
        {
            _navMock = new Mock<INavService>();

            _vm = new InvoiceDetailsViewModel(_navMock.Object);
        }

        [Test]
        public void Init_ParameterProvided_SelectedInvoiceIsSet()
        {
            //Arrange
            var mockInvoice = new Mock<Invoice>().Object;
            _vm.SelectedInvoice = null;

            //Act
            _vm.Init(mockInvoice);

            //Assert
            Assert.IsNotNull(_vm.SelectedInvoice, "SelectedInvoice is null after being initialized with a valid Invoice object.");
        }

        public void Init_ParameterNotProvided_ThrowsInvoiceNotProvidedException()
        {
            //Assert
            Assert.Throws(typeof(InvoiceNotProvidedException), () => _vm.Init());
        }
    }
}
