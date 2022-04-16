using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Response
{
    public class Response
    {
        public int State { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
