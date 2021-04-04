using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    partial class FrequencyTable<T>
    {
        private struct Enumerator : IEnumerator<T>
        {
            private T _Current;
            private int _Frequency;
            private int _Index;
            private readonly FrequencyTable<T> _Table;
            private readonly int _Version;

            public T Current => this._Current;
            Object IEnumerator.Current => this._Current;

            internal Enumerator(FrequencyTable<T> table)
            {
                this._Current = default (T);
                this._Frequency = 0;
                this._Index = -1;
                this._Table = table;
                this._Version = table._Version;
            }

            private void _CheckState()
            {
                if (this._Version != this._Table._Version)
                {
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");
                }
            }

            public void Dispose() { }

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