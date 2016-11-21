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

using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;
using System.Runtime.Caching;
using MaxMind.GeoIP2;
using System.Net;
using System.Collections.Specialized;
using System.Web.Compilation;
using System.ComponentModel;
using System.Linq.Expressions;

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

    public interface IFinder
    { void Log(string value); }

    public class AppFinder : IFinder
    { public void Log(string value) { Console.WriteLine(value); } }
    public class AppFinder2 : IFinder
    { public void Log(string value) { Console.WriteLine(value); } }

    public interface IRepo<T>
    { void Init(); }

    public class EfRepo<T> : IRepo<T>
    { public void Init() { Console.WriteLine("Ef repo"); } }

    class Program
    {
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        public static void Demo1(int x)
        {
            Console.WriteLine(x);
        }

        public static void Demo1<T>(int x)
        {
            Console.WriteLine("Generic");
            Console.WriteLine(x);
        }
        public static void mymethod(
              int int1m, out string str2m, ref string str3m)
        {
            str2m = "in mymethod";
        }

        public class DemoValue
        {
            public int MyProperty { get; set; }

            public override string ToString()
            {
                return MyProperty.ToString();
            }
        }

        public class DemoClass
        {
            private DemoValue _value;

            public DemoClass(DemoValue value)
            {
                this._value = value;
            }

            public DemoValue MyProperty { get { return _value; } set { _value = value; } }
        }

        private static string ToUnichar(string hexString)
        {
            var b = new byte[2];

            // Take hexadecimal as text and make a Unicode char number
            b[0] = Convert.ToByte(hexString.Substring(2, 2), 16);
            b[1] = Convert.ToByte(hexString.Substring(0, 2), 16);
            // Get the character the number represents
            var returnChar = Encoding.Unicode.GetString(b);
            return returnChar;
        }

        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public sealed class Singleton
        {
            public static string x = EchoAndReturn("In type initializer");

            public static string EchoAndReturn(string s)
            {
                Console.WriteLine(s);
                return s;
            }

            private static readonly Singleton instance = new Singleton();

            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            //static Singleton()
            //{
            //    Console.WriteLine("in static ctor");
            //}

            private Singleton()
            {
                Console.WriteLine("in ctor");
            }

            public static Singleton Instance
            {
                get
                {
                    return instance;
                }
            }
        }

        /// <summary>
        /// Gets IP addresses of the local computer
        /// </summary>
        public static string GetLocalIP()
        {
            string _IP = null;

            // Resolves a host name or IP address to an IPHostEntry instance.
            // IPHostEntry - Provides a container class for Internet host address information. 
            System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            // IPAddress class contains the address of a computer on an IP network. 
            foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
            {
                Console.WriteLine(_IPAddress.AddressFamily + " : " + _IPAddress.ToString());
                // InterNetwork indicates that an IP version 4 address is expected 
                // when a Socket connects to an endpoint
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    _IP = _IPAddress.ToString();
                }
            }
            return _IP;
        }


        private string AssemblySkipLoadingPattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";
        private string AssemblyRestrictToLoadingPattern = ".*";
        public virtual bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, AssemblySkipLoadingPattern)
                && Matches(assemblyFullName, AssemblyRestrictToLoadingPattern);
        }
        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public enum Color
        {
            Red,
            Green,
            Blue
        }

        static void Main(string[] args)
        {
            bool? bundleFiles = default(bool?);
            Console.WriteLine(bundleFiles.HasValue);

            return;

            var demoC = TypeDescriptor.GetConverter(typeof(int?));
            Console.WriteLine(demoC);

            Console.WriteLine(typeof(Nop.Data.Mapping.AffiliateMap.AffiliateMap).ToString());

            Console.WriteLine(typeof(Nop.Data.Mapping.AffiliateMap.AffiliateMap).Name);

            var _inputValues = new NameValueCollection();

            _inputValues.Add("a", "123123");
            _inputValues.Add("a", "hello");
            _inputValues.Add("a", "hello22");
            _inputValues.Add("sdfs", "sdfsfewr");

            Console.WriteLine(_inputValues["a"]);

            foreach (string item in _inputValues.Keys)
            {
                Console.WriteLine(item);
                string[] values = _inputValues.GetValues(item);
                foreach (var v in values)
                {
                    Console.WriteLine(v);
                }
                Console.WriteLine();
            }

            return;

            Console.WriteLine(3 / (float)2);
            return;
            List<int> all = new List<int>();
            List<int> child = new List<int>();
            all.AddRange(child);
            Console.WriteLine(all.Count);
            return;
            double price = 200;
            double percent = 40;
            var resultxx = price - (price) / (100 + percent) * percent;
            Console.WriteLine(resultxx);
            return;
            List<Pet> pets =
             new List<Pet>{ new Pet { Name="Barley", Age=8 },
                                new Pet { Name="Boots", Age=4 },
                                new Pet { Name="Whiskers", Age=1 } };


            List<Pet> pets2 = new List<Pet>();

            foreach (Pet pet in pets2.DefaultIfEmpty())
            {
                Console.WriteLine(pet.Name);
            }

            return;


            var a = new int[] { 1, 3, 4, 5, 7, 99, 123123 };
            DemoValue[] b = {
                new DemoValue { MyProperty = 3 },
                new DemoValue { MyProperty = 7 },
                new DemoValue { MyProperty = 99 },
                new DemoValue { MyProperty = 13212 },
                new DemoValue { MyProperty = 567 },
            };



            var resultff = from b1 in b
                           join a1 in a
                           on b1.MyProperty equals a1
                           //select b1;
                           into ab
                           //from sm in ab.DefaultIfEmpty()
                           from sm in ab.DefaultIfEmpty()
                           select sm;

            var query = from ax in resultff
                        select ax;
            foreach (var item in resultff)
            {
                Console.WriteLine(item.GetType() + " " + item);
            }

            return;
            var caching = System.Runtime.Caching.MemoryCache.Default;
            int cacheTime = 10;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            caching.Add(new System.Runtime.Caching.CacheItem("123", null), policy);

            if (caching.Contains("123"))
            {
                if (caching["123"] == null)
                {
                    Console.WriteLine("is null");
                }
            }

            return;
            File.WriteAllText(@"unicode2.txt", ToUnichar("01F6"), Encoding.Unicode);

            return;

            char cc = '中';
            Console.WriteLine(CharUnicodeInfo.GetUnicodeCategory(cc));

            Console.WriteLine(typeof(Nop.Data.EfDataProviderManager));
            Console.WriteLine(typeof(Nop.Data.EfDataProviderManager).Name);

            return;

            Hashtable ht = new Hashtable();
            BaseEntity search111 = new BaseEntity() { Id = 111 };
            BaseEntity search222 = new BaseEntity() { Id = 222 };

            Console.WriteLine(search111.GetHashCode());
            Console.WriteLine(search222.GetHashCode());
            ht.Add(search111, 100);
            ht.Add(search222, 200);


            if (ht.ContainsKey(new BaseEntity() { Id = 111 }))
            {
                Console.WriteLine("contains");
            }
            else
            {
                Console.WriteLine("not contains");
            }
            return;
            Console.WriteLine(System.Web.Mvc.HttpVerbs.Get);
            return;
            string path_ = @"E:\Source\Repos\nopCommerce_3.80_Beta_Source\Libraries\Nop.Core\Html\1.txt";

            File.WriteAllText(path_, "1231\n123\n12\r1123" + Environment.NewLine + "helo\r\n");
            string value_ = File.ReadAllText(path_);

            return;

            string text = @"<b>Hellosdf>>>>sdfsdf</b>123";
            var result = Regex.Replace(text, "(<[^>]*>)([^<]*)", "$2");
            Console.WriteLine("result: " + result);

            foreach (Match item in Regex.Matches(text, "(<[^>]*>)([^<]*)"))
            {
                Console.WriteLine("group1: " + item.Groups[1].Value);
                Console.WriteLine("group2: " + item.Groups[2].Value);
            }
            return;

            DemoValue value = new DemoValue();
            value.MyProperty = 123;

            DemoClass classV = new DemoClass(value);
            Console.WriteLine(classV.MyProperty);


            return;

            //Console.WriteLine("\nReflection.Parameterinfo");
            //Type Mytype = Type.GetType("Nop.Core.Tests.Program");
            //Console.WriteLine(Mytype);

            ////Get and display the method.
            //MethodBase Mymethodbase = Mytype.GetMethod("mymethod");
            //Console.Write("\nMymethodbase = " + Mymethodbase);

            ////Get the ParameterInfo array.
            //ParameterInfo[] Myarray = Mymethodbase.GetParameters();

            ////Get and display the ParameterInfo of each parameter.
            //foreach (ParameterInfo Myparam in Myarray)
            //{
            //    Console.Write("\nFor param eter # " + Myparam.Position
            //       + ", the ParameterType is - " + Myparam.ParameterType + ", " + Myparam.GetType());
            //}
            //return;

            //Demo1(22);
            //Demo1<string>(22);
            //Console.WriteLine();
            //return;
            string an2 = @"C:\Users\Administrator\Source\Repos\_nopcommerceLocalDemo\Tests\Nop.Core.Tests\bin\Debug\Nop.Core.dll";
            string an1 = @"E:\Source\Repos\nopCommerce_3.80_Beta_Source\Libraries\Nop.Core\bin\Debug\Autofac.dll";
            Console.WriteLine(AppDomain.CurrentDomain.GetAssemblies().Count());
            AssemblyName an = AssemblyName.GetAssemblyName(an1);
            Assembly.Load(an);
            Console.WriteLine(AppDomain.CurrentDomain.GetAssemblies().Count());
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
