using BuildingBlocks.EventSourcing;
using MediatR;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Application.Queries;
using FavoDeMel.Catalogo.Application.Services;
using FavoDeMel.Catalogo.Data.Dapper.Abstractions;
using FavoDeMel.Catalogo.Data.Dapper.Connection;
using FavoDeMel.Catalogo.Data.EF.Context;
using FavoDeMel.Catalogo.Data.EF.Repository;
using FavoDeMel.Catalogo.Domain.CommandHandlers;
using FavoDeMel.Catalogo.Domain.Commands;
using FavoDeMel.Catalogo.Domain.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;
using FavoDeMel.Domain.Core.CommonMessages.Notifications;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Data.EventSourcing;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();


            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            //services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            //services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddScoped<CatalogoDbContext>();
            //services.AddScoped<TransferDbContext>();

            //Finders
            services.AddTransient<IBackendConnectionFactory, BackendConnectionFactory>();

            foreach (var item in GetClassName("Finder"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            //Queries Commands
            services.AddTransient<IRequestHandler<GetAllAccountQuery, IEnumerable<AccountDTO>>, GetAllAccountQueryHandler>();

        }

        private static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName))
                return new Dictionary<Type, Type[]>();

            Type type = typeof(FinderSql);

            var assembly = type.Assembly;
            var ts = assembly.GetTypes().ToList();

            var result = new Dictionary<Type, Type[]>();

            foreach (var item in ts.Where(s => !s.IsInterface))
            {
                var isIsAssignable = type.IsAssignableFrom(item);

                if (isIsAssignable)
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);
                }
            }
            return result;
        }
    }
}
