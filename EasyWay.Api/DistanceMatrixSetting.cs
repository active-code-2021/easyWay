using EasyWay.Core;

namespace EasyWay.Api
{
    public class DistanceMatrixSetting : IDistanceMatrixSettings
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}
