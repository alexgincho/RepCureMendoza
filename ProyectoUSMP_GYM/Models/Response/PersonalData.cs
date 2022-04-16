using ProyectoUSMP_GYM.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Response
{
    public class PersonalData
    {
        public Personaladm Personal { get; set; }
        public List<Menu> Menu { get; set; }
    }
}
