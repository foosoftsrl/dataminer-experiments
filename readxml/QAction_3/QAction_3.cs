using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;
using System.Xml.Serialization;
using Skyline.DataMiner.Scripting;
using System.Xml;
using System.IO;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class items
    {

        private itemsItem[] itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public itemsItem[] item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class itemsItem
    {

        private byte idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    public static void Run(SLProtocolExt protocol)
    {
        try
        {
            protocol.Anumber = (double)protocol.Anumber + 2;
            // Get Text based xml data as a string.
            string textData = @"
                <items>
                  <item id=""1""/>
                  <item id=""2""/>
                  <item id=""3""/>
                  <item id=""4""/>
                  <item id=""5""/>
                  <item id=""6""/>
                </items>
            ";
            // Convert C# Generated Classes into Qaction Rows.
            items items_ = XmlDeserializeFromString<items>(textData);

            List<object[]> instances = new List<object[]>();
            // Convert Generated class into Connector Row data.
            foreach (itemsItem instance in items_.item)
            {
                instances.Add(new TablenameQActionRow
                {
                    Tablenameinstance_2001 = instance.id,
                    Tablenameinstance2_2002 = instance.id,
                }.ToObjectArray());
            }
            protocol.Debug = items_.item.Length;
            protocol.FillArray(Parameter.Tablename.tablePid, instances, NotifyProtocol.SaveOption.Full);
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
    public static T XmlDeserializeFromString<T>(this string xmlTextData)
    {
        object result;
        try
        {
            using (TextReader reader = new StringReader(xmlTextData))
            {
                var serializer = new XmlSerializer(typeof(T));
                result = serializer.Deserialize(reader);
            }
            return (T)result;
        }
        catch
        {
            return default(T);
        }
    }
}
