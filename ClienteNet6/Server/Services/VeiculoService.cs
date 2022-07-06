using ClienteNet6.Server.Context;
using ClienteNet6.Shared.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ClienteNet6.Server.Services
{
    public interface IVeiculoService
    {

    }

    /// <summary>
    /// Veiculo Service
    /// </summary>
    public class VeiculoService : IVeiculoService
    {
        private readonly UserService _userService;
        private readonly AppGerVeiculosContext _context;

        /// <summary>
        /// Requires a user
        /// </summary>
        /// <param name="userService">currently user</param>
        /// <param name="context">Db context</param>
        public VeiculoService(AppGerVeiculosContext context,UserService userService)
        {
            _userService = userService;
            _context = context;
        }

    }
}
