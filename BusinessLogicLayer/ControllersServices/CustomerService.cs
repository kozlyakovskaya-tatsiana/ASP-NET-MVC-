using DataAccessLayer.EntityModels;
using DataAccessLayer.Repositories.Models;
using System.Linq;
using System.Collections.Generic;
using BusinessLogicLayer.Filters;

namespace BusinessLogicLayer.ControllersServices
{
    public static class CustomerService
    {
        public static IEnumerable<Customer> Customers
        {
            get
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.CustomersRepository.GetAll().ToArray();
                }
            }
        }

        public static IEnumerable<Customer> SortCustomers(IEnumerable<Customer> customers, CustomerSortCriteria customerSortCriteria)
        {
            switch (customerSortCriteria)
            {
                case CustomerSortCriteria.Default:
                    return customers.OrderBy(cust => cust.Id);

                case CustomerSortCriteria.Ascending:
                    return customers.OrderBy(cust => cust.FullName);

                case CustomerSortCriteria.Descending:
                    return customers.OrderByDescending(cust => cust.FullName);

                default:
                    return null;
            }
        }

        public static Customer GetCustomer(int customerId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.CustomersRepository.Get(customerId);
            }
        }

        public static void DeleteCustomer(int customerId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var customer = unitOfWork.CustomersRepository.Get(customerId);

                if (customer == null)
                    return;

                unitOfWork.CustomersRepository.Remove(customerId);

                unitOfWork.Save();
            }
        }

        public static void CreateCustomer(Customer customer)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.CustomersRepository.Create(customer);

                unitOfWork.Save();
            }
        }

        public static void EditCustomer(Customer customer)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.CustomersRepository.Update(customer);

                unitOfWork.Save();
            }
        }

        public static bool IsExisted(string customerName)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.CustomersRepository.GetAll().Where(cust => cust.FullName.Equals(customerName)).Count() > 0;
            }
        }
    }
}
