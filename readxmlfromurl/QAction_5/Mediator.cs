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
        public TransferStatus PlaytimeTransferStatus { get; set; }

        [JsonProperty("SequenceId")]
        public long SequenceId { get; set; }

        [JsonProperty("TransferStatus")]
        public TransferStatus TransferStatus { get; set; }

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
        public StickyObject[] Object { get; set; }
    }

    public partial class StickyObject
    {
        [JsonProperty("TemplateParameter")]
        public ObjectTemplateParameter[] TemplateParameter { get; set; }
    }

    public partial class ObjectTemplateParameter
    {
        [JsonProperty("GeneralType")]
        public GeneralType GeneralType { get; set; }

        [JsonProperty("Name")]
        public PurpleName Name { get; set; }

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
        public TemplateParameterListTemplateParameter[] TemplateParameter { get; set; }
    }

    public partial class TemplateParameterListTemplateParameter
    {
        [JsonProperty("GeneralType")]
        public GeneralType GeneralType { get; set; }

        [JsonProperty("Name")]
        public FluffyName Name { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }

        [JsonProperty("Type")]
        public TypeEnum Type { get; set; }
    }

    public enum Rate { Ndf25 };

    public enum MachineStatus { Done, Error, Play, PlayCued, Unknown };

    public enum TransferStatus { MaterialUnknown, NoMedia, Ready, Workflow };

    public enum GeneralType { String, TemplateParameterListCompound };

    public enum PurpleName { AdSalesContentReconcileKeyText, AudioAttenuationText, CiakTxprofile2TxProfile, CommercialPositionText, CommercialRemarksText, EnablerLegacyCompoundList, EnablerSwitchInGraphic, EnablerSwitchInUserText1, GfxOffsetDurationCompoundList, GfxStartEndCompoundList, MaterialIncodeDurationMatIdIncodeDuration, MaterialSegmentMatId, MaterialSegmentSegmentGroup, MaterialSegmentSegmentIndex, MaterialSegmentSegmentType, MediaSourceText, OtbGfxOffsetDurationCompoundList, OttRestartGraphic, OttRestartUserText1, OttRestartUserText2, OttRestartUserText3, ParentalRatingGraphic, ParentalRatingTextText, ParentalRatingUserText1, PrgmamTxprofile2TxProfile, ProductPartText, ProductPlacementText, PromoTxprofile2TxProfile, ScteBroadcastBreakStartInsertSegmentationDescriptor, ScteBroadcastBreakStartOffset, ScteBroadcastBreakStartPresetName, ScteBroadcastBreakStartTimeSignalRequest, ScteBroadcastProviderAdvStartInsertSegmentationDescriptor, ScteBroadcastProviderAdvStartOffset, ScteBroadcastProviderAdvStartPresetName, ScteBroadcastProviderAdvStartTimeSignalRequest, ShortformTxprofile2TxProfile, SpotaffTxprofile2TxProfile, SpotnoaffTxprofile2TxProfile, Subtitle01ClosedSubtitles, Subtitle02ClosedSubtitles, TransitionTransDwell, TransitionTransSpeed, TransitionTransType, TxprofileTxProfile };

    public enum TypeEnum { TemplateParameter };

    public enum FluffyName { Duration, DurationExtensionFrames, EnablerLegacyDuration, EnablerLegacyGraphic, EnablerLegacyOffset, EnablerLegacyOffsetType, EnablerLegacyUserText1, GfxOffsetDurationDuration, GfxOffsetDurationMatId, GfxOffsetDurationOffset, GfxOffsetDurationOffsetType, GfxOffsetDurationStream, GfxOffsetDurationTransSpeed, GfxOffsetDurationTransSpeedIn, GfxStartEndEndOffset, GfxStartEndMatId, GfxStartEndStartOffset, GfxStartEndStream, GfxStartEndTransSpeed, GfxStartEndTransSpeedIn, InCode, MatId, OtbGfxOffsetDurationDuration, OtbGfxOffsetDurationMatId, OtbGfxOffsetDurationOffset, OtbGfxOffsetDurationOffsetType, OtbGfxOffsetDurationStream, OtbGfxOffsetDurationTransSpeed, OtbGfxOffsetDurationTransSpeedIn, OtbGfxOffsetDurationUserText1, PreRollTime, SegmentationEventId, SegmentationUpid, SegmentationUpidType };

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
                TransferStatusConverter.Singleton,
                GeneralTypeConverter.Singleton,
                PurpleNameConverter.Singleton,
                TypeEnumConverter.Singleton,
                ValueUnionConverter.Singleton,
                FluffyNameConverter.Singleton,
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

    internal class TransferStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TransferStatus) || t == typeof(TransferStatus?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Material unknown":
                    return TransferStatus.MaterialUnknown;
                case "No media":
                    return TransferStatus.NoMedia;
                case "Ready":
                    return TransferStatus.Ready;
                case "Workflow":
                    return TransferStatus.Workflow;
            }
            throw new Exception("Cannot unmarshal type TransferStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TransferStatus)untypedValue;
            switch (value)
            {
                case TransferStatus.MaterialUnknown:
                    serializer.Serialize(writer, "Material unknown");
                    return;
                case TransferStatus.NoMedia:
                    serializer.Serialize(writer, "No media");
                    return;
                case TransferStatus.Ready:
                    serializer.Serialize(writer, "Ready");
                    return;
                case TransferStatus.Workflow:
                    serializer.Serialize(writer, "Workflow");
                    return;
            }
            throw new Exception("Cannot marshal type TransferStatus");
        }

        public static readonly TransferStatusConverter Singleton = new TransferStatusConverter();
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

    internal class PurpleNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurpleName) || t == typeof(PurpleName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "adSalesContentReconcileKey-text":
                    return PurpleName.AdSalesContentReconcileKeyText;
                case "audioAttenuation-text":
                    return PurpleName.AudioAttenuationText;
                case "ciak-txprofile2-txProfile":
                    return PurpleName.CiakTxprofile2TxProfile;
                case "commercialPosition-text":
                    return PurpleName.CommercialPositionText;
                case "commercialRemarks-text":
                    return PurpleName.CommercialRemarksText;
                case "enablerLegacy-compoundList":
                    return PurpleName.EnablerLegacyCompoundList;
                case "enablerSwitchIn-graphic":
                    return PurpleName.EnablerSwitchInGraphic;
                case "enablerSwitchIn-userText1":
                    return PurpleName.EnablerSwitchInUserText1;
                case "gfxOffsetDuration-compoundList":
                    return PurpleName.GfxOffsetDurationCompoundList;
                case "gfxStartEnd-compoundList":
                    return PurpleName.GfxStartEndCompoundList;
                case "materialIncodeDuration-matIdIncodeDuration":
                    return PurpleName.MaterialIncodeDurationMatIdIncodeDuration;
                case "materialSegment-matId":
                    return PurpleName.MaterialSegmentMatId;
                case "materialSegment-segmentGroup":
                    return PurpleName.MaterialSegmentSegmentGroup;
                case "materialSegment-segmentIndex":
                    return PurpleName.MaterialSegmentSegmentIndex;
                case "materialSegment-segmentType":
                    return PurpleName.MaterialSegmentSegmentType;
                case "mediaSource-text":
                    return PurpleName.MediaSourceText;
                case "otbGfxOffsetDuration-compoundList":
                    return PurpleName.OtbGfxOffsetDurationCompoundList;
                case "ottRestart-graphic":
                    return PurpleName.OttRestartGraphic;
                case "ottRestart-userText1":
                    return PurpleName.OttRestartUserText1;
                case "ottRestart-userText2":
                    return PurpleName.OttRestartUserText2;
                case "ottRestart-userText3":
                    return PurpleName.OttRestartUserText3;
                case "parentalRating-graphic":
                    return PurpleName.ParentalRatingGraphic;
                case "parentalRating-userText1":
                    return PurpleName.ParentalRatingUserText1;
                case "parentalRatingText-text":
                    return PurpleName.ParentalRatingTextText;
                case "prgmam-txprofile2-txProfile":
                    return PurpleName.PrgmamTxprofile2TxProfile;
                case "productPart-text":
                    return PurpleName.ProductPartText;
                case "productPlacement-text":
                    return PurpleName.ProductPlacementText;
                case "promo-txprofile2-txProfile":
                    return PurpleName.PromoTxprofile2TxProfile;
                case "scteBroadcastBreakStart-insertSegmentationDescriptor":
                    return PurpleName.ScteBroadcastBreakStartInsertSegmentationDescriptor;
                case "scteBroadcastBreakStart-offset":
                    return PurpleName.ScteBroadcastBreakStartOffset;
                case "scteBroadcastBreakStart-presetName":
                    return PurpleName.ScteBroadcastBreakStartPresetName;
                case "scteBroadcastBreakStart-timeSignalRequest":
                    return PurpleName.ScteBroadcastBreakStartTimeSignalRequest;
                case "scteBroadcastProviderAdvStart-insertSegmentationDescriptor":
                    return PurpleName.ScteBroadcastProviderAdvStartInsertSegmentationDescriptor;
                case "scteBroadcastProviderAdvStart-offset":
                    return PurpleName.ScteBroadcastProviderAdvStartOffset;
                case "scteBroadcastProviderAdvStart-presetName":
                    return PurpleName.ScteBroadcastProviderAdvStartPresetName;
                case "scteBroadcastProviderAdvStart-timeSignalRequest":
                    return PurpleName.ScteBroadcastProviderAdvStartTimeSignalRequest;
                case "shortform-txprofile2-txProfile":
                    return PurpleName.ShortformTxprofile2TxProfile;
                case "spotaff-txprofile2-txProfile":
                    return PurpleName.SpotaffTxprofile2TxProfile;
                case "spotnoaff-txprofile2-txProfile":
                    return PurpleName.SpotnoaffTxprofile2TxProfile;
                case "subtitle01-closedSubtitles":
                    return PurpleName.Subtitle01ClosedSubtitles;
                case "subtitle02-closedSubtitles":
                    return PurpleName.Subtitle02ClosedSubtitles;
                case "transition-transDwell":
                    return PurpleName.TransitionTransDwell;
                case "transition-transSpeed":
                    return PurpleName.TransitionTransSpeed;
                case "transition-transType":
                    return PurpleName.TransitionTransType;
                case "txprofile-txProfile":
                    return PurpleName.TxprofileTxProfile;
            }
            throw new Exception("Cannot unmarshal type PurpleName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurpleName)untypedValue;
            switch (value)
            {
                case PurpleName.AdSalesContentReconcileKeyText:
                    serializer.Serialize(writer, "adSalesContentReconcileKey-text");
                    return;
                case PurpleName.AudioAttenuationText:
                    serializer.Serialize(writer, "audioAttenuation-text");
                    return;
                case PurpleName.CiakTxprofile2TxProfile:
                    serializer.Serialize(writer, "ciak-txprofile2-txProfile");
                    return;
                case PurpleName.CommercialPositionText:
                    serializer.Serialize(writer, "commercialPosition-text");
                    return;
                case PurpleName.CommercialRemarksText:
                    serializer.Serialize(writer, "commercialRemarks-text");
                    return;
                case PurpleName.EnablerLegacyCompoundList:
                    serializer.Serialize(writer, "enablerLegacy-compoundList");
                    return;
                case PurpleName.EnablerSwitchInGraphic:
                    serializer.Serialize(writer, "enablerSwitchIn-graphic");
                    return;
                case PurpleName.EnablerSwitchInUserText1:
                    serializer.Serialize(writer, "enablerSwitchIn-userText1");
                    return;
                case PurpleName.GfxOffsetDurationCompoundList:
                    serializer.Serialize(writer, "gfxOffsetDuration-compoundList");
                    return;
                case PurpleName.GfxStartEndCompoundList:
                    serializer.Serialize(writer, "gfxStartEnd-compoundList");
                    return;
                case PurpleName.MaterialIncodeDurationMatIdIncodeDuration:
                    serializer.Serialize(writer, "materialIncodeDuration-matIdIncodeDuration");
                    return;
                case PurpleName.MaterialSegmentMatId:
                    serializer.Serialize(writer, "materialSegment-matId");
                    return;
                case PurpleName.MaterialSegmentSegmentGroup:
                    serializer.Serialize(writer, "materialSegment-segmentGroup");
                    return;
                case PurpleName.MaterialSegmentSegmentIndex:
                    serializer.Serialize(writer, "materialSegment-segmentIndex");
                    return;
                case PurpleName.MaterialSegmentSegmentType:
                    serializer.Serialize(writer, "materialSegment-segmentType");
                    return;
                case PurpleName.MediaSourceText:
                    serializer.Serialize(writer, "mediaSource-text");
                    return;
                case PurpleName.OtbGfxOffsetDurationCompoundList:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-compoundList");
                    return;
                case PurpleName.OttRestartGraphic:
                    serializer.Serialize(writer, "ottRestart-graphic");
                    return;
                case PurpleName.OttRestartUserText1:
                    serializer.Serialize(writer, "ottRestart-userText1");
                    return;
                case PurpleName.OttRestartUserText2:
                    serializer.Serialize(writer, "ottRestart-userText2");
                    return;
                case PurpleName.OttRestartUserText3:
                    serializer.Serialize(writer, "ottRestart-userText3");
                    return;
                case PurpleName.ParentalRatingGraphic:
                    serializer.Serialize(writer, "parentalRating-graphic");
                    return;
                case PurpleName.ParentalRatingUserText1:
                    serializer.Serialize(writer, "parentalRating-userText1");
                    return;
                case PurpleName.ParentalRatingTextText:
                    serializer.Serialize(writer, "parentalRatingText-text");
                    return;
                case PurpleName.PrgmamTxprofile2TxProfile:
                    serializer.Serialize(writer, "prgmam-txprofile2-txProfile");
                    return;
                case PurpleName.ProductPartText:
                    serializer.Serialize(writer, "productPart-text");
                    return;
                case PurpleName.ProductPlacementText:
                    serializer.Serialize(writer, "productPlacement-text");
                    return;
                case PurpleName.PromoTxprofile2TxProfile:
                    serializer.Serialize(writer, "promo-txprofile2-txProfile");
                    return;
                case PurpleName.ScteBroadcastBreakStartInsertSegmentationDescriptor:
                    serializer.Serialize(writer, "scteBroadcastBreakStart-insertSegmentationDescriptor");
                    return;
                case PurpleName.ScteBroadcastBreakStartOffset:
                    serializer.Serialize(writer, "scteBroadcastBreakStart-offset");
                    return;
                case PurpleName.ScteBroadcastBreakStartPresetName:
                    serializer.Serialize(writer, "scteBroadcastBreakStart-presetName");
                    return;
                case PurpleName.ScteBroadcastBreakStartTimeSignalRequest:
                    serializer.Serialize(writer, "scteBroadcastBreakStart-timeSignalRequest");
                    return;
                case PurpleName.ScteBroadcastProviderAdvStartInsertSegmentationDescriptor:
                    serializer.Serialize(writer, "scteBroadcastProviderAdvStart-insertSegmentationDescriptor");
                    return;
                case PurpleName.ScteBroadcastProviderAdvStartOffset:
                    serializer.Serialize(writer, "scteBroadcastProviderAdvStart-offset");
                    return;
                case PurpleName.ScteBroadcastProviderAdvStartPresetName:
                    serializer.Serialize(writer, "scteBroadcastProviderAdvStart-presetName");
                    return;
                case PurpleName.ScteBroadcastProviderAdvStartTimeSignalRequest:
                    serializer.Serialize(writer, "scteBroadcastProviderAdvStart-timeSignalRequest");
                    return;
                case PurpleName.ShortformTxprofile2TxProfile:
                    serializer.Serialize(writer, "shortform-txprofile2-txProfile");
                    return;
                case PurpleName.SpotaffTxprofile2TxProfile:
                    serializer.Serialize(writer, "spotaff-txprofile2-txProfile");
                    return;
                case PurpleName.SpotnoaffTxprofile2TxProfile:
                    serializer.Serialize(writer, "spotnoaff-txprofile2-txProfile");
                    return;
                case PurpleName.Subtitle01ClosedSubtitles:
                    serializer.Serialize(writer, "subtitle01-closedSubtitles");
                    return;
                case PurpleName.Subtitle02ClosedSubtitles:
                    serializer.Serialize(writer, "subtitle02-closedSubtitles");
                    return;
                case PurpleName.TransitionTransDwell:
                    serializer.Serialize(writer, "transition-transDwell");
                    return;
                case PurpleName.TransitionTransSpeed:
                    serializer.Serialize(writer, "transition-transSpeed");
                    return;
                case PurpleName.TransitionTransType:
                    serializer.Serialize(writer, "transition-transType");
                    return;
                case PurpleName.TxprofileTxProfile:
                    serializer.Serialize(writer, "txprofile-txProfile");
                    return;
            }
            throw new Exception("Cannot marshal type PurpleName");
        }

        public static readonly PurpleNameConverter Singleton = new PurpleNameConverter();
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

    internal class FluffyNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FluffyName) || t == typeof(FluffyName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "duration":
                    return FluffyName.Duration;
                case "durationExtensionFrames":
                    return FluffyName.DurationExtensionFrames;
                case "enablerLegacy-duration":
                    return FluffyName.EnablerLegacyDuration;
                case "enablerLegacy-graphic":
                    return FluffyName.EnablerLegacyGraphic;
                case "enablerLegacy-offset":
                    return FluffyName.EnablerLegacyOffset;
                case "enablerLegacy-offsetType":
                    return FluffyName.EnablerLegacyOffsetType;
                case "enablerLegacy-userText1":
                    return FluffyName.EnablerLegacyUserText1;
                case "gfxOffsetDuration-duration":
                    return FluffyName.GfxOffsetDurationDuration;
                case "gfxOffsetDuration-matId":
                    return FluffyName.GfxOffsetDurationMatId;
                case "gfxOffsetDuration-offset":
                    return FluffyName.GfxOffsetDurationOffset;
                case "gfxOffsetDuration-offsetType":
                    return FluffyName.GfxOffsetDurationOffsetType;
                case "gfxOffsetDuration-stream":
                    return FluffyName.GfxOffsetDurationStream;
                case "gfxOffsetDuration-transSpeed":
                    return FluffyName.GfxOffsetDurationTransSpeed;
                case "gfxOffsetDuration-transSpeedIn":
                    return FluffyName.GfxOffsetDurationTransSpeedIn;
                case "gfxStartEnd-endOffset":
                    return FluffyName.GfxStartEndEndOffset;
                case "gfxStartEnd-matId":
                    return FluffyName.GfxStartEndMatId;
                case "gfxStartEnd-startOffset":
                    return FluffyName.GfxStartEndStartOffset;
                case "gfxStartEnd-stream":
                    return FluffyName.GfxStartEndStream;
                case "gfxStartEnd-transSpeed":
                    return FluffyName.GfxStartEndTransSpeed;
                case "gfxStartEnd-transSpeedIn":
                    return FluffyName.GfxStartEndTransSpeedIn;
                case "inCode":
                    return FluffyName.InCode;
                case "matId":
                    return FluffyName.MatId;
                case "otbGfxOffsetDuration-duration":
                    return FluffyName.OtbGfxOffsetDurationDuration;
                case "otbGfxOffsetDuration-matId":
                    return FluffyName.OtbGfxOffsetDurationMatId;
                case "otbGfxOffsetDuration-offset":
                    return FluffyName.OtbGfxOffsetDurationOffset;
                case "otbGfxOffsetDuration-offsetType":
                    return FluffyName.OtbGfxOffsetDurationOffsetType;
                case "otbGfxOffsetDuration-stream":
                    return FluffyName.OtbGfxOffsetDurationStream;
                case "otbGfxOffsetDuration-transSpeed":
                    return FluffyName.OtbGfxOffsetDurationTransSpeed;
                case "otbGfxOffsetDuration-transSpeedIn":
                    return FluffyName.OtbGfxOffsetDurationTransSpeedIn;
                case "otbGfxOffsetDuration-userText1":
                    return FluffyName.OtbGfxOffsetDurationUserText1;
                case "preRollTime":
                    return FluffyName.PreRollTime;
                case "segmentationEventId":
                    return FluffyName.SegmentationEventId;
                case "segmentationUpid":
                    return FluffyName.SegmentationUpid;
                case "segmentationUpidType":
                    return FluffyName.SegmentationUpidType;
            }
            throw new Exception("Cannot unmarshal type FluffyName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FluffyName)untypedValue;
            switch (value)
            {
                case FluffyName.Duration:
                    serializer.Serialize(writer, "duration");
                    return;
                case FluffyName.DurationExtensionFrames:
                    serializer.Serialize(writer, "durationExtensionFrames");
                    return;
                case FluffyName.EnablerLegacyDuration:
                    serializer.Serialize(writer, "enablerLegacy-duration");
                    return;
                case FluffyName.EnablerLegacyGraphic:
                    serializer.Serialize(writer, "enablerLegacy-graphic");
                    return;
                case FluffyName.EnablerLegacyOffset:
                    serializer.Serialize(writer, "enablerLegacy-offset");
                    return;
                case FluffyName.EnablerLegacyOffsetType:
                    serializer.Serialize(writer, "enablerLegacy-offsetType");
                    return;
                case FluffyName.EnablerLegacyUserText1:
                    serializer.Serialize(writer, "enablerLegacy-userText1");
                    return;
                case FluffyName.GfxOffsetDurationDuration:
                    serializer.Serialize(writer, "gfxOffsetDuration-duration");
                    return;
                case FluffyName.GfxOffsetDurationMatId:
                    serializer.Serialize(writer, "gfxOffsetDuration-matId");
                    return;
                case FluffyName.GfxOffsetDurationOffset:
                    serializer.Serialize(writer, "gfxOffsetDuration-offset");
                    return;
                case FluffyName.GfxOffsetDurationOffsetType:
                    serializer.Serialize(writer, "gfxOffsetDuration-offsetType");
                    return;
                case FluffyName.GfxOffsetDurationStream:
                    serializer.Serialize(writer, "gfxOffsetDuration-stream");
                    return;
                case FluffyName.GfxOffsetDurationTransSpeed:
                    serializer.Serialize(writer, "gfxOffsetDuration-transSpeed");
                    return;
                case FluffyName.GfxOffsetDurationTransSpeedIn:
                    serializer.Serialize(writer, "gfxOffsetDuration-transSpeedIn");
                    return;
                case FluffyName.GfxStartEndEndOffset:
                    serializer.Serialize(writer, "gfxStartEnd-endOffset");
                    return;
                case FluffyName.GfxStartEndMatId:
                    serializer.Serialize(writer, "gfxStartEnd-matId");
                    return;
                case FluffyName.GfxStartEndStartOffset:
                    serializer.Serialize(writer, "gfxStartEnd-startOffset");
                    return;
                case FluffyName.GfxStartEndStream:
                    serializer.Serialize(writer, "gfxStartEnd-stream");
                    return;
                case FluffyName.GfxStartEndTransSpeed:
                    serializer.Serialize(writer, "gfxStartEnd-transSpeed");
                    return;
                case FluffyName.GfxStartEndTransSpeedIn:
                    serializer.Serialize(writer, "gfxStartEnd-transSpeedIn");
                    return;
                case FluffyName.InCode:
                    serializer.Serialize(writer, "inCode");
                    return;
                case FluffyName.MatId:
                    serializer.Serialize(writer, "matId");
                    return;
                case FluffyName.OtbGfxOffsetDurationDuration:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-duration");
                    return;
                case FluffyName.OtbGfxOffsetDurationMatId:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-matId");
                    return;
                case FluffyName.OtbGfxOffsetDurationOffset:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-offset");
                    return;
                case FluffyName.OtbGfxOffsetDurationOffsetType:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-offsetType");
                    return;
                case FluffyName.OtbGfxOffsetDurationStream:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-stream");
                    return;
                case FluffyName.OtbGfxOffsetDurationTransSpeed:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-transSpeed");
                    return;
                case FluffyName.OtbGfxOffsetDurationTransSpeedIn:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-transSpeedIn");
                    return;
                case FluffyName.OtbGfxOffsetDurationUserText1:
                    serializer.Serialize(writer, "otbGfxOffsetDuration-userText1");
                    return;
                case FluffyName.PreRollTime:
                    serializer.Serialize(writer, "preRollTime");
                    return;
                case FluffyName.SegmentationEventId:
                    serializer.Serialize(writer, "segmentationEventId");
                    return;
                case FluffyName.SegmentationUpid:
                    serializer.Serialize(writer, "segmentationUpid");
                    return;
                case FluffyName.SegmentationUpidType:
                    serializer.Serialize(writer, "segmentationUpidType");
                    return;
            }
            throw new Exception("Cannot marshal type FluffyName");
        }

        public static readonly FluffyNameConverter Singleton = new FluffyNameConverter();
    }
}
