using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PortataViewModels
{
    public class PortataDeletedPortataViewModel
    {
        public Portata Portata { get; set; }
        public PortataDeletedPortataViewModel(string tipologia)
        {
            Portata = new Portata(tipologia);
        }
        public PortataDeletedPortataViewModel(Portata portata)
        {
            Portata = portata;
        }
    }
}
