using FirstTask.Data;
using FirstTask.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models.ViewModels
{
    public class ChangeRoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
