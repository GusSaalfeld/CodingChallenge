using System;

namespace C__implementation
{
    public class BadParticleEffect : ParticleEffect  {
        public void spawn() {
            Console.WriteLine("Bad timing! You've lost health!");
        }
    };
}