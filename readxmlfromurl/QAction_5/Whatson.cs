
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Pharos
{

    private PharosImportMetaData importMetaDataField;

    private PharosPlaylist playlistField;

    private PharosMaterial[] materialField;

    /// <remarks/>
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

    /// <remarks/>
    public PharosPlaylist Playlist
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosImportMetaData
{

    private string noteField;

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylist
{

    private string channelNameField;

    private PharosPlaylistBlock[] blockListField;

    private PharosPlaylistBookmark[] bookmarkListField;

    /// <remarks/>
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

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Block", IsNullable = false)]
    public PharosPlaylistBlock[] BlockList
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

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Bookmark", IsNullable = false)]
    public PharosPlaylistBookmark[] BookmarkList
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBlock
{

    private ulong blockNameField;

    private bool blockNameFieldSpecified;

    private ulong previousBlockNameField;

    private bool previousBlockNameFieldSpecified;

    private PharosPlaylistBlockPlaylistItem[] playlistItemField;

    /// <remarks/>
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

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
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

    /// <remarks/>
    public ulong PreviousBlockName
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

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
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

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PlaylistItem")]
    public PharosPlaylistBlockPlaylistItem[] PlaylistItem
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBlockPlaylistItem
{

    private string itemReferenceField;

    private string startModeField;

    private string startDateField;

    private string startTimecodeField;

    private string scheduledDurationField;

    private string scheduleReferenceField;

    private string scheduledTitleField;

    private PharosPlaylistBlockPlaylistItemTemplate templateField;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
    public PharosPlaylistBlockPlaylistItemTemplate Template
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBlockPlaylistItemTemplate
{

    private string templateNameField;

    private PharosPlaylistBlockPlaylistItemTemplateDataElement[] dataElementListField;

    /// <remarks/>
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

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("DataElement", IsNullable = false)]
    public PharosPlaylistBlockPlaylistItemTemplateDataElement[] DataElementList
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBlockPlaylistItemTemplateDataElement
{

    private string nameField;

    private string typeField;

    private PharosPlaylistBlockPlaylistItemTemplateDataElementValue valueField;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
    public PharosPlaylistBlockPlaylistItemTemplateDataElementValue Value
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBlockPlaylistItemTemplateDataElementValue
{

    private PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundList dataElementCompoundListField;

    private string[] textField;

    /// <remarks/>
    public PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundList DataElementCompoundList
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundList
{

    private PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundListDataElement[] dataElementListField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("DataElement", IsNullable = false)]
    public PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundListDataElement[] DataElementList
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBlockPlaylistItemTemplateDataElementValueDataElementCompoundListDataElement
{

    private string nameField;

    private string typeField;

    private string valueField;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosPlaylistBookmark
{

    private string userNameField;

    private string notesField;

    private string colourField;

    private string topItemRefField;

    private string bottomItemRefField;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
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

    private PharosMaterialDataElement[] dataElementListField;

    private PharosMaterialMarker[] markerField;

    private PharosMaterialSegment[] segmentListField;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("DataElement", IsNullable = false)]
    public PharosMaterialDataElement[] DataElementList
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

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialDuration
{

    private string rateField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialOwner
{

    private string nameField;

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialTrack
{

    private string mediaNameField;

    private PharosMaterialTrackTrackDefinition trackDefinitionField;

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialTrackTrackDefinition
{

    private string trackTypeNameField;

    private PharosMaterialTrackTrackDefinitionTrackFile trackFileField;

    private byte positionField;

    private bool positionFieldSpecified;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialTrackTrackDefinitionTrackFile
{

    private string nameField;

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialTrackTypeLink
{

    private string trackTypeNameField;

    private string stateMachineField;

    private string stateNameField;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialDataElement
{

    private string nameField;

    private string typeField;

    private string valueField;

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialMarker
{

    private string markerTypeField;

    private PharosMaterialMarkerTimecode timecodeField;

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialMarkerTimecode
{

    private string rateField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
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

/// <remarks/>
[System.SerializableAttribute()]
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialSegmentSegmentGroup
{

    private string nameField;

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialSegmentSegmentType
{

    private string nameField;

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialSegmentMarkerIn
{

    private string timecodeField;

    private string frameRateField;

    /// <remarks/>
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

    /// <remarks/>
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class PharosMaterialSegmentMarkerOut
{

    private string timecodeField;

    private string frameRateField;

    /// <remarks/>
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

    /// <remarks/>
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