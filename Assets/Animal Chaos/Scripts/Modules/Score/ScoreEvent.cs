namespace GDPP2.UjiKom.Scoring {
    public struct ScoreEvent {
        public int Score { get; private set; }

        public ScoreEvent(int score) {
            Score = score;
        }
    }
}