using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstracts
{
    public interface IResult
    {
        public string Message { get;}
        public bool Success { get;}
    }
}