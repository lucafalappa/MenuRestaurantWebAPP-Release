using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PietanzaViewModels
{
    public class PietanzaUpdatedPietanzaViewModel
    {
        public Pietanza Pietanza { get; set; }
        public PietanzaUpdatedPietanzaViewModel()
        {
            Pietanza = new Pietanza();
        }
        public PietanzaUpdatedPietanzaViewModel(Pietanza pietanza)
        {
            Pietanza = pietanza;
        }
    }
}
