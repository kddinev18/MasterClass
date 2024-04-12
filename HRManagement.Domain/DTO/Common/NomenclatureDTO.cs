﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Domain.DTO.Common
{
    public class NomenclatureDTO<T>
    {
        public T Code { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}
