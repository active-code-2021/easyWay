using EasyWay.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWay.Api
{
    public class DatabaseSettings : IDatabaseSettings
    {
        
   
             public string customerCollectionName { get; set; }
             public string deliveryManCollectionName { get; set; }
             public string orderCollectionName { get; set; }
             public string ConnectionString { get; set; }
             public string DatabaseName { get; set; }
     }

      
 }
