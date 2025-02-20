using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstracts
{
    public interface IDataResult<T>:IResult
    {
        public T Data { get; }
    }
}