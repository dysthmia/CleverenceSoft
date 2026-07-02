namespace SolvedProblems.Solutions;
public static class Task2 
{
    public static class Server
    {
        private static int _count;
        private static readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        public static int GetCount()
        {
            _locker.EnterReadLock();
            try
            {
                return _count;
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public static void AddToCount(int value)
        {
            _locker.EnterWriteLock();
            try
            {
                checked
                {
                    _count += value;
                }
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }
    }
}