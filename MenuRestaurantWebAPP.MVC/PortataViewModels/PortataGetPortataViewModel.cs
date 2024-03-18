using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PortataViewModels
{
    public class PortataGetPortataViewModel
    {
        public List<Portata> Portate { get; set; }
        public PortataGetPortataViewModel()
        {
            Portate = new List<Portata>();
        }
        public PortataGetPortataViewModel(List<Portata> portate)
        {
            Portate = portate;
        }
    }
}
