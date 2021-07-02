using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Data
{
    public class CustomUser : Microsoft.AspNetCore.Identity.IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}
