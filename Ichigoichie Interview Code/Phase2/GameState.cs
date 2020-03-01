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
            //Check if transition is valid
            if(currMode != Mode.NotStarted)
                throw new InvalidStateTransitionException("Finish Game", Enum.GetName(typeof(Mode), currMode));

            //Transition
            currMode = Mode.MainGameplay;
            Console.WriteLine("Starting Gameplay");
        }

        /// <summary> Changes Mode from MainGameplay to PreparingNewSection </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != MainGameplay </exception>
        public void PrepareNewSection() {
            //Check if transition is valid
            if(currMode != Mode.MainGameplay)
                throw new InvalidStateTransitionException("Prepare new section", Enum.GetName(typeof(Mode), currMode));

            //Transition
            currMode = Mode.PreparingNextSection;
            Console.WriteLine("Preparing new section");
        }

        /// <summary> Changes Mode from MainGameplay to ComboMode </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != MainGameplay </exception>
        public void EnterComboMode() {
            //Check if transition is valid
            if(currMode != Mode.MainGameplay)
                throw new InvalidStateTransitionException("Entering Combo Mode", Enum.GetName(typeof(Mode), currMode));

            //Transition
            currMode = Mode.ComboMode;
            Console.WriteLine("Entering Combo-Mode");
        }

        /// <summary> Changes Mode from ComboMode to MainGameplay </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != ComboMode </exception>
        public void ExitComboMode() {
            //Check if transition is valid
            if(currMode != Mode.ComboMode)
                throw new InvalidStateTransitionException("Exiting Combo Mode", Enum.GetName(typeof(Mode), currMode));

            //Transition
            currMode = Mode.MainGameplay;
            Console.WriteLine("Exiting Combo Mode");
        }

        /// <summary> Changes Mode from PreparingNextSection to Minigame </summary>
        /// <exception cref="InvalidStateTransitionException"> Thrown if currMode != PreparingNextSection </exception>
        public void StartMinigame() {
            //Check if transition is valid
            if(currMode != Mode.PreparingNextSection)
                throw new InvalidStateTransitionException("Finish Game", Enum.GetName(typeof(Mode), currMode));

            //Transition
            currMode = Mode.Minigame;
            Console.WriteLine("Starting Minigame");
        }

        /// <summary> Changes Mode from MiniGame to MainGameplay </summary>
        /// <exception cref="InvalidStateTransitionException"> 
        /// Thrown if currMode != NotStarted OR if GameState.IsAtLastSection() 
        /// </exception>
        public void StartNextSection() {
            //Check if transition is valid
            if(currMode != Mode.Minigame)
                throw new InvalidStateTransitionException("Starting Next Section", Enum.GetName(typeof(Mode), currMode));
            if(IsAtLastSection())
                throw new InvalidStateTransitionException("Starting Next Section", currSection, sectionCount);

            //Transition
            currSection++;
            currMode = Mode.MainGameplay;
            Console.WriteLine("Starting next section");
        }

        /// <summary> Changes Mode from MiniGame to GameFinished </summary>
        /// <exception cref="InvalidStateTransitionException"> 
        /// Thrown if currMode != MiniGame OR if !GameState.IsAtLastSection() 
        /// </exception>
        public void FinishGame() {
            //Check if transition is valid
            if(currMode != Mode.Minigame)
                throw new InvalidStateTransitionException("Finish Game", Enum.GetName(typeof(Mode), currMode));
            if(!IsAtLastSection())
                throw new InvalidStateTransitionException("Finish Game", currSection, sectionCount);
            
            //Transition
            currMode = Mode.GameFinished;
            currSection = 0;
            Console.WriteLine("Finishing game");
        }
    }
}