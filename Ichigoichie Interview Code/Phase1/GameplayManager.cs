using System;
using System.Drawing;

namespace C__implementation
{

    /// <summary>
    /// This class setups and plays through the game 
    /// It first instantiates the musical loops, loopsPerFG HashMap, and particle system. 
    /// Then it plays through each beat and measure until its reached the end
    /// This class is responsible for handling any exceptions thrown by anything it calls.
    /// Assumption: Since focused on design, only needed to represent MusicalLoops, not create UI for them.
    /// </summary>
    class GameplayManager
    {
        private uint beat = 0;
        public uint getBeat() { return beat; }
        private uint measure = 0;
        public uint getMeasure() { return measure; }
        public uint totalCurrBeats => measure * BEATS_PER_MEASURE + beat;
        private const uint BEATS_PER_MEASURE = 4;
        private NewMeasureChecker newMeasureHandler = new NewMeasureChecker();
        private MusicalLoop[] loops = new MusicalLoop[2];
        private ParticleSystem pSystem = new ParticleSystem();
        private LoopFGHashMap lFGHashMap = new LoopFGHashMap();

        /// <summary>
        /// This method setups and plays through the game 
        /// First it setups up match 
        /// Then it plays through each beat and measure until its reached the end
        /// This method is responsible for handling any exceptions thrown by anything it calls
        /// </summary>
        // <param name="totalBeatsInMatch"> unsigned int that determines how many beats the match will be.</param>
        public void playMatch(uint totalBeatsInMatch) {
            Console.WriteLine("Starting match.");

            try {
                InitializeMusicalLoops();
                loops[1].StartSyncing(null, measure, beat);

                //Iterate through each measure until the match is done
                for(measure = 0; (measure * BEATS_PER_MEASURE) + beat < totalBeatsInMatch;) {
                    // Start of Measure: Call NewMeasureEvent event
                    if(beat == 0) {
                        NewMeasureEventArgs args = new NewMeasureEventArgs();
                        args.TotalMeasures = measure;
                        newMeasureHandler.OnNewMeasure(args);
                    }

                    //Loop body
                    Console.WriteLine("Beat: "+ beat);
                    beat++;
                    
                    //Demonstrates that only one active loop per fg will exist unless programmer error.
                    if(measure == 1 && beat == 2) loops[0].StartSyncing(loops[1], measure, beat);

                    //New measure, reset the beat
                    if(beat == 4) {
                        beat = 0;
                        measure++; 
                    }
                }
            // Handle exceptions
            } catch (ArgumentException e) {
                Console.WriteLine(e.Message);
                System.Environment.Exit(-1);
            } catch (MultipleActiveLoopsInFGException e) {
                Console.WriteLine(e.Message);
                System.Environment.Exit(-1);
            }
        }  

        /// <summary>
        /// Initializes a match's loop, and adds it to the loopPerFGHashMap
        /// </summary>
        private void InitializeMusicalLoops() {
            Material mat = new Material(Color.FromName("Black"));
            string soundfile = "bass.png";
            LoopFunctionalGroup fg = LoopFunctionalGroup.Percussion;

            //Fill the loops array with musical loops
            for(int i = 0; i < loops.Length; i++) {
                MusicalLoop loop = new MusicalLoop(fg, soundfile, mat, measure, pSystem, lFGHashMap);
                loops[i] = loop;
                lFGHashMap.Add(loop);
                newMeasureHandler.NewMeasure += loop.OnNewMeasureEvent;
            }
        }
    }
}