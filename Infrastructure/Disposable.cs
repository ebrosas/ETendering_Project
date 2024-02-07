namespace System
{
    public static class Disposable
    {
        public static void Using<TDisposable>(Func<TDisposable> factory, Action command, Action<TDisposable> complete)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                command();
                complete(disposable);
            }
        }

        public static TResult Using<TResult, TDisposable>(Func<TDisposable> factory, Func<TDisposable, TResult> query)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return query(disposable);
            }
        }
    }
}