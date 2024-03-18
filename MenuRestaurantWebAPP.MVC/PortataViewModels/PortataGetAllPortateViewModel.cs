using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PortataViewModels
{
    public class PortataGetAllPortateViewModel
    {
        public List<Portata> Portate { get; set; }
        public PortataGetAllPortateViewModel()
        {
            Portate = new List<Portata>();
        }
        public PortataGetAllPortateViewModel(List<Portata> portate)
        {
            Portate = portate;
        }
    }
}
