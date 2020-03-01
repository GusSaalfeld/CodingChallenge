using System;

namespace C__implementation
{
    /// <summary>
    /// Defines a MusicalLoop object: a sound loop of a soundfile, assigned to a functional group
    /// </summary>
    class MusicalLoop
    {
        private readonly string soundFile;
        private readonly ParticleSystem particleSystem;
        private readonly LoopFunctionalGroup funcGroup;
        public LoopFunctionalGroup GetLoopFunctionalGroup(){ return funcGroup; }
        private readonly LoopFGHashMap lFGHashMap;
        private Material mat;
        private uint measureOfLastStateChange = 0;
        private MusicalLoop prevLoop;
        public bool phasingOut { get; set; }
        
        /// <summary> MusicalLoop Constructor </summary>
        /// <exception cref="ArgumentNullException"> Thrown if input material, particle system, or LoopFGHashMap are null </exception>
        /// <exception cref="MultipleActiveLoopsInFGException"> Thrown if fg has multiple active loops </exception>
        public MusicalLoop (LoopFunctionalGroup fg1, string soundFile1, Material mat1, uint currMeasure, ParticleSystem particleSystem1, LoopFGHashMap lFGHashMap1) {
            // Pre-conditions
            if(mat1 == null) throw new ArgumentNullException(String.Format("{0} is null", mat1), 
                                      "mat1");
            if(particleSystem1 == null) throw new ArgumentNullException(String.Format("{0} is null", particleSystem1), 
                                      "particleSystem1");
            if(lFGHashMap1 == null) throw new ArgumentNullException(String.Format("{0} is null", lFGHashMap1), 
                                      "lFGHashMap1");
            if (!lFGHashMap1.CheckMaxOneActiveFGLoop(fg1)) throw new MultipleActiveLoopsInFGException(fg1.ToString());
            
            // Initialize variables
            this.funcGroup = fg1;
            this.soundFile = soundFile1;
            this.mat = mat1;
            this.particleSystem = particleSystem1;
            this.lFGHashMap = lFGHashMap1; 
            SetLoopState(LoopState.Inactive, currMeasure);
        }
        private LoopState _state; 

        /// <summary> LoopState Setter  
        /// (calling it rather than _state will ensure variables that need to be changed and
        /// methods() that need to be called with the LoopState are
        /// Will discuss my struggle with information hiding vs. principle of least suprise in interview
        /// </summary>
        /// <exception cref="ArgumentNullException"> Thrown if NewLoopState is null </exception>
        /// <exception cref="MultipleActiveLoopsInFGException"> 
        /// Thrown if after method, multiple loops associated with one fg are now active </exception>
        private void SetLoopState(LoopState value, uint currMeasure) {
                //Preconditions
                if (value == null) throw new ArgumentNullException(String.Format("{0} is null", value), 
                                      "value");

                //Change State
                this._state = value;

                //Every time Loop State is changed, must also:
                mat.BeginAnimation();
                this.measureOfLastStateChange = currMeasure;
                Console.WriteLine("Set MusicalLoop's new state to: " + _state.Name);

                if (!lFGHashMap.CheckMaxOneActiveFGLoop(funcGroup)) throw new MultipleActiveLoopsInFGException(funcGroup.ToString());
        }
        public LoopState GetLoopState() { return _state; }
        /// <summary> Changes the MusicalLoop's state to Syncing </summary>
        /// <param name="prevLoop"> (MusicalLoop) Musical Loops that is currently active in the fg </param>
        /// <param name="currMeasure"> (unsigned int) The match's current measure </param>
        /// <param name="currBeat"> (unsigned int) The match's current beat </param>
        public void StartSyncing(MusicalLoop prevLoop, uint currMeasure, uint currBeat) {  
            CheckActionQuality(currBeat);
            SetLoopState(LoopState.Syncing, currMeasure);
            this.prevLoop = prevLoop;
            if(prevLoop != null) prevLoop.phasingOut = true;
        }

        /// <summary> Changes the MusicalLoop's state to Inactive </summary>
        /// <param name="currMeasure"> (unsigned int) The match's current measure </param>
        public void SetInactive(uint currMeasure) {
            SetLoopState(LoopState.Inactive, currMeasure); 
        }
        
        /// <summary> 
        /// Method called to handle NewMeasureEvent: 
        /// - Makes prevLoop Inactive, 
        /// - Updates to nextState if necessary 
        /// </summary>
        /// <param name="source"> (Object) object which triggered the event </param>
        /// <param name="e"> (NewMeasureEventArgs) arguments associated with event </param>
        public void OnNewMeasureEvent(object source, NewMeasureEventArgs e) {
            if(!phasingOut) {
                Console.WriteLine("Musical Loop recieved NewMeasureEvent!");
                LoopState lState = GetLoopState();

                if(lState.Name.Equals("Syncing")) {
                    if(prevLoop != null) {
                        prevLoop.SetInactive(e.TotalMeasures);
                        prevLoop = null;
                    }
                    particleSystem.PlayQueuedEffects();
                }

                LoopState newState = lState.NewMeasureHandler(e.TotalMeasures, measureOfLastStateChange);
                if(newState != null) SetLoopState(newState, e.TotalMeasures);
            }
        }

         /// <summary> Check's quality of changing state given beat it was done on. 
         /// Queues appropriate ParticleEffect
         /// Assumption: Bad state means you played it on the wrong beat
         /// </summary>
         /// <param name="currBeat"> (unsigned int) Match's current beat </param>
        private void CheckActionQuality(uint currBeat) {
            switch(currBeat) {
                case 0:
                    particleSystem.AddParticleEffect(GOOD_EFFECT);
                    break;
                default:
                    particleSystem.AddParticleEffect(BAD_EFFECT);
                    break;
            }
        }

        //Stored in MusicalLoop rather than ParticleSystem, because the speed > memory.
        private static readonly ParticleEffect NEUTRAL = new NeutralParticleEffect();
        private static readonly ParticleEffect GOOD_EFFECT = new GoodParticleEffect();
        private static readonly ParticleEffect BAD_EFFECT = new BadParticleEffect();
    }
}