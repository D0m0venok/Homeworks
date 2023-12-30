namespace VG.Utilites
{
    public struct Injected<T>
    {
        private T _value;
        private bool _inited;

        public T Value
        {
            get
            {
                if (!_inited)
                {
                    _inited = true;
                    _value = DI.Container.Get<T>();
                }
                return _value;
            }
        }
    }
}