using System;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, string key)
            : base($"Entity \"{name}\" ({key}) not found") { }
    }
}
