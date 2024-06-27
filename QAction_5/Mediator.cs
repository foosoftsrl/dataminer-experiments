/// Generated here: https://app.quicktype.io/?l=csharp
namespace Mediator
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Welcome
    {
        [JsonProperty("PharosCs")]
        public PharosCs PharosCs { get; set; }
    }

    public partial class PharosCs
    {
        [JsonProperty("CommandList")]
        public CommandList CommandList { get; set; }
    }

    public partial class CommandList
    {
        [JsonProperty("Command")]
        public Command[] Command { get; set; }
    }

    public partial class Command
    {
        [JsonProperty("GeneralType")]
        public string GeneralType { get; set; }

        [JsonProperty("Subsystem")]
        public string Subsystem { get; set; }

        [JsonProperty("Method")]
        public string Method { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Context")]
        public string Context { get; set; }

        [JsonProperty("ExecutionTime")]
        public double ExecutionTime { get; set; }

        [JsonProperty("Output")]
        public Output Output { get; set; }
    }

    public partial class Output
    {
        [JsonProperty("ResultSet")]
        public ResultSet ResultSet { get; set; }
    }

    public partial class ResultSet
    {
        [JsonProperty("RowCount")]
        public long RowCount { get; set; }

        [JsonProperty("Rows")]
        public Row[] Rows { get; set; }
    }

    public partial class Row
    {
        [JsonProperty("Status")]
        public Status Status { get; set; }

        [JsonProperty("StartDateTime")]
        public StartDateTime StartDateTime { get; set; }

        [JsonProperty("InFadeRate")]
        public InFadeRate InFadeRate { get; set; }

        [JsonProperty("InTransitionName")]
        public InTransitionName InTransitionName { get; set; }

        [JsonProperty("ScheduleReference")]
        public InTransitionName ScheduleReference { get; set; }

        [JsonProperty("State")]
        public InTransitionName State { get; set; }

        [JsonProperty("TrimMaterialId")]
        public InTransitionName TrimMaterialId { get; set; }

        [JsonProperty("Title")]
        public InTransitionName Title { get; set; }

        [JsonProperty("Duration")]
        public Duration Duration { get; set; }

        [JsonProperty("id")]
        public InFadeRate Id { get; set; }

        [JsonProperty("TemplateParameterList")]
        public RowTemplateParameterList TemplateParameterList { get; set; }
    }

    public partial class Duration
    {
        [JsonProperty("GenericList")]
        public DurationGenericList GenericList { get; set; }
    }

    public partial class DurationGenericList
    {
        [JsonProperty("Size")]
        public long Size { get; set; }

        [JsonProperty("Object")]
        public PurpleObject[] Object { get; set; }
    }

    public partial class PurpleObject
    {
        [JsonProperty("Nanos")]
        public long Nanos { get; set; }

        [JsonProperty("Rate")]
        public Rate Rate { get; set; }

        [JsonProperty("Time")]
        public string Time { get; set; }
    }

    public partial class InFadeRate
    {
        [JsonProperty("GenericList")]
        public InFadeRateGenericList GenericList { get; set; }
    }

    public partial class InFadeRateGenericList
    {
        [JsonProperty("Size")]
        public long Size { get; set; }

        [JsonProperty("Object")]
        public long[] Object { get; set; }
    }

    public partial class InTransitionName
    {
        [JsonProperty("GenericList")]
        public InTransitionNameGenericList GenericList { get; set; }
    }

    public partial class InTransitionNameGenericList
    {
        [JsonProperty("Size")]
        public long Size { get; set; }

        [JsonProperty("Object")]
        public string[] Object { get; set; }
    }

    public partial class StartDateTime
    {
        [JsonProperty("GenericList")]
        public StartDateTimeGenericList GenericList { get; set; }
    }

    public partial class StartDateTimeGenericList
    {
        [JsonProperty("Size")]
        public long Size { get; set; }

        [JsonProperty("Object")]
        public FluffyObject[] Object { get; set; }
    }

    public partial class FluffyObject
    {
        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("NanoOfDay")]
        public long NanoOfDay { get; set; }

        [JsonProperty("Rate")]
        public Rate Rate { get; set; }

        [JsonProperty("ISO8601")]
        public string Iso8601 { get; set; }
    }

    public partial class Status
    {
        [JsonProperty("GenericList")]
        public StatusGenericList GenericList { get; set; }
    }

    public partial class StatusGenericList
    {
        [JsonProperty("Size")]
        public long Size { get; set; }

        [JsonProperty("Object")]
        public TentacledObject[] Object { get; set; }
    }

    public partial class TentacledObject
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("ChainApparatusLocator")]
        public string ChainApparatusLocator { get; set; }

        [JsonProperty("MachineStatus")]
        public MachineStatus MachineStatus { get; set; }

        [JsonProperty("PlaytimeTransferStatus")]
        public string PlaytimeTransferStatus { get; set; }

        [JsonProperty("SequenceId")]
        public long SequenceId { get; set; }

        [JsonProperty("TransferStatus")]
        public string TransferStatus { get; set; }

        [JsonProperty("TxMedia")]
        public string TxMedia { get; set; }
    }

    public partial class RowTemplateParameterList
    {
        [JsonProperty("GenericList")]
        public TemplateParameterListGenericList GenericList { get; set; }
    }

    public partial class TemplateParameterListGenericList
    {
        [JsonProperty("Size")]
        public long Size { get; set; }

        [JsonProperty("Object")]
        public TemplateParameterListElement[] Object { get; set; }
    }

    public partial class TemplateParameter
    {
        [JsonProperty("GeneralType")]
        public GeneralType GeneralType { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Value")]
        public ValueUnion Value { get; set; }

        [JsonProperty("Type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("Runtime", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Runtime { get; set; }
    }

    public partial class ValueClass
    {
        [JsonProperty("TemplateParameterListCompound")]
        public TemplateParameterListCompound TemplateParameterListCompound { get; set; }
    }

    public partial class TemplateParameterListCompound
    {
        [JsonProperty("TemplateParameterList")]
        public TemplateParameterListElement[] TemplateParameterList { get; set; }
    }

    public partial class TemplateParameterListElement
    {
        [JsonProperty("TemplateParameter")]
        public TemplateParameter[] TemplateParameter { get; set; }
    }

    public enum Rate { Ndf25 };

    public enum MachineStatus { Done, Error, Play, PlayCued, Unknown };

    public enum GeneralType { String, TemplateParameterListCompound };

    public enum TypeEnum { TemplateParameter };

    public static class TemplateParameterName
    {
        public static readonly string SegmentationUpid = "segmentationUpid";
        public static readonly string EnablerLegacyUserText1 = "enablerLegacy-userText1";
        public static readonly string AdSalesContentReconcileKeyText = "adSalesContentReconcileKey-text";
        public static readonly string ScteBroadcastBreakStartInsertSegmentationDescriptor = "scteBroadcastBreakStart-insertSegmentationDescriptor";
        public static readonly string ScteBroadcastProviderAdvStartInsertSegmentationDescriptor = "scteBroadcastProviderAdvStart-insertSegmentationDescriptor";
        public static readonly string EnablerLegacyCompoundList = "enablerLegacy-compoundList";
    }

    public partial struct ValueUnion
    {
        public string String;
        public ValueClass ValueClass;

        public static implicit operator ValueUnion(string String) => new ValueUnion { String = String };
        public static implicit operator ValueUnion(ValueClass ValueClass) => new ValueUnion { ValueClass = ValueClass };
    }

    public static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                RateConverter.Singleton,
                MachineStatusConverter.Singleton,
                GeneralTypeConverter.Singleton,
                TypeEnumConverter.Singleton,
                ValueUnionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class RateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Rate) || t == typeof(Rate?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "NDF25")
            {
                return Rate.Ndf25;
            }
            throw new Exception("Cannot unmarshal type Rate");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Rate)untypedValue;
            if (value == Rate.Ndf25)
            {
                serializer.Serialize(writer, "NDF25");
                return;
            }
            throw new Exception("Cannot marshal type Rate");
        }

        public static readonly RateConverter Singleton = new RateConverter();
    }

    internal class MachineStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MachineStatus) || t == typeof(MachineStatus?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Done":
                    return MachineStatus.Done;
                case "Error":
                    return MachineStatus.Error;
                case "Play":
                    return MachineStatus.Play;
                case "PlayCued":
                    return MachineStatus.PlayCued;
                case "Unknown":
                    return MachineStatus.Unknown;
            }
            throw new Exception("Cannot unmarshal type MachineStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MachineStatus)untypedValue;
            switch (value)
            {
                case MachineStatus.Done:
                    serializer.Serialize(writer, "Done");
                    return;
                case MachineStatus.Error:
                    serializer.Serialize(writer, "Error");
                    return;
                case MachineStatus.Play:
                    serializer.Serialize(writer, "Play");
                    return;
                case MachineStatus.PlayCued:
                    serializer.Serialize(writer, "PlayCued");
                    return;
                case MachineStatus.Unknown:
                    serializer.Serialize(writer, "Unknown");
                    return;
            }
            throw new Exception("Cannot marshal type MachineStatus");
        }

        public static readonly MachineStatusConverter Singleton = new MachineStatusConverter();
    }

    internal class GeneralTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GeneralType) || t == typeof(GeneralType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "String":
                    return GeneralType.String;
                case "TemplateParameterListCompound":
                    return GeneralType.TemplateParameterListCompound;
            }
            throw new Exception("Cannot unmarshal type GeneralType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GeneralType)untypedValue;
            switch (value)
            {
                case GeneralType.String:
                    serializer.Serialize(writer, "String");
                    return;
                case GeneralType.TemplateParameterListCompound:
                    serializer.Serialize(writer, "TemplateParameterListCompound");
                    return;
            }
            throw new Exception("Cannot marshal type GeneralType");
        }

        public static readonly GeneralTypeConverter Singleton = new GeneralTypeConverter();
    }


    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "template parameter")
            {
                return TypeEnum.TemplateParameter;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.TemplateParameter)
            {
                serializer.Serialize(writer, "template parameter");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class ValueUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ValueUnion) || t == typeof(ValueUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new ValueUnion { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<ValueClass>(reader);
                    return new ValueUnion { ValueClass = objectValue };
            }
            throw new Exception("Cannot unmarshal type ValueUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ValueUnion)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.ValueClass != null)
            {
                serializer.Serialize(writer, value.ValueClass);
                return;
            }
            throw new Exception("Cannot marshal type ValueUnion");
        }

        public static readonly ValueUnionConverter Singleton = new ValueUnionConverter();
    }
}
