using Nop.Core.Infrastructure;
using Nop.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication1
{
    class Program
    {
        public interface IConsumer<T>
        {
            void HandleEvent(T eventMessage);
        }

        public class TimeConsumer<T> : IConsumer<T>
        {
            public void HandleEvent(T eventMessage)
            {
                Console.WriteLine(eventMessage);
            }
        }

        public static bool MyInterfaceFilter(Type typeObj, Object criteriaObj)
        {
            var result = ((Type)criteriaObj).IsAssignableFrom(typeObj);
            return result;
        }

        static void Main(string[] args)
        {
            OfficialFeedManager om = new OfficialFeedManager();

            var method = typeof(OfficialFeedManager).GetMethod("GetDocument", BindingFlags.Static | BindingFlags.NonPublic);
            
            if (method != null)
            {
                foreach (var item in method.GetParameters())
              
                {
                    Console.WriteLine(item.ParameterType);
                }
                   XmlDocument doc = (XmlDocument)method.Invoke(null, new object[] { "getCategories=1", new object[0] });
                var xNodes = doc.SelectNodes(@"/categories/category").Cast<XmlNode>();
                foreach (var node in xNodes)
                {
                    Console.WriteLine(node.FirstChild.InnerText);
                } 
            }
            Console.WriteLine();
            List<OfficialFeedCategory> result = om.GetCategories().ToList();
            result.ForEach(x => Console.WriteLine(x.Name));
            return;

            TypeFilter myFilter = new TypeFilter(MyInterfaceFilter);
            AppDomainTypeFinder typeFinder = new AppDomainTypeFinder();
            var types = typeFinder.FindClassesOfType(typeof(IConsumer<>));


        }
    }
}
