using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo;

namespace Hosting.Tests
{
    namespace EngineeringInterface.Test
    {
        public class MappingTesterHelper<TSource, TTarget>
        {
            protected TSource source;
            protected TTarget target;

            public PropertyInfo[] SourceProperties
            {
                get { return typeof (TSource).GetProperties().Where(c => c.PropertyType.IsScalar()).ToArray(); }
            }

            public PropertyInfo[] TargetProperties
            {
                get { return typeof (TTarget).GetProperties().Where(c => c.PropertyType.IsScalar()).ToArray(); }
            }

            public PropertyInfo[] CommonProperties
            {
                get
                {
                    var sourceProperties = SourceProperties.Select(c => c.Name).ToArray();
                    var targetProperties = TargetProperties.Select(c => c.Name).ToArray();
                    var common = sourceProperties.Intersect(targetProperties).ToArray();
                    return SourceProperties.Where(c => common.Contains(c.Name)).ToArray();
                }
            }
        }
    }
}