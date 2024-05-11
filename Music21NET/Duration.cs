namespace Music21NET.Duration;

using OffsetQLIn = Double;

static class DurationConstants
{
    static Dictionary<string, double> typeToDuration = new Dictionary<string, double>
    {
        { "duplex-maxima", 64.0 },
        { "maxima", 32.0 },
        { "longa", 16.0 },
        { "breve", 8.0 },
        { "whole", 4.0 },
        { "half", 2.0 },
        { "quarter", 1.0 },
        { "eighth", 0.5 },
        { "16th", 0.25 },
        { "32nd", 0.125 },
        { "64th", 0.0625 },
        { "128th", 0.03125 },
        { "256th", 0.015625 },
        { "512th", 0.015625 / 2.0 },
        { "1024th", 0.015625 / 4.0 },
        { "2048th", 0.015625 / 8.0 },
        { "zero", 0.0 },
    };

    static Dictionary<double, string> typeFromNumDict = new Dictionary<double, string>
    {
        { 1.0, "whole" },
        { 2.0, "half" },
        { 4.0, "quarter" },
        { 8.0, "eighth" },
        { 16.0, "16th" },
        { 32.0, "32nd" },
        { 64.0, "64th" },
        { 128.0, "128th" },
        { 256.0, "256th" },
        { 512.0, "512th" },
        { 1024.0, "1024th" },
        { 2048.0, "2048th" },
        { 0.0, "zero" },
        { 0.5, "breve" },
        { 0.25, "longa" },
        { 0.125, "maxima" },
        { 0.0625, "duplex-maxima" },
    };

    static List<double> typeFromNumDictKeys = typeFromNumDict.Keys.OrderBy(x => x).ToList();

    internal static List<string> ordinalTypeFromNum = new List<string>
    {
        "duplex-maxima",
        "maxima",
        "longa",
        "breve",
        "whole",
        "half",
        "quarter",
        "eighth",
        "16th",
        "32nd",
        "64th",
        "128th",
        "256th",
        "512th",
        "1024th",
        "2048th",
    };
}

class DurationException(string message) : Exception(message);

public class Duration
{
    bool isGrace = false;
    string Type { get; set; }
    int Dots { get; set; }
    OffsetQLIn QuarterLength { get; set; }
    DurationTuple DurationTuple { get; set; }
    IEnumerable<DurationTuple> Components { get; set; }

    //  Music21Object Client { get; set; }

    private bool _componentNeedsUpdating = false;
    private bool _quarterLengthNeedsUpdating = false;
    private bool _typeNeedsUpdating = false;

    private string _unlinkedType = null;

    // TODO: keep porting

    Duration(
        object typeOrDuration = null,
        string type = null,
        int dots = 0,
        OffsetQLIn quarterLength = 0,
        DurationTuple durationTuple = null,
        IEnumerable<DurationTuple> components = null
    // Music21Object client = null
    )
    {
        // Check the type of typeOrDuration and assign it to the appropriate property
        if (typeOrDuration is string)
        {
            Type = (string)typeOrDuration;
        }
        else if (typeOrDuration is OffsetQLIn)
        {
            QuarterLength = (OffsetQLIn)typeOrDuration;
        }
        else if (typeOrDuration is DurationTuple)
        {
            DurationTuple = (DurationTuple)typeOrDuration;
        }
        Type = type;
        Dots = dots;
        QuarterLength = quarterLength;
        DurationTuple = durationTuple;
        Components = components;
        // Client = client;
    }
}

class DurationTuple
{ //: NamedTuple {
    string Type { get; set; }
    int Dots { get; set; }
    OffsetQLIn QuarterLength { get; set; }

    DurationTuple(string type, int dots, OffsetQLIn quarterLength)
    {
        Type = type;
        Dots = dots;
        QuarterLength = quarterLength;
    }

    int ordinal
    {
        get
        {
            try
            {
                return DurationConstants.ordinalTypeFromNum.IndexOf(Type);
            }
            catch (Exception e)
            {
                throw new DurationException($"Could not determine durationNumber from {Type}");
            }
        }
    }
}
