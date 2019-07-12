using Microsoft.EntityFrameworkCore;
using System;
using ParentEspoir.Persistence;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using MediatR.Pipeline;
using ParentEspoir.Application.Infrastructure;
using FluentValidation.AspNetCore;
using ParentEspoir.Common;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ParentEspoir.Application.Test
{
    public class TestBase
    {
        protected ServiceCollection _services;
        protected IServiceProvider _serviceProvider;
        protected IMediator _mediator;
        protected DateTimeForTest _time;

        public TestBase()
        {
            _services = new ServiceCollection();

            _services.AddHttpContextAccessor();

            _services.AddSingleton<IDateTime, DateTimeForTest>();
            
            _services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            _services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            _services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            _services.AddMediatR(typeof(GetCustomerDescriptionQueryHandler).GetTypeInfo().Assembly);
            
            _services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerModelValidator>());
            
            _services.AddEntityFrameworkInMemoryDatabase().AddDbContext<ParentEspoirDbContext>(
                option => option.UseInMemoryDatabase(Guid.NewGuid().ToString())
            );

            _services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ParentEspoirDbContext>()
                .AddDefaultTokenProviders();

            _serviceProvider = _services.BuildServiceProvider();

            _mediator = _serviceProvider.GetRequiredService<IMediator>();
            _time = (DateTimeForTest)_serviceProvider.GetRequiredService<IDateTime>();
        }

        public ParentEspoirDbContext GetDbContext()
        {
            return _serviceProvider.GetRequiredService<ParentEspoirDbContext>();
        }
    }
}