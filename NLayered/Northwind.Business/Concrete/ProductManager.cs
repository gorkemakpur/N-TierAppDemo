using FluentValidation;
using Northwind.Business.Abstract;
using Northwind.Business.Utilities;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            ValidationTools.Validate(new ProductValidator(),product);
                _productDal.Add(product);

        }

        public void Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
            }
            catch (DbUpdateException e)
            {

                throw new Exception("Silme gerçekleşmedi.");
            }

        }

        public List<Product> GetAll()
        {
            //business code

            return _productDal.GetAll();
        }

        public List<Product> GetProductsByName(string productName)
        {
            return _productDal.GetAll(x => x.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        List<Product> IProductService.GetProductsByCategory(int categoryId)
        {
            return _productDal.GetAll(x => x.CategoryID == categoryId);
        }
    }
}
