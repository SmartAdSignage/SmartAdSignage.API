using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Resources
{
    public static class Resources
    {
        private static readonly Lazy<ResourceManager> ResourceManager = new(() =>
        {
            var resourceType = typeof(Resources);
            return new ResourceManager(resourceType.FullName, resourceType.Assembly);
        });

        public static string Get(string key)
        {
            try
            {
                var resourceValue = ResourceManager.Value.GetString(key);
                return resourceValue ?? key;
            }
            catch
            {
                return key;
            }
        }
    }
}
