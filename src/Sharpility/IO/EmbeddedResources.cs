using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using Sharpility.Base;

namespace Sharpility.IO
{
    public static class EmbeddedResources
    {
        public static ICollection<string> Resources(Type classLoader)
        {
            Predicate<string> acceptAll = element => true;
            return Resources(classLoader, acceptAll);
        }

        public static ICollection<string> Resources(Type classLoader, Predicate<string> filter)
        {
            var assembly = Assembly.GetAssembly(classLoader);
            var results = ImmutableList.CreateBuilder<string>();
            foreach (var resourceName in assembly.GetManifestResourceNames()
                .Where(resourceName => filter(resourceName)))
            {
                results.Add(resourceName);
            }
            return results
                .ToImmutable();
        }

        public static Stream LoadResource(Type classLoader, string resource)
        {
            var assembly = Assembly.GetAssembly(classLoader);
            var stream = assembly.GetManifestResourceStream(resource);
            Precognitions.IsNotNull(stream, 
                String.Format("Could not find resource: '{0}' at {1}", resource, classLoader));
            return stream;
        }

        public static string LoadResourceContent(Type classLoader, string resource)
        {
            using (var stream = LoadResource(classLoader, resource))
            {
                return Streams.ReadAll(stream);
            }
        }
    }
}
