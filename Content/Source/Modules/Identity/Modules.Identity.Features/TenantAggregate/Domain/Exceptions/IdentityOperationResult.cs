using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.IdentityModule.Domain.Exceptions
{
    public class IdentityOperationResult
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public static IdentityOperationResult Success() => new IdentityOperationResult { Successful = true };
        public static IdentityOperationResult Fail(string message) => new IdentityOperationResult { Successful = false, Message = message };
    }
    public class IdentityOperationResult<T>
    {
        public bool Successful { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }
        public static IdentityOperationResult<T> Success(T t) => new IdentityOperationResult<T> { Successful = true, Value = t };
        public static IdentityOperationResult<T> Fail(string message) => new IdentityOperationResult<T> { Successful = false, Message = message };
    }
}
