using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Reflection;
using Cyber_Employee.Interface;
using System.Linq;

namespace CyberEmployee.Tests
{
    [TestClass]
    public class NinjectTest
    {
        [TestMethod]
        public void TestNinjectBindings()
        {
            //Create Kernel
            var kernel = new StandardKernel();

            var assembly = Assembly.Load("Cyber_Employee");

            kernel.Load(assembly);
            kernel.Get<IStaffManager>();
            kernel.Get<IProjectManager>();

            // best way to do this
            var interfaces = assembly.GetTypes()
                                    .Where(t => t.FullName.StartsWith("Cyber_Employee.Interface"))
                                    .Where(t => t.IsInterface).ToList();

            foreach (var iInterface in interfaces)
            {
                kernel.Get(iInterface);
            }
        }
    }
}
