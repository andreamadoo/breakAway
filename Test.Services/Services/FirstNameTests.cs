using AutoFixture;
using BreakAway.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreakAway.Services;

namespace Tests
{
    [TestFixture]
    public class FirstNameTests
    {

        private IFixture _fixture;
        private IFilter _sut;
        private FilterModel _filterModel;

        [SetUp]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new FirstName();
            _filterModel = new FilterModel();

        }


        [Test]
        public void FilterCheck_returns_false_if_filtermodel_is_null()
        {
            //Arrange
            //var firstName = _fixture.Build<FirstName>().Create();


            //Act

            var result = _sut.FilterCheck(null);

            //Assert

            Assert.That(result.Equals(false));
        }

        [Test]
        public void FilterCheck_returns_true_if_filtermodel_is_not_null()
        {
            //Arrange
  
            //Act

            var result = _sut.FilterCheck(_filterModel);

            //Assert

            Assert.That(result.Equals(true));
        }
    }
}
