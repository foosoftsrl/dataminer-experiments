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
    public Startdatetime StartDateTime { get; set; }

    public Schedulereference ScheduleReference { get; set; }

    public Trimmaterialid TrimMaterialId { get; set; }

    public Duration Duration { get; set; }

    public Id Id { get; set; }
}

public class Startdatetime
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
    public string Date { get; set; }

    public long NanoOfDay { get; set; }

    public string Rate { get; set; }

    public DateTime ISO8601 { get; set; }
}

public class Schedulereference
{
    public Genericlist1 GenericList { get; set; }
}

public class Genericlist1
{
    public int Size { get; set; }

    public string[] Object { get; set; }
}

public class Trimmaterialid
{
    public Genericlist2 GenericList { get; set; }
}

public class Genericlist2
{
    public int Size { get; set; }

    public string[] Object { get; set; }
}

public class Duration
{
    public Genericlist3 GenericList { get; set; }
}

public class Genericlist3
{
    public int Size { get; set; }

    public Object1[] Object { get; set; }
}

public class Object1
{
    public long Nanos { get; set; }

    public string Rate { get; set; }

    public string Time { get; set; }
}

public class Id
{
    public Genericlist4 GenericList { get; set; }
}

public class Genericlist4
{
    public int Size { get; set; }

    public int[] Object { get; set; }
}
