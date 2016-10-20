using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

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
    }// Define a class with a property.
    public class TestClass
    {
        private string caption = "A Default caption";
        public string Caption
        {
            get { return caption; }
            set
            {
                if (caption != value)
                {
                    caption = value;
                }
            }
        }
    }



    class Program
    {
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

 
        static void Main(string[] args)
        {
            AssemblyName an = AssemblyName.GetAssemblyName(@"C:\Users\Administrator\Source\Repos\_nopcommerceLocalDemo\Tests\Nop.Core.Tests\bin\Debug\Nop.Core.dll");
            Console.WriteLine(an);
            Console.WriteLine(an.FullName);
            return;
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(item.FullName);
            }
            Console.WriteLine("================");
            System.Web.Compilation.BuildManager.AddReferencedAssembly(Assembly.Load(an));


            Console.WriteLine(Guid.NewGuid().ToString("N"));
            Console.WriteLine(Guid.NewGuid().ToString());
            var path = AppDomain.CurrentDomain.BaseDirectory + ("\\global.asax");
            var path2 = AppDomain.CurrentDomain.BaseDirectory + ("\\global2.asax");

            File.Copy(path, path2, true);

            var shadowCopiedPlug = new FileInfo(path);
            Console.WriteLine(shadowCopiedPlug.CreationTime);
            shadowCopiedPlug = new FileInfo(path2);
            Console.WriteLine(shadowCopiedPlug.CreationTime);

            return;

            foreach (var item in Environment.NewLine)
            {
                Console.WriteLine((int)item);
            }

            Nop.Core.Data.DataSettings setting = new Data.DataSettings();
            Console.WriteLine(setting.DataProvider == null);
            return;
            long x = 123123;
            Console.WriteLine((x is int) ? "is int" : "not int");

            Console.WriteLine(System.ComponentModel.TypeDescriptor.GetConverter(typeof(int)));
            string[] arr2 = new string[1];
            Console.WriteLine(arr2.Any());
            return;

            TestClass t = new TestClass();

            // Get the type and PropertyInfo.
            Type myType = t.GetType();
            PropertyInfo pinfo = myType.GetProperty("Caption");

            pinfo.SetValue(t, "HEllo WORK", new object[1]);
            Console.WriteLine(pinfo.GetValue(t));
            return;

            string str = "\"Helo&quot;";
            using (var sw = new StringWriter())
            using (var xwr = new XmlTextWriter(sw))
            {
                xwr.WriteString(str);
                Console.WriteLine(sw.ToString());
            }

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
