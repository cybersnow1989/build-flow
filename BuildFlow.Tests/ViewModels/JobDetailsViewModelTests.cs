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
    internal class JobDetailsViewModelTests
    {
        public JobDetailsViewModel _vm;
        public Mock<INavService> _navMock;

        [SetUp]
        public void Setup()
        {
            _navMock = new Mock<INavService>();

            _vm = new JobDetailsViewModel(_navMock.Object);
        }

        [Test]
        public void Init_ParameterProvided_SelectedJobIsSet()
        {
            //Arrange
            var mockJob = new Mock<Job>().Object;
            _vm.SelectedJob = null;

            //Act
            _vm.Init(mockJob);

            //Assert
            Assert.IsNotNull(_vm.SelectedJob, "SelectedJob is null after being initialized with a valid Job object.");
        }

        [Test]
        public void Init_ParameterNotProvided_ThrowsJobNotProvidedException()
        {
            //Assert
            Assert.Throws(typeof(JobNotProvidedException), () => _vm.Init());
        }
    }
}
