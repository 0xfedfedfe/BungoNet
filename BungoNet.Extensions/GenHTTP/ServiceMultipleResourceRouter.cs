using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using GenHTTP.Api.Content;
using GenHTTP.Api.Protocol;

using GenHTTP.Modules.Conversion.Providers;
using GenHTTP.Modules.Reflection;
using GenHTTP.Modules.Webservices;

namespace BungoNet.Extensions.GenHTTP
{
    public sealed class ServiceMultipleResourceRouter : IHandler
    {

        #region Get-/Setters

        private MethodCollection Methods { get; }

        public IHandler Parent { get; }

        public SerializationRegistry Serialization { get; }

        public ResponseProvider ResponseProvider { get; }

        public List<object> Instances { get; }

        #endregion

        #region Initialization

        public ServiceMultipleResourceRouter(IHandler parent, List<object> instances, SerializationRegistry formats)
        {
            Parent = parent;

            Instances = instances;
            Serialization = formats;

            ResponseProvider = new(formats);

            Methods = new(this, AnalyzeMethods(instances));
        }

        private IEnumerable<Func<IHandler, MethodHandler>> AnalyzeMethods(List<object> instances)
        {
            foreach (var instance in instances)
            {
                var type = instance.GetType();

                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    var attribute = method.GetCustomAttribute<ResourceMethodAttribute>(true);

                    if (attribute is not null)
                    {
                        var path = PathArguments.Route(attribute.Path);

                        yield return (parent) => new MethodHandler(parent, method, path, () => instance, attribute, ResponseProvider.GetResponse, Serialization);
                    }
                }
            }
        }

        #endregion

        #region Functionality

        public ValueTask PrepareAsync() => Methods.PrepareAsync();

        public IEnumerable<ContentElement> GetContent(IRequest request) => Methods.GetContent(request);

        public ValueTask<IResponse?> HandleAsync(IRequest request) => Methods.HandleAsync(request);

        #endregion

    }

}
