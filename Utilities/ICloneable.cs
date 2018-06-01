using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}
