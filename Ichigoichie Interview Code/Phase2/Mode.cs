namespace StateMachine
{

    /// <summary> 
    /// Enum defining all possible modes (states) a game can be in  
    /// </summary>
    public enum Mode {
        NotStarted,
        MainGameplay,
        ComboMode,
        PreparingNextSection,
        Minigame,
        GameFinished
    }
}