namespace Postman2CSharp.UI.Shared
{
    public ref struct First()
    {
        private bool _isFirst = true;
        public bool IsFirst => _isFirst && !(_isFirst = false);
    }
    public ref struct First<T>(Func<T, bool> predicate)
    {
        private bool _isFirst = true;
        public bool IsFirst(T item)
        {
            if (_isFirst == false) return false;
            return predicate(item) && !(_isFirst = false);
        }
    }
}
