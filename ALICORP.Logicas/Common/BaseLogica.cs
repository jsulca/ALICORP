using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Logicas.Common
{
    public abstract class BaseLogica
    {
        private List<string> Errors { get; set; } = new List<string>();

        public bool HasErrors => Errors.Count > 0;
        public void AddError(string error) => Errors.Add(error);
        public void ClearErrors() => Errors = new List<string>();
        public string[] GetErrors() => Errors.ToArray();
    }
}
