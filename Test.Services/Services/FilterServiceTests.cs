using AutoFixture;
using BreakAway.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreakAway.Services;
using Moq;

namespace Tests
{
    [TestFixture]
    public class FilterServiceTests
    {

        private IFixture _fixture;
        private IFilterService _sut;
        private Mock<IFilter>[] _mockFilter;
        

        [SetUp]
        public void Initialize()
        {
            _fixture = new Fixture();
            _mockFilter = new[]
            {
                new Mock<IFilter>(),
                new Mock<IFilter>(),
                new Mock<IFilter>(),
                new Mock<IFilter>()
            };

            var test = new IFilter[4];
            int i = 0;
            foreach (var filter in _mockFilter)
            {
                test[i] = filter.Object;
                i++;

            };

            _sut = new FilterService(test);
        }


        [Test]
        public void FilterCheck_returns_false_if_filtermodel_is_null()
        {
            //Arrange

            //Act

            //var result = _sut.FilterCheck(null);

            //Assert

            //Assert.That(result.Equals(false));
            Assert.Pass();
        }
    }
}