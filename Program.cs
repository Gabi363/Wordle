namespace WORDLE;

public class Program
{
    [STAThread]
    static void Main()
    {
        string word = random_word("words_list.txt");
        // if(word == null) return;
        Console.Write("xx");
        Console.WriteLine(word + "xx");
        Game game = new Game(word);
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1(game));
    }

    static string random_word(string path){
        var lines = File.ReadAllLines(path);
        Random r = new Random();
        int randomLineNumber = r.Next(0, lines.Length - 1);
        string word = lines[randomLineNumber];
        return word;
    }
}