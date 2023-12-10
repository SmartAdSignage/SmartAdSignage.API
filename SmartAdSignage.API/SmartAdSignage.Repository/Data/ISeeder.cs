using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Data
{
    public interface ISeeder
    {
        Task EnsureSeedDataAsync(IServiceProvider serviceProvider);
    }
}
