namespace Music21NET.Note;

class LyricException(string message) : Exception(message);

class NoteException(string message) : Exception(message);

class NotRestException(string message) : Exception(message);

enum SyllabicChoices
{
    Begin,
    Single,
    End,
    Middle,
    Composite
}

class Lyric
{
    string Identifier { get; set; }
    int Number { get; set; }
    string Text { get; set; }
    SyllabicChoices? Syllabic { get; set; }
    List<Lyric> Components { get; set; }
    string ElisionBefore { get; set; }

    Lyric(
        string text = "",
        int number = 1,
        bool applyRaw = false,
        SyllabicChoices? syllabic = null,
        string identifier = null
    )
    {
        Identifier = identifier;
        Number = number;
        Text = text;
        Syllabic = syllabic;
        Components = null;
        ElisionBefore = " ";

        if (!string.IsNullOrEmpty(text))
        {
            SetTextAndSyllabic(text, applyRaw);
        }

        if (syllabic != null)
        {
            Syllabic = syllabic;
        }
    }

    private void SetTextAndSyllabic(string text, bool applyRaw)
    {
        throw new NotImplementedException();
    }
}

public class GeneralNote
{
    protected bool isNote;
    protected bool isRest;
    protected bool isChord = false;

    Duration.Duration Duration { get; set; }
    string Lyric { get; set; }

    protected GeneralNote(Duration.Duration duration = null, string lyric = null)
    {
        Duration = duration;
        Lyric = lyric;
    }
}

public class NotRest : GeneralNote
{
    protected NotRest()
    {
        isNote = true;
        isRest = false;
    }
}

public class Note : NotRest
{
    public Pitch.Pitch Pitch { get; set; }

    public Note(string name)
    {
        throw new NotImplementedException();
    }

    public Note(Pitch.Pitch? pitch = null, string? name = null, string? nameWithOctave = null)
    {
        throw new NotImplementedException();
    }

    public Note(int? pitch = null, string? name = null, string? nameWithOctave = null)
    {
        throw new NotImplementedException();
    }

    public Note(string? pitch = null, string? name = null, string? nameWithOctave = null)
    {
        throw new NotImplementedException();
    }
}
