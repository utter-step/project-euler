using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public struct CycleDetails<T>
    {
        public IEnumerable<T> CycleHead { get; private set; }
        public IEnumerable<T> CycleBody { get; private set; }

        public int CycleLength { get; private set; }

        private CycleDetails(IEnumerable<T> head, IEnumerable<T> body)
            : this()
        {
            CycleHead = head;
            CycleBody = body;
        }

        public CycleDetails(IEnumerable<T> head, IEnumerable<T> body, int cycleLength)
            : this(head, body)
        {
            CycleLength = cycleLength; 
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var elem in CycleHead)
            {
                sb.AppendFormat("{0} -> ", elem);
            }

            sb.Append("{ ");

            foreach (var elem in CycleBody)
            {
                sb.AppendFormat("{0} -> ", elem);
            }

            sb.AppendFormat("{0} -> ... ", CycleBody.First());

            sb.Append("}");

            return sb.ToString();
        }
    }

    public abstract class Cycle<T>
    {
        public CycleDetails<T> CycleDetails { get; private set; }

        protected readonly T _initialState;
        private T _currentState;

        protected Cycle(T initialState, bool instantCompute=true)
        {
            _initialState = initialState;
            _currentState = initialState;

            if (instantCompute)
            {
                ComputeCycle();
            }
        }

        protected virtual void ComputeCycle()
        {
            var set = new HashSet<T>();

            while (set.Add(_currentState))
            {
                NextState();
            }

            var cycle = set.ToList();

            int splitIndex = cycle.IndexOf(_currentState);

            var body = cycle.Skip(splitIndex);
            var head = cycle.Take(splitIndex);

            CycleDetails = new CycleDetails<T>(head, body, set.Count - splitIndex);
        }

        protected virtual void NextState()
        {
            _currentState = MakeStep(_currentState);
        }

        protected abstract T MakeStep(T state);

        public override string ToString()
        {
            return CycleDetails.ToString();
        }
    }
}
