namespace UtterlyComplete.UnitTests.Infrastructure.Data.Mocks
{
    internal class MockAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public MockAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }

        public ValueTask<bool> MoveNextAsync()
        {
            Task<bool> task = Task.FromResult(_inner.MoveNext());

            return new ValueTask<bool>(task);
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();

            return new ValueTask();
        }
    }
}
