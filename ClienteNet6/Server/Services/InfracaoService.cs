using ClienteNet6.Server.Context;
using ClienteNet6.Server.Context.Model;
using ClienteNet6.Server.Services.Exceptions;
using ClienteNet6.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace ClienteNet6.Server.Services
{
    public interface IInfracaoService
    {
        /// <summary>
        /// Add infração async
        /// </summary>
        /// <param name="veiculo">new model</param>
        /// <returns>async</returns>
        /// <exception cref="ConflictPostException"></exception>
        Task AddInfracao(InfracaoDto infracao);

        /// <summary>
        /// Get infração async
        /// </summary>
        /// <param name="ait">Id</param>
        /// <returns>async <see cref="InfracaoDto"/> or null</returns>
        Task<InfracaoDto> GetInfracao(string ait);

        /// <summary>
        /// Get Enumerable infração async
        /// </summary>
        /// <returns>async <see cref="IEnumerable{InfracaoDto}"/>, can have be zero count</returns>
        Task<IEnumerable<InfracaoDto>> GetInfracao();

        /// <summary>
        /// Delete infração async
        /// </summary>
        /// <param name="ait">id</param>
        /// <returns>async</returns>
        /// <exception cref="NoContentException"></exception>
        Task DeleteInfracao(string ait);

        /// <summary>
        /// Update infração async
        /// </summary>
        /// <param name="ait">id</param>
        /// <param name="infracao">New model</param>
        /// <returns>async</returns>
        /// <exception cref="NoContentException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Task UpdateInfracao(string ait, InfracaoDto infracao);
    }

    public class InfracaoService : IInfracaoService
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IUserService _userService;
        private readonly AppGerVeiculosContext _context;

        /// <summary>
        /// Requires a user
        /// </summary>
        /// <param name="userService">currently user</param>
        /// <param name="context">Db context</param>
        public InfracaoService(AppGerVeiculosContext context, IVeiculoService veiculoService, IUserService userService)
        {
            _veiculoService = veiculoService;
            _context = context;
            _userService = userService;
        }

        public async Task AddInfracao(InfracaoDto infracao)
        {
            // validate if vehicle exists
            var veiculo = await _veiculoService.GetVeiculo(infracao.Renavam);

            if (veiculo is null)
            {
                throw new NoContentException($"Renavam {infracao.Renavam} inexistente.");
            }

            // validate if infr exists
            if (await GetInfracao(infracao.Ait) is not null)
            {
                throw new ConflictPostException($"Infração {infracao.Ait} já existe.");
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var infracaoDb = new Infracao
                {
                    Renavam = veiculo.Renavam,
                    Ait = infracao.Ait,
                    Emissao = infracao.Emissao,
                    Validade = infracao.Validade,
                    Local = infracao.Local
                };
                _context.Infracoes.Add(infracaoDb);

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task DeleteInfracao(string ait)
        {
            Infracao infracaoDb = MapperInfracao(await GetInfracao(ait));

            if (infracaoDb is null)
            {
                throw new NoContentException($"Não existe a infração {ait}.");
            }

            // delete
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Entry(infracaoDb).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task<InfracaoDto> GetInfracao(string ait)
        {
            var user = _userService.GetUser();

            // validate if infr exists and validate if vehicle of infr exists in currently user of http context
            InfracaoDto infracaoDto =
                await
                (from infracao in _context.Infracoes.AsNoTracking()
                 join UsuarioVeiculo in _context.UsuarioVeiculos.AsNoTracking().Where(uv => uv.IdUser.Equals(user.Id))
                    on infracao.Renavam equals UsuarioVeiculo.IdVeiculo
                where infracao.Ait.Equals(ait)
                 select new InfracaoDto
                 {
                    Ait = infracao.Ait,
                    Emissao = infracao.Emissao,
                    Local = infracao.Local,
                    Renavam = infracao.Renavam,
                    Validade = infracao.Validade
                 }
                 ).AsNoTracking().FirstOrDefaultAsync().ConfigureAwait(false);

            return infracaoDto;
        }

        public async Task<IEnumerable<InfracaoDto>> GetInfracao()
        {
            var user = _userService.GetUser();

            // validate if infr exists and validate if vehicle of infr exists in currently user of http context
            var infracoesDto =
                await
                (from infracao in _context.Infracoes.AsNoTracking()
                 join UsuarioVeiculo in _context.UsuarioVeiculos.AsNoTracking().Where(uv => uv.IdUser.Equals(user.Id))
                    on infracao.Renavam equals UsuarioVeiculo.IdVeiculo
                 select new InfracaoDto
                 {
                     Ait = infracao.Ait,
                     Emissao = infracao.Emissao,
                     Local = infracao.Local,
                     Renavam = infracao.Renavam,
                     Validade = infracao.Validade
                 }
                 ).AsNoTracking().ToListAsync().ConfigureAwait(false);

            return infracoesDto;
        }

        public async Task UpdateInfracao(string ait, InfracaoDto infracao)
        {
            if (!ait.Equals(infracao.Ait))
            {
                throw new ArgumentException($"Ait {ait} divergente de modelo {nameof(infracao)}.");
            }

            Infracao infracaoDb = MapperInfracao(await GetInfracao(ait));

            if (infracaoDb is null)
                throw new NoContentException($"Não existe infração {ait}.");

            // verify vehicle change
            if (infracao.Renavam != infracaoDb.Renavam)
            {
                if (await _veiculoService.GetVeiculo(infracao.Renavam) is null)
                    throw new NoContentException($"Não há veículo cadastrado com renavam {infracao.Renavam}.");
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var updateInfracao =
                    new Infracao
                    {
                        Ait = infracao.Ait,
                        Renavam = infracao.Renavam,
                        Emissao = infracao.Emissao,
                        Local = infracao.Local,
                        Validade = infracao.Validade
                    };


                _context.Entry(infracaoDb).CurrentValues.SetValues(updateInfracao);
                _context.Entry(updateInfracao).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }

        /// <summary>
        /// mapper
        /// </summary>
        /// <param name="infracaoDto">dto</param>
        /// <returns>Model <see cref="Infracao"/> or null</returns>
        private static Infracao MapperInfracao(InfracaoDto infracaoDto)
        {
            if (infracaoDto is null)
                return null;

            return new Infracao
            {
                Ait = infracaoDto.Ait,
                Emissao = infracaoDto.Emissao,
                Local = infracaoDto.Local,
                Renavam = infracaoDto.Renavam,
                Validade = infracaoDto.Validade
            };
        }
    }
}
