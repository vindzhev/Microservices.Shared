namespace MicroservicesPOC.Shared.Extensions
{
    using System;
    
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    
    using MicroservicesPOC.Shared.Common.Models;
    
    public class QuestionAnswerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(QuestionAnswerDTO);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            return (jObject["type"].Value<int>()) switch
            {
                (int)QuestionType.Numeric => jObject.ToObject<NumericQuestionAnswerDTO>(serializer),
                (int)QuestionType.Text => jObject.ToObject<TextQuestionAnswerDTO>(serializer),
                (int)QuestionType.Choice => jObject.ToObject<ChoiceQuestionAnswerDTO>(serializer),
                _ => null,
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => serializer.Serialize(writer, value);
    }
}
