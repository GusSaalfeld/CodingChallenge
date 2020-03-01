using System;
using System.Drawing;

namespace C__implementation
{
    /// <summary>
    /// Material class stores color and handles changing animation
    /// Depending on how often color would be changed, this should be immutable for thread-saftey
    /// Because color will interact with the GUI (which exists on another thread
    /// </summary>
    class Material
    {
        private Color col;
        
        /// <summary> Material constructor </summary>
        /// <exception cref="ArgumentNullException"> Thrown if input color is null </exception>
        /// <param name="col"> Color object.</param>
        public Material (Color col) {
            if(col == null) throw new ArgumentNullException(String.Format("{0} is null", col), 
                                      "col");
            this.col = col;
        }

        /// <summary> Begins the color changing animation </summary>
        //Assumption: This task was to represent an abstract musicalloop, so pre-defined animation wasn't implemented
        public void BeginAnimation() {
            Console.WriteLine("Beginning color change animation");
        }
    }

        
}