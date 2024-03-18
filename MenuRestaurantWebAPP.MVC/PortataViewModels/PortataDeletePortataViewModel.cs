using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PortataViewModels
{
    public class PortataDeletePortataViewModel
    {
        public Portata Portata { get; set; }
        public PortataDeletePortataViewModel(string tipologia)
        {
            Portata = new Portata(tipologia);
        }
        public PortataDeletePortataViewModel(Portata portata)
        {
            Portata = portata;
        }
    }
}
