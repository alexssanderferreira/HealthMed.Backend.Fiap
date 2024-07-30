﻿using HealthMed.Backend.Aplicacao.Contratos.Persistencia;
using HealthMed.Backend.Infraestrutura.Persistencia;
using HealthMed.Backend.Infraestrutura.Repositorio;
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

            return services;
        }
    }
}
