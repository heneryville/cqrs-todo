using Fasterflect;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Commands
{
    public class CommandConverter : JsonConverter<Command>
    {
        private static Dictionary<string, Type> commandsMap;

        static CommandConverter()
        {
            commandsMap = typeof(Command).Assembly
                                         .GetTypes()
                                         .Where(x => typeof(Command).IsAssignableFrom(x) && x != typeof(Command))
                                         .ToDictionary(DeriveCommandType, x => x);
        }

        private static Dictionary<Type, string> typeCache = new Dictionary<Type, string>();

        public static string DeriveCommandType(Type type)
        {
            var cached = "";
            if (typeCache.TryGetValue(type, out cached)) return cached;
            var name = type.Name;
            if (name.EndsWith("Command", StringComparison.OrdinalIgnoreCase)) name = name.Substring(0, name.Length - "command".Length);
            var regex = new Regex(@"([A-Z][a-z_]+)");
            var parts = regex.Matches(name);
            var normalizedTypeName = parts.First().Value + "." + string.Join("", parts.Skip(1).Select(x => x.Value));
            typeCache[type] = normalizedTypeName.ToLower();
            return normalizedTypeName.ToLower();
        }

        public override Command Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var obj = JsonDocument.ParseValue(ref reader);
            var key = obj.RootElement.EnumerateObject().Single(x => string.Equals(x.Name, "Type", StringComparison.OrdinalIgnoreCase)).Value.ToString();
            if (!commandsMap.ContainsKey(key.ToLower())) throw new Exception("Unknown command: " + key);
            var targetObj = commandsMap[key.ToLower()];

            var converter = options.GetConverter(targetObj) as JsonConverter<Command>;
            if(converter == null)
            {
                var text = obj.RootElement.GetRawText();
                return JsonSerializer.Deserialize(text, targetObj, options) as Command;
            }
            return converter.Read(ref reader, targetObj, options);
        }

        public override void Write(Utf8JsonWriter writer, Command value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Command).IsAssignableFrom(typeToConvert);
        }
    }
}
