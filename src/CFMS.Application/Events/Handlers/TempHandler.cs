using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Events.Handlers
{
    public class TempHandler
    {
        private readonly Queue<string> _errors = new();

        public void AddError(string message)
        {
            _errors.Enqueue(message);
        }

        public bool HasErrors() => _errors.Count > 0;

        public string PopError() => _errors.Dequeue();
    }
}
