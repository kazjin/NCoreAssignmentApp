using NCoreAssignmentApp.Authenication;
using System.Security.Principal;

namespace NCoreAssignmentApp.Authentication
{
    public class AuthenticationService
    {
        public AuthenticationService() { }

        public bool CanReadXmlFile(RoleType roleType, bool isEncrypted, bool useRealImplementation = false)
        {
            if(useRealImplementation) 
            {
                return ProcessWindowsPrincipal(roleType.ToString());
            }

            switch (roleType)
            {
                case RoleType.User:
                    if (isEncrypted)
                        return false;
                    return true;

                case RoleType.Manager:
                case RoleType.Administration:
                    return true;
            }

            return false;
        }

        private bool ProcessWindowsPrincipal(string role)
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            WindowsPrincipal? myPrincipal = Thread.CurrentPrincipal as WindowsPrincipal;

            return myPrincipal.IsInRole(role);
        }
    }
}
