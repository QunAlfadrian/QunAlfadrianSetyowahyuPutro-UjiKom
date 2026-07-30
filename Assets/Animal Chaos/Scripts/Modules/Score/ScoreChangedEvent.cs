namespace GDPP2.UjiKom.Scoring {
    public struct ScoreChangedEvent {
        public int Score { get; private set; }

        public ScoreChangedEvent(int score) {
            Score = score;
        }
    }
}