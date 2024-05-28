// C# Artifacts computed from sample.xml with "Paste special" option

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.

/// <remarks/>

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Data
{
    private string channelCodeField;

    private System.DateTime scheduleDateField;

    private DataBreak[] breaksField;

    /// <remarks/>
    public string ChannelCode
    {
        get
        {
            return this.channelCodeField;
        }

        set
        {
            this.channelCodeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime ScheduleDate
    {
        get
        {
            return this.scheduleDateField;
        }

        set
        {
            this.scheduleDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Break", IsNullable = false)]
    public DataBreak[] Breaks
    {
        get
        {
            return this.breaksField;
        }

        set
        {
            this.breaksField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBreak
{
    private string breakIDField;

    private byte breakVersionForTAField;

    private string breakNominalTimeField;

    private string breakLockedTimeField;

    private object breakPositionField;

    private string breakSimulcastField;

    private string breakCommercialProductSalesField;

    private string breakQualityTypeField;

    private string breakCountDownField;

    private string breakScreenLayoutField;

    private string breakNoteField;

    private DataBreakTimeAllocation[] timeAllocationsField;

    /// <remarks/>
    public string BreakID
    {
        get
        {
            return this.breakIDField;
        }

        set
        {
            this.breakIDField = value;
        }
    }

    /// <remarks/>
    public byte BreakVersionForTA
    {
        get
        {
            return this.breakVersionForTAField;
        }

        set
        {
            this.breakVersionForTAField = value;
        }
    }

    /// <remarks/>
    public string BreakNominalTime
    {
        get
        {
            return this.breakNominalTimeField;
        }

        set
        {
            this.breakNominalTimeField = value;
        }
    }

    /// <remarks/>
    public string BreakLockedTime
    {
        get
        {
            return this.breakLockedTimeField;
        }

        set
        {
            this.breakLockedTimeField = value;
        }
    }

    /// <remarks/>
    public object BreakPosition
    {
        get
        {
            return this.breakPositionField;
        }

        set
        {
            this.breakPositionField = value;
        }
    }

    /// <remarks/>
    public string BreakSimulcast
    {
        get
        {
            return this.breakSimulcastField;
        }

        set
        {
            this.breakSimulcastField = value;
        }
    }

    /// <remarks/>
    public string BreakCommercialProductSales
    {
        get
        {
            return this.breakCommercialProductSalesField;
        }

        set
        {
            this.breakCommercialProductSalesField = value;
        }
    }

    /// <remarks/>
    public string BreakQualityType
    {
        get
        {
            return this.breakQualityTypeField;
        }

        set
        {
            this.breakQualityTypeField = value;
        }
    }

    /// <remarks/>
    public string BreakCountDown
    {
        get
        {
            return this.breakCountDownField;
        }

        set
        {
            this.breakCountDownField = value;
        }
    }

    /// <remarks/>
    public string BreakScreenLayout
    {
        get
        {
            return this.breakScreenLayoutField;
        }

        set
        {
            this.breakScreenLayoutField = value;
        }
    }

    /// <remarks/>
    public string BreakNote
    {
        get
        {
            return this.breakNoteField;
        }

        set
        {
            this.breakNoteField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("TimeAllocation", IsNullable = false)]
    public DataBreakTimeAllocation[] TimeAllocations
    {
        get
        {
            return this.timeAllocationsField;
        }

        set
        {
            this.timeAllocationsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBreakTimeAllocation
{
    private string timeAllocationTypeField;

    private byte timeAllocationPositionField;

    private ushort timeAllocationNominalDurationField;

    private string timeAllocationOTTSubstitutionField;

    private string timeAllocationDenyTrailerBeforeField;

    private string timeAllocationSecondaryTypeField;

    private object timeAllocationSecondaryReconcileKeyField;

    private object timeAllocationSecondaryProductCodeField;

    private object timeAllocationSecondaryBrandField;

    private object timeAllocationPictureFrameLayoutField;

    private DataBreakTimeAllocationContent[] contentsField;

    /// <remarks/>
    public string TimeAllocationType
    {
        get
        {
            return this.timeAllocationTypeField;
        }

        set
        {
            this.timeAllocationTypeField = value;
        }
    }

    /// <remarks/>
    public byte TimeAllocationPosition
    {
        get
        {
            return this.timeAllocationPositionField;
        }

        set
        {
            this.timeAllocationPositionField = value;
        }
    }

    /// <remarks/>
    public ushort TimeAllocationNominalDuration
    {
        get
        {
            return this.timeAllocationNominalDurationField;
        }

        set
        {
            this.timeAllocationNominalDurationField = value;
        }
    }

    /// <remarks/>
    public string TimeAllocationOTTSubstitution
    {
        get
        {
            return this.timeAllocationOTTSubstitutionField;
        }

        set
        {
            this.timeAllocationOTTSubstitutionField = value;
        }
    }

    /// <remarks/>
    public string TimeAllocationDenyTrailerBefore
    {
        get
        {
            return this.timeAllocationDenyTrailerBeforeField;
        }

        set
        {
            this.timeAllocationDenyTrailerBeforeField = value;
        }
    }

    /// <remarks/>
    public string TimeAllocationSecondaryType
    {
        get
        {
            return this.timeAllocationSecondaryTypeField;
        }

        set
        {
            this.timeAllocationSecondaryTypeField = value;
        }
    }

    /// <remarks/>
    public object TimeAllocationSecondaryReconcileKey
    {
        get
        {
            return this.timeAllocationSecondaryReconcileKeyField;
        }

        set
        {
            this.timeAllocationSecondaryReconcileKeyField = value;
        }
    }

    /// <remarks/>
    public object TimeAllocationSecondaryProductCode
    {
        get
        {
            return this.timeAllocationSecondaryProductCodeField;
        }

        set
        {
            this.timeAllocationSecondaryProductCodeField = value;
        }
    }

    /// <remarks/>
    public object TimeAllocationSecondaryBrand
    {
        get
        {
            return this.timeAllocationSecondaryBrandField;
        }

        set
        {
            this.timeAllocationSecondaryBrandField = value;
        }
    }

    /// <remarks/>
    public object TimeAllocationPictureFrameLayout
    {
        get
        {
            return this.timeAllocationPictureFrameLayoutField;
        }

        set
        {
            this.timeAllocationPictureFrameLayoutField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Content", IsNullable = false)]
    public DataBreakTimeAllocationContent[] Contents
    {
        get
        {
            return this.contentsField;
        }

        set
        {
            this.contentsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBreakTimeAllocationContent
{
    private uint contentReconcileKeyField;

    private byte contentOrderField;

    private string contentProductCodeField;

    private string contentAdvertiserNameField;

    private string contentBrandField;

    private string contentTypeField;

    private byte contentTotalDurationField;

    private string contentUnsponsoredDurationField;

    private string contentAdvertisementLimitCheckField;

    private string[] contentRiskTypesField;

    private string contentDestinationField;

    private object contentPictureFrameLayoutField;

    private string contentSubtitledField;

    private DataBreakTimeAllocationContentContentSubtitles contentSubtitlesField;

    private string contentAudiodescriptedField;

    private string contentEnablerField;

    /// <remarks/>
    public uint ContentReconcileKey
    {
        get
        {
            return this.contentReconcileKeyField;
        }

        set
        {
            this.contentReconcileKeyField = value;
        }
    }

    /// <remarks/>
    public byte ContentOrder
    {
        get
        {
            return this.contentOrderField;
        }

        set
        {
            this.contentOrderField = value;
        }
    }

    /// <remarks/>
    public string ContentProductCode
    {
        get
        {
            return this.contentProductCodeField;
        }

        set
        {
            this.contentProductCodeField = value;
        }
    }

    /// <remarks/>
    public string ContentAdvertiserName
    {
        get
        {
            return this.contentAdvertiserNameField;
        }

        set
        {
            this.contentAdvertiserNameField = value;
        }
    }

    /// <remarks/>
    public string ContentBrand
    {
        get
        {
            return this.contentBrandField;
        }

        set
        {
            this.contentBrandField = value;
        }
    }

    /// <remarks/>
    public string ContentType
    {
        get
        {
            return this.contentTypeField;
        }

        set
        {
            this.contentTypeField = value;
        }
    }

    /// <remarks/>
    public byte ContentTotalDuration
    {
        get
        {
            return this.contentTotalDurationField;
        }

        set
        {
            this.contentTotalDurationField = value;
        }
    }

    /// <remarks/>
    public string ContentUnsponsoredDuration
    {
        get
        {
            return this.contentUnsponsoredDurationField;
        }

        set
        {
            this.contentUnsponsoredDurationField = value;
        }
    }

    /// <remarks/>
    public string ContentAdvertisementLimitCheck
    {
        get
        {
            return this.contentAdvertisementLimitCheckField;
        }

        set
        {
            this.contentAdvertisementLimitCheckField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("ContentRiskType", IsNullable = false)]
    public string[] ContentRiskTypes
    {
        get
        {
            return this.contentRiskTypesField;
        }

        set
        {
            this.contentRiskTypesField = value;
        }
    }

    /// <remarks/>
    public string ContentDestination
    {
        get
        {
            return this.contentDestinationField;
        }

        set
        {
            this.contentDestinationField = value;
        }
    }

    /// <remarks/>
    public object ContentPictureFrameLayout
    {
        get
        {
            return this.contentPictureFrameLayoutField;
        }

        set
        {
            this.contentPictureFrameLayoutField = value;
        }
    }

    /// <remarks/>
    public string ContentSubtitled
    {
        get
        {
            return this.contentSubtitledField;
        }

        set
        {
            this.contentSubtitledField = value;
        }
    }

    /// <remarks/>
    public DataBreakTimeAllocationContentContentSubtitles ContentSubtitles
    {
        get
        {
            return this.contentSubtitlesField;
        }

        set
        {
            this.contentSubtitlesField = value;
        }
    }

    /// <remarks/>
    public string ContentAudiodescripted
    {
        get
        {
            return this.contentAudiodescriptedField;
        }

        set
        {
            this.contentAudiodescriptedField = value;
        }
    }

    /// <remarks/>
    public string ContentEnabler
    {
        get
        {
            return this.contentEnablerField;
        }

        set
        {
            this.contentEnablerField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBreakTimeAllocationContentContentSubtitles
{

    private DataBreakTimeAllocationContentContentSubtitlesContentSubtitle contentSubtitleField;

    /// <remarks/>
    public DataBreakTimeAllocationContentContentSubtitlesContentSubtitle ContentSubtitle
    {
        get
        {
            return this.contentSubtitleField;
        }

        set
        {
            this.contentSubtitleField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBreakTimeAllocationContentContentSubtitlesContentSubtitle
{

    private string contentSubtitleLanguageField;

    private string contentSubtitleTypeField;

    /// <remarks/>
    public string ContentSubtitleLanguage
    {
        get
        {
            return this.contentSubtitleLanguageField;
        }

        set
        {
            this.contentSubtitleLanguageField = value;
        }
    }

    /// <remarks/>
    public string ContentSubtitleType
    {
        get
        {
            return this.contentSubtitleTypeField;
        }

        set
        {
            this.contentSubtitleTypeField = value;
        }
    }
}
