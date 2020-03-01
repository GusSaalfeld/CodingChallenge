using System;

namespace StateMachine {
    
    /// <summary> 
    /// Custom Exception which should be thrown if GameState attempts an invalid transition
    /// </summary>

    [Serializable()]
    class InvalidStateTransitionException : Exception
    {
        public InvalidStateTransitionException() : base() {}        
         
        public InvalidStateTransitionException(string stateTransition, string currState)
            : base(String.Format("Invalid State Transition: Failed to {0} because current state was {1}", stateTransition, currState))
        {}

        //Used for if currentState makes the transition invalid
        public InvalidStateTransitionException(string stateTransition, int currSection, int sectionCount)
            : base(String.Format("Invalid State Transition: Failed to {0} because currSection was {1} and sectionCount was {2}", stateTransition, currSection, sectionCount))
        {}

        public InvalidStateTransitionException(string stateTransition, string currState, System.Exception inner)
            : base(String.Format("Invalid State Transition: Failed to {0} because current state was {1}", stateTransition, currState), inner)
        {}

        protected InvalidStateTransitionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}