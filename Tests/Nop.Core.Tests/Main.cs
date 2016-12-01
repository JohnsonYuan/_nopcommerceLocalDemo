﻿using System;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Builder;
using System.Reflection;
using Nop.Core.Caching;
using System.Web;
using Nop.Core.Fakes;
using Nop.Core.Infrastructure;

namespace Nop.Core.Tests
{
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

        public class MagicClass
        {
            private int magicBaseValue;

            public MagicClass()
            {
                magicBaseValue = 9;
            }

            public int ItsMagic(int preMagic)
            {
                return preMagic * magicBaseValue;
            }
        }



        static void Main()
        {
            return;

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().AsSelf().As<IOutput>().InstancePerRequest();
            builder.RegisterType<TodayWriter>().As<IDateWriter>().InstancePerMatchingLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag); ;

            builder.Register(c => new FakeHttpContext("~")).As<HttpContextBase>().InstancePerLifetimeScope();

            builder.RegisterSource(new AutofacDemo.Features.AnyConcreteTypeNotAlreadyRegisteredSource());
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
