using System;

namespace C__implementation
{
    /// <summary> 
    /// Defines the arguments NewMeasureEvent takes in 
    /// Events follow the observer design pattern, which lowers coupling
    /// </summary>
    public class NewMeasureEventArgs : EventArgs {
        public uint TotalMeasures { get; set;}    
    }

    /// <summary>
    /// Class defines NewMeasureEvent, which should be called every time there is a new measure
    /// </summary>
    public class NewMeasureChecker
    {
        public event EventHandler<NewMeasureEventArgs> NewMeasure;
        public virtual void OnNewMeasure(NewMeasureEventArgs e) {
            EventHandler<NewMeasureEventArgs> handler = NewMeasure;
            handler?.Invoke(this, e);
        }
    }
}