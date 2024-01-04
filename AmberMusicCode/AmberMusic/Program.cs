using Csv;

NeuralNetwork neuralNetwork = new NeuralNetwork();
    
CSVCode musicData = new("/music.csv"); 
List<InputData> inputData = musicData.getInputData();
List<string> outputData = musicData.getOutputData();

Console.WriteLine("Start Training.....");

// Train the neural network using a training set.
// Do it 10,000 times
neuralNetwork.Train(inputData, outputData, 10000);
Console.WriteLine("End training ...\n\n");

        // Predict
        Console.WriteLine("Considering new situation [1, 0, 0] -> ?\n");
        var result = neuralNetwork.Think(new double[,] { { 1, 0, 0 } });

        Console.WriteLine(result);
        Console.ReadKey();
    }
}