﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCOA.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
