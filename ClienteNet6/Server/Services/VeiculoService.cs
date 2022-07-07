using ClienteNet6.Server.Context;
using ClienteNet6.Server.Context.Model;
using ClienteNet6.Server.Services.Exceptions;
using ClienteNet6.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace ClienteNet6.Server.Services
{
    public interface IVeiculoService
    {
        Task AddVeiculo(VeiculoDto veiculo);
        Task<VeiculoDto> GetVeiculo(int renavam);
        Task<IEnumerable<VeiculoDto>> GetVeiculo();
        Task DeleteVeiculo(int renavam);
        Task UpdateVeiculo(int renavam, VeiculoDto veiculo);
    }

    /// <summary>
    /// Veiculo Service
    /// </summary>
    public class VeiculoService : IVeiculoService
    {
        private readonly IUserService _userService;
        private readonly AppGerVeiculosContext _context;
        
        /// <summary>
        /// Requires a user
        /// </summary>
        /// <param name="userService">currently user</param>
        /// <param name="context">Db context</param>
        public VeiculoService(AppGerVeiculosContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        public async Task AddVeiculo(VeiculoDto veiculo)
        {
            var user = _userService.GetUser();

            if (_context.Veiculos.AsNoTracking().Where(veiculo => veiculo.Renavam.Equals(veiculo.Renavam)).Any())
                throw new ConflictPostException("Veiculo já existe.");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var veiculoDb = new Veiculo
                {
                    Chassi = veiculo.Chassi,
                    Cor = veiculo.Cor,
                    Modelo = veiculo.Modelo,
                    Placa = veiculo.Placa,
                    Renavam = veiculo.Renavam
                };
                _context.Veiculos.Add(veiculoDb);

                await _context.SaveChangesAsync();

                var userVeiDb = new UsuarioVeiculo
                {
                    IdUser = user.Id,
                    IdVeiculo = veiculoDb.Renavam
                };
                _context.UsuarioVeiculos.Add(userVeiDb);

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task DeleteVeiculo(int renavam)
        {
            var user = _userService.GetUser();

            var veiculoDb =
                await
                (from userVeiculo in _context.UsuarioVeiculos.AsNoTracking()
                 join veiculos in _context.Veiculos.AsNoTracking() on userVeiculo.IdVeiculo equals veiculos.Renavam
                 where userVeiculo.IdUser.Equals(user.Id)
                 select veiculos).Where(veiculo => veiculo.Renavam.Equals(renavam)).AsNoTracking().FirstOrDefaultAsync();

            if (veiculoDb is null)
                throw new NoContentException($"Veiculo com renavam={renavam} não está cadastrado no usuário={user.Email}.");

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Entry(veiculoDb).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task<VeiculoDto> GetVeiculo(int renavam)
        {
            var user = _userService.GetUser();

            var obj =
                await
                (from userVeiculo in _context.UsuarioVeiculos.AsNoTracking()
                 join veiculos in _context.Veiculos.AsNoTracking() on userVeiculo.IdVeiculo equals veiculos.Renavam
                 where veiculos.Renavam.Equals(renavam) && userVeiculo.IdUser == user.Id
                 select new VeiculoDto
                 {
                     Chassi = veiculos.Chassi,
                     Cor = veiculos.Cor,
                     Placa = veiculos.Placa,
                     Renavam = veiculos.Renavam
                 }).AsNoTracking().FirstOrDefaultAsync().ConfigureAwait(false);
            return obj;
        }

        public async Task<IEnumerable<VeiculoDto>> GetVeiculo()
        {
            var user = _userService.GetUser();

            return
                await
                (from userVeiculo in _context.UsuarioVeiculos.AsNoTracking()
                 join veiculos in _context.Veiculos.AsNoTracking() on userVeiculo.IdVeiculo equals veiculos.Renavam
                 where userVeiculo.IdUser == user.Id
                 select new VeiculoDto
                 {
                     Chassi = veiculos.Chassi,
                     Cor = veiculos.Cor,
                     Placa = veiculos.Placa,
                     Renavam = veiculos.Renavam
                 }).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateVeiculo(int renavam, VeiculoDto veiculo)
        {
            if (!renavam.Equals(veiculo.Renavam))
                throw new ArgumentException("Renavam diferente do veículo passado.");

            var user = _userService.GetUser();

            var veiculoDb =
                await
                (from userVeiculo in _context.UsuarioVeiculos.AsNoTracking()
                 join veiculos in _context.Veiculos.AsNoTracking() on userVeiculo.IdVeiculo equals veiculos.Renavam
                 where userVeiculo.IdUser.Equals(user.Id)
                 select veiculos).Where(veiculo => veiculo.Renavam.Equals(renavam)).AsNoTracking().FirstOrDefaultAsync();

            if (veiculoDb is null)
                throw new NoContentException();

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var updateVeiculo = 
                    new Veiculo
                    {
                        Chassi = veiculo.Chassi,
                        Cor = veiculo.Cor,
                        Renavam = renavam,
                        Modelo = veiculo.Modelo,
                        Placa = veiculo.Placa
                    };
                

                _context.Entry(veiculoDb).CurrentValues.SetValues(updateVeiculo);
                _context.Entry(updateVeiculo).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }
    }
}
