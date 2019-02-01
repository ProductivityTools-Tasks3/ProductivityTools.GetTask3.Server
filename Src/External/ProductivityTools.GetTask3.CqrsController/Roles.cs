using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.CqrsController
{
    public enum RoleRequirement
    {
        AllowAnonymous,
        AllowAnyRole,
        RequireRoles
    }

    public class Roles
    {
        public RoleRequirement RoleRequirement { get; }

        public static Roles AnyRole => new Roles(RoleRequirement.AllowAnyRole);

        private Roles(RoleRequirement roleRequirement)
        {
            RoleRequirement = roleRequirement;
        }
    }
}
