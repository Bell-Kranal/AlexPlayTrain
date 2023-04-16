using System;

namespace Trees
{
    public interface IEnabler
    {
        public event Action EnabledOrDisabled;
    }
}