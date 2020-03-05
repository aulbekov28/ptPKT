using ptPKT.Core.Entities.Identity;

namespace ptPKT.Tests
{
    public class FakeAppUser
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Password { get; set; }

        public AppUser GetAppUser()
        {
            return new AppUser
            {
                Email = Email,
                UserName = UserName,
                FirstName = FirstName,
                SecondName = SecondName,
            };
        }
    }

    public class AppUserBuilder
    {
        private readonly FakeAppUser _entity = new FakeAppUser
        {
            Email = "default@mail.test",
            Password = "defaultpassword",
            UserName = "userName",
            FirstName = "firstName",
            SecondName = "secondName"
        };

        public FakeAppUser Build() => _entity;

        public AppUserBuilder NotRegistered(string email = "absolutelyNewEmail@mail.test")
        {
            _entity.Email = email;
            return this;
        }

        public AppUserBuilder WithWrongPassword(string password = "wrongpassword")
        {
            _entity.Password = password;
            return this;
        }

        public AppUserBuilder NewUserRegistration()
        {
            _entity.Email = "absolutelyNewEmail@mail.test";
            _entity.FirstName = "firstName";
            _entity.SecondName = "secondName";
            _entity.UserName = "userName";
            _entity.Password = "newpassword";
            return this;
        }
    }
}
