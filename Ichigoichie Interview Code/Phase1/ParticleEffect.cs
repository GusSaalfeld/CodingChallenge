namespace C__implementation
{
    /// <summary>
    /// Particle Effect interface, all particle effects should implement this
    /// Assumption: Particle effects implementation doesn't overlap, otherwise make
    /// this an abstract class 
    /// </summary>
    public interface ParticleEffect {
        public abstract void spawn();
    };
}