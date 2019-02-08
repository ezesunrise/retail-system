using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem.Models
{
    public abstract class Role
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";

        public const string AnyAdmin = SuperAdmin + "," + Admin;
        public const string AdminOrManager = Admin + "," + Manager;
        public const string ManagerOrUser = Manager + "," + User;

        public const string AnyButSuperAdmin = Admin + "," + Manager + "," + User;
        public const string AnyButUser = SuperAdmin + "," + Admin + "," + Manager;

        public const string Any = SuperAdmin + "," + Admin + "," + Manager + "," + User;
    }
}
