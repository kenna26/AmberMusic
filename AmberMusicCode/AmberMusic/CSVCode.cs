using Csv;

public class CSVCode
{
    protected CSV doc;
    protected List<SongData> allSongData;

    public CSVCode(string path)
    {
        doc = new CSV(File.OpenRead("/music.csv"));
        foreach (var row in doc)
        {
            SongData songData = new(row["Song"], row["Artist"], row["Genre"], row["Emotion"]);
            allSongData.Add(songData);
        }
    }

    public override string ToString()
    {
        String printing = "";
        foreach (var item in allSongData)
        {
            printing += $"Song: {item.Song}, Artist: {item.Artist}, Genre: {item.Genre}, Emotion: {item.Emotion} \n";
        }

        return printing;
    }

    public List<InputData> getInputData()
    {
        var inputDatas = new List<InputData>();
        foreach (var item in allSongData)
        {
            inputDatas.Add(new InputData(item.Song, item.Artist, item.Genre));
        }
        return inputDatas;
    }

    public List<string> getOutputData()
    {
        var outputDatas = new List<string>();
        foreach (var item in allSongData)
        {
            outputDatas.Add(item.Emotion);
        }

        return outputDatas;

    }
}

public record SongData (string Song, string Artist, string Genre, string Emotion);
//TODO make a ToString for SongData

public record InputData (string Song, string Artist, string Genre);
