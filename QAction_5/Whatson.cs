#pragma warning disable SA1516
#pragma warning disable SA1505
#pragma warning disable SA1605 // missing summary
#pragma warning disable SA1300 // lowercase elements
namespace Whatson
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Pharos
    {

        private PharosImportMetaData importMetaDataField;

        private Playlist playlistField;

        private PharosMaterial[] materialField;

        public PharosImportMetaData ImportMetaData
        {
            get
            {
                return this.importMetaDataField;
            }
            set
            {
                this.importMetaDataField = value;
            }
        }

        public Playlist Playlist
        {
            get
            {
                return this.playlistField;
            }
            set
            {
                this.playlistField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("Material")]
        public PharosMaterial[] Material
        {
            get
            {
                return this.materialField;
            }
            set
            {
                this.materialField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosImportMetaData
    {

        private string noteField;

        public string Note
        {
            get
            {
                return this.noteField;
            }

            set
            {
                this.noteField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Playlist
    {

        private string channelNameField;

        private Block[] blockListField;

        private Bookmark[] bookmarkListField;

        public string ChannelName
        {
            get
            {
                return this.channelNameField;
            }
            set
            {
                this.channelNameField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Block", IsNullable = false)]
        public Block[] BlockList
        {
            get
            {
                return this.blockListField;
            }
            set
            {
                this.blockListField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Bookmark", IsNullable = false)]
        public Bookmark[] BookmarkList
        {
            get
            {
                return this.bookmarkListField;
            }
            set
            {
                this.bookmarkListField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Block
    {

        private ulong blockNameField;

        private bool blockNameFieldSpecified;

        private string previousBlockNameField;

        private bool previousBlockNameFieldSpecified;

        private PlaylistItem[] playlistItemField;

        public ulong BlockName
        {
            get
            {
                return this.blockNameField;
            }
            set
            {
                this.blockNameField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool BlockNameSpecified
        {
            get
            {
                return this.blockNameFieldSpecified;
            }
            set
            {
                this.blockNameFieldSpecified = value;
            }
        }

        public string PreviousBlockName
        {
            get
            {
                return this.previousBlockNameField;
            }
            set
            {
                this.previousBlockNameField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool PreviousBlockNameSpecified
        {
            get
            {
                return this.previousBlockNameFieldSpecified;
            }
            set
            {
                this.previousBlockNameFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("PlaylistItem")]
        public PlaylistItem[] PlaylistItem
        {
            get
            {
                return this.playlistItemField;
            }
            set
            {
                this.playlistItemField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaylistItem
    {

        private string itemReferenceField;

        private string startModeField;

        private string startDateField;

        private string startTimecodeField;

        private string scheduledDurationField;

        private string scheduleReferenceField;

        private string scheduledTitleField;

        private Template templateField;

        public string ItemReference
        {
            get
            {
                return this.itemReferenceField;
            }
            set
            {
                this.itemReferenceField = value;
            }
        }

        public string StartMode
        {
            get
            {
                return this.startModeField;
            }
            set
            {
                this.startModeField = value;
            }
        }

        public string StartDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }

        public string StartTimecode
        {
            get
            {
                return this.startTimecodeField;
            }
            set
            {
                this.startTimecodeField = value;
            }
        }

        public string ScheduledDuration
        {
            get
            {
                return this.scheduledDurationField;
            }
            set
            {
                this.scheduledDurationField = value;
            }
        }

        public string ScheduleReference
        {
            get
            {
                return this.scheduleReferenceField;
            }
            set
            {
                this.scheduleReferenceField = value;
            }
        }

        public string ScheduledTitle
        {
            get
            {
                return this.scheduledTitleField;
            }
            set
            {
                this.scheduledTitleField = value;
            }
        }

        public Template Template
        {
            get
            {
                return this.templateField;
            }
            set
            {
                this.templateField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Template
    {

        private string templateNameField;

        private DataElement[] dataElementListField;

        public string TemplateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("DataElement", IsNullable = false)]
        public DataElement[] DataElementList
        {
            get
            {
                return this.dataElementListField;
            }
            set
            {
                this.dataElementListField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataElement
    {

        private string nameField;

        private string typeField;

        private DataElementValue valueField;

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        public DataElementValue Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataElementValue
    {

        private DataElementCompoundList dataElementCompoundListField;

        private string[] textField;

        public DataElementCompoundList DataElementCompoundList
        {
            get
            {
                return this.dataElementCompoundListField;
            }
            set
            {
                this.dataElementCompoundListField = value;
            }
        }

        [System.Xml.Serialization.XmlTextAttribute]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataElementCompoundList
    {

        private DataElement[] dataElementListField;

        [System.Xml.Serialization.XmlArrayItemAttribute("DataElement", IsNullable = false)]
        public DataElement[] DataElementList
        {
            get
            {
                return this.dataElementListField;
            }
            set
            {
                this.dataElementListField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Bookmark
    {

        private string userNameField;

        private string notesField;

        private string colourField;

        private string topItemRefField;

        private string bottomItemRefField;

        public string UserName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        public string Notes
        {
            get
            {
                return this.notesField;
            }
            set
            {
                this.notesField = value;
            }
        }

        public string Colour
        {
            get
            {
                return this.colourField;
            }
            set
            {
                this.colourField = value;
            }
        }

        public string TopItemRef
        {
            get
            {
                return this.topItemRefField;
            }
            set
            {
                this.topItemRefField = value;
            }
        }

        public string BottomItemRef
        {
            get
            {
                return this.bottomItemRefField;
            }
            set
            {
                this.bottomItemRefField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterial
    {

        private string matIdField;

        private string titleField;

        private string subTitleField;

        private string materialTypeField;

        private PharosMaterialDuration durationField;

        private PharosMaterialOwner ownerField;

        private PharosMaterialTrack[] trackField;

        private PharosMaterialTrackTypeLink[] trackTypeLinkField;

        private DataElement[] dataElementListField;

        private PharosMaterialMarker[] markerField;

        private PharosMaterialSegment[] segmentListField;

        public string MatId
        {
            get
            {
                return this.matIdField;
            }
            set
            {
                this.matIdField = value;
            }
        }

        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        public string SubTitle
        {
            get
            {
                return this.subTitleField;
            }
            set
            {
                this.subTitleField = value;
            }
        }

        public string MaterialType
        {
            get
            {
                return this.materialTypeField;
            }
            set
            {
                this.materialTypeField = value;
            }
        }

        public PharosMaterialDuration Duration
        {
            get
            {
                return this.durationField;
            }
            set
            {
                this.durationField = value;
            }
        }

        public PharosMaterialOwner Owner
        {
            get
            {
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("Track")]
        public PharosMaterialTrack[] Track
        {
            get
            {
                return this.trackField;
            }
            set
            {
                this.trackField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("TrackTypeLink")]
        public PharosMaterialTrackTypeLink[] TrackTypeLink
        {
            get
            {
                return this.trackTypeLinkField;
            }
            set
            {
                this.trackTypeLinkField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("DataElement", IsNullable = false)]
        public DataElement[] DataElementList
        {
            get
            {
                return this.dataElementListField;
            }
            set
            {
                this.dataElementListField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("Marker")]
        public PharosMaterialMarker[] Marker
        {
            get
            {
                return this.markerField;
            }
            set
            {
                this.markerField = value;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Segment", IsNullable = false)]
        public PharosMaterialSegment[] SegmentList
        {
            get
            {
                return this.segmentListField;
            }
            set
            {
                this.segmentListField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialDuration
    {

        private string rateField;

        private string valueField;

        [System.Xml.Serialization.XmlAttributeAttribute]
        public string rate
        {
            get
            {
                return this.rateField;
            }
            set
            {
                this.rateField = value;
            }
        }

        [System.Xml.Serialization.XmlTextAttribute]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialOwner
    {

        private string nameField;

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialTrack
    {

        private string mediaNameField;

        private PharosMaterialTrackTrackDefinition trackDefinitionField;

        public string MediaName
        {
            get
            {
                return this.mediaNameField;
            }
            set
            {
                this.mediaNameField = value;
            }
        }

        public PharosMaterialTrackTrackDefinition TrackDefinition
        {
            get
            {
                return this.trackDefinitionField;
            }
            set
            {
                this.trackDefinitionField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialTrackTrackDefinition
    {

        private string trackTypeNameField;

        private PharosMaterialTrackTrackDefinitionTrackFile trackFileField;

        private byte positionField;

        private bool positionFieldSpecified;

        public string TrackTypeName
        {
            get
            {
                return this.trackTypeNameField;
            }
            set
            {
                this.trackTypeNameField = value;
            }
        }

        public PharosMaterialTrackTrackDefinitionTrackFile TrackFile
        {
            get
            {
                return this.trackFileField;
            }
            set
            {
                this.trackFileField = value;
            }
        }

        public byte Position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool PositionSpecified
        {
            get
            {
                return this.positionFieldSpecified;
            }
            set
            {
                this.positionFieldSpecified = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialTrackTrackDefinitionTrackFile
    {

        private string nameField;

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialTrackTypeLink
    {

        private string trackTypeNameField;

        private string stateMachineField;

        private string stateNameField;

        public string TrackTypeName
        {
            get
            {
                return this.trackTypeNameField;
            }
            set
            {
                this.trackTypeNameField = value;
            }
        }

        public string StateMachine
        {
            get
            {
                return this.stateMachineField;
            }
            set
            {
                this.stateMachineField = value;
            }
        }

        public string StateName
        {
            get
            {
                return this.stateNameField;
            }
            set
            {
                this.stateNameField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialMarker
    {

        private string markerTypeField;

        private PharosMaterialMarkerTimecode timecodeField;

        public string MarkerType
        {
            get
            {
                return this.markerTypeField;
            }
            set
            {
                this.markerTypeField = value;
            }
        }

        public PharosMaterialMarkerTimecode Timecode
        {
            get
            {
                return this.timecodeField;
            }
            set
            {
                this.timecodeField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialMarkerTimecode
    {

        private string rateField;

        private string valueField;

        [System.Xml.Serialization.XmlAttributeAttribute]
        public string rate
        {
            get
            {
                return this.rateField;
            }
            set
            {
                this.rateField = value;
            }
        }

        [System.Xml.Serialization.XmlTextAttribute]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialSegment
    {

        private string nameField;

        private PharosMaterialSegmentSegmentGroup segmentGroupField;

        private PharosMaterialSegmentSegmentType segmentTypeField;

        private byte indexField;

        private PharosMaterialSegmentMarkerIn markerInField;

        private PharosMaterialSegmentMarkerOut markerOutField;

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        public PharosMaterialSegmentSegmentGroup SegmentGroup
        {
            get
            {
                return this.segmentGroupField;
            }
            set
            {
                this.segmentGroupField = value;
            }
        }

        public PharosMaterialSegmentSegmentType SegmentType
        {
            get
            {
                return this.segmentTypeField;
            }
            set
            {
                this.segmentTypeField = value;
            }
        }

        public byte Index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        public PharosMaterialSegmentMarkerIn MarkerIn
        {
            get
            {
                return this.markerInField;
            }
            set
            {
                this.markerInField = value;
            }
        }

        public PharosMaterialSegmentMarkerOut MarkerOut
        {
            get
            {
                return this.markerOutField;
            }
            set
            {
                this.markerOutField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialSegmentSegmentGroup
    {

        private string nameField;

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialSegmentSegmentType
    {

        private string nameField;

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialSegmentMarkerIn
    {

        private string timecodeField;

        private string frameRateField;

        public string Timecode
        {
            get
            {
                return this.timecodeField;
            }
            set
            {
                this.timecodeField = value;
            }
        }

        public string FrameRate
        {
            get
            {
                return this.frameRateField;
            }
            set
            {
                this.frameRateField = value;
            }
        }
    }

    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PharosMaterialSegmentMarkerOut
    {

        private string timecodeField;

        private string frameRateField;

        public string Timecode
        {
            get
            {
                return this.timecodeField;
            }
            set
            {
                this.timecodeField = value;
            }
        }

        public string FrameRate
        {
            get
            {
                return this.frameRateField;
            }
            set
            {
                this.frameRateField = value;
            }
        }
    }
}