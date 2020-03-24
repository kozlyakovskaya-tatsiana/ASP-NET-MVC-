using BusinessLogicLayer.Filters;
using DataAccessLayer.EntityModels;
using DataAccessLayer.Repositories.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.ControllersServices
{
    public static class ProductService
    {
        public static IEnumerable<Product> Products
        {
            get
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.ProductsRepository.GetAll().ToArray();
                }
            }
        }

        public static IEnumerable<Product> SortProducts(IEnumerable<Product> products, ProductSortCriteria productSortCriteria)
        {
            switch (productSortCriteria)
            {
                case ProductSortCriteria.Default:
                    return products.OrderBy(prod => prod.Id);

                case ProductSortCriteria.AscendingByName:
                    return products.OrderBy(prod => prod.Name);

                case ProductSortCriteria.DescendingByName:
                    return products.OrderByDescending(prod => prod.Name);

                case ProductSortCriteria.AscendingByCost:
                    return products.OrderBy(prod => prod.Cost);

                case ProductSortCriteria.DescendingByCost:
                    return products.OrderByDescending(prod => prod.Cost);

                default:
                    return null;
            }
        }

        public static Product GetProduct(int productId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.ProductsRepository.Get(productId);
            }
        }

        public static void DeleteProduct(int productId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var product = unitOfWork.ProductsRepository.Get(productId);

                if (product == null)
                    return;

                unitOfWork.ProductsRepository.Remove(productId);

                unitOfWork.Save();
            }
        }

        public static void CreateProduct(Product product)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.ProductsRepository.Create(product);

                unitOfWork.Save();
            }
        }

        public static void EditProduct(Product product)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.ProductsRepository.Update(product);

                unitOfWork.Save();
            }
        }

        public static bool IsExisted(string productName, double productCost)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.ProductsRepository.GetAll().FirstOrDefault(prod => prod.Name.Equals(productName) && prod.Cost == productCost) != null;
            }
        }
    }
}
