using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ra.Helpers;

namespace Ra.Tests
{
    [TestClass]
    public class AssemblyInitializer
    {


        [AssemblyInitialize]
        public void Init()
        {
            ConfigurationStore._configurationPath = System.AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
