using System.Linq;
using System.Security.Principal;

namespace Nop.Core.Fakes
{
    /// <summary>
    /// Fake principal
    /// </summary>
    public class FakePrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private readonly string[] _roles;

        public FakePrincipal(IIdentity identity, string[] roles)
        {
            this._identity = identity;
            this._roles = roles;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            return _roles != null && _roles.Contains(role);
        }
    }
}
