namespace ValidateScopes
{
    public class ScopedDependency
    {
        public static int _counter = 0;

        public ScopedDependency()
        {
            ++_counter;
        }

        public int GetNextCounter()
        {
            return _counter;
        }
    }
}