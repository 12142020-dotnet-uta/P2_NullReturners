using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Linq;

namespace Models.Tests
{
    public class UnitTest1
    {

        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
           // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }

        [Fact]
        public void ValidateUser()
        {
            var user = new User
            {
                ID = Guid.NewGuid(),
                UserName = "jerry",
                Password = "jerryrice",
                FullName = "Jerry Rice",
                PhoneNumber = "111-111-1111",
                Email = "jerryrice@gmail.com",
                TeamID = 1,
                RoleID = 1
            };

            var errorcount = ValidateModel(user).Count;
            Assert.Equal(0, errorcount);
        }

        [Fact]
        public void InvalidateUser()
        {
            var user = new User
            {
                ID = Guid.NewGuid(),
                UserName = "jerry",
                Password = "jerryrice",
                FullName = "Jerry Rice",
                PhoneNumber = "111-111-1111",
                Email = "1234",
                TeamID = 1,
                RoleID = 1
            };

            var results = ValidateModel(user);
            Assert.True(results.Count > 0);
            Assert.Contains(results, v => v.MemberNames.Contains("Email"));
        }


    }
}
