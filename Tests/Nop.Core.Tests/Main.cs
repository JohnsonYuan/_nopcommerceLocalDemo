using System;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Builder;
using System.Reflection;
using Nop.Core.Caching;
using System.Web;
using Nop.Core.Fakes;
using Nop.Core.Infrastructure;
using System.Web.Routing;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Helpers.AntiXsrf;
using System.IO;
using System.Web.Security;

namespace Nop.Core.Tests
{
    public static class ByteExtentions
    {
        public static void OutputByteArray(this byte[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
                if (i == a.Length - 1)
                    Console.WriteLine();
            }
        }
    }

    class MainTest
    {
        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public void Output()
            {
                Console.WriteLine(Name + " : " + Age);
            }
        }
        public class HandlerFactory
        {
            public T GetHandler<T>() where T : struct
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }
        public class Person : Object
        {
            public String FirstName;
            public String LastName;

            public override String ToString()
            {
                return (FirstName + " " + LastName).Trim();
            }
        }

        public class Order
        {
            public int orDerid { get; set; }
            public int orderid { get; set; }
            public Customer Customer { get; set; }
            public decimal GetTotal()
            {
                return (decimal)2.9;
            }
        }
        public class Customer
        {
            public string Name { get; set; }
        }
        public class OrderDto
        {
            public int ORDERID2 { get; set; }
            public string CustomerName { get; set; }
            public decimal Total { get; set; }
            public List<int> TotalCounts { get; set; }
        }

