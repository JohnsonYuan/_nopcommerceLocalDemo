using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nop.Data
{
    public class Hello2
    { }
    public class Hello1 : Hello2
    { }

    public class HelloClass<T> : IList<T>
    {
        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class DemoClass<T> : List<T>
    {
        private void Init(IEnumerable<T> source)
        {
            AddRange(source);
        }
    }

    public class DemoDemo
    {
        public static void Demo()
        {
            Console.WriteLine(Assembly.GetExecutingAssembly());

            //dynamically load all configuration
            //System.Type configType = typeof(LanguageMap);   //any of your configuration classes here
            //var typesToRegister = Assembly.GetAssembly(configType).GetTypes()

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(List<>));
            foreach (var type in typesToRegister)
            {
                //dynamic configurationInstance = Activator.CreateInstance(type);
                Console.WriteLine(type.BaseType.GetType());
                Console.WriteLine(type.BaseType.GetGenericTypeDefinition());
            }

            Console.WriteLine(typeof(Hello1).BaseType.GetType());
            Console.WriteLine(typeof(Hello1).BaseType.GetGenericTypeDefinition());
        }
    }

}
