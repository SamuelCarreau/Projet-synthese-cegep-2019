using Microsoft.AspNetCore.Identity;
using Shouldly;
using System.Collections;
using System.Linq;
using Xunit;
using System;
using System.Text;
using MediatR;
using FluentValidation;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application.Test.User
{
    public class UsersTest : TestBase
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private string UserId { get; set; }

        private static readonly string USER1_EMAIL = "admin@admin.com";
        private static readonly string USER1_PASSWORD = "?1234Soleil";
        private static readonly string USER2_EMAIL = "user@user.com";
        private static readonly string USER2_PASSWORD = "Okijuh877213!!/";

        public UsersTest() : base()
        {
            _userManager = _serviceProvider.GetService(typeof(UserManager<AppUser>)) as UserManager<AppUser>;
            _roleManager = _serviceProvider.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

            var result = _userManager.CreateAsync(new AppUser
            {
                Email = USER1_EMAIL,
                UserName = USER1_EMAIL
            }, USER1_PASSWORD).Result;

            AssertSucced(result);

            UserId = _userManager.Users.First().Id;
        }

        [Fact]
        public void GetUserTest()
        {
            var result = _mediator.Send(new GetUserQuery
            {
                Id = UserId
            }).Result;

            result.Id.ShouldBe(UserId);
            result.Email.ShouldBe(USER1_EMAIL);
            result.UserName.ShouldBe(USER1_EMAIL);
        }

        [Fact]
        public void GetUsersTest()
        {
            AssertSucced(_userManager.CreateAsync(new AppUser
            {
                Email = USER2_EMAIL,
                UserName = USER2_EMAIL
            }, USER2_PASSWORD).Result);

            var users = _mediator.Send(new GetUsersQuery()).Result;

            users.GetType().GetInterfaces().ShouldContain(typeof(IEnumerable));

            users.Count().ShouldBe(2);
            users.ShouldContain(u => u.UserName == USER1_EMAIL);
            users.ShouldContain(u => u.UserName == USER2_EMAIL);
        }

        [Fact]
        public void GetUserEditQuery()
        {
            var result = _mediator.Send(new GetUserEditQuery { Id = UserId }).Result;

            result.ShouldBeOfType(typeof(UpdateUserCommand));

            result.Id.ShouldBe(UserId);
            result.Email.ShouldBe(USER1_EMAIL);
            result.UserName.ShouldBe(USER1_EMAIL);

            _userManager.ResetPasswordAsync(_userManager.Users.Single(u => u.Id == UserId), result.PasswordToken, "IjUhYg3477*&783///")
                .Result.Succeeded.ShouldBe(true);

            result.NewPassword.ShouldBe("");
        }

        [Fact]
        public void GetRoleEmptyListTest()
        {
            var result = _mediator.Send(new GetRolesQuery { UserId = UserId }).Result;

            result.User.ShouldBe(_userManager.Users.Single(u => u.Id == UserId));
            result.UserRoles.Count.ShouldBe(0);
            result.IdentityRoles.Count().ShouldBe(0);
        }

        [Fact]
        public void GetEmptyTest()
        {
            AssertSucced(_roleManager.CreateAsync(new IdentityRole("Utilisateur")).Result);
            AssertSucced(_roleManager.CreateAsync(new IdentityRole("Administrateur")).Result);
            AssertSucced(_userManager.AddToRoleAsync(_userManager.Users.Single(u => u.Id == UserId), "Utilisateur").Result);

            var result = _mediator.Send(new GetRolesQuery { UserId = UserId }).Result;

            result.User.ShouldBe(_userManager.Users.Single(u => u.Id == UserId));

            result.UserRoles.Count.ShouldBe(1);
            result.UserRoles[0].ShouldBe("Utilisateur");

            result.IdentityRoles.Count().ShouldBe(2);
            result.IdentityRoles.ShouldContain(r => r.Name == "Utilisateur");
            result.IdentityRoles.ShouldContain(r => r.Name == "Administrateur");
        }

        [Theory]
        [InlineData(new string[] { "Administrateur", "Utilisateur" }, new string[] { "Administrateur", "Utilisateur" })]
        [InlineData(new string[] { "Utilisateur" }, new string[] { "Administrateur" })]
        [InlineData(new string[] {  }, new string[] { "Administrateur", "Utilisateur" })]
        [InlineData(new string[] { "Administrateur", "Utilisateur" }, new string[] {  })]
        public void UpdateUserRolesTest(string[] initialUserRole, string[] newUserRoles)
        {
            foreach (var role in initialUserRole.Concat(newUserRoles))
            {
                if (!_roleManager.RoleExistsAsync(role).Result)
                {
                    AssertSucced(_roleManager.CreateAsync(new IdentityRole { Name = role }).Result);
                }
            }

            var result = _mediator.Send(new UpdateUserRolesCommand { UserId = UserId, Roles = newUserRoles }).Result;

            result.ShouldBeOfType(typeof(Unit));

            var user = _userManager.Users.Single(u => u.Id == UserId);

            foreach (var role in newUserRoles)
            {
                _userManager.IsInRoleAsync(user, role).Result.ShouldBe(true);
            }
            
            foreach (var role in initialUserRole)
            {
                if (!newUserRoles.Contains(role))
                {
                    _userManager.IsInRoleAsync(user, role).Result.ShouldBe(false);
                }
            }
        }

        [Fact]
        public void UpdateUserRolesValidatorTest()
        {
            _mediator.Send(new UpdateUserRolesCommand { UserId = "invalidId" })
                .ShouldThrow(typeof(ValidationException));
        }

        [Fact]
        public void UpdateUserEmailTest()
        {
            _userManager.FindByEmailAsync("newEmail@monmail.com").Result.ShouldBeNull();

            var command = _mediator.Send(new GetUserEditQuery { Id = UserId }).Result;

            command.Email = "newEmail@monmail.com";

            var result = _mediator.Send(command).Result;

            result.ShouldBeOfType(typeof(Unit));

            _userManager.FindByEmailAsync("newEmail@monmail.com").Result.ShouldNotBeNull();
        }

        [Fact]
        public void UpdateUserEmailInvalidTest()
        {
            _userManager.FindByEmailAsync("newEmail@monmail.com").Result.ShouldBeNull();

            var command = _mediator.Send(new GetUserEditQuery { Id = UserId }).Result;

            command.Email = "invalidMail@";

            var result = _mediator.Send(command).ShouldThrow(typeof(ValidationException));
        }

        [Fact]
        public void UpdateUsernameTest()
        {
            _userManager.FindByNameAsync("newEmail@monmail.com").Result.ShouldBeNull();

            var command = _mediator.Send(new GetUserEditQuery { Id = UserId }).Result;

            command.UserName = "newEmail@monmail.com";

            var result = _mediator.Send(command).Result;

            result.ShouldBeOfType(typeof(Unit));

            _userManager.FindByNameAsync("newEmail@monmail.com").Result.ShouldNotBeNull();
        }

        [Fact]
        public void UpdateUsernameInvalidTest()
        {
            var command = _mediator.Send(new GetUserEditQuery { Id = UserId }).Result;

            command.UserName = "invalidMail@";

            var result = _mediator.Send(command).ShouldThrow(typeof(ValidationException));
        }

        //[Fact]
        //public void UpdateUsernameToNameOfOtherUserTest()
        //{
        //    AssertSucced(_userManager.CreateAsync(new AppUser { Email = USER2_EMAIL, UserName = USER2_EMAIL }).Result);

        //    var command = _mediator
        //        .Send(new GetUserEditQuery { Id = UserId }).Result;

        //    command.UserName = USER2_EMAIL;

        //    var result = _mediator.Send(command)
        //        .ShouldThrow(typeof(ValidationException));
        //}

        [Fact]
        public void UpdatePasswordTest()
        {
            var command = _mediator.Send(new GetUserEditQuery { Id = UserId }).Result;

            command.NewPassword = "mySecurePa55word!";

            var result = _mediator.Send(command).Result;

            result.ShouldBeOfType(typeof(Unit));

            _userManager.CheckPasswordAsync(_userManager.Users.Where(u => u.Id == UserId).Single(), "mySecurePa55word!")
                .Result.ShouldBe(true);
        }

        //[Theory]
        //[InlineData("mmm")]
        //[InlineData("monnom123")]
        //public void UpdateInvalidPasswordTest(string password)
        //{
        //    var command = _mediator
        //        .Send(new GetUserEditQuery { Id = UserId }).Result;

        //    command.NewPassword = password;

        //    _mediator.Send(command)
        //        .ShouldThrow(typeof(ValidationException));
        //}

        [Fact]
        public void DeleteUserTest()
        {
            _mediator.Send(new DeleteUserCommand { Id = UserId })
                .Result.ShouldBeOfType(typeof(Unit));

            _userManager.FindByIdAsync(UserId).Result.ShouldBeNull();
        }

        [Fact]
        public void CreateUserTest()
        {
            _mediator.Send(new CreateUserCommand { Email = USER2_EMAIL, Password = USER2_PASSWORD })
                .Result.ShouldBeOfType(typeof(Unit));

            _userManager.FindByNameAsync(USER2_EMAIL).Result.ShouldNotBeNull();
            _userManager.FindByEmailAsync(USER2_EMAIL).Result.ShouldNotBeNull();
        }

        [Fact]
        public void CreateUserException()
        {
            _mediator.Send(new CreateUserCommand { Email = USER1_EMAIL, Password = USER1_PASSWORD })
                .ShouldThrow(typeof(ValidationException));
        }

        private void AssertSucced(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    sb.Append(error.Code);
                    sb.Append(" : ");
                    sb.Append(error.Description);
                    sb.Append(" ");
                }

                throw new Exception(sb.ToString());
            }
        }
    }
}
