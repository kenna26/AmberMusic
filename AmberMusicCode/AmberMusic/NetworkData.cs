using System.Transactions;

namespace AmberMusic;

public class NetworkData
{
    
    int inputNodes = 2;
    int hiddenNodes = 3;
    protected int outputNodes = 1;
    protected double learningRate = 0.1;

    private Network nueralNetwork;
    
    public NetworkData()
    {
        nueralNetwork = new Network(inputNodes, hiddenNodes, outputNodes, learningRate);

    }

    public void Train()
    {
        // Training example
        double[] input = { 0, 1 };
        double[] target = { 1 };
        nueralNetwork.Train(input, target); 
    }

    public void TestInput()
    {
        // Testing example
        double[] testInput = { 1, 0 };
        double[] output = nueralNetwork.FeedForward(testInput);

        Console.WriteLine("Output: " + output[0]);
    }
}