using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PietanzaViewModels
{
    public class PietanzaDeletedPietanzaViewModel
    {
        public Pietanza Pietanza { get; set; }
        public PietanzaDeletedPietanzaViewModel()
        {
            Pietanza = new Pietanza();
        }
        public PietanzaDeletedPietanzaViewModel(Pietanza pietanza)
        {
            Pietanza = pietanza;
        }
    }
}
