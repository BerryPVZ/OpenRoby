using System;
using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang
{
    public abstract class GenericGenerator<T> : IEnumerable<T>, IEnumerable
    {
        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public abstract class GenericGeneratorEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        protected int _state;
        private T _current;

        public T Current => _current;
        object IEnumerator.Current => _current;

        protected bool Yield(int state, T value)
        {
            _state = state;
            _current = value;
            return true;
        }

        protected void YieldDefault(int state)
        {
            _state = state;
            _current = default(T);
        }

        public abstract bool MoveNext();
        public void Reset() => throw new NotSupportedException();
        public void Dispose() { }
    }
}
