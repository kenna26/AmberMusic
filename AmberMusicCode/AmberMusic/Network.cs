namespace AmberMusic;
using System;


public class Network
{
    private double[,] weightsInputHidden;
    private double[,] weightsHiddenOutput;
    private double[] biasHidden;
    private double[] biasOutput;
    private double learningRate;

    public Network(int inputNodes, int hiddenNodes, int outputNodes, double learningRate)
    {
        weightsInputHidden = new double[inputNodes, hiddenNodes];
        weightsHiddenOutput = new double[hiddenNodes, outputNodes];
        biasHidden = new double[hiddenNodes];
        biasOutput = new double[outputNodes];
        this.learningRate = learningRate;

        // Initialize weights and biases with random values
        Random random = new Random();

        for (int i = 0; i < inputNodes; i++)
        {
            for (int j = 0; j < hiddenNodes; j++)
            {
                weightsInputHidden[i, j] = random.NextDouble() - 0.5;
            }
        }

        for (int i = 0; i < hiddenNodes; i++)
        {
            for (int j = 0; j < outputNodes; j++)
            {
                weightsHiddenOutput[i, j] = random.NextDouble() - 0.5;
            }
            biasHidden[i] = random.NextDouble() - 0.5;
        }

        for (int i = 0; i < outputNodes; i++)
        {
            biasOutput[i] = random.NextDouble() - 0.5;
        }
    }

    private double Sigmoid(double x)
    {
        return 1.0 / (1.0 + Math.Exp(-x));
    }

    public double[] FeedForward(double[] inputs)
    {
        int inputNodes = weightsInputHidden.GetLength(0);
        int hiddenNodes = weightsInputHidden.GetLength(1);
        int outputNodes = weightsHiddenOutput.GetLength(1);

        double[] hiddenLayerInputs = new double[hiddenNodes];
        double[] hiddenLayerOutputs = new double[hiddenNodes];
        double[] finalOutputs = new double[outputNodes];

        // Calculate inputs to hidden layer
        for (int i = 0; i < hiddenNodes; i++)
        {
            double sum = 0;
            for (int j = 0; j < inputNodes; j++)
            {
                sum += inputs[j] * weightsInputHidden[j, i];
            }
            sum += biasHidden[i];
            hiddenLayerInputs[i] = sum;
            hiddenLayerOutputs[i] = Sigmoid(sum);
        }

        // Calculate inputs to output layer
        for (int i = 0; i < outputNodes; i++)
        {
            double sum = 0;
            for (int j = 0; j < hiddenNodes; j++)
            {
                sum += hiddenLayerOutputs[j] * weightsHiddenOutput[j, i];
            }
            sum += biasOutput[i];
            finalOutputs[i] = Sigmoid(sum);
        }

        return finalOutputs;
    }

    // Example method for training the network (using Backpropagation algorithm)
    public void Train(double[] inputs, double[] targets)
    {
        int inputNodes = weightsInputHidden.GetLength(0);
        int hiddenNodes = weightsInputHidden.GetLength(1);
        int outputNodes = weightsHiddenOutput.GetLength(1);

        // Feedforward
        double[] hiddenLayerOutputs = new double[hiddenNodes];
        double[] finalOutputs = FeedForward(inputs);

        // Calculate output layer errors
        double[] outputErrors = new double[outputNodes];
        for (int i = 0; i < outputNodes; i++)
        {
            outputErrors[i] = targets[i] - finalOutputs[i];
        }

        // Calculate hidden layer errors
        double[] hiddenErrors = new double[hiddenNodes];
        for (int i = 0; i < hiddenNodes; i++)
        {
            double error = 0;
            for (int j = 0; j < outputNodes; j++)
            {
                error += outputErrors[j] * weightsHiddenOutput[i, j];
            }
            hiddenErrors[i] = error;
        }

        // Update weights and biases for hidden to output layer
        for (int i = 0; i < hiddenNodes; i++)
        {
            for (int j = 0; j < outputNodes; j++)
            {
                double deltaOutput = outputErrors[j] * finalOutputs[j] * (1 - finalOutputs[j]);
                double deltaWeight = deltaOutput * hiddenLayerOutputs[i] * learningRate;
                weightsHiddenOutput[i, j] += deltaWeight;
            }
        }

        // Update weights and biases for input to hidden layer
        for (int i = 0; i < inputNodes; i++)
        {
            for (int j = 0; j < hiddenNodes; j++)
            {
                double deltaHidden = hiddenErrors[j] * hiddenLayerOutputs[j] * (1 - hiddenLayerOutputs[j]);
                double deltaWeight = deltaHidden * inputs[i] * learningRate;
                weightsInputHidden[i, j] += deltaWeight;
            }
        }
    }
}