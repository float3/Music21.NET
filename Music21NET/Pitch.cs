using Music21NET.Key;

namespace Music21NET.Pitch;

public class PitchException(string message) : Exception(message);

public class Pitch {

    public bool SpellingIsInferred = false;
    public string name;
    public Pitch() { }

    public delegate double DissonanceScoreDelegate(List<Pitch> pitches, bool smallPythagoreanRatio, bool accidentalPenalty, bool triadAward);
    private static double DissonanceScore(List<Pitch> pitches, bool smallPythagoreanRatio, bool accidentalPenalty, bool triadAward) {
        throw new NotImplementedException();
    }

    static public List<Pitch> SimplifyMultipleEnharmonics(List<Pitch> pitches, DissonanceScoreDelegate? criterion = null, KeySignature? keyContext = null) {
        criterion ??= DissonanceScore;
        List<Pitch> oldPitches = pitches.Select(p => p).ToList();

        if (keyContext != null) {
            oldPitches.Insert(0, keyContext.AsKey("major").Tonic);
        }

        List<Pitch> simplifiedPitches;
        if (oldPitches.Count < 5) {
            simplifiedPitches = BruteForceEnharmonicsSearch(oldPitches, criterion);
        } else {
            simplifiedPitches = GreedyEnharmonicsSearch(oldPitches, criterion);
        }

        for (int i = 0; i < oldPitches.Count; i++) {
            simplifiedPitches[i].SpellingIsInferred = oldPitches[i].SpellingIsInferred;
        }

        if (keyContext != null) {
            simplifiedPitches.RemoveAt(0);
        }

        return simplifiedPitches;
    }

    private static List<Pitch> GreedyEnharmonicsSearch(List<Pitch> oldPitches, DissonanceScoreDelegate criterion) {
        throw new NotImplementedException();
    }

    private static List<Pitch> BruteForceEnharmonicsSearch(List<Pitch> oldPitches, DissonanceScoreDelegate criterion) {
        throw new NotImplementedException();
    }
}
