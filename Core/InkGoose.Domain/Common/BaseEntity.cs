﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkGoose.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        // TODO: ensure that DateTime is always in UTC (ex: DateTime.UtcNow)
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
