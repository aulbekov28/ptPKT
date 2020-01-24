namespace ptPKT.Tests
{
    public class FakeAppUser
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Password { get; set; }
    }

    public class AppUserBuilder
    {
        private FakeAppUser _entity = new FakeAppUser()
        {
            Email = "default@mail.test",
            Password = "defaultpassword",
            UserName = "userName",
            FirstName = "firstName",
            SecondName = "secondName"
        };

        public FakeAppUser Build() => _entity;

        public AppUserBuilder NotRegistered()
        {
            _entity.Email = "absolutelyNewEmail@mail.test";
            return this;
        }

        public AppUserBuilder WithWrongPassword()
        {
            _entity.Password = "wrongpassword";
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

        public AppUserBuilder Default()
        {
            return this;
        }
    }
}
