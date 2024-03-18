using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PortataViewModels
{
    public class PortataCreatedPortataViewModel
    {
        public Portata Portata { get; set; }
        public PortataCreatedPortataViewModel(string tipologia)
        {
            Portata = new Portata(tipologia);
        }
        public PortataCreatedPortataViewModel(Portata portata)
        {
            Portata = portata;
        }
    }
}
