using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PietanzaViewModels
{
    public class PietanzaDeletePietanzaViewModel
    {
        public Pietanza Pietanza { get; set; }
        public PietanzaDeletePietanzaViewModel()
        {
            Pietanza = new Pietanza();
        }
        public PietanzaDeletePietanzaViewModel(Pietanza pietanza)
        {
            Pietanza = pietanza;
        }
    }
}
