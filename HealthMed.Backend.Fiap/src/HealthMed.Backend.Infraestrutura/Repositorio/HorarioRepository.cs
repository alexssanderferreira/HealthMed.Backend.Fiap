﻿using HealthMed.Backend.Aplicacao.Contratos.Persistencia;
using HealthMed.Backend.Dominio.Entidades;
using HealthMed.Backend.Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Backend.Infraestrutura.Repositorio
{
    public class HorarioRepository : RepositoryBase<Horarios>, IHorarioRepository
    {
        public HorarioRepository(HealthMedContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetQuantidadeHorariosPorMedico(DateTime dataInicio, DateTime dataFim, Guid idMedico)
        {
            int quantidade = _dbContext.Horarios.Where(h => (h.Medico.Id == idMedico) &&
                                                               ((dataInicio >= h.HorarioInicio && dataInicio <= h.HorarioFinal) ||
                                                                (dataFim >= h.HorarioInicio && dataFim <= h.HorarioFinal) ||
                                                                (h.HorarioInicio >= dataInicio && h.HorarioInicio <= dataFim) ||
                                                                (h.HorarioFinal >= dataInicio && h.HorarioFinal <= dataFim))).Count();
            return quantidade;
        }

        public async Task<bool> GetHorarioDisponivel(Guid id)
        {
            return _dbContext.Horarios.Where(h => h.Id == id).Select(h => h.Disponivel).FirstOrDefault();
        }

        public override async Task<Horarios> ObterPorIdAsync(Guid id)
        {
            return await _dbContext.Horarios
                .Include(h => h.Medico)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
