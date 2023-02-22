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
           
            //Act

            var result = _sut.FilterCheck(null);

            //Assert

            Assert.That(result.Equals(false));
        }

        [Test]
        public void FilterCheck_returns_false_if_filtermodel_firstName_is_null()
        {
            //Arrange
  
            //Act

            var result = _sut.FilterCheck(_filterModel);

            //Assert

            Assert.That(result.Equals(false));
        }

        [Test]
        public void FilterCheck_returns_true_if_filtermodel_firstName_is_not_null()
        {
            //Arrange
            //_filterModel.FirstName = "ffff";
            var test = _fixture.Create<FilterModel>();
       
            //Act

            var result = _sut.FilterCheck(test);

            //Assert

            Assert.That(result.Equals(true));
        }

        [Test]
        public void FilterSearch_returns_same_list_if_filemodel_is_null()
        {
            //Arrange
            var contactItem = new List<ContactItem>();
            
            //Act
            var result = _sut.FilterSearch(contactItem,null);

            //Assert
            Assert.AreEqual(result, contactItem);
        }

        [Test]
        public void FilterSearch_returns_same_list_if_filemodel_is_not_null()
        {
            //Arrange
            var contactItem = new List<ContactItem>();
            var filterModelTest = _fixture.Create<FilterModel>();
          

            //Act

            var result = _sut.FilterSearch(contactItem, filterModelTest);

            //Assert

            Assert.AreEqual(result, contactItem);
        }

        [Test]
        public void FilterSearch_throws_ArgumentNullException_if_contactItem_is_null()
        {
            //Arrange


            //Act

           

            //Assert

            
            Assert.Throws<ArgumentNullException>(() => _sut.FilterSearch(null, _filterModel));
        }

        [Test]
        public void FilterSearch_throws_ArgumentNullException_if_contactItem_is_null_and_fileModel_Is_null()
        {
            //Arrange


            //Act



            //Assert


            Assert.Throws<ArgumentNullException>(() => _sut.FilterSearch(null, null));
        }







    }
}
