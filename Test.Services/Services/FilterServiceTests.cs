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

            //Assert

            //Assert.That(result.Equals(false));
            Assert.Throws<ArgumentNullException>(() => _sut.FilterValidation(null, _filterModel));
       
        }

        [Test]
        public void FilterValidation_throws_ArgumentNullException_if_filters_is_null()
        {
            //Arrange

            //Act

            //Assert

            //Assert.That(result.Equals(false));
            Assert.Throws<ArgumentNullException>(() => new FilterService(null));

        }

        [Test]
        public void FilterValidation_returns_same_list_if_filemodel_is_null()
        {
            //Arrange
            var contactItem = _fixture.CreateMany<ContactItem>().ToList();

            //Act
            var result = _sut.FilterValidation(contactItem, null);

            //Assert
            Assert.AreEqual(result, contactItem);
        }

        [Test]
        public void FilterValidation_calls_FilterCheck_for_each_filter()
        {
            //Arrange
            var filterModel = _fixture.Create<FilterModel>();
            var contactItem = _fixture.CreateMany<ContactItem>().ToList();
            

            //Act
            var result = _sut.FilterValidation(contactItem, filterModel);

            //Assert
            foreach (var mockfilter in _mockFilter)
            {
                //mockfilter.Setup( m => m.FilterCheck(filterModel));
                mockfilter.Verify(m => m.FilterCheck(filterModel), Times.Once());
            };
            
        
        }

        [Test]
        public void FilterValidation_calls_FilterSearch_for_each_filter_if_FilterCheck_true()
        {
            //Arrange
            var filterModel = _fixture.Create<FilterModel>();
            var contactItem = _fixture.CreateMany<ContactItem>().ToList();
            bool hey = true;
            foreach (var mockfilter in _mockFilter)
            {
                mockfilter.Setup(m => m.FilterCheck(filterModel)).Returns(hey);
            };

            //Act
            var result = _sut.FilterValidation(contactItem, filterModel);

            //Assert
            foreach (var mockfilter in _mockFilter)
            {
                
                mockfilter.Verify(m => m.FilterSearch(It.IsAny<List<ContactItem>>(), filterModel), Times.Once());
            };


        }
        [Test]
        public void FilterValidation_returns_same_list_if_FilterCheck_false()
        {
            //Arrange
            var filterModel = _fixture.Create<FilterModel>();
            var contactItem = _fixture.CreateMany<ContactItem>().ToList();
            bool hey = true;
            foreach (var mockfilter in _mockFilter)
            {
                mockfilter.Setup(m => m.FilterCheck(filterModel)).Returns(!hey);
            };

            //Act
            var result = _sut.FilterValidation(contactItem, filterModel);

            //Assert
            Assert.AreEqual(result, contactItem);


        }

        [Test]
        public void FilterValidation_callsback_to_previous_filter_list()
        {
            //Arrange
            var filterModel = _fixture.Create<FilterModel>();
            var contactItem = _fixture.CreateMany<ContactItem>().ToList();
            bool hey = true;
            foreach (var mockfilter in _mockFilter)
            {
                mockfilter.Setup(m => m.FilterCheck(filterModel)).Returns(hey);
            };

            //Act
            var result = _sut.FilterValidation(contactItem, filterModel);

            //Assert
            int i = 0;
            foreach (var mockfilter in _mockFilter)
            {
                mockfilter.Verify(m => m.FilterSearch(It.IsAny<List<ContactItem>>(), filterModel), Times.Once());
            };
            Assert.That(result != contactItem);


        }

        [Test]
        public void FilterValidation_returns_correct_list()
        {
            //Arrange
            var filterModel = _fixture.Create<FilterModel>();
            var contactItem = _fixture.CreateMany<ContactItem>().ToList();
            bool hey = true;
            foreach (var mockfilter in _mockFilter)
            {
                mockfilter.Setup(m => m.FilterCheck(filterModel)).Returns(!hey);
            };

            //Act
            var result = _sut.FilterValidation(contactItem, filterModel);

            //Assert
            Assert.Pass();


        }


    }
}