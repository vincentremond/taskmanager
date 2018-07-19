using System;
using TaskManager.Contract.Utilities;

namespace TaskManager.Utilities
{
    public class IdentifierProvider : IIdentifierProvider
    {
        public string CreateNew()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
