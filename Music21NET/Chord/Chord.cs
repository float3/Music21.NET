using Music21NET.Key;
using Music21NET.Note;

namespace Music21NET.Chord;

class ChordException(string message) : Exception(message);

public class ChordBase : NotRest
{
    protected ChordBase()
    {
        isNote = false;
        isRest = false;
    }

    protected ChordBase(object notes)
        : base()
    {
        throw new NotImplementedException();
    }
}

public class Chord : ChordBase
{
    private readonly List<Note.Note> _notes;

    public Chord()
        : base()
    {
        isChord = true;
    }

    public Chord(object notes)
        : base(notes)
    {
        isChord = true;
        _notes = Initialize(notes);
        SimplifyEnharmonics();
    }

    private List<Note.Note> Initialize(object notes)
    {
        return notes switch
        {
            IEnumerable<string> stringNotes => stringNotes.Select(n => new Note.Note(n)).ToList(),
            IEnumerable<Pitch.Pitch> pitchNotes
                => pitchNotes.Select(p => new Note.Note(p)).ToList(),
            IEnumerable<Chord> chords => chords.SelectMany(c => c._notes).ToList(),
            IEnumerable<Note.Note> noteNotes => noteNotes.ToList(),
            IEnumerable<int> intNotes => intNotes.Select(n => new Note.Note(n)).ToList(),
            _ => throw new ChordException($"Unsupported note type: {notes.GetType()}")
        };
    }

    private List<Pitch.Pitch> Pitches
    {
        get { return _notes.Select(n => n.Pitch).ToList(); }
        set
        {
            _notes.Clear();
            foreach (Pitch.Pitch p in value)
            {
                _notes.Add(new Note.Note(pitch: p));
            }
        }
    }

    public string PitchedCommonName
    {
        get
        {
            string nameStr = CommonName;
            if (nameStr == "empty chord")
            {
                return nameStr;
            }

            if (nameStr == "note" || nameStr == "unison")
            {
                return Pitches[0].name;
            }

            if (
                PitchClassCardinality <= 2
                || nameStr.Contains("enharmonic")
                || nameStr.Contains("forte class")
                || nameStr.Contains("semitone")
            )
            {
                // Root detection gives weird results for pitchedCommonName
                Pitch.Pitch bass = Bass();
                string bassName = bass.name.Replace("-", "b");
                return $"{nameStr} above {bassName}";
            }
            else
            {
                try
                {
                    Pitch.Pitch root = Root();
                    string rootName = root.name.Replace("-", "b");
                    return $"{rootName}-{nameStr}";
                }
                catch (ChordException) // If a root cannot be found
                {
                    Pitch.Pitch root = Pitches[0];
                    string rootName = root.name.Replace("-", "b");
                    return $"{rootName}-{nameStr}";
                }
            }
        }
    }

    private Pitch.Pitch Root()
    {
        throw new NotImplementedException();
    }

    private Pitch.Pitch Bass()
    {
        throw new NotImplementedException();
    }

    public int PitchClassCardinality { get; }

    public string CommonName
    {
        get { throw new NotImplementedException(); }
    }

    private Chord SimplifyEnharmonics(bool inPlace = false, KeySignature? keyContext = null)
    {
        Chord returnObj = inPlace ? this : new Chord(_notes);

        List<Pitch.Pitch> pitches = Pitch.Pitch.SimplifyMultipleEnharmonics(
            Pitches,
            keyContext: keyContext
        );
        for (int i = 0; i < pitches.Count; i++)
        {
            returnObj._notes[i].Pitch = pitches[i];
        }

        return returnObj;
    }
}