        public static bool AreByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool areEqual = true;
            for (int i = 0; i < a.Length; i++)
            {
                areEqual &= (a[i] == b[i]);
            }
            return areEqual;
        }



        static void Main()
        {
            var cookieToken = new AntiForgeryToken()
            {
                // SecurityToken will be populated automatically.
                IsSessionToken = true
            };
            // Console.WriteLine(cookieToken.SecurityToken);

            byte[] newByte = new byte[]
            {
                1      ,
                36     ,
                255    ,
                6      ,
                166    ,
                145    ,
                62     ,
                221    ,
                232    ,
                38     ,
                 102   ,
                 166   ,
                 8     ,
                 143   ,
                 125   ,
                 165   ,
                 243   ,
                 0     ,
                 0     ,
                 0     ,
                 0     ,
            };

            string[] _purposes = new string[] { "System.Web.Helpers.AntiXsrf.AntiForgeryToken.v1" };

            var test1 = MachineKey.Protect(newByte, _purposes);
            var test2 = MachineKey.Protect(newByte, _purposes);


            Console.WriteLine(AreByteArraysEqual(test1, test2)); ;

            var x1 = HttpServerUtility.UrlTokenEncode(MachineKey.Protect(newByte, _purposes));
            var x2 = HttpServerUtility.UrlTokenEncode(MachineKey.Protect(newByte, _purposes));

            Console.WriteLine(x1);
            Console.WriteLine(x2);
            var x3 = MachineKey.Unprotect(HttpServerUtility.UrlTokenDecode(x1), _purposes);
            var x4 =MachineKey.Unprotect(HttpServerUtility.UrlTokenDecode(x2), _purposes);

            Console.WriteLine(x3);
            Console.WriteLine(x4);

            Console.WriteLine(AreByteArraysEqual(x3, x4)); ;


            return;
            AntiForgeryToken formToken = new AntiForgeryToken()
            {
                SecurityToken = cookieToken.SecurityToken,
                IsSessionToken = false
            };
            AntiForgeryTokenSerializer serializer = new AntiForgeryTokenSerializer(new MachineKey45CryptoSystem());
            var formVal1 = serializer.Serialize(formToken);
            Console.WriteLine(formVal1);
            //Console.WriteLine(formToken.ClaimUid);
            serializer.Deserialize(formVal1).SecurityToken.GetData().OutputByteArray();
            Console.WriteLine();
            Console.WriteLine();
            var formVal2 = serializer.Serialize(formToken);
            Console.WriteLine(formVal2);
           // Console.WriteLine(formToken.ClaimUid);
            serializer.Deserialize(formVal2).SecurityToken.GetData().OutputByteArray();
            Console.WriteLine();
            Console.WriteLine();
            var cookieVal = serializer.Serialize(cookieToken);
            Console.WriteLine(cookieVal);
            serializer.Deserialize(cookieVal).SecurityToken.GetData().OutputByteArray();

            var cookieVal2 = serializer.Serialize(cookieToken);
            Console.WriteLine(cookieVal2);
            serializer.Deserialize(cookieVal2).SecurityToken.GetData().OutputByteArray();
            

            return;
 
            //string cookieValue = "emTIesw2uxjqSIp8y67CHsMc_cxMxEJMO-CVy3tQLnJQPD2Fjywsi8k1hppsm8XkM5T0ZU8q0WUP4dgk7IngZl3jzDZuBt97n0a5-PUENqk1";
            //string fromValue1 = "";
            //string fromValue2 = "";
            ////var cookieToken = serializer.Deserialize(cookieValue);
            ////var formToken2 = serializer.Deserialize("ZgFqPlKwjNHB1imWLFWOhfxo-MXpLk4hr2J1yhmPt12dPsEgyREn3VO1IMjqUcZ4gkHV6dORMDMkORxsGhaB_yGU_iJ8-ozRyGnNHtwRHLYEtpTi0");
            //var formToken = serializer.Deserialize("UMTdP94vuMVKtLnzzxrGVLt7lVM_51-psWSI79cGa6gGn1KDRwCDJ8j9Z-O4am9i6pD06jQX5fALSxxqsQB-3L4DHwYudWoQoOApexREUz6P_nOECzbdgQlK5-kX9Xkd0");
            //Console.WriteLine(formToken.SecurityToken);
            ////Console.WriteLine(formToken2.SecurityToken);
            ////Console.WriteLine(cookieToken.SecurityToken);
            //return;

            var builderx = new ContainerBuilder();
            builderx.RegisterType<ConsoleOutput> ().As< IOutput> ().InstancePerLifetimeScope();
            var containerx = builderx.Build();
            using (var scope1 = containerx.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
            {
                var outputer = scope1.Resolve<IOutput>();
                outputer.Write("hello");
            }

            return; 
            var str = "[quote=(yuan)]123123";
            str = Regex.Replace(str, @"\[quote=(.+?)\]", String.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Console.WriteLine(str);
            str = "[quote=(yuan)]123123";
            Console.WriteLine(Regex.Matches(str, @"\[quote=(.+?)\]").Count);
            foreach (Match item in Regex.Matches(str, @"\[quote=(.+?)\]"))
            {
                Console.WriteLine(item.Groups.Count);
            }
            return;
            string sentence = "the quick brown fox jumps over the lazy dog";

            // Split the string into individual words.
            string[] words = sentence.Split(' ');

            // Prepend each word to the beginning of the 
            // new sentence to reverse the word order.
            string reversed = words.Aggregate((workingSentence, next) =>
            {
                Console.WriteLine("current: " + workingSentence + "\t" + next);
                return next + " " + workingSentence;
            });

            Console.WriteLine(reversed);

            //Mapper.Configuration.AssertConfigurationIsValid();
            
            return;
            // Lambda expression as executable code.
            Func<int, bool> deleg = i => i < 5;
            // Invoke the delegate and display the output.
            Console.WriteLine("deleg(4) = {0}", deleg(4));

            // Lambda expression as data in the form of an expression tree.
            System.Linq.Expressions.Expression<Func<int, bool>> expr = i => i < 5;
            // Compile the expression tree into executable code.
            Func<int, bool> deleg2 = expr.Compile();
            // Invoke the method and print the output.
            Console.WriteLine("deleg2(4) = {0}", deleg2(4));
            return;
            var routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.NivoSlider.Controllers"},
                {"area", null},
                {"widgetZone", "home_top"}
            };

            var props = TypeDescriptor.GetProperties(routeValues);

            foreach (PropertyDescriptor item in props)
            {
                Console.WriteLine(item.Name + " " + item.GetValue(routeValues).ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
            var props2 = routeValues.GetType().GetProperties();
            foreach (var item in props2)
            {
                Console.WriteLine(item.Name + " " + item.GetValue(routeValues).ToString());
            }
            return;

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().AsSelf().As<IOutput>().InstancePerRequest();
            builder.RegisterType<TodayWriter>().As<IDateWriter>().InstancePerMatchingLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag); ;

            builder.Register(c => new FakeHttpContext("~")).As<HttpContextBase>().InstancePerLifetimeScope();

            //builder.RegisterSource(new AutofacDemo.Features.AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerLifetimeScope();

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();

            var container = builder.Build();
            using (var scopeContainer = container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
            {
                //var myComponent = scopeContainer.Resolve<MyComponent>();
                var myComponent = scopeContainer.Resolve<ConsoleOutput>();
                var pets = scopeContainer.Resolve<Pet>();
                pets.Output();


                // test for cache
                var iCache = scopeContainer.Resolve<ICacheManager>();
                Console.WriteLine(iCache.GetType());

                iCache = scopeContainer.ResolveNamed<ICacheManager>("nop_cache_static");
                Console.WriteLine(iCache.GetType());

                try
                {
                    iCache = scopeContainer.ResolveNamed<ICacheManager>("nop_cache_static11");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return;

            HandlerFactory hf = new HandlerFactory();
            var method = typeof(HandlerFactory).GetMethod("GetHandler");
            Console.WriteLine(method);
            method = method.MakeGenericMethod(typeof(int));
            Console.WriteLine(method.ToString());
            Console.WriteLine(method);
            int x = 3;
            var result = method.Invoke(hf, null);
            Console.WriteLine(method.IsGenericMethod);
            return;
        }

    }


    // This interface helps decouple the concept of
    // "writing output" from the Console class. We
    // don't really "care" how the Write operation
    // happens, just that we can write.
    public interface IOutput
    {
        void Write(string content);
    }

    // This implementation of the IOutput interface
    // is actually how we write to the Console. Technically
    // we could also implement IOutput to write to Debug
    // or Trace... or anywhere else.
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }// This interface decouples the notion of writing
     // a date from the actual mechanism that performs
     // the writing. Like with IOutput, the process
     // is abstracted behind an interface.
    public interface IDateWriter
    {
        void WriteDate();
    }

    // This TodayWriter is where it all comes together.
    // Notice it takes a constructor parameter of type
    // IOutput - that lets the writer write to anywhere
    // based on the implementation. Further, it implements
    // WriteDate such that today's date is written out;
    // you could have one that writes in a different format
    // or a different date.
    public class TodayWriter : IDateWriter
    {
        private IOutput _output;
        public TodayWriter(IOutput output)
        {
            this._output = output;
        }

        public void WriteDate()
        {
            this._output.Write(DateTime.Today.ToShortDateString());
        }
    }

}
