using NCoreAssignmentApp.Authenication;
using NCoreAssignmentApp.Authentication;

namespace NCoreAssignmentApp.Tests
{
    public class AuthenticationServiceTests
    {
        private AuthenticationService _sut;

        public AuthenticationServiceTests() 
        {
            _sut = new AuthenticationService();
        }

        [Fact]
        public void GivenRoleUserAndFileIsEncrypted_WhenUsingSimpleImplementation_ThenShouldNotAllowToRead()
        {
            var canRead = _sut.CanReadFile(RoleType.User, true);

            Assert.False(canRead);
        }

        [Fact]
        public void GivenRoleUserAndFileNormal_WhenUsingSimpleImplementation_ThenShouldAllowToRead()
        {
            var canRead = _sut.CanReadFile(RoleType.User, false);

            Assert.True(canRead);
        }

        [Theory]
        [InlineData(RoleType.Manager)]
        [InlineData(RoleType.Administration)]
        public void GivenRoleManagerOrAdminAndFileIsEncrypted_WhenUsingSimpleImplementation_ThenShouldAllowToRead(RoleType roleType)
        {
            var canRead = _sut.CanReadFile(roleType, true);

            Assert.True(canRead);
        }
    }
}
