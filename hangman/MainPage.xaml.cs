using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Maui.Controls;
namespace hangman;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{

    public string Spotlight
    {
        get => spotlight;
        set
        {
            spotlight = value;
            OnPropertyChanged();
        }

    }

    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged();
        }
    }

    public int Chances
    {
        get => chances;
        set
        {
            chances = value;
            OnPropertyChanged();
        }
    }

    public string Image
    {
        get => image;
        set
        {
            image = value;
            OnPropertyChanged();
        }
    }

    List<string> words = new List<string>() {
        "python",
        "maui",
        "javascript",
        "csharp",
        "objc",
        "swift",
        "django",
        "java",
        "database",
        "sql",
        "xaml",
        "json",
        "aspdotnet",
        "node",
        "react"
    };
    string answer;
    private string spotlight;
    List<char> guessed = new List<char>();

    private string message;
    private int chances;
    private string image;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        get_button();
        restart();
    }

    void get_button()
    {
        string alpha = "abcdefghijklmnopqrstuvwxyz";
        foreach (char a in alpha)
        {
            Button button = new Button
            {

                Text = a.ToString(),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };
            button.Clicked += Button_Clicked;
            alpha_input_Grid.Add(button);


        }
    }


    private void restart() {
        Chances = 6;
        Image = $"img{6 - Chances}.jpg";
        Message = "";
        guessed = new List<char>();
        pickWord();
        calculate_word(answer, guessed);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var parameter = btn.Text.ToCharArray();
        btn.IsEnabled = false;
        if (guessed.IndexOf(parameter[0]) == -1)
        {
            guessed.Add(parameter[0]);
            handleGuess();
        }
    }

    private void handleGuess()
    {
        calculate_word(answer, guessed);

    }

    private bool IfGameWon(string t)
    {
        t = t.Replace(" ", "");
        if (t != answer)
        {
            return false;
        }
        Message = "You Win!";
        disable_button();
        return true;

    }

    private void pickWord()
    {
        answer = words[new Random().Next(0, words.Count)];
        Debug.WriteLine(answer);
    }

    private void calculate_word(string answer, List<char> guessed)
    {
        try
        {
            var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_')).ToArray();
            IfGameWon(string.Join(' ', temp));
            if (IfGameWon(string.Join(' ', temp)) == false)
            {
                if (Spotlight == string.Join(' ', temp))
                {
                    Chances -= 1;
                    Image = $"img{6 - Chances}.jpg";
                    if (Chances <= 0)
                    {
                        disable_button();
                        Message = "You Lost!";
                    }
                }
            }
            
            Spotlight = string.Join(' ', temp);
        }
        catch
        {
            var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_')).ToArray();
            Spotlight = string.Join(' ', temp);
        }

    }

    private void disable_button()
    {
        foreach(var b in alpha_input_Grid.Children) {
            var btn = (Button)b;
            btn.IsEnabled = false;
        }
    }

    private void enable_button()
    {
        foreach (var b in alpha_input_Grid.Children)
        {
            var btn = (Button)b;
            btn.IsEnabled = true;
        }
    }

    void reset_button_Clicked(System.Object sender, System.EventArgs e)
    {
        restart();

        enable_button();
    }
}


