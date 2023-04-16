using System;

namespace Logic
{
    public interface ISimpleUpgrade : IUpgrade
    {
        public event Action Upgraded;
        public void Upgrade();
    }
}