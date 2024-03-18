using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PietanzaViewModels
{
    public class PietanzaUpdatePietanzaViewModel
    {
        public Pietanza Pietanza { get; set; }
        public List<Portata> Portate { get; set; }
        public PietanzaUpdatePietanzaViewModel(List<Portata> portate)
        {
            Pietanza = new Pietanza();
            Portate = portate;
        }
        public PietanzaUpdatePietanzaViewModel(Pietanza pietanza, List<Portata> portate)
        {
            Pietanza = pietanza;
            Portate = portate;
        }
    }
}
