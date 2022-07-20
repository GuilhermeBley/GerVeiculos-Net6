using ClienteNet6.Server.Context;
using ClienteNet6.Server.Context.Model;

namespace ClienteNet6.Server.Services
{
    public interface ILogService
    {
        Task SendLog(Log log);
    }

    internal class LogService : ILogService
    {
        private readonly AppGerVeiculosContext _context;

        public LogService(AppGerVeiculosContext context)
        {
            _context = context;
        }

        public async Task SendLog(Log log)
        {
            await InsertLog(log);
        }

        private async Task InsertLog(Log log)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Logs.Add(log);

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
