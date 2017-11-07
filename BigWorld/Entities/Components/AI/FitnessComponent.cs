namespace BigWorld.Entities.Components.AI
{
    public class FitnessComponent : Component
    {
        public double Value { get; set; }

        public void Reset()
        {
            Value = 0;
        }
    }
}