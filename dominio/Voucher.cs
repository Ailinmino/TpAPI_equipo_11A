﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vouchers
    {
        public string CodigoVoucher {  get; set; }
        public DateTime FechaCanje { get; set; }
        public Articulo Articulo { get; set; }
        public Cliente Cliente { get; set; }

    }
}
