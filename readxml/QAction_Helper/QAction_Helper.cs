// <auto-generated>This is auto-generated code by DIS. Do not modify.</auto-generated>
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Skyline.DataMiner.Scripting
{
public static class Parameter
{
	/// <summary>PID: 10000 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int adsalesiterationcounter_10000 = 10000;
	/// <summary>PID: 10000 | Type: read</summary>
	public const int adsalesiterationcounter = 10000;
	/// <summary>PID: 10001 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int adsalesdebugmsg_10001 = 10001;
	/// <summary>PID: 10001 | Type: read</summary>
	public const int adsalesdebugmsg = 10001;
	/// <summary>PID: 10100 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int mediatoriterationcounter_10100 = 10100;
	/// <summary>PID: 10100 | Type: read</summary>
	public const int mediatoriterationcounter = 10100;
	/// <summary>PID: 10101 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int mediatordebugmsg_10101 = 10101;
	/// <summary>PID: 10101 | Type: read</summary>
	public const int mediatordebugmsg = 10101;
	/// <summary>PID: 10200 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int mergediterationcounter_10200 = 10200;
	/// <summary>PID: 10200 | Type: read</summary>
	public const int mergediterationcounter = 10200;
	/// <summary>PID: 10201 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int mergeddebugmsg_10201 = 10201;
	/// <summary>PID: 10201 | Type: read</summary>
	public const int mergeddebugmsg = 10201;
	public class Write
	{
		/// <summary>PID: 10002 | Type: write</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const int adsalesprocessfile_10002 = 10002;
		/// <summary>PID: 10002 | Type: write</summary>
		public const int adsalesprocessfile = 10002;
		/// <summary>PID: 10102 | Type: write</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const int mediatorprocessfile_10102 = 10102;
		/// <summary>PID: 10102 | Type: write</summary>
		public const int mediatorprocessfile = 10102;
		/// <summary>PID: 10202 | Type: write</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const int mergedprocesstable_10202 = 10202;
		/// <summary>PID: 10202 | Type: write</summary>
		public const int mergedprocesstable = 10202;
	}
	public class Adsales
	{
		/// <summary>PID: 2000</summary>
		public const int tablePid = 2000;
		/// <summary>IDX: 0</summary>
		public const int indexColumn = 0;
		/// <summary>PID: 2001</summary>
		public const int indexColumnPid = 2001;
		public class Pid
		{
			/// <summary>PID: 2001 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int adsalesid_2001 = 2001;
			/// <summary>PID: 2001 | Type: read</summary>
			public const int adsalesid = 2001;
			/// <summary>PID: 2002 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int adsalestitle_2002 = 2002;
			/// <summary>PID: 2002 | Type: read</summary>
			public const int adsalestitle = 2002;
			/// <summary>PID: 2003 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int adsalestime_2003 = 2003;
			/// <summary>PID: 2003 | Type: read</summary>
			public const int adsalestime = 2003;
			public class Write
			{
			}
		}
		public class Idx
		{
			/// <summary>IDX: 0 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int adsalesid_2001 = 0;
			/// <summary>IDX: 0 | Type: read</summary>
			public const int adsalesid = 0;
			/// <summary>IDX: 1 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int adsalestitle_2002 = 1;
			/// <summary>IDX: 1 | Type: read</summary>
			public const int adsalestitle = 1;
			/// <summary>IDX: 2 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int adsalestime_2003 = 2;
			/// <summary>IDX: 2 | Type: read</summary>
			public const int adsalestime = 2;
		}
	}
	public class Mediator
	{
		/// <summary>PID: 2100</summary>
		public const int tablePid = 2100;
		/// <summary>IDX: 0</summary>
		public const int indexColumn = 0;
		/// <summary>PID: 2101</summary>
		public const int indexColumnPid = 2101;
		public class Pid
		{
			/// <summary>PID: 2101 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mediatorid_2101 = 2101;
			/// <summary>PID: 2101 | Type: read</summary>
			public const int mediatorid = 2101;
			/// <summary>PID: 2102 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mediatordate_2102 = 2102;
			/// <summary>PID: 2102 | Type: read</summary>
			public const int mediatordate = 2102;
			public class Write
			{
			}
		}
		public class Idx
		{
			/// <summary>IDX: 0 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mediatorid_2101 = 0;
			/// <summary>IDX: 0 | Type: read</summary>
			public const int mediatorid = 0;
			/// <summary>IDX: 1 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mediatordate_2102 = 1;
			/// <summary>IDX: 1 | Type: read</summary>
			public const int mediatordate = 1;
		}
	}
	public class Mergedtable
	{
		/// <summary>PID: 3000</summary>
		public const int tablePid = 3000;
		/// <summary>IDX: 0</summary>
		public const int indexColumn = 0;
		/// <summary>PID: 3001</summary>
		public const int indexColumnPid = 3001;
		public class Pid
		{
			/// <summary>PID: 3001 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergedtablecolumn1_3001 = 3001;
			/// <summary>PID: 3001 | Type: read</summary>
			public const int mergedtablecolumn1 = 3001;
			/// <summary>PID: 3002 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergedtablecolumn2_3002 = 3002;
			/// <summary>PID: 3002 | Type: read</summary>
			public const int mergedtablecolumn2 = 3002;
			/// <summary>PID: 3003 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergedtablecolumn3_3003 = 3003;
			/// <summary>PID: 3003 | Type: read</summary>
			public const int mergedtablecolumn3 = 3003;
			/// <summary>PID: 3004 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergeddatatablecolumn4_3004 = 3004;
			/// <summary>PID: 3004 | Type: read</summary>
			public const int mergeddatatablecolumn4 = 3004;
			/// <summary>PID: 3005 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergeddatatablecolumn5_3005 = 3005;
			/// <summary>PID: 3005 | Type: read</summary>
			public const int mergeddatatablecolumn5 = 3005;
			public class Write
			{
			}
		}
		public class Idx
		{
			/// <summary>IDX: 0 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergedtablecolumn1_3001 = 0;
			/// <summary>IDX: 0 | Type: read</summary>
			public const int mergedtablecolumn1 = 0;
			/// <summary>IDX: 1 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergedtablecolumn2_3002 = 1;
			/// <summary>IDX: 1 | Type: read</summary>
			public const int mergedtablecolumn2 = 1;
			/// <summary>IDX: 2 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergedtablecolumn3_3003 = 2;
			/// <summary>IDX: 2 | Type: read</summary>
			public const int mergedtablecolumn3 = 2;
			/// <summary>IDX: 3 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergeddatatablecolumn4_3004 = 3;
			/// <summary>IDX: 3 | Type: read</summary>
			public const int mergeddatatablecolumn4 = 3;
			/// <summary>IDX: 4 | Type: read</summary>
			[EditorBrowsable(EditorBrowsableState.Never)]
			public const int mergeddatatablecolumn5_3005 = 4;
			/// <summary>IDX: 4 | Type: read</summary>
			public const int mergeddatatablecolumn5 = 4;
		}
	}
}
public class WriteParameters
{
	/// <summary>PID: 10002  | Type: write | DISCREETS: Process Adsales XML File = check</summary>
	public System.Object Adsalesprocessfile {get { return Protocol.GetParameter(10002); }set { Protocol.SetParameter(10002, value); }}
	/// <summary>PID: 10102  | Type: write | DISCREETS: Process Mediator JSON File = check</summary>
	public System.Object Mediatorprocessfile {get { return Protocol.GetParameter(10102); }set { Protocol.SetParameter(10102, value); }}
	/// <summary>PID: 10202  | Type: write | DISCREETS: Merge Tables = check</summary>
	public System.Object Mergedprocesstable {get { return Protocol.GetParameter(10202); }set { Protocol.SetParameter(10202, value); }}
	public SLProtocolExt Protocol;
	public WriteParameters(SLProtocolExt protocol)
	{
		Protocol = protocol;
	}
}
public interface SLProtocolExt : SLProtocol
{
	/// <summary>PID: 2000</summary>
	AdsalesQActionTable adsales { get; set; }
	/// <summary>PID: 2100</summary>
	MediatorQActionTable mediator { get; set; }
	/// <summary>PID: 3000</summary>
	MergedtableQActionTable mergedtable { get; set; }
	object Afterstartup_dummy { get; set; }
	object Triggerqaction_dummy { get; set; }
	object Triggermergedqaction_dummy { get; set; }
	object Adsalesid_2001 { get; set; }
	object Adsalesid { get; set; }
	object Adsalestitle_2002 { get; set; }
	object Adsalestitle { get; set; }
	object Adsalestime_2003 { get; set; }
	object Adsalestime { get; set; }
	object Mediatorid_2101 { get; set; }
	object Mediatorid { get; set; }
	object Mediatordate_2102 { get; set; }
	object Mediatordate { get; set; }
	object Mergedtablecolumn1_3001 { get; set; }
	object Mergedtablecolumn1 { get; set; }
	object Mergedtablecolumn2_3002 { get; set; }
	object Mergedtablecolumn2 { get; set; }
	object Mergedtablecolumn3_3003 { get; set; }
	object Mergedtablecolumn3 { get; set; }
	object Mergeddatatablecolumn4_3004 { get; set; }
	object Mergeddatatablecolumn4 { get; set; }
	object Mergeddatatablecolumn5_3005 { get; set; }
	object Mergeddatatablecolumn5 { get; set; }
	object Adsalesiterationcounter_10000 { get; set; }
	object Adsalesiterationcounter { get; set; }
	object Adsalesdebugmsg_10001 { get; set; }
	object Adsalesdebugmsg { get; set; }
	object Adsalesprocessfile_10002 { get; set; }
	object Adsalesprocessfile { get; set; }
	object Mediatoriterationcounter_10100 { get; set; }
	object Mediatoriterationcounter { get; set; }
	object Mediatordebugmsg_10101 { get; set; }
	object Mediatordebugmsg { get; set; }
	object Mediatorprocessfile_10102 { get; set; }
	object Mediatorprocessfile { get; set; }
	object Mergediterationcounter_10200 { get; set; }
	object Mergediterationcounter { get; set; }
	object Mergeddebugmsg_10201 { get; set; }
	object Mergeddebugmsg { get; set; }
	object Mergedprocesstable_10202 { get; set; }
	object Mergedprocesstable { get; set; }
	WriteParameters Write { get; set; }
}
public class ConcreteSLProtocolExt : ConcreteSLProtocol, SLProtocolExt
{
	/// <summary>PID: 2000</summary>
	public AdsalesQActionTable adsales { get; set; }
	/// <summary>PID: 2100</summary>
	public MediatorQActionTable mediator { get; set; }
	/// <summary>PID: 3000</summary>
	public MergedtableQActionTable mergedtable { get; set; }
	/// <summary>PID: 1  | Type: dummy</summary>
	public System.Object Afterstartup_dummy {get { return GetParameter(1); }set { SetParameter(1, value); }}
	/// <summary>PID: 5  | Type: dummy</summary>
	public System.Object Triggerqaction_dummy {get { return GetParameter(5); }set { SetParameter(5, value); }}
	/// <summary>PID: 6  | Type: dummy</summary>
	public System.Object Triggermergedqaction_dummy {get { return GetParameter(6); }set { SetParameter(6, value); }}
	/// <summary>PID: 2001  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalesid_2001 {get { return GetParameter(2001); }set { SetParameter(2001, value); }}
	/// <summary>PID: 2001  | Type: read</summary>
	public System.Object Adsalesid {get { return GetParameter(2001); }set { SetParameter(2001, value); }}
	/// <summary>PID: 2002  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalestitle_2002 {get { return GetParameter(2002); }set { SetParameter(2002, value); }}
	/// <summary>PID: 2002  | Type: read</summary>
	public System.Object Adsalestitle {get { return GetParameter(2002); }set { SetParameter(2002, value); }}
	/// <summary>PID: 2003  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalestime_2003 {get { return GetParameter(2003); }set { SetParameter(2003, value); }}
	/// <summary>PID: 2003  | Type: read</summary>
	public System.Object Adsalestime {get { return GetParameter(2003); }set { SetParameter(2003, value); }}
	/// <summary>PID: 2101  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mediatorid_2101 {get { return GetParameter(2101); }set { SetParameter(2101, value); }}
	/// <summary>PID: 2101  | Type: read</summary>
	public System.Object Mediatorid {get { return GetParameter(2101); }set { SetParameter(2101, value); }}
	/// <summary>PID: 2102  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mediatordate_2102 {get { return GetParameter(2102); }set { SetParameter(2102, value); }}
	/// <summary>PID: 2102  | Type: read</summary>
	public System.Object Mediatordate {get { return GetParameter(2102); }set { SetParameter(2102, value); }}
	/// <summary>PID: 3001  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergedtablecolumn1_3001 {get { return GetParameter(3001); }set { SetParameter(3001, value); }}
	/// <summary>PID: 3001  | Type: read</summary>
	public System.Object Mergedtablecolumn1 {get { return GetParameter(3001); }set { SetParameter(3001, value); }}
	/// <summary>PID: 3002  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergedtablecolumn2_3002 {get { return GetParameter(3002); }set { SetParameter(3002, value); }}
	/// <summary>PID: 3002  | Type: read</summary>
	public System.Object Mergedtablecolumn2 {get { return GetParameter(3002); }set { SetParameter(3002, value); }}
	/// <summary>PID: 3003  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergedtablecolumn3_3003 {get { return GetParameter(3003); }set { SetParameter(3003, value); }}
	/// <summary>PID: 3003  | Type: read</summary>
	public System.Object Mergedtablecolumn3 {get { return GetParameter(3003); }set { SetParameter(3003, value); }}
	/// <summary>PID: 3004  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergeddatatablecolumn4_3004 {get { return GetParameter(3004); }set { SetParameter(3004, value); }}
	/// <summary>PID: 3004  | Type: read</summary>
	public System.Object Mergeddatatablecolumn4 {get { return GetParameter(3004); }set { SetParameter(3004, value); }}
	/// <summary>PID: 3005  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergeddatatablecolumn5_3005 {get { return GetParameter(3005); }set { SetParameter(3005, value); }}
	/// <summary>PID: 3005  | Type: read</summary>
	public System.Object Mergeddatatablecolumn5 {get { return GetParameter(3005); }set { SetParameter(3005, value); }}
	/// <summary>PID: 10000  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalesiterationcounter_10000 {get { return GetParameter(10000); }set { SetParameter(10000, value); }}
	/// <summary>PID: 10000  | Type: read</summary>
	public System.Object Adsalesiterationcounter {get { return GetParameter(10000); }set { SetParameter(10000, value); }}
	/// <summary>PID: 10001  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalesdebugmsg_10001 {get { return GetParameter(10001); }set { SetParameter(10001, value); }}
	/// <summary>PID: 10001  | Type: read</summary>
	public System.Object Adsalesdebugmsg {get { return GetParameter(10001); }set { SetParameter(10001, value); }}
	/// <summary>PID: 10002  | Type: write | DISCREETS: Process Adsales XML File = check</summary>
	public System.Object Adsalesprocessfile_10002 {get { return GetParameter(10002); }set { SetParameter(10002, value); }}
	/// <summary>PID: 10002  | Type: write | DISCREETS: Process Adsales XML File = check</summary>
	public System.Object Adsalesprocessfile {get { return Write.Adsalesprocessfile; }set { Write.Adsalesprocessfile = value; }}
	/// <summary>PID: 10100  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mediatoriterationcounter_10100 {get { return GetParameter(10100); }set { SetParameter(10100, value); }}
	/// <summary>PID: 10100  | Type: read</summary>
	public System.Object Mediatoriterationcounter {get { return GetParameter(10100); }set { SetParameter(10100, value); }}
	/// <summary>PID: 10101  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mediatordebugmsg_10101 {get { return GetParameter(10101); }set { SetParameter(10101, value); }}
	/// <summary>PID: 10101  | Type: read</summary>
	public System.Object Mediatordebugmsg {get { return GetParameter(10101); }set { SetParameter(10101, value); }}
	/// <summary>PID: 10102  | Type: write | DISCREETS: Process Mediator JSON File = check</summary>
	public System.Object Mediatorprocessfile_10102 {get { return GetParameter(10102); }set { SetParameter(10102, value); }}
	/// <summary>PID: 10102  | Type: write | DISCREETS: Process Mediator JSON File = check</summary>
	public System.Object Mediatorprocessfile {get { return Write.Mediatorprocessfile; }set { Write.Mediatorprocessfile = value; }}
	/// <summary>PID: 10200  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergediterationcounter_10200 {get { return GetParameter(10200); }set { SetParameter(10200, value); }}
	/// <summary>PID: 10200  | Type: read</summary>
	public System.Object Mergediterationcounter {get { return GetParameter(10200); }set { SetParameter(10200, value); }}
	/// <summary>PID: 10201  | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergeddebugmsg_10201 {get { return GetParameter(10201); }set { SetParameter(10201, value); }}
	/// <summary>PID: 10201  | Type: read</summary>
	public System.Object Mergeddebugmsg {get { return GetParameter(10201); }set { SetParameter(10201, value); }}
	/// <summary>PID: 10202  | Type: write | DISCREETS: Merge Tables = check</summary>
	public System.Object Mergedprocesstable_10202 {get { return GetParameter(10202); }set { SetParameter(10202, value); }}
	/// <summary>PID: 10202  | Type: write | DISCREETS: Merge Tables = check</summary>
	public System.Object Mergedprocesstable {get { return Write.Mergedprocesstable; }set { Write.Mergedprocesstable = value; }}
	public WriteParameters Write { get; set; }
	public ConcreteSLProtocolExt()
	{
		adsales = new AdsalesQActionTable(this, 2000, "adsales");
		mediator = new MediatorQActionTable(this, 2100, "mediator");
		mergedtable = new MergedtableQActionTable(this, 3000, "mergedtable");
		Write = new WriteParameters(this);
	}
}
/// <summary>IDX: 0</summary>
public class AdsalesQActionTable : QActionTable, IEnumerable<AdsalesQActionRow>
{
	public AdsalesQActionTable(SLProtocol protocol, int tableId, string tableName) : base(protocol, tableId, tableName) { }
	IEnumerator IEnumerable.GetEnumerator() { return (IEnumerator) GetEnumerator(); }
	public IEnumerator<AdsalesQActionRow> GetEnumerator() { return new QActionTableEnumerator<AdsalesQActionRow>(this); }
}
/// <summary>IDX: 0</summary>
public class MediatorQActionTable : QActionTable, IEnumerable<MediatorQActionRow>
{
	public MediatorQActionTable(SLProtocol protocol, int tableId, string tableName) : base(protocol, tableId, tableName) { }
	IEnumerator IEnumerable.GetEnumerator() { return (IEnumerator) GetEnumerator(); }
	public IEnumerator<MediatorQActionRow> GetEnumerator() { return new QActionTableEnumerator<MediatorQActionRow>(this); }
}
/// <summary>IDX: 0</summary>
public class MergedtableQActionTable : QActionTable, IEnumerable<MergedtableQActionRow>
{
	public MergedtableQActionTable(SLProtocol protocol, int tableId, string tableName) : base(protocol, tableId, tableName) { }
	IEnumerator IEnumerable.GetEnumerator() { return (IEnumerator) GetEnumerator(); }
	public IEnumerator<MergedtableQActionRow> GetEnumerator() { return new QActionTableEnumerator<MergedtableQActionRow>(this); }
}
/// <summary>IDX: 0</summary>
public class AdsalesQActionRow : QActionTableRow
{
	/// <summary>PID: 2001 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalesid_2001 { get { if (base.Columns.ContainsKey(0)) { return base.Columns[0]; } else { return null; } } set { if (base.Columns.ContainsKey(0)) { base.Columns[0] = value; } else { base.Columns.Add(0, value); } } }
	/// <summary>PID: 2001 | Type: read</summary>
	public System.Object Adsalesid { get { if (base.Columns.ContainsKey(0)) { return base.Columns[0]; } else { return null; } } set { if (base.Columns.ContainsKey(0)) { base.Columns[0] = value; } else { base.Columns.Add(0, value); } } }
	/// <summary>PID: 2002 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalestitle_2002 { get { if (base.Columns.ContainsKey(1)) { return base.Columns[1]; } else { return null; } } set { if (base.Columns.ContainsKey(1)) { base.Columns[1] = value; } else { base.Columns.Add(1, value); } } }
	/// <summary>PID: 2002 | Type: read</summary>
	public System.Object Adsalestitle { get { if (base.Columns.ContainsKey(1)) { return base.Columns[1]; } else { return null; } } set { if (base.Columns.ContainsKey(1)) { base.Columns[1] = value; } else { base.Columns.Add(1, value); } } }
	/// <summary>PID: 2003 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Adsalestime_2003 { get { if (base.Columns.ContainsKey(2)) { return base.Columns[2]; } else { return null; } } set { if (base.Columns.ContainsKey(2)) { base.Columns[2] = value; } else { base.Columns.Add(2, value); } } }
	/// <summary>PID: 2003 | Type: read</summary>
	public System.Object Adsalestime { get { if (base.Columns.ContainsKey(2)) { return base.Columns[2]; } else { return null; } } set { if (base.Columns.ContainsKey(2)) { base.Columns[2] = value; } else { base.Columns.Add(2, value); } } }
	public AdsalesQActionRow() : base(0, 3) { }
	public AdsalesQActionRow(System.Object[] oRow) : base(0, 3, oRow) { }
	public static implicit operator AdsalesQActionRow(System.Object[] source) { return new AdsalesQActionRow(source); }
	public static implicit operator System.Object[](AdsalesQActionRow source) { return source.ToObjectArray(); }
}
/// <summary>IDX: 0</summary>
public class MediatorQActionRow : QActionTableRow
{
	/// <summary>PID: 2101 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mediatorid_2101 { get { if (base.Columns.ContainsKey(0)) { return base.Columns[0]; } else { return null; } } set { if (base.Columns.ContainsKey(0)) { base.Columns[0] = value; } else { base.Columns.Add(0, value); } } }
	/// <summary>PID: 2101 | Type: read</summary>
	public System.Object Mediatorid { get { if (base.Columns.ContainsKey(0)) { return base.Columns[0]; } else { return null; } } set { if (base.Columns.ContainsKey(0)) { base.Columns[0] = value; } else { base.Columns.Add(0, value); } } }
	/// <summary>PID: 2102 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mediatordate_2102 { get { if (base.Columns.ContainsKey(1)) { return base.Columns[1]; } else { return null; } } set { if (base.Columns.ContainsKey(1)) { base.Columns[1] = value; } else { base.Columns.Add(1, value); } } }
	/// <summary>PID: 2102 | Type: read</summary>
	public System.Object Mediatordate { get { if (base.Columns.ContainsKey(1)) { return base.Columns[1]; } else { return null; } } set { if (base.Columns.ContainsKey(1)) { base.Columns[1] = value; } else { base.Columns.Add(1, value); } } }
	public MediatorQActionRow() : base(0, 2) { }
	public MediatorQActionRow(System.Object[] oRow) : base(0, 2, oRow) { }
	public static implicit operator MediatorQActionRow(System.Object[] source) { return new MediatorQActionRow(source); }
	public static implicit operator System.Object[](MediatorQActionRow source) { return source.ToObjectArray(); }
}
/// <summary>IDX: 0</summary>
public class MergedtableQActionRow : QActionTableRow
{
	/// <summary>PID: 3001 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergedtablecolumn1_3001 { get { if (base.Columns.ContainsKey(0)) { return base.Columns[0]; } else { return null; } } set { if (base.Columns.ContainsKey(0)) { base.Columns[0] = value; } else { base.Columns.Add(0, value); } } }
	/// <summary>PID: 3001 | Type: read</summary>
	public System.Object Mergedtablecolumn1 { get { if (base.Columns.ContainsKey(0)) { return base.Columns[0]; } else { return null; } } set { if (base.Columns.ContainsKey(0)) { base.Columns[0] = value; } else { base.Columns.Add(0, value); } } }
	/// <summary>PID: 3002 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergedtablecolumn2_3002 { get { if (base.Columns.ContainsKey(1)) { return base.Columns[1]; } else { return null; } } set { if (base.Columns.ContainsKey(1)) { base.Columns[1] = value; } else { base.Columns.Add(1, value); } } }
	/// <summary>PID: 3002 | Type: read</summary>
	public System.Object Mergedtablecolumn2 { get { if (base.Columns.ContainsKey(1)) { return base.Columns[1]; } else { return null; } } set { if (base.Columns.ContainsKey(1)) { base.Columns[1] = value; } else { base.Columns.Add(1, value); } } }
	/// <summary>PID: 3003 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergedtablecolumn3_3003 { get { if (base.Columns.ContainsKey(2)) { return base.Columns[2]; } else { return null; } } set { if (base.Columns.ContainsKey(2)) { base.Columns[2] = value; } else { base.Columns.Add(2, value); } } }
	/// <summary>PID: 3003 | Type: read</summary>
	public System.Object Mergedtablecolumn3 { get { if (base.Columns.ContainsKey(2)) { return base.Columns[2]; } else { return null; } } set { if (base.Columns.ContainsKey(2)) { base.Columns[2] = value; } else { base.Columns.Add(2, value); } } }
	/// <summary>PID: 3004 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergeddatatablecolumn4_3004 { get { if (base.Columns.ContainsKey(3)) { return base.Columns[3]; } else { return null; } } set { if (base.Columns.ContainsKey(3)) { base.Columns[3] = value; } else { base.Columns.Add(3, value); } } }
	/// <summary>PID: 3004 | Type: read</summary>
	public System.Object Mergeddatatablecolumn4 { get { if (base.Columns.ContainsKey(3)) { return base.Columns[3]; } else { return null; } } set { if (base.Columns.ContainsKey(3)) { base.Columns[3] = value; } else { base.Columns.Add(3, value); } } }
	/// <summary>PID: 3005 | Type: read</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public System.Object Mergeddatatablecolumn5_3005 { get { if (base.Columns.ContainsKey(4)) { return base.Columns[4]; } else { return null; } } set { if (base.Columns.ContainsKey(4)) { base.Columns[4] = value; } else { base.Columns.Add(4, value); } } }
	/// <summary>PID: 3005 | Type: read</summary>
	public System.Object Mergeddatatablecolumn5 { get { if (base.Columns.ContainsKey(4)) { return base.Columns[4]; } else { return null; } } set { if (base.Columns.ContainsKey(4)) { base.Columns[4] = value; } else { base.Columns.Add(4, value); } } }
	public MergedtableQActionRow() : base(0, 5) { }
	public MergedtableQActionRow(System.Object[] oRow) : base(0, 5, oRow) { }
	public static implicit operator MergedtableQActionRow(System.Object[] source) { return new MergedtableQActionRow(source); }
	public static implicit operator System.Object[](MergedtableQActionRow source) { return source.ToObjectArray(); }
}
}
