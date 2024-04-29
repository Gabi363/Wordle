using WORDLE;

public class Game{
    public int max_words = 6;
    public int max_length = 5;
    public int row = 0;
    public int column = 0;
    public bool input_blocked = false;
    private string current_word = "";
    public string word;
    private List<Label> letters_list = new List<Label>();

    public Game(string word){
        this.word = word;
    }

    public void enter_char(Label key, Label tile){
        tile.Text = key.Text;
        current_word += key.Text;
        letters_list.Add(tile);
        increment();
    }

    public EndType enter(){
        if(check_word()) return EndType.CorrectWord;

        input_blocked = false;
        letters_list.Clear();
        if(row == max_words) return EndType.FalseWordEnd;

        return EndType.FalseWordContinue;
    }
    public void delete(){
        if(letters_list.Count > 0){
            letters_list[letters_list.Count-1].Text = "";
            letters_list.RemoveAt(letters_list.Count-1);
            current_word = current_word.Remove(current_word.Length-1);
            if(input_blocked){
                input_blocked = false;
                row--;
                column = max_length-1;
            }
            else column--;
        }
    }
    private void increment(){
        if(column == max_length-1){
            row++;
            column = 0;
            input_blocked = true;
        }
        else column++;
    }

    private bool check_word(){

        Dictionary<string, int> letter_count = new Dictionary<string, int>();
        int correct_letters = 0;
        for(int i=0; i<max_length; i++){

            string letter = letters_list[i].Text; //
            if(!letter_count.ContainsKey(letter)) letter_count[letter] = 0;

            if(letter == word[i].ToString()){
                correct_letters++;
                letters_list[i].BackColor = Color.Green;
                letter_count[letter] ++;
                int above = letter_count[letter] - word.Count(c => c.ToString()==letter);
                if(above > 0){
                    for(int j=0; j<max_length && above>0; j++){
                        if(letters_list[j].Text == letter && letters_list[j].BackColor == Color.DarkOrange){ //
                            letters_list[j].BackColor = Color.Black;
                            above--;
                        }
                    }
                }
                
            }
            else if(word.Contains(letter)){
                if(letter_count[letter] < word.Count(c => c.ToString()==letter)){
                    letters_list[i].BackColor = Color.DarkOrange;
                    letter_count[letter] ++;
                }
            }
        }

        return correct_letters == max_length;
    }

}