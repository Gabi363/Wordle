namespace WORDLE;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent(){

        // this.SuspendLayout();
        this.Size = new System.Drawing.Size(size_x,size_y);
        this.Text =  "WORDLE";
        this.BackColor = Color.Black;
        create_keyboard();
        create_tiles();
        this.CenterToScreen();
        // this.panel_keys.ResumeLayout(false);
        // this.ResumeLayout(false);
    } 

    #endregion 

    private Panel panel_keys = new Panel();
    private Panel panel_tiles = new Panel();
    private int size_x = 1100;
    private int size_y = 1300;

    private void create_keyboard(){

        String[] keys_array = { "QWERTYUIOP", "ASDFGHJKL", "eZXCVBNMd" };
        int key_width = 80;
        int key_height = 110;
        int space = 15;

        for(int i=0; i<keys_array.Length; i++){
            int indent;
            if(i == 0 || i ==2) indent = 0;
            else indent = key_width/2;

            for(int j=0; j<keys_array[i].Length; j++){
                string letter = keys_array[i][j].ToString();

                Label key = new Label{
                    Height = key_height,
                    Top = (i * (key_height + space) + space),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Calibri", 16, FontStyle.Bold),
                    BackColor = Color.DimGray,
                    ForeColor = Color.White
                };

                if(letter == "e") {
                    key.Text = "ENTER";
                    key.Width = 3 * key_width / 2;
                    key.Left = j * (key.Width + space) + indent;
                    key.Font = new Font("Calibri", 12, FontStyle.Bold);
                    indent = key_width / 2;
                }
                else if(letter == "d") {
                    key.Text = "DELETE";
                    key.Width = 3 * key_width / 2;
                    key.Left = j * (key_width + space) + indent;
                    key.Font = new Font("Calibri", 12, FontStyle.Bold);
                }
                else{
                    key.Text = letter;
                    key.Width = key_width;
                    key.Left = j * (key.Width + space) + indent;
                }
                key.Click += key_click;
                key.Visible = true;
                panel_keys.Controls.Add(key);
                keys_dict.Add(letter, key);
            }
        }
        panel_keys.Size = new Size(10*key_width + 10*space, 3*key_height + 3*space);
        panel_keys.Location = new Point(size_x/2 - panel_keys.Size.Width/2, 
                                        size_y - 4*panel_keys.Size.Height/3);

        // panel_keys.TabIndex = 1;
        // panel_keys.SuspendLayout();
        this.Controls.Add(panel_keys);
    }

    private void create_tiles(){
        int tile_size = 90;
        int space = 15;

        for(int i=0; i<6; i++){
            for(int j=0; j<game.max_length; j++){
                Label tile = new Label{
                    Width = tile_size,
                    Height = tile_size,
                    Top = (i * (tile_size + space) + space),
                    Left = j * (tile_size + space),
                    Font = new Font("Calibri", 20, FontStyle.Bold),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "",
                    Name = i.ToString() + j.ToString(),
                    ForeColor = Color.White,
                    BackColor = Color.Black
                };
                tile.Visible = true;
                tiles_dict.Add(tile.Name, tile);
                panel_tiles.Controls.Add(tile);
            }
            panel_tiles.Size = new Size(5*(tile_size+space), 6*(tile_size+space));
            // panel_tiles.Location = new Point(size_x/2 - panel_keys.Size.Width/2, 
            //                                 size_y - panel_keys.Size.Height);
            panel_tiles.Location = new Point(size_x/2 - panel_tiles.Size.Width/2,
                                            size_y/20);
            this.Controls.Add(panel_tiles);
        }
    }

    private void end(bool win){
        Panel panel_end = new Panel();
        Label end_window = new Label{
                    // Width = size_x/3,
                    // Height = size_y/4,
                    // Top = size_x/9,
                    // Left = size_x/9,
                    Font = new Font("Calibri", 20, FontStyle.Bold),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White
                };
        if(win){ 
            end_window.Text = "You won!";
            end_window.BackColor = Color.Green;
        }
        else{ 
            end_window.Text = "You lost. Correct word: " + game.word.ToUpper();
            end_window.BackColor = Color.Red;
        }

        end_window.Visible = true;

        end_window.Size = new Size(8*size_x/9, size_y/5);
        // end_window.Location = new Point((size_x-end_window.Size.Width)/2, 
        //                                 (size_y-end_window.Size.Height)/2);
        // end_window.CenterToParent();
        panel_end.Controls.Add(end_window);
        panel_end.Size = new Size(8*size_x/9, size_y/5);
        panel_end.Location = new Point((size_x-end_window.Size.Width)/2, 
                                        (size_y-end_window.Size.Height)/2);
        panel_end.Controls[panel_end.Controls.Count-1].BringToFront();

        this.Controls.Add(panel_end);
        // this.Controls.Add(end_window);
        this.Controls[this.Controls.Count-1].BringToFront();
    }
}
