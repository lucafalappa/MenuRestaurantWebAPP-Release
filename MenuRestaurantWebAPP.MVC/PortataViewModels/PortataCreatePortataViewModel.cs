using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PortataViewModels
{
    public class PortataCreatePortataViewModel
    {
        public Portata Portata { get; set; }
        public PortataCreatePortataViewModel(string tipologia)
        {
            Portata = new Portata(tipologia);
        }
        public PortataCreatePortataViewModel(Portata portata)
        {
            Portata = portata;
        }
    }
}
