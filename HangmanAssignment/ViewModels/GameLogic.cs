

using System.Collections.ObjectModel;

namespace HangmanAssignment.ViewModels
{
    public class GameLogic
    {


        public ObservableCollection<Tries> Tries { get; set; }

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
        }
        
    }

        public class Tries
    {
        public int Id { get; set; }
        public string Image { get; set; }
    }
}
