﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksDemo.Core.Entities
{
    public abstract class EntityBase
    {
        public Guid Id {get; set;}
        public DateTime DateCreated { get; set;}
        public DateTime DateModified { get; set;}   
    }
}
