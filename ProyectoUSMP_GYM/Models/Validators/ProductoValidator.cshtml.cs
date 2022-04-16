using FluentValidation;
using ProyectoUSMP_GYM.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM.Models.Validators
{
    public class ProductoValidator:AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty().WithMessage("Error. El Codigo no puede ir vacio");
            RuleFor(x => x.Codigo).MaximumLength(10).WithMessage("Error. El Codigo tiene un maximo de 10 Digitos");
            RuleFor(x => x.Codigo).MinimumLength(10).WithMessage("Error. El Codigo debe tener 10 Digitos");
        }
    }
}