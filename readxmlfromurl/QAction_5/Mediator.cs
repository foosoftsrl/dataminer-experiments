namespace Mediator
{
    using System;

    public class Rootobject
    {
        public Pharoscs PharosCs { get; set; }
    }

    public class Pharoscs
    {
        public Commandlist CommandList { get; set; }
    }

    public class Commandlist
    {
        public string SessionKey { get; set; }

        public Command[] Command { get; set; }
    }

    public class Command
    {
        public string GeneralType { get; set; }

        public string Subsystem { get; set; }

        public string Method { get; set; }

        public string Type { get; set; }

        public bool Success { get; set; }

        public string Context { get; set; }

        public float ExecutionTime { get; set; }

        public Output Output { get; set; }

        public Parameterlist ParameterList { get; set; }
    }

    public class Parameterlist
    {
        public Cql Cql { get; set; }
    }

    public class Cql
    {
        public string String { get; set; }
    }

    public class Output
    {
        public Resultset ResultSet { get; set; }
    }

    public class Resultset
    {
        public int RowCount { get; set; }

        public Row[] Rows { get; set; }
    }

    public class Row
    {
        public Status Status { get; set; }

        public Startdatetime StartDateTime { get; set; }

        public Infaderate InFadeRate { get; set; }

        public Intransitionname InTransitionName { get; set; }

        public Schedulereference ScheduleReference { get; set; }

        public State State { get; set; }

        public Trimmaterialid TrimMaterialId { get; set; }

        public Title Title { get; set; }

        public Duration Duration { get; set; }

        public Id Id { get; set; }

        public Templateparameterlist TemplateParameterList { get; set; }
    }

    public class Status
    {
        public Genericlist GenericList { get; set; }
    }

    public class Genericlist
    {
        public int Size { get; set; }

        public Object[] Object { get; set; }
    }

    public class Object
    {
        public int Id { get; set; }

        public string ChainApparatusLocator { get; set; }

        public string MachineStatus { get; set; }

        public string PlaytimeTransferStatus { get; set; }

        public int SequenceId { get; set; }

        public string TransferStatus { get; set; }

        public string TxMedia { get; set; }
    }

    public class Startdatetime
    {
        public Genericlist1 GenericList { get; set; }
    }

    public class Genericlist1
    {
        public int Size { get; set; }

        public Object1[] Object { get; set; }
    }

    public class Object1
    {
        public string Date { get; set; }

        public long NanoOfDay { get; set; }

        public string Rate { get; set; }

        public String ISO8601 { get; set; }
    }

    public class Infaderate
    {
        public Genericlist2 GenericList { get; set; }
    }

    public class Genericlist2
    {
        public int Size { get; set; }

        public int[] Object { get; set; }
    }

    public class Intransitionname
    {
        public Genericlist3 GenericList { get; set; }
    }

    public class Genericlist3
    {
        public int Size { get; set; }

        public string[] Object { get; set; }
    }

    public class Schedulereference
    {
        public Genericlist4 GenericList { get; set; }
    }

    public class Genericlist4
    {
        public int Size { get; set; }

        public string[] Object { get; set; }
    }

    public class State
    {
        public Genericlist5 GenericList { get; set; }
    }

    public class Genericlist5
    {
        public int Size { get; set; }

        public string[] Object { get; set; }
    }

    public class Trimmaterialid
    {
        public Genericlist6 GenericList { get; set; }
    }

    public class Genericlist6
    {
        public int Size { get; set; }

        public string[] Object { get; set; }
    }

    public class Title
    {
        public Genericlist7 GenericList { get; set; }
    }

    public class Genericlist7
    {
        public int Size { get; set; }

        public string[] Object { get; set; }
    }

    public class Duration
    {
        public Genericlist8 GenericList { get; set; }
    }

    public class Genericlist8
    {
        public int Size { get; set; }

        public Object2[] Object { get; set; }
    }

    public class Object2
    {
        public long Nanos { get; set; }

        public string Rate { get; set; }

        public string Time { get; set; }
    }

    public class Id
    {
        public Genericlist9 GenericList { get; set; }
    }

    public class Genericlist9
    {
        public int Size { get; set; }

        public int[] Object { get; set; }
    }

    public class Templateparameterlist
    {
        public Genericlist10 GenericList { get; set; }
    }

    public class Genericlist10
    {
        public int Size { get; set; }

        public Object3[] Object { get; set; }
    }

    public class Object3
    {
        public Templateparameter[] TemplateParameter { get; set; }
    }

    public class Templateparameter
    {
        public string GeneralType { get; set; }

        public string Name { get; set; }

        public object Value { get; set; }

        public string Type { get; set; }

        public string DropTargetField { get; set; }

        public bool Runtime { get; set; }
    }
}