﻿using PharmacyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace PharmacyApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
