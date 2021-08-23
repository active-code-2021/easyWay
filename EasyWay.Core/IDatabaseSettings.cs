using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Core
{
    public interface IDatabaseSettings
    {
        string CustomerCollectionName { get; set; }
        string deliveryManCollectionName { get; set; }
        public string orderCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
