using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Tests
{// Create a class having six properties.
    public class PropertyClass
    {
        public String Property1
        {
            get { return "hello"; }
        }
        public static String PropertyStatic
        {
            get { return "HelloI"; }
        }
        public String Property2
        {
            get { return "hello"; }
        }

        protected String Property3
        {
            get { return "hello"; }
        }

        private Int32 Property4
        {
            get { return 32; }
        }

        internal String Property5
        {
            get { return "value"; }
        }

        protected internal String Property6
        {
            get { return "value"; }
        }
    }

    class BaseEntity1 : BaseEntity
    {
    }
    class BaseEntity2 : BaseEntity
    {
    }

    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(PropertyClass);
            // Get the public properties.
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Console.WriteLine(propInfos.Length);
            return;

            BaseEntity1 be1 = new BaseEntity1();
            BaseEntity1 be11 = new BaseEntity1(); 
            Console.WriteLine(be1.GetHashCode());
            Console.WriteLine(be11.GetHashCode());
            return;
            BaseEntity2 be2 = new BaseEntity2();
            Dictionary<BaseEntity1, int> dic = new Dictionary<BaseEntity1, int>();
            dic.Add(be1, 1);
            dic.Add(be11, 2);
        }
    }
}
