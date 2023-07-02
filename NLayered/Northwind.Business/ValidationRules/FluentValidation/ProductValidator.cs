using FluentValidation;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.ProductName).NotEmpty().WithMessage("Ürün Adı Boş Olamaz");
            RuleFor(x=>x.CategoryID).NotEmpty();
            RuleFor(x=>x.UnitPrice).NotEmpty();
            RuleFor(x=>x.QuantityPerUnit).NotEmpty();
            RuleFor(x=>x.UnitsInStock).NotEmpty();


            RuleFor(x => x.UnitPrice).GreaterThan(0);
            RuleFor(x => x.UnitsInStock).GreaterThanOrEqualTo((short)0);
            RuleFor(x => x.UnitPrice).GreaterThan(10).When(x => x.CategoryID == 2);

            RuleFor(x => x.ProductName).Must(StartWithA).WithMessage("Ürün adı A ile başlamalıdır");

        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
