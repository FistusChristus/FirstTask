using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models.DbModels
{
    public class CustomUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}
