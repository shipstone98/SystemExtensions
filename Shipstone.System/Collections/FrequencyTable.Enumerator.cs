using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    partial class FrequencyTable<T>
    {
        private class Enumerator : IEnumerator<T>
        {
            private T _Current;
            private int _Frequency;
            private int _Index;
            private bool _IsDisposed;
            internal bool _IsModified;
            private readonly FrequencyTable<T> _Table;

            public T Current => this._Current;
            Object IEnumerator.Current => this._Current;

            internal Enumerator(FrequencyTable<T> table)
            {
                this._Index = -1;
                this._Table = table;
                this._Table._Enumerators.Add(this);
            }

            private void _CheckState()
            {
                if (this._IsDisposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                if (this._IsModified)
                {
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");
                }
            }

            public void Dispose()
            {
                if (!this._IsDisposed)
                {
                    this._Table._Enumerators.Remove(this);
                    this._IsDisposed = true;
                }

                GC.SuppressFinalize(this);
            }

            public bool MoveNext()
            {
                this._CheckState();

                if (this._Index == -1)
                {
                    ++ this._Index;

                    if (this._Table._Items.Count == 0)
                    {
                        return false;
                    }

                    this._Current = this._Table._Items[this._Index];
                }


                else if (this._Index == this._Table._Items.Count)
                {
                    return false;
                }

                else if (++ this._Frequency == this._Table._Frequencies[this._Index])
                {
                    if (++ this._Index == this._Table._Items.Count)
                    {
                        return false;
                    }

                    this._Current = this._Table._Items[this._Index];
                    this._Frequency = 0;
                }

                return true;
            }
            
            public void Reset()
            {
                this._CheckState();
                this._Frequency = 0;
                this._Index = -1;
            }
        }
    }
}