using System;
using System.Collections.Generic;
using System.Linq;
using Hosting.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Models;
using RunAsWrapper.Core.Operations.Apps;
using Voodoo.TestData;

namespace Hosting.Tests.Operations.Apps
{


    namespace EngineeringInterface.Test.Business.Operations.Settings
    {
        [TestClass]
        public class ApplicationsMapperTests
        {
            private Randomizer randomizer = new Randomizer();

            [TestMethod]
            public void Map_MapBack_PropertiesTheSame()
            {
                TestHelper.SetRandomDataSeed(1);
                var source = new Application();
                randomizer.Randomize(source);
                var message = Mapper.Map(source);
                var target = new Application();
                Mapper.Map(message, target);

                Assert.AreEqual(source.Key, message.Key);

                var helper = new MappingTesterHelper<Application, AppMessage>();
                foreach (var property in helper.CommonProperties.Where(c => c.Name != "Id").ToArray())
                {
                    var original = property.GetValue(source, null);
                    var mapped = property.GetValue(target, null);
                    Assert.AreEqual(original, mapped, property.Name);
                }
            }
        }
    }
}
