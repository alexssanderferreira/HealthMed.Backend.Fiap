﻿using HealthMed.Backend.Aplicacao.Contratos.Persistencia;
using HealthMed.Backend.Infraestrutura.Persistencia;
using HealthMed.Backend.Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.Backend.Infraestrutura
{
    public static class RegistraServicosInfraestrutura
    {
        public static IServiceCollection AddServicosInfraEstrutura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HealthMedContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));


            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioRepository));
            services.AddScoped(typeof(IHorarioRepository), typeof(HorarioRepository));
            services.AddScoped(typeof(IAgendamentosRepository), typeof(AgendamentoRepository));          

            return services;
        }

        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<HealthMedContext>();

                dbContext.Database.Migrate();         
            }
        }
    }
}
