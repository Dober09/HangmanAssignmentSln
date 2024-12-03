

using System.Collections.ObjectModel;
using System.Windows.Input;

using HangmanAssignment.Models;

namespace HangmanAssignment.ViewModels
{
    public class GameLogic : BaseViewModel
    {

        // players lives till game over
        private int _lives;

        // image to be displayed 
        private string _photo;


        //The Guess Button
        public ICommand GuessCommand;


        public string Photo
        {
            get { return _photo; }

            set { _photo = value;
                OnPropertyChanged();
            
                }
        }



        public int Lives
        {
            get => _lives;
            set
            {

            }
        }

        /// The list of all the stages in an app  and images 
        public ObservableCollection<Tries> Tries { get; set; }

        private string secretword;

        
        public string SecretWord
        {
            get => secretword;
            set
            {
                secretword = value;
                OnPropertyChanged();
            }
        }


        public GameLogic() { 
            Tries = new ObservableCollection<Tries> { 
                new Tries{Id=1,Image="hang1.png"},
                new Tries{Id=2,Image="hang2.png"},
                new Tries{Id=3,Image="hang3.png"},
                new Tries{Id=4,Image="hang4.png"},
                new Tries{Id=5,Image="hang5.png"},
                new Tries{Id=6,Image="hang6.png"},
                new Tries{Id=7,Image="hang7.png"},
                new Tries{Id=8,Image="hang8.png"},
            };
            
            //the word is currentguess
            SecretWord = "currentguess";


            char[] charArray = new char[SecretWord.Length];
            for (int i = 0; i < SecretWord.Length; i++)
            {                                                                    
                charArray[i] = SecretWord[i];
            }

            GuessCommand = new Command(async () =>  GuessMethod());
        }


        private async void GuessMethod()
        {
            //check what life of the page
            if(Lives > 8)
            {
                //Do something
                //change the image
                //and increase the lives
            }
            else
            {
                //the player loss the game 
                //
            }

        }




       
        
    }

   
}
