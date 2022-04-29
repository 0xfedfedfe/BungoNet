using System;

using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Conversion.Providers;

namespace BungoNet.Serialization.BnetApi
{
    public sealed class BnetApiFormat : ISerializationFormat
    {
        public ValueTask<object?> DeserializeAsync(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IResponseBuilder> SerializeAsync(IRequest request, object response)
        {
            var result = request.Respond()
                                .Content(new BnetApiContent(response))
                                .Type(new FlexibleContentType(ContentType.TextPlain));

            return new ValueTask<IResponseBuilder>(result);
        }
    }
}
