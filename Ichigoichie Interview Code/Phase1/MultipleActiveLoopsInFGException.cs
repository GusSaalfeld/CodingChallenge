using System;

namespace C__implementation {
    

    /// <summary> 
    /// Custom Exception which should be thrown if a functional group has multiple active loops  
    /// </summary>
    [Serializable()]
    class MultipleActiveLoopsInFGException : Exception
    {
        public MultipleActiveLoopsInFGException() : base() {}        
         
        public MultipleActiveLoopsInFGException(string loopFG)
            : base(String.Format("ERROR: Multiple Active Loops in FG: {0}.", loopFG))
        {}

        public MultipleActiveLoopsInFGException(string loopFG, System.Exception inner)
            : base(String.Format("ERROR: Multiple Active Loops in FG: {0}.", loopFG), inner)
        {}

        protected MultipleActiveLoopsInFGException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}