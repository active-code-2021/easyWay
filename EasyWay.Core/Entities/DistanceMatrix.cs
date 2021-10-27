using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Core.Entities
{
    public class DistanceMatrix
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<Row>rows { get; set; }
        public string status { get; set; }

    }

    public class Result
    {
        public string text { get; set; }
        public int value { get; set; }
    }
    public class Element
    {
        public Result  distance { get; set; }
        public Result duration { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public List<Element> elements { get; set; }
    }

}
