namespace CRUDaster.Comparers
{
    //compares objects based on a key selector function
    public class PropertyEqualityComparer<T, TKey>(Func<T, TKey> keySelector) : IEqualityComparer<T>
    {
        private readonly Func<T, TKey> _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));

        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return EqualityComparer<TKey>.Default.Equals(_keySelector(x), _keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            if (obj is null) return 0;
            return EqualityComparer<TKey>.Default.GetHashCode(_keySelector(obj));
        }
    }
}
