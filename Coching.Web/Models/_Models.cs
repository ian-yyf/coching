using Public.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coching.Web.Models
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTime.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(this.DateTimeFormat));
    }

    public class DateTimeNullConverter : JsonConverter<DateTime?>
    {
        public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : DateTime.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
            => writer.WriteStringValue(value?.ToString(this.DateTimeFormat));
    }
    
    public class PopupItemViewModel<T, R> : ItemViewModel<T>
    {
        public PopupItemViewModel()
        {

        }

        public PopupItemViewModel(string actionName, string actionTitle, string callback)
            : base(actionName, actionTitle)
        {
            Callback = callback;
        }

        public PopupItemViewModel(Guid keyGuid, string actionName, string actionTitle, T oldData, string callback)
            : base(keyGuid, actionName, actionTitle, oldData, null)
        {
            Callback = callback;
        }

        public R Result { get; set; }
        public string Callback { get; set; }

        public bool Success
        {
            get
            {
                return Result != null;
            }
        }

        public string getResult()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new DateTimeNullConverter());
            return JsonSerializer.Serialize(Result, options);
        }
    }
}
