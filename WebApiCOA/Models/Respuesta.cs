using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCOA.Models
{
    public class Respuesta
    {
        public StatusCodeResult Status { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }
}
