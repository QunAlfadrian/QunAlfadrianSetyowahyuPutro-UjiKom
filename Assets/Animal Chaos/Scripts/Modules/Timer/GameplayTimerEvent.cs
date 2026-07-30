public struct GameplayTimerEvent {
    public float TimeElapsed { get; private set; }
    public float TimeRemaining { get; private set; }

    public GameplayTimerEvent(float timeElapsed, float timeRemaining) {
        TimeElapsed = timeElapsed;
        TimeRemaining = timeRemaining;
    }
}