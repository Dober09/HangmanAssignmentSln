

using HangmanAssignment.Models;
using System.Windows.Input;

namespace HangmanAssignment.ViewModels
{
    public class GameLogic : BaseViewModel
    {
        //list of all pictutre
        private List<Tries> ImagesList = new List<Tries> {
        new Tries {Image="hang1.png"},
        new Tries {Image="hang2.png"},
        new Tries {Image="hang3.png"},
        new Tries {Image="hang4.png"},
        new Tries {Image="hang5.png"},
        new Tries {Image="hang6.png"},
        new Tries {Image="hang7.png"},
        new Tries {Image="hang8.png"},
     };

        //position of the loss
        private int position;

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        // the image displayed
        private Tries imagePositon;

        public Tries ImagePositon
        {
            get { return imagePositon; }
            set
            {
                imagePositon = value;
                OnPropertyChanged();
            }
        }



        public string Word; 
        public GameLogic()
        {
            Position = 0;
            InitialWord();

            ImagePositon = ImagesList[Position];
            PlayCommand = new Command(async =>  PlayMethod());
        }

        private void InitialWord()
        {
            Word = "lebohang";
            SecretWord = "";
            foreach(char letter in  Word)
            {
                SecretWord +=" _ ";
            }

        }


        //methods playmethod
        private async void PlayMethod()
        {
           
           
                PlayLogic();
            
            
           

        }

        private void PlayLogic()
        {
            
            if (Word.ToLower().Contains(UserGuess.ToLower()))
            {
                for (int i = 0; i < SecretWord.Length; i++)
                {
                    if (Word[i].ToString() == UserGuess.ToLower())
                    {
                        SecretWord += $" {UserGuess.ToLower()} ";
                    }
                    else
                    {
                        SecretWord += " _ ";
                    }

                }
            }
        }

        // the secret Word
        private string? secretWord;
        public string SecretWord
        {
            get { return secretWord; }
            set { secretWord = value; OnPropertyChanged(); }

        }


        // the user guess value

        private string? userGuess;

        public string? UserGuess
        {
            get { return userGuess; }
            set
            {
                userGuess = value;
            }
        }


        public ICommand PlayCommand {  get; }





    }
   
}
