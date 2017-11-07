namespace BigWorld.Entities.Components.AI
{
    public class OutputNeuron : Neuron
    {
        public double Value { get; private set; }
        
        protected override double InternGetValue(int run)
        {
            var linkValue = GetLinkValue(run);

            Value = 0;
            
            if (linkValue < 0)
                return 0;

            Value = linkValue > 1 ? 1 : linkValue;
            
            return linkValue;
        }

    }
}