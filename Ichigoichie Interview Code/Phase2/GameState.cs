using System;

namespace StateMachine
{
    /// <summary>
    /// GameState object: holds current state + section, and allows
    /// you to transition between them 
    /// 
    /// Assumption 1: Complete it to specifications, rather than my own design.
    /// In work environment, I would put forward my own suggestion to debate its merits, but given limited information
    /// its best to assume the designer had a better reason than you (in this case likely to maximise saftey)
    /// I would work my GameState around 3 object types and 1 method
    /// Data Types:
    /// - States
    /// - Legal State Transitions
    /// - Events
    /// Method: AppleNextState() which would:
    /// Executes the event passed into it, which:
    ///     Checks whether desired transition is legal
    ///         If its not: throw exception
    ///         If it is: Transition (+ run additional code)
    /// Assumption 2: Don't make immutable, given the specified method's return types
    /// </summary>
    class GameState {
        private readonly int sectionCount;
        private int currSection;
        private Mode currMode;
        private int maxSections => sectionCount - 1; 

        /// <summary> GameState Constructor </summary>
        /// <exception cref="ArgumentException"> Thrown if sectionCount < 0 </exception>
        public GameState(int sectionCount) {
            if(sectionCount < 1) throw new ArgumentException(String.Format("{0} is less than 1", sectionCount), 
                                      "sectionCount");

            this.sectionCount = sectionCount;
            currSection = 0;
            currMode = Mode.NotStarted;
            Console.WriteLine("Successfully initialized new Game State");
        }

        // Getters
        public Mode GetGameMode() { return currMode; }
        public int GetCurrentSection() { return currSection; }
        public bool IsAtLastSection() => (currSection == maxSections);
        public bool IsInGame() { return currMode == Mode.MainGameplay ||
                                        currMode == Mode.ComboMode ||
                                        currMode == Mode.PreparingNextSection ||
                                        currMode == Mode.Minigame; }

        /// <summary> Changes Mode from NotStarted to Main Gameplay </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != NotStarted </exception>
        public void StartGameplay() {
            //Transition
            ChangeState(Mode.NotStarted, Mode.MainGameplay, "Start Gameplay");
            Console.WriteLine("Starting Gameplay");
        }

        /// <summary> Changes Mode from MainGameplay to PreparingNewSection </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != MainGameplay </exception>
        public void PrepareNewSection() {
            //Transition
            ChangeState(Mode.MainGameplay, Mode.PreparingNextSection, "Preparing new section");
            Console.WriteLine("Preparing new section");
        }

        /// <summary> Changes Mode from MainGameplay to ComboMode </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != MainGameplay </exception>
        public void EnterComboMode() {
            //Transition
            ChangeState(Mode.MainGameplay, Mode.ComboMode, "Entering Combo-Mode");
            Console.WriteLine("Entering Combo-Mode");
        }

        /// <summary> Changes Mode from ComboMode to MainGameplay </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != ComboMode </exception>
        public void ExitComboMode() {
            //Transition
            ChangeState(Mode.ComboMode, Mode.MainGameplay, "Exit Combo-Mode");
            Console.WriteLine("Exiting Combo Mode");
        }

        /// <summary> Changes Mode from PreparingNextSection to Minigame </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != PreparingNextSection </exception>
        public void StartMinigame() {
            //Transition
            ChangeState(Mode.PreparingNextSection, Mode.Minigame, "Start Minigame");
            Console.WriteLine("Starting Minigame");
        }

        /// <summary> Changes Mode from MiniGame to MainGameplay </summary>
        /// <exception cref="InvalidStateTransitionException"> 
        /// Thrown if currMode != NotStarted OR if GameState.IsAtLastSection() 
        /// </exception>
        public void StartNextSection() {
            if(IsAtLastSection())
                throw new InvalidStateTransitionException("Starting Next Section", currSection, sectionCount);

            //Transition
            currSection++;
            ChangeState(Mode.Minigame , Mode.MainGameplay , "Starting Next Section");
            Console.WriteLine("Starting next section");
        }

        /// <summary> Changes Mode from MiniGame to GameFinished </summary>
        /// <exception cref="InvalidStateTransitionException"> 
        /// Thrown if !GameState.IsAtLastSection() OR currMode != Minigame
        /// </exception>
        public void FinishGame() {
            if(!IsAtLastSection())
                throw new InvalidStateTransitionException("Finish Game", currSection, sectionCount);
            
            //Transition
            ChangeState(Mode.Minigame, Mode.GameFinished, "Finish Game");
            currSection = 0;
            Console.WriteLine("Finishing game");
        }

        /// <summary> Changes currMode from to newState if currMode == validCurrState </summary>
        /// <param name="validCurrMode"> (Mode) The validCurrentMode to be able to successfully transition. </param>
        /// <param name="newMode"> (Mode) The new mode to transition to. </param>
        /// <param name="transitionName"> (string) the name of the transition. </param>
        /// <exception cref="InvalidStateTransitionException"> thrown if currMode != validCurrState </exception>
        private void ChangeState(Mode validCurrMode, Mode newMode, string transitionName) {
            if(currMode != validCurrMode)
                throw new InvalidStateTransitionException(transitionName, Enum.GetName(typeof(Mode), currMode));
            currMode = newMode;
        }
    }
}