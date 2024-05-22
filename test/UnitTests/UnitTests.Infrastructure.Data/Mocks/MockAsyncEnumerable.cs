using System.Linq.Expressions;

namespace UtterlyComplete.UnitTests.Infrastructure.Data.Mocks
{
    internal class MockAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public MockAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public MockAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new MockAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new MockAsyncQueryProvider<T>(this); }
        }
    }
}
