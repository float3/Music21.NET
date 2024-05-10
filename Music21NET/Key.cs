namespace Music21NET.Key;

public class KeyException(string message) : Exception(message);

public class Key : KeySignature {
    private int _sharps = 0;
    private string? _mode = null;
    public Pitch.Pitch Tonic = null;

    public Key(string tonic, string mode) {
        throw new NotImplementedException();
    }
    public Key(Pitch.Pitch tonic, string mode) {
        throw new NotImplementedException();
    }
    public Key(Note.Note tonic, string mode) {
        throw new NotImplementedException();
    }
}
public class KeySignature {
    public Key AsKey(string major) {
        throw new NotImplementedException();
    }
}