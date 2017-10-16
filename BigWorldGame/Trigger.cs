using System;

namespace BigWorldGame
{
    public class Trigger<T>
    {
        private T oldState = default(T);

        public Trigger()
        {
            
        }

        public Trigger(T startState)
        {
            oldState = startState;
        }
        
        public bool IsChanged(T state)
        {
            var result = !state.Equals(oldState);

            oldState = state;

            return result;
        }

        public bool IsChanged(T state, Func<T, bool> testFunc)
        {
            return IsChanged(state) && testFunc(state);
        }
    }
}