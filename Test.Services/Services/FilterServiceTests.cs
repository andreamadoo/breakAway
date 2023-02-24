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
        private FilterModel _filterModel;


        [SetUp]
        public void Initialize()
        {
            _fixture = new Fixture();
            _filterModel = _fixture.Create<FilterModel>();
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
        public void FilterValidation_throws_ArgumentNullException_if_contactitems_is_null()
        {
            //Arrange

            //Act

            //var result = _sut.FilterCheck(null);

            //Assert

            //Assert.That(result.Equals(false));
            Assert.Throws<ArgumentNullException>(() => _sut.FilterValidation(null, _filterModel));
       
        }

        [Test]
        public void FilterValidation_returns_same_list_if_filemodel_is_null()
        {
            //Arrange
            var contactItem = _fixture.Build<ContactItem>().CreateMany().ToList();

            //Act
            var result = _sut.FilterValidation(contactItem, null);

            //Assert
            Assert.AreEqual(result, contactItem);
        }

        [Test]
        public void FilterValidation_returns_same_list_if_filemodel_firstname_is_null()
        {
            //Arrange
            var filterModelWithoutFirstName = _fixture.Build<FilterModel>().Without(i => i.FirstName).Create();
            var contactItem = _fixture.Build<ContactItem>().CreateMany().ToList();

            //Act
            var result = _sut.FilterValidation(contactItem, filterModelWithoutFirstName);

            //Assert
            Assert.AreEqual(result, contactItem);
        }

        [Test]
        public void FilterValidation_returns_same_list_if_filemodel_firstname_is_empty()
        {
            //Arrange

            //maybe here im suppose to be using array of filters instead of creating a filtermodel like this?
            //repetitive tests not sure if correct?

            var filterModelWithEmptyFirstName = _fixture.Build<FilterModel>().With(i => i.FirstName, "").Create();
            var contactItem = _fixture.Build<ContactItem>().CreateMany().ToList();

            //Act
            var result = _sut.FilterValidation(contactItem, filterModelWithEmptyFirstName);

            //Assert
            Assert.AreEqual(result, contactItem);
        }
    }
}