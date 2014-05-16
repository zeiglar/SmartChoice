using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Webs
{
    public interface IContent<T>
    {
        T Extract(T source);
        bool IsCorrect(T source);
    }
}