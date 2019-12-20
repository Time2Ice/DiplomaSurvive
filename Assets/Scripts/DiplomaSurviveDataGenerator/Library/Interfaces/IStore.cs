using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface IStore<T, TGetParam> : IStore<T>
    {
        T Get(TGetParam param);
    }

    public interface IStore<T>
    {
        T Get();
        void Add(T element);
        T GetByIndex(int index);
        ICollection<T> GetAll();
        bool Remove(T element);
    }
}
