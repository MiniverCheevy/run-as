using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo;

namespace Hosting.CodeGeneration
{
    public class CodeWalker
    {
        public const string Namespace = "Hosting.Controllers";
        public List<Resource> Resources { get; set; }
        public CodeWalker()
        {
           Resources = new List<Resource>();
            var assembly = typeof (CodeWalker).Assembly;
            var types = assembly.GetTypes();
            var interestingTypes =
                types.Where(c => c.GetCustomAttributes(typeof (RestAttribute), false).Any()).ToArray();

            var resources = interestingTypes.ToLookup(c => c.GetCustomAttributes(typeof(RestAttribute), false).First().To<RestAttribute>().Resource,
                                                   c =>
                                                   buildVerb(
                                                       c.GetCustomAttributes(typeof (RestAttribute), false).First(), c));
            foreach (var key in resources.Select(c => c.Key).ToArray())
            {
                var resource = new Resource() { Name = key, Namespace = Namespace , ClassName = string.Format("{0}Controller",key)};
                resource.Verbs.AddRange(resources[key]);
                Resources.Add(resource);
            }
        }

        public static Dictionary<Verb, RestMethod> Methods
        {
            get
            {
                return new Dictionary<Verb, RestMethod>()
                    {
                        {Verb.Get, new RestMethod() {Attribute = "[HttpGet]", Name = "Get", Parameter = "[FromUri]"}},
                        {
                            Verb.Post, new RestMethod() {Attribute = "[HttpPost]", Name = "Post", Parameter = "[FromUri]"}
                        },
                        {Verb.Put, new RestMethod() {Attribute = "[HttpPut]", Name = "Put", Parameter = "[FromUri]"}},
                        {
                            Verb.Delete,
                            new RestMethod() {Attribute = "[HttpDelete]", Name = "Delete", Parameter = string.Empty}
                        },
                    };
            }
        }        

        private RestMethod buildVerb(object attributeObject, Type operationType)
        {
            var attribute = attributeObject.To<RestAttribute>();
            var baseType = operationType.BaseType;
            var type = operationType;

            while (type.BaseType != null && type.GetGenericArguments().Count() != 2)
            {
                type = type.BaseType;
            }

            var typeArguments = type.GetGenericArguments();
            var verb =  Methods[attribute.Verb];
            verb.RequestTypeName = FixUpTypeNamex(typeArguments[0]);
            verb.ResponseTypeName = FixUpTypeNamex(typeArguments[1]);
            verb.OperationTypeName = FixUpTypeNamex(operationType);
            return verb;
        }


        public static string FixUpTypeNamex(Type type)
        {
            var result = type.FullName;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                result = string.Format("{0}?", Nullable.GetUnderlyingType(type).FullName);
            }

            else if (type.IsGenericType)
            {
                var inner = string.Empty;
                foreach (var t in type.GetGenericArguments())
                {
                    if (t.IsGenericType)
                    {
                        var outer1 = t.GetGenericTypeDefinition().FullName;
                        var ary1 = outer1.Split(@"`".ToCharArray());
                        outer1 = ary1[0];

                        var inner1 = string.Empty;
                        foreach (var t1 in t.GetGenericArguments())
                        {
                            inner1 += t1.FullName;
                            inner1 += ",";
                        }
                        inner1 = inner1.TrimEnd(",".ToCharArray());
                        inner += string.Format("{1}<{0}>", inner1, outer1);
                    }
                    else
                    {
                        inner += t.FullName;
                        inner += ",";
                    }
                }
                inner = inner.TrimEnd(",".ToCharArray());
                var name = type.GetGenericArguments()[0].FullName;
                var outer = type.GetGenericTypeDefinition().FullName;
                var ary = outer.Split(@"`".ToCharArray());
                outer = ary[0];
                result = string.Format("{1}<{0}>", inner, outer);
            }
            else
            {
                return result;
            }


            return result;
        }
        
        [Serializable]
        public class Resource
        {
            public string Namespace { get; set; }
            public string ClassName { get; set; }
            public List<RestMethod> Verbs { get; set; }
            public string Name { get; set; }

            public Resource()
            {
                Verbs = new List<RestMethod>();
            }
        }
        [Serializable]
        public class RestMethod
        {
            public Verb Method { get; set; }
            public string RequestTypeName { get; set; }
            public string ResponseTypeName { get; set; }
            public string OperationTypeName { get; set; }
            public string Attribute { get; set; }
            public string Name { get; set; }
            public string Parameter { get; set; }
        }
    }
}