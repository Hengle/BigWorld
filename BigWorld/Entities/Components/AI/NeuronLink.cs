namespace BigWorld.Entities.Components.AI
{
    public class NeuronLink
    {
        private readonly Neuron inputNeuron;
        private readonly double weight;

        public NeuronLink(Neuron inputNeuron, double weight)
        {
            this.inputNeuron = inputNeuron;
            this.weight = weight;
        }

        private double oldValue;
        private int oldRun;

        private int currentRun;
        private double? currentValue;
        
        public double GetValue(int run)
        {
            if (currentRun == run)
            {
                if (currentValue.HasValue)
                    return currentValue.Value;

                return oldValue;
            }

            oldRun = currentRun;
            currentRun = run;
            oldValue = currentValue ?? 0;
            
            currentValue = null;
            currentValue = inputNeuron.GetValue(run) * weight;

            return currentValue.Value;
        }
        

        
    }
}