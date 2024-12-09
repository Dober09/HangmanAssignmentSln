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
            new Question { Quiz = "What is your name ? ", Answer = "Alan" },
            new Question { Quiz = "Who is your facilitator ? ", Answer = "Mathobela" },
            new Question { Quiz = "When do we knock off ? ", Answer = "Three" },
            new Question { Quiz = "How many traineers are in the industry ", Answer = "Four"},
            new Question { Quiz = "Who is the BodyBuilder in the lab ",Answer = "Starman" },
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
            // Create a string with underscores separated by spaces
            SecretWord = string.Join(" ", Enumerable.Repeat("_", Word.Length));
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

        private void RevealLetters(char guessedLetter)
        {
            // Split the current secret word by spaces
            string[] secretWordParts = SecretWord.Split(' ');

            // Create a new array to store updated parts
            string[] updatedParts = new string[secretWordParts.Length];

            for (int i = 0; i < Word.Length; i++)
            {
                if (char.ToLower(Word[i]) == char.ToLower(guessedLetter))
                {
                    // Replace the underscore with the actual letter
                    updatedParts[i] = Word[i].ToString();
                }
                else
                {
                    // Keep the existing part (underscore or previously revealed letter)
                    updatedParts[i] = secretWordParts[i];
                }
            }

            // Rejoin the parts with spaces
            SecretWord = string.Join(" ", updatedParts);
        }

        // Reveal correctly guessed letters
      
        private void HandleIncorrectGuess()
        {
            incorrectGuesses++;
            Position = Math.Min(incorrectGuesses, ImagesList.Count - 1);
            ImagePosition = ImagesList[Position];
        }

        // Check if the game is won or lost
        private void CheckGameStatus()
        {
            // Remove spaces from SecretWord for comparison
            string cleanedSecretWord = SecretWord.Replace(" ", "");
            string cleanedWord = Word;

            // Check for win condition
            if (cleanedSecretWord.ToLower() == cleanedWord.ToLower())
            {
                CurrentGameState = GameState.Won;
                App.Current.MainPage.BackgroundColor =Colors.Green ;
                App.Current.MainPage.DisplayAlert($"Congratulations! You {CurrentGameState} ", $"You won! The word was {Word}", "Next");
                RestartGame();
            }
            // Check for lose condition
            if (incorrectGuesses >= ImagesList.Count - 1)
            {
                CurrentGameState = GameState.Lost;
                
               App.Current.MainPage.DisplayAlert($"Game Over! You {CurrentGameState}", $"The correct word was {Word}", "Try Again");
                RestartGame();
            }
        }
    
    }
}