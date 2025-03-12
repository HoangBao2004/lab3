using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASC.Tests.TestUtilities
{
    public class FakeSession : ISession
    {
        private readonly Dictionary<string, byte[]> sessionFactory = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;
        public string Id => Guid.NewGuid().ToString();
        public IEnumerable<string> Keys => sessionFactory.Keys;

        public void Remove(string key)
        {
            sessionFactory.Remove(key);
        }

        public void Clear()
        {
            sessionFactory.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Set(string key, byte[] value)
        {
            sessionFactory[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            return sessionFactory.TryGetValue(key, out value);
        }
    }
}
