using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository
{
    public interface ISeeder
    {
        Task EnsureSeedDataAsync(IServiceProvider serviceProvider);
    }
}
