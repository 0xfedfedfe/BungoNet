using System;

using System.Diagnostics.CodeAnalysis;

namespace BungoNet.Extensions.GenHTTP
{
    /// <summary>
    /// Entry point to add webservice resources to another router.
    /// </summary>
    public static class ServiceMultipleResource
    {
        /// <summary>
        /// Provides a router that will invoke the methods of the
        /// specified resource type to generate responses.
        /// </summary>
        /// <typeparam name="T">The resource type to be provided</typeparam>
        public static ServiceMultipleResourceBuilder From<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>() where T : new() => new ServiceMultipleResourceBuilder().AddType<T>();

        /// <summary>
        /// Provides a router that will invoke the methods of the
        /// specified resource instance to generate responses.
        /// </summary>
        /// <param name="instance">The instance to be provided</param>
        public static ServiceMultipleResourceBuilder From(object instance) => new ServiceMultipleResourceBuilder().AddInstance(instance);
    }
}
