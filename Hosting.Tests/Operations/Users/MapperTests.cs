using System;
using System.Collections.Generic;
using System.Linq;
using Hosting.Operations.Users;
using Hosting.Tests.EngineeringInterface.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.TestData;
using ra.Models;

namespace Hosting.Tests.Operations.Users
{
    [TestClass]
    public class UserAccountsMapperTests
    {
        private Randomizer randomizer = new Randomizer();

        [TestMethod]
        public void Map_MapBack_PropertiesTheSame()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new UserAccount();
            randomizer.Randomize(source);
            var message = Mapper.Map(source);
            var target = new UserAccount();
            Mapper.Map(message, target);

            Assert.AreEqual(source.Key, message.Key);

            var helper = new MappingTesterHelper<UserAccount, UserMessage>();
            foreach (var property in helper.CommonProperties.Where(c => c.Name != "Id").ToArray())
            {
                var original = property.GetValue(source, null);
                var mapped = property.GetValue(target, null);
                Assert.AreEqual(original, mapped, property.Name);
            }
        }
    }
}