using System.Collections.Generic;
using CluedIn.Crawling.Enalyzer.Core;

namespace CluedIn.Crawling.Enalyzer.Integration.Test
{
  public static class EnalyzerConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { EnalyzerConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
