using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDDD.DTO
{
    [Validator(typeof(ListarDTOValidator))]
    public class ListarDTO
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }

    public class ListarDTOValidator : AbstractValidator<ListarDTO>
    {
        public ListarDTOValidator()
        {
            RuleFor(x => x.Take).LessThan(100).WithMessage("Take não pode ser maior que 100!");
        }
    }
}
