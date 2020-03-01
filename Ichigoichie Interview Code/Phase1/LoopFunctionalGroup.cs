using System.Collections.Generic;
using System;

namespace C__implementation
{
    /// </summary> FunctionalGroups that a musical loop can be in <summary>
    public enum LoopFunctionalGroup {
        Percussion,
        Saxophone,
        Synthesizer,
        Bassline,
        Vocals,
        Keyboard,
        Flute,
        Guitar
    };

    /// <summary> HashMap that stores each musical loop associated with a functional group </summary>
    class LoopFGHashMap {
        
        private Dictionary<int, List<MusicalLoop>> loopsByFGGroup = new Dictionary<int, List<MusicalLoop>>();
        /// <summary> LoopFGHashMap constructor </summary>
        public LoopFGHashMap() {
            int numLoopStates = Enum.GetNames(typeof(LoopFunctionalGroup)).Length;
            for(int i = 0; i < numLoopStates; i++) {
                List<MusicalLoop> newMList = new List<MusicalLoop>();
                loopsByFGGroup.Add(i, newMList);
            }
        }

        /// <summary> add a MusicalLoop to the LoopFGHashMap </summary>
        /// <param name="loop"> (MusicalLoop) loop to be added to the fg HashMap. </param>
        public void Add(MusicalLoop loop) {
            if (loop == null) throw new ArgumentNullException(String.Format("Tried to add {0}, but it is null", loop), 
                                      "loop");

            int key = loop.GetLoopState().GetHashCode();
            List<MusicalLoop> value;
            if(loopsByFGGroup.TryGetValue(key, out value))
                value.Add(loop);
        }
        
        /// <summary> remove a MusicalLoop from the LoopFGHashMap </summary>
        /// <param name="loop"> (MusicalLoop) loop to be removed from the fg HashMap. </param>
        public void Remove(MusicalLoop loop) {
            if (loop == null) throw new ArgumentNullException(String.Format("Tried to remove {0}, but it is null", loop), 
                                      "loop");

            int key = loop.GetLoopState().GetHashCode();
            List<MusicalLoop> value;
            if(loopsByFGGroup.TryGetValue(key, out value))
                value.Remove(loop);
        }

        /// <summary> Checks that a fg has at most one active musical loop </summary>
        /// <returns> true if one or fewer active loops, false otherwise</returns>
        public bool CheckMaxOneActiveFGLoop(LoopFunctionalGroup fg) {

            int activeLoops = 0;
            int key = fg.GetHashCode();
            List<MusicalLoop> fgList;
            
            // Given fg as key, calculate number of active loops in its list of musical loops
            if(loopsByFGGroup.TryGetValue(key, out fgList)) {
                foreach (MusicalLoop loop in fgList) {
                    if(loop.GetLoopState() != LoopState.Inactive && loop.GetLoopState() != LoopState.Syncing) {
                        activeLoops++;
                    }
                }
                if(activeLoops > 1) return false;
            } else {
                Console.WriteLine("ERROR: FG Group not found in LoopFGHashMap!");
                return false;
            }
            return true;
        }
    }
}