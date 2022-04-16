using FluentValidation;
using ProyectoUSMP_GYM.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Validators
{
    public class PersonalValidator:AbstractValidator<Personaladm>
    {
        public PersonalValidator()
        {
            RuleFor(x=>x.Dni).NotEmpty().WithMessage("Error. El Dni no puede ir vacio");
            RuleFor(x => x.Dni).MaximumLength(8).WithMessage("Error. El Dni tiene un Maximo de 8 Digitos");
            RuleFor(x => x.Dni).MinimumLength(8).WithMessage("Error. El Dni no contiene contiene 8 Digitos");
        }
    }
}
