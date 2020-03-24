using BusinessLogicLayer.Filters;
using DataAccessLayer.EntityModels;
using DataAccessLayer.Repositories.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.ControllersServices
{
    public class SaleService
    {
        public static IEnumerable<Sale> Sales
        {
            get
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.SalesRepository.GetAll().ToArray();
                }
            }
        }

        public static IEnumerable<Sale> SortSales(IEnumerable<Sale> sales, SaleSortCriteria sortCriteria)
        {
            switch (sortCriteria)
            {
                case SaleSortCriteria.Default:
                    return sales.OrderBy(sale => sale.Id);

                case SaleSortCriteria.AscendingSum:
                    return sales.OrderBy(sale => sale.Sum);

                case SaleSortCriteria.DescendingSum:
                    return sales.OrderByDescending(sale => sale.Sum);

                default:
                    return null;
            }
        }

        public static IEnumerable<Sale> FilterByManager(IEnumerable<Sale> sales, int managerId)
        {
            return sales.Where(sale => sale.ManagerId == managerId);
        }

        public static IEnumerable<Sale> FilterByCustomer(IEnumerable<Sale> sales, int customerId)
        {
            return sales.Where(sale => sale.CustomerId == customerId);
        }

        public static Sale GetSale(int saleId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.SalesRepository.Get(saleId);
            }
        }

        public static void CreateSale(Sale sale)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.SalesRepository.Create(sale);

                unitOfWork.Save();
            }
        }

        public static void DeleteSale(int saleId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var sale = unitOfWork.SalesRepository.Get(saleId);

                if (sale == null)
                    return;

                unitOfWork.SalesRepository.Remove(saleId);

                unitOfWork.Save();
            }
        }

        public static void EditSale(Sale sale)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.SalesRepository.Update(sale);

                unitOfWork.Save();
            }
        }
    }
}
