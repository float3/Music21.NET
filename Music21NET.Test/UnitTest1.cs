using Music21NET.Chord;

namespace Music21NET.Test;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void Test1()
    {
        Chord.Chord x = new Chord.Chord("C E G");
        Console.WriteLine(x.PitchedCommonName);
    }
}
