using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class RandomHashSet<T>
    {
        HashSet<T> set;
        List<T> data;

        public RandomHashSet()
        {
            set = new HashSet<T>();
            data = new List<T>();
        }

        public bool Contains(T obj)
        {
            return set.Contains(obj);
        }

        public T RandomElement()
        {
            if (set.Count > 0)
            {
                Random rand = new Random();
                return data[rand.Next(set.Count)];
            }
            else return default(T);
        }
        public int Size()
        {
            return data.Count;
        }

        public void Add(T obj)
        {
            if (!set.Contains(obj))
            {
                set.Add(obj);
                data.Add(obj);
            }
        }

        public void Clear()
        {
            set.Clear();
            data.Clear();
        }

        public T Get(int index)
        {
            if (index < 0 || index >= data.Count) return default(T);
            return data[index];
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= data.Count) return;
            set.Remove(data[index]);
            data.Remove(data[index]);
        }
        
        public void Remove(T obj)
        {
            set.Remove(obj);
            data.Remove(obj);
        }

        public List<T> GetData()
        {
            return data;
        }

        public void AddSorted(Gene obj)
        {
            for (int i = 0; i < this.Size(); i++)
            {
                int innovation = ((Gene)(object)data[i]).GetInnovationNumber();
                if (obj.GetInnovationNumber() < innovation)
                {
                    data.Insert(i, (T)(object)obj);
                    set.Add((T)(object)obj);
                    return;
                }
            }
            data.Add((T)(object)obj);
            set.Add((T)(object)obj);
        }
    }
}
