using System;

namespace ExternalSorting.Optional
{
    public class Option<T>
    {
        public bool HasValue { get; private set; }

        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("Accessing empty value");
                return _value;
            }

            private set
            {
                HasValue = true;
                _value = value;

            }
        }
        private T _value;


        public Option()
        {
            HasValue = false;
        }

        public Option(T value)
        {
            HasValue = true;
            _value = value;
        }
    }
}