using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.IdentityModule.Domain.Exceptions
{
    public class IdentityOperationException : Exception
    {
        public IdentityOperationException(string message = "") : base(message)
        {

        }
    }
}
