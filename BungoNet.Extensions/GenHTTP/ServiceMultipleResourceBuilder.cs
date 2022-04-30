using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GenHTTP.Api.Content;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Modules.Conversion;
using GenHTTP.Modules.Conversion.Providers;

namespace BungoNet.Extensions.GenHTTP
{
    public sealed class ServiceMultipleResourceBuilder : IHandlerBuilder<ServiceMultipleResourceBuilder>
    {
        private readonly List<object> _Instances = new();

        private IBuilder<SerializationRegistry>? _Formats;

        private readonly List<IConcernBuilder> _Concerns = new();

        #region Functionality

        public ServiceMultipleResourceBuilder AddType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>() where T : new() => AddInstance(new T());

        public ServiceMultipleResourceBuilder AddInstance(object instance)
        {
            _Instances.Add(instance);
            return this;
        }

        public ServiceMultipleResourceBuilder Formats(IBuilder<SerializationRegistry> registry)
        {
            _Formats = registry;
            return this;
        }

        public ServiceMultipleResourceBuilder Add(IConcernBuilder concern)
        {
            _Concerns.Add(concern);
            return this;
        }

        public IHandler Build(IHandler parent)
        {
            var formats = (_Formats ?? Serialization.Default()).Build();

            var instances = _Instances ?? throw new BuilderMissingPropertyException("instances");

            return Concerns.Chain(parent, _Concerns, (p) => new ServiceMultipleResourceRouter(p, instances, formats));
        }

        #endregion

    }
}
