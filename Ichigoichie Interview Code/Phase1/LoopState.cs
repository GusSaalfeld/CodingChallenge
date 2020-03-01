using System;

namespace C__implementation
{
    /// <summary>
    /// Defines all valid LoopStates, and how to transition between them
    /// Immutable object was my workaround for C# inability to have enums with fields and methods
    /// </summary>
    public sealed class LoopState {

        private const int NUM_LOOP_STATES = 5; 
        public static readonly LoopState Inactive =   new LoopState(0, "Inactive", false, 0);
        public static readonly LoopState Syncing =    new LoopState(1, "Syncing", true, 1);
        public static readonly LoopState Good =       new LoopState(2, "Good", true, 1);
        public static readonly LoopState Faltering =  new LoopState(3, "Faltering", true, 1);
        public static readonly LoopState Penalizing = new LoopState(4, "Penalizing", false, 0);
            
        public string Name {get; private set;}
        public int Index {get; private set;}       
        // i.e. after a certain number of bars, should transition to the next LoopState.
        public bool SelfTransitions {get; private set;} 
        public uint MeasuresBeforeTransitioning { get; private set;}
        public static LoopState[] loopStates { get; } = new LoopState[] {Inactive, Syncing, Good, Faltering, Penalizing};
 
        /// <summary> LoopState constructor </summary>
        /// <exception cref="ArgumentException"> Thrown if index > 0 or index < NUM_LOOP_STATES  </exception>
        private LoopState(int index, string name, bool selfTransitions, uint measuresBeforeTransitioning) {
            if(index >= NUM_LOOP_STATES) throw new ArgumentException(
                            String.Format("Tried to give enum an index over the NUM_LOOP_STATES"));
            if(index < 0) throw new ArgumentException(
                            String.Format("Tried to give enum an index less than 0"));
            Name = name;
            Index = index;
            SelfTransitions = selfTransitions;
            MeasuresBeforeTransitioning = measuresBeforeTransitioning;
        }

        /// <summary> Handle's whether to increment to the next state on a new measure </summary>
        /// <param name="totalMeasures"> (unsigned int) total number of measures that have passed in the match. </param>
        /// <param name="measuresSinceLastState"> (unsigned int) number of measures that have passed since a loop's last state change. </param>
        /// <exception cref="ArgumentException"> Thrown should change state and the_next_index > NUM_LOOP_STATES  </exception>
        /// <returns> loopState at the next index if it should change state, otherwise returns null </returns>
        public LoopState NewMeasureHandler(uint totalMeasures, uint measuresSinceLastState) {
            if (SelfTransitions && (MeasuresBeforeTransitioning + measuresSinceLastState) <= totalMeasures) {
                if(Index + 1 >= NUM_LOOP_STATES) throw new ArgumentException(
                            String.Format("Tried to increment enumState to next state, but you are already at end state")); 
                return loopStates[Index + 1]; 
            }
            return null;
        }

        /// <summary> override object's Equals() method </summary>
        public override bool Equals(object obj) {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            LoopState oState = obj as LoopState;
            return (Name == oState.Name && Index == oState.Index && SelfTransitions == oState.SelfTransitions);
        }

        /// <summary> override object's GetHashCode() method </summary>
        public override int GetHashCode() {
            return Index;
        }

        /// <summary> override object's ToString() method </summary>
        public override string ToString() {
            return Name;
        }
    }
}