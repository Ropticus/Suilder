using System;

namespace Suilder.Test.Builder.Tables
{
    [Flags]
    public enum PersonFlags
    {
        None = 0,
        ValueA = 1,
        ValueB = 2,
        ValueC = 4,
        ValueD = 8,
        ValueE = 16
    }
}