namespace WORDLE;

public partial class Form1 : Form
{
    private Game game;
    private Dictionary<String, Label> keys_dict = new Dictionary<string, Label>();
    private Dictionary<String, Label> tiles_dict = new Dictionary<string, Label>();
    public Form1(Game game){
        this.game = game;
        InitializeComponent();
    }
    
    private void key_click(object sender, EventArgs e){
        try{
            Label key = (Label)sender;
            if(key.Text == "ENTER"){
                EndType end_type = game.enter();
                if(end_type == EndType.CorrectWord) end(game.word);
                else if(end_type == EndType.FalseWordEnd) end("");
                return;
            }
            if(key.Text == "DELETE"){
                game.delete();
                return;
            }
            if(game.input_blocked) return;

            Label tile = tiles_dict[game.row.ToString() + game.column.ToString()];
            game.enter_char(key, tile);
        } catch(Exception ex){
            Console.WriteLine(ex);
        }
    }


}
