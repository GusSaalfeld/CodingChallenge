using System;
using NUnit.Framework;

namespace StateMachine
{
    /// <summary> 
    /// While this testing suite is not exhaustive (~100% test coverage),
    /// It should serve the purposes of demonstrating this project's general functionality 
    /// </summary> 

    [TestFixture]
    class Tests
    {
       // Single Section
        [Test]
        public void RunCorrectly1() {
            GameState gState = null;
            int sectionCount = 1;
            
            try {
                gState = new GameState(sectionCount);
                
                //Run through a functional gameplay loop
                gState.StartGameplay();
                gState.EnterComboMode();
                gState.ExitComboMode();
                gState.PrepareNewSection();
                gState.StartMinigame();
                gState.FinishGame();
                
            //Fail if any exception is caught
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Assert.Fail();
            }
        }

        //Multiple Sections
        [Test]
        public void RunCorrectly2() {
            GameState gState = null;
            int sectionCount = 2;
            
            try {
                gState = new GameState(sectionCount);
                
                //Run through a functional gameplay loop
                gState.StartGameplay();
                gState.EnterComboMode();
                gState.ExitComboMode();
                gState.PrepareNewSection();
                gState.StartMinigame();
                gState.StartNextSection();
                gState.PrepareNewSection();
                gState.StartMinigame();
                gState.FinishGame();
                
            //Fail if any exception is caught
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Assert.Fail();
            }
        }

        //One successful state change, then a failed state change
        [Test]
        public void InvalidStateChange1() {
            GameState gState = null;
            int sectionCount = 2;
            
            try {
                gState = new GameState(sectionCount);
                
                //One successful state change, then a failed state change
                gState.StartGameplay();
                gState.StartMinigame();

                //No exceptions: fail
                Assert.Fail();
                
            //If the exception was caught, test successful
            } catch(Exception e) {
                
            }
        }

        //Fail because try to prepare new section when game should exit
        [Test]
        public void InvalidStateChange2() {
            GameState gState = null;
            int sectionCount = 1;
            
            try {
                gState = new GameState(sectionCount);
                
                //fail because try to prepare new section when game should exit
                gState.StartGameplay();
                gState.EnterComboMode();
                gState.ExitComboMode();
                gState.StartNextSection();

                //No exceptions: fail
                Assert.Fail();
                
            //If the exception was caught, test successful
            } catch(Exception e) {
               
            }
        }

        // Fail because try to end game when theres another section
        [Test]
        public void InvalidStateChange3() {
            GameState gState = null;
            int sectionCount = 2;
            
            try {
                gState = new GameState(sectionCount);
                
                //fail because try to end game when theres another section
                gState.StartGameplay();
                gState.EnterComboMode();
                gState.ExitComboMode();
                gState.FinishGame();

                //No exceptions: fail
                Assert.Fail();
                
            //If the exception was caught, test successful
            } catch(Exception e) {
               
            }
        }
    }
}