namespace MicroservicesPOC.Shared.Common.ContractResolvers
{
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class ProtectedSettersContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (!prop.Writable)
            {
                var property = member as PropertyInfo;
                if (property != null)
                    prop.Writable = property.GetSetMethod(true) != null;
            }

            return prop;
        }
    }
}
