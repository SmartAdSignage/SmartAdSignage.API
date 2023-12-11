using Microsoft.Extensions.Configuration;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class DatabaseService : IDatabaseService
    {
        public readonly IConfiguration _configuration;
        public readonly IUnitOfWork _unitOfWork;
        public DatabaseService(IConfiguration configuration, IUnitOfWork unitOfWork) 
        {
            this._configuration = configuration;
            this._unitOfWork = unitOfWork;
        }
        public async Task CreateBackupAsync()
        {
            var directory = _configuration.GetSection("Backup")["BackupPath"];
            var filename = $"SmartAdSignage_backup_{DateTime.Now:yyyyMMddHHmmss}.bak";

            var backupPath = Path.Combine(directory, filename);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await _unitOfWork.CreateDatabaseBackupAsync(backupPath);
        }
    }
}
