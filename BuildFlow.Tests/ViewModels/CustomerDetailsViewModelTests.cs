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
    [TestFixture]
    public class CustomerDetailsViewModelTests
    {
        private CustomerDetailsViewModel _vm;
        private Mock<INavService> _navMock;

        [SetUp]
        public void Setup()
        {
            _navMock = new Mock<INavService>();

            _vm = new CustomerDetailsViewModel(_navMock.Object);
        }

        [Test]
        public void Init_ParameterProvided_SelectedCustomerIsSet()
        {
            //Arrange
            var mockCustomer = new Mock<Customer>().Object;
            _vm.SelectedCustomer = null;

            //Act
            _vm.Init(mockCustomer);

            //Assert
            Assert.IsNotNull(_vm.SelectedCustomer, "SelectedCustomer is null after being initialized with a valid Customer object.");
        }

        [Test]
        public void Init_ParameterNotProvided_ThrowsCustomerNotProvidedException()
        {
            //Assert
            Assert.Throws(typeof(CustomerNotProvidedException), () => _vm.Init());
        }
    }
}
