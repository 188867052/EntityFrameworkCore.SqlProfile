using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.SqlProfile
{
    public class EntityFrameworkLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new EntityFrameworkLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}