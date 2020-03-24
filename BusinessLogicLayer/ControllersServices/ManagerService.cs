using BusinessLogicLayer.Filters;
using DataAccessLayer.EntityModels;
using DataAccessLayer.Repositories.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.ControllersServices
{
    public static class ManagerService
    {
        public static IEnumerable<Manager> Managers
        {
            get
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.ManagersRepository.GetAll().ToArray();
                }
            }
        }

        public static IEnumerable<Manager> SortManagers(IEnumerable<Manager> managers, ManagerSortCriteria managerSortCriteria)
        {
            switch (managerSortCriteria)
            {
                case ManagerSortCriteria.Default:
                    return managers.OrderBy(man => man.Id);

                case ManagerSortCriteria.Ascending:
                    return managers.OrderBy(man => man.Name);

                case ManagerSortCriteria.Descending:
                    return managers.OrderByDescending(man => man.Name);

                default:
                    return null;
            }
        }

        public static Manager GetManager(int managerId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.ManagersRepository.Get(managerId);
            }
        }

        public static void DeleteManager(int managerId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var manager = unitOfWork.ManagersRepository.Get(managerId);

                if (manager == null)
                    return;

                unitOfWork.ManagersRepository.Remove(managerId);

                unitOfWork.Save();
            }
        }

        public static void CreateManager(Manager manager)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.ManagersRepository.Create(manager);

                unitOfWork.Save();
            }
        }

        public static void EditManager(Manager manager)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.ManagersRepository.Update(manager);

                unitOfWork.Save();
            }
        }

        public static bool IsExisted(string managerName)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.ManagersRepository.GetAll().FirstOrDefault(man => man.Name.Equals(managerName)) != null;
            }
        }
    }
}
