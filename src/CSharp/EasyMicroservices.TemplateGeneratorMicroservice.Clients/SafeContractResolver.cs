using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Clients
{
    internal class SafeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProp = base.CreateProperty(member, memberSerialization);
            jsonProp.Required = Required.Default;
            return jsonProp;
        }
    }

    internal class MyJsonSerializerSettings : JsonSerializerSettings
    {
        public MyJsonSerializerSettings(JsonSerializerSettings settings)
        {
            this.ContractResolver = new SafeContractResolver();
        }
    }

    internal class MyHttpClient : HttpClient
    {
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
