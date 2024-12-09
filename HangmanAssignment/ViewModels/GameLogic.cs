using HangmanAssignment.Models;
using System.Windows.Input;

namespace HangmanAssignment.ViewModels
{
    public class GameLogic : BaseViewModel
    {
        // List of all hangman stage images
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

        // List of predefined questions for word selection
        List<Question> Questions = new List<Question>
        {
            new Question { Quiz = "What is your name?", Answer = "Lebohang" },
            new Question { Quiz = "Who is your facilitator?", Answer = "Mathobela" },
            new Question { Quiz = "When do we knock off?", Answer = "Four" }
        };

        // Game state properties
        private int position;
        public int Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        // Current hangman image
        private Tries imagePosition;
        public Tries ImagePosition
        {
            get => imagePosition;
            set
            {
                imagePosition = value;
                OnPropertyChanged();
            }
        }

        // Game status properties
        private GameState currentGameState;
        public GameState CurrentGameState
        {
            get => currentGameState;
            set
            {
                currentGameState = value;
                OnPropertyChanged();
            }
        }

        // Word to guess
        public string Word { get; private set; }

        // Current state of the secret word
        private string secretWord;
        public string SecretWord
        {
            get => secretWord;
            set
            {
                secretWord = value;
                OnPropertyChanged();
            }
        }

        // User's current guess
        private string userGuess;
        public string UserGuess
        {
            get => userGuess;
            set
            {
                userGuess = value;
                OnPropertyChanged();
            }
        } 
        
        private Question selectedQuestion;
        public Question SelectedQuestion
        {
            get => selectedQuestion;
            set
            {
                selectedQuestion = value;
                OnPropertyChanged();
            }
        }

        // Game commands
        public ICommand PlayCommand { get; }
        public ICommand RestartGameCommand { get; }

        // Game state tracking
        private List<char> guessedLetters = new List<char>();
        private int incorrectGuesses = 0;

        // Constructor
        public GameLogic()
        {
            InitializeGame();
            PlayCommand = new Command(PlayMethod);
            RestartGameCommand = new Command(RestartGame);
        }

        // Game state enum
        public enum GameState
        {
            Playing,
            Won,
            Lost
        }

        
        // Initialize game setup
        private void InitializeGame()
        {
            // Randomly select a word from predefined questions
            Random random = new Random();
            SelectedQuestion = Questions[random.Next(Questions.Count)];
            Word = SelectedQuestion.Answer.ToLower();

            ResetSecretWord();
            Position = 0;
            ImagePosition = ImagesList[Position];
            guessedLetters.Clear();
            incorrectGuesses = 0;
            CurrentGameState = GameState.Playing;
        }

        // Reset the secret word display
        private void ResetSecretWord()
        {
            SecretWord = new string('_', Word.Length);
        }

        // Restart the game
        private void RestartGame()
        {
            InitializeGame();
        }

        // Main game logic method
        private void PlayMethod()
        {
            // Prevent playing after game is over
            if (CurrentGameState != GameState.Playing)
                return;

            if (string.IsNullOrWhiteSpace(UserGuess))
                return;

            char guessedLetter = UserGuess.ToLower()[0];

            // Prevent repeated guesses
            if (guessedLetters.Contains(guessedLetter))
                return;

            guessedLetters.Add(guessedLetter);

            // Check if the guessed letter is in the word
            if (Word.Contains(guessedLetter))
            {
                RevealLetters(guessedLetter);
            }
            else
            {
                HandleIncorrectGuess();
            }

            // Clear the guess after processing
            UserGuess = string.Empty;

            // Check game end conditions
            CheckGameStatus();
        }

        // Reveal correctly guessed letters
        private void RevealLetters(char guessedLetter)
        {
            char[] secretWordChars = SecretWord.ToCharArray();

            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] == guessedLetter)
                {
                    secretWordChars[i] = Word[i];
                }
            }

            SecretWord = new string(secretWordChars);
        }

        // Handle incorrect guesses
        private void HandleIncorrectGuess()
        {
            incorrectGuesses++;
            Position = Math.Min(incorrectGuesses, ImagesList.Count - 1);
            ImagePosition = ImagesList[Position];
        }

        // Check if the game is won or lost
        private void CheckGameStatus()
        {
            // Check for win condition
            if (SecretWord.ToLower() == Word)
            {
                CurrentGameState = GameState.Won;

                App.Current.MainPage.DisplayAlert("Won", $"{Word} is the correct ", "next");
            }

            // Check for lose condition
            if (incorrectGuesses >= ImagesList.Count - 1)
            {
                CurrentGameState = GameState.Lost;
            }
        }
    }
}