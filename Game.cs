using WORDLE;

public class Game{
    public int max_words = 6;
    public int max_length = 5;
    public int row = 0;
    public int column = 0;
    public bool input_blocked = false;
    private string current_word = "";
    public string word;
    public Color yellow = Color.FromArgb(225, 204, 0);
    public Color green = Color.FromArgb(0, 153, 51);
    public Color red = Color.FromArgb(236, 19, 19);


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
        // string entered_word = String.Concat(letters_list.ToList)

        Dictionary<string, int> letter_count = new Dictionary<string, int>();
        int correct_letters = 0;
        for(int i=0; i<max_length; i++){

            string letter = letters_list[i].Text; //
            if(!letter_count.ContainsKey(letter)) letter_count[letter] = 0;

            if(letter == word[i].ToString()){
                correct_letters++;
                letters_list[i].BackColor = green;
                letter_count[letter] ++;
                int above = letter_count[letter] - word.Count(c => c.ToString()==letter);
                if(above > 0){
                    for(int j=0; j<max_length && above>0; j++){
                        if(letters_list[j].Text == letter && letters_list[j].BackColor == yellow){ //
                            letters_list[j].BackColor = Color.Black;
                            above--;
                        }
                    }
                }
                
            }
            else if(word.Contains(letter)){
                if(letter_count[letter] < word.Count(c => c.ToString()==letter)){
                    letters_list[i].BackColor = yellow;
                    letter_count[letter] ++;
                }
            }
            else{
                foreach(Label l in Form1.panel_keys.Controls){
                    if(l.Text.ToLower() == letter.ToLower()) l.BackColor = Color.FromArgb(38, 38, 38);
                }
                
            }
        }

        return correct_letters == max_length;
    }

}