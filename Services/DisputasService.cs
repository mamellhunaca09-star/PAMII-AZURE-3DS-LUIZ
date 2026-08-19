using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RpgApi.Data;
using RpgApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace RpgApi.services
{
    public class DisputasService
    {
        private readonly DataContext _context;
        public DisputasService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<DisputaDto>> ObterDisputas()
        {
            var resultado = _context.Database.SqlQueryRaw<DisputaDto>(
                @"select
                D.Id, at.Nome [Atacante], op.Nome  as Oponente,
                d.Tx_Narracao Narracao, us.Username NomeUsuarioAtacante,
                usOp.Username NomeUsuarioOponente
                from tb_disputas d
                inner join TB_PERSONAGENS at on d.AtacanteId = at.Id
                inner join TB_PERSONAGENS op on d.OponenteId = op.Id
                LEFT join TB_USUARIOS us on at.UsuarioId = us.Id
                LEFT join TB_USUARIOS usOp on op.UsuarioId = usOp.Id"
            );

            return resultado.ToList();
        }


    }
}