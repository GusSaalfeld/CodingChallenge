using System.Collections.Generic;

namespace C__implementation
{
    /// <summary>
    /// This class employs the command pattern in order to queue and play a list of particle effects
    /// This implementation ensures that if the same particle effect is queued twice, it only plays once
    /// The command pattern consequences: 
    /// - seperates operation from client context (lower coupling)
    /// - Can specify, queue, and execute commands at different times
    /// - Downside: lots of overhead if commands are simple 
    /// </summary>
    class ParticleSystem
    { 
        private Dictionary<string, ParticleEffect> queuedEffects = new Dictionary<string, ParticleEffect>();

        /// <summary> plays all queued effects and then clears the list </summary>
        public void PlayQueuedEffects() {
            foreach(KeyValuePair<string, ParticleEffect> effect in queuedEffects) {
                effect.Value.spawn();
            }
            EmptyQueuedEffects();
        }

        /// <summary> Empty's the queued effect list </summary>
        public void EmptyQueuedEffects() {
            queuedEffects.Clear();
        }

        /// <summary> Add's a particle effect to the queue </summary>
        /// <param name="addParticle"> Particle effect to be added to the list </param>
        public void AddParticleEffect(ParticleEffect addParticle) {
            string addKey = addParticle.ToString();
            
            //Since dictionary, ensures no overlapping particle effects!
            queuedEffects.Add(addKey, addParticle);
        }
    }
}