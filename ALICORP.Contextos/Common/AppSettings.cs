using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Contextos.Common
{
    internal class AppSettings
    {
        public static string StrConnection = ConfigurationManager.ConnectionStrings["ALICORP"]?.ConnectionString;
    }
}
