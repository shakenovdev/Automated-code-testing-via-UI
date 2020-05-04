using BL.ExecutorActions;
using BL.ExecutorActions.Interfaces;
using BL.Services;
using BL.Services.Interfaces;
using DAL;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BL
{
    public static class ServiceContainer
    {
        public static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ScenarioContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IScenarioRepository, ScenarioRepository>();
            services.AddTransient<IFolderRepository, FolderRepository>();

            services.AddSingleton<IReflectedCollection, ReflectedCollection>();
            services.AddTransient<IVariableAction, VariableAction>();
            services.AddTransient<IMethodAction, MethodAction>();
            services.AddTransient<IAssertAction, AssertAction>();
            services.AddTransient<IExecutor, Executor>(serviceProvider =>
            {
                var reflectedCollection = serviceProvider.GetService<IReflectedCollection>();
                var variableAction = serviceProvider.GetService<IVariableAction>();
                var methodAction = new MethodAction(variableAction, reflectedCollection);
                var assertAction = new AssertAction(variableAction);

                return new Executor(variableAction, methodAction, assertAction);
            });
            
            services.AddTransient<IScenarioCreatorService, ScenarioCreatorService>();
            services.AddTransient<IScenarioExecutorService, ScenarioExecutorService>();
            services.AddTransient<IScenarioListService, ScenarioListService>();

            return services.BuildServiceProvider();
        }
    }
}