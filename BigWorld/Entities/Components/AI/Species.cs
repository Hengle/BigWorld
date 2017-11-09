using System.Threading;

namespace BigWorld.Entities.Components.AI
{
    public class Species
    {
        private static int globalSpeciesIdentifier = 0;

        public readonly int SpeciesIdentifier = Interlocked.Increment(ref globalSpeciesIdentifier);
        
        public Species(Genome motherGenome)
        {
            MotherGenome = motherGenome;
        }

        public Genome MotherGenome { get; }

        public bool Check(Genome genome)
        {
            return MotherGenome.Distance(genome) < 3;
        }
    }
}