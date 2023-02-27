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
    public class FirstNameTests
    {



        [Test, CustomAutoData]
        public void FilterCheck_returns_false_if_filtermodel_is_null(FirstName _sut)
        {
            //Arrange
           
            //Act

            var result = _sut.FilterCheck(null);

            //Assert

            Assert.That(result.Equals(false));
        }
        //CHECK EMPTY
        [Test, CustomAutoData]
        public void FilterCheck_returns_false_if_filtermodel_firstName_is_null(IFixture fixture, FirstName _sut)
        {
            //Arrange
            var model = fixture.Build<FilterModel>().Without(i => i.FirstName).Create();
           
            //Act
            var result = _sut.FilterCheck(model);

            //Assert
            Assert.That(result.Equals(false));
        }

        [Test, CustomAutoData]
        public void FilterCheck_returns_false_if_filtermodel_firstName_is_empty(IFixture fixture, FirstName _sut)
        {
            //Arrange
            var model = fixture.Build<FilterModel>().With(i => i.FirstName, "").Create();

            //Act
            var result = _sut.FilterCheck(model);

            //Assert
            Assert.That(result.Equals(false));
        }

        [Test, CustomAutoData]
        public void FilterCheck_returns_true_if_filtermodel_firstName_is_not_null(FilterModel model, FirstName _sut)
        {
            //Arrange
      
            //Act
            var result = _sut.FilterCheck(model);

            //Assert
            Assert.That(result.Equals(true));
        }

        [Test, CustomAutoData]
        public void FilterSearch_returns_same_list_if_filtermodel_is_null(List<ContactItem> contactItems, FirstName sut)
        {
            //Arrange

            //Act
            var result = sut.FilterSearch(contactItems,null);

            //Assert
            Assert.AreEqual(result, contactItems);
        }

        [Test, CustomAutoData]
        public void FilterSearch_returns_contacts_with_firstname_matching_filter(string firstName, FirstName _sut , IFixture fixture)
        {
            //Arrange
            var contactItem = fixture.Build<ContactItem>().With(i => i.LastName, firstName).CreateMany().ToList();
            var model = fixture.Build<FilterModel>().With(i => i.LastName, firstName).Create();

            //Act

            var result = _sut.FilterSearch(contactItem, model);

            //Assert

            Assert.That(result.All(i => i.FirstName == firstName));
        }

        [Test, CustomAutoData]
        public void FilterSearch_does_not_returns_contacts_with_firstname_matching_filter(string randomFirstName,FirstName _sut, List<ContactItem> contactItems, IFixture fixture)
        {
            //Arrange
            var model = fixture.Build<FilterModel>().With(i => i.FirstName, randomFirstName).Create();
           
            Assume.That(contactItems.All(i => !i.FirstName.Equals(model.FirstName)));

            //Act

            var result = _sut.FilterSearch(contactItems, model);

            //Assert

            Assert.That(result.Count == 0);
        }

        /*
         * - We get back the contacts with firstname matching the filter  DONE
         * - We don't get back contacts with firstname that does not match the filter   DONE
         */

        [Test, CustomAutoData]
        public void FilterSearch_throws_ArgumentNullException_if_contactItem_is_null(FirstName _sut, FilterModel model)
        {
            //Arrange

            //Act

            //Assert

            
            Assert.Throws<ArgumentNullException>(() => _sut.FilterSearch(null, model));
        }




    }
}
