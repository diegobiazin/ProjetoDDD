using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDDD.DataAccess;
using ProjetoDDD.Interfaces;
using ProjetoDDD.Interfaces.Repositories;
using ProjetoDDD.Services.Interfaces;
using ProjetoDDD.Services.Classes;
using RHCloud.DataAccess.Repositories;

namespace ProjetoDDD.IoC
{
    public class UnityContainerLoader
    {
        public static UnityContainer Load()
        {
            var container = new UnityContainer();
            RegistryUnitOfWork(container);
            RegistryRepositories(container);
            RegistryServices(container);
            RegistryConexao(container);
            return container;
        }

        private static void RegistryConexao(UnityContainer container)
        {
            container.RegisterType<IConnection, Connection>(new ContainerControlledLifetimeManager());
        }

        private static void RegistryServices(UnityContainer container)
        {
            container.RegisterType<IProdutoService, ProdutoService>(new HierarchicalLifetimeManager());
        }

        private static void RegistryRepositories(UnityContainer container)
        {
            container.RegisterType<IProdutoRepository, ProdutoRepository>(new HierarchicalLifetimeManager());
        }

        private static void RegistryUnitOfWork(UnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
        }

    }
}
