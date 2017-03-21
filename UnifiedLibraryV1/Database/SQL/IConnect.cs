using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Database.SQL{
    public interface IConnect<T,K,V> {
        void Connect();
        void Disconnect();
        void Add(T obj);
        void Add(Dictionary<K, V> kvPairs);
        void Remove(T obj);
        void Remove(Int32 index);
    }
}
