using System.Text;
using System.Text.Json;
using System.Xml;
using Csv;

public class CSVCode
{
    protected CSV input;
    protected CSV output;
    public double[,] inputData;
    public double[,] outputData;

    public CSVCode(string path)
    {
        input = new CSV(File.OpenRead("/music.csv"));
        output = new CSV(File.OpenRead("/music.csv"));
        inputData = new double[input.RowCount, input.ColCount];
        outputData = new double[output.RowCount, output.ColCount];
    }
    
}

public record SongData (string Song, string Artist, string Genre, string Emotion);
//TODO make a ToString for SongData

public record InputData (string Song, string Artist, string Genre);
