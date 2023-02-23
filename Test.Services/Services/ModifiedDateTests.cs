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
    public class ModifiedDateTests
    {

        private IFixture _fixture;
        private IFilter _sut;
        private FilterModel _filterModel;


        [SetUp]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new ModifiedDate();
            _filterModel = _fixture.Create<FilterModel>();
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
        //CHECK EMPTY
        [Test]
        public void FilterCheck_returns_false_if_filtermodel_modifieddate_is_null()
        {
            //Arrange
            var filterModelWithoutModifiedDate = _fixture.Build<FilterModel>().Without(i => i.ModifiedDate).Create();

            //Act

            var result = _sut.FilterCheck(filterModelWithoutModifiedDate);

            //Assert

            Assert.That(result.Equals(false));
        }

        [Test]
        public void FilterCheck_returns_false_if_filtermodel_modifieddate_is_empty()
        {
            //Arrange
            var filterModelWithEmptyModifiedDate = _fixture.Build<FilterModel>().With(i => i.ModifiedDate, "").Create();

            //Act

            var result = _sut.FilterCheck(filterModelWithEmptyModifiedDate);

            //Assert

            Assert.That(result.Equals(false));
        }

        [Test]
        public void FilterCheck_returns_true_if_filtermodel_modifieddate_is_not_null()
        {
            //Arrange

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
            var contactItem = _fixture.Build<ContactItem>().CreateMany().ToList();

            //Act
            var result = _sut.FilterSearch(contactItem, null);

            //Assert
            Assert.AreEqual(result, contactItem);
        }

        [Test]
        public void FilterSearch_returns_contacts_with_modifieddate_matching_filter()
        {
            //Arrange
            var datetime = _fixture.Create<DateTime>();
            var contactItem = _fixture.Build<ContactItem>().With(i => i.ModifiedDate, datetime).CreateMany().ToList();
            var filterModelTest = _fixture.Build<FilterModel>().With(i => i.ModifiedDate, datetime.ToString()).Create();
            


            //Act

            var result = _sut.FilterSearch(contactItem, filterModelTest);

            //Assert

            Assert.That(result.All(i => i.ModifiedDate == datetime));
        }

        [Test]
        public void FilterSearch_does_not_returns_contacts_with_modifieddate_matching_filter()
        {
            //Arrange

            var contactItem = _fixture.Build<ContactItem>().CreateMany().ToList();
            var filterModelTest = _fixture.Build<FilterModel>().With(i => i.ModifiedDate, _fixture.Create<DateTime>().ToString()).Create();

            Assume.That(contactItem.All(i => !i.ModifiedDate.ToString().Equals(filterModelTest.ModifiedDate)));



            //Act

            var result = _sut.FilterSearch(contactItem, filterModelTest);

            //Assert

            Assert.That(result.Count == 0);
        }

        /*
         * - We get back the contacts with firstname matching the filter  DONE
         * - We don't get back contacts with firstname that does not match the filter   DONE
         */

        [Test]
        public void FilterSearch_throws_ArgumentNullException_if_contactItem_is_null()
        {
            //Arrange


            //Act



            //Assert


            Assert.Throws<ArgumentNullException>(() => _sut.FilterSearch(null, _filterModel));
        }




    }
}
