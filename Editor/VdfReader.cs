using System;

namespace Gameloop.Vdf
{
    public abstract class VdfReader : IDisposable
    {
        protected const int MaximumTokenSize = 4096;

        public VdfSerializerSettings Settings { get; }
        public bool CloseInput { get; set; }
        public string Value { get; set; }

        protected internal State CurrentState { get; protected set; }

        protected VdfReader() : this(VdfSerializerSettings.Default) { }

        protected VdfReader(VdfSerializerSettings settings)
        {
            Settings = settings;

            CurrentState = State.Start;
            Value = null;
            CloseInput = true;
        }

        public abstract bool ReadToken();

        void IDisposable.Dispose()
        {
            if (CurrentState == State.Closed)
                return;

            Close();
        }

        public virtual void Close()
        {
            CurrentState = State.Closed;
            Value = null;
        }

        protected internal enum State
        {
            Start,
            Property,
            Object,
            Conditional,
            Finished,
            Closed
        }
    }
}
