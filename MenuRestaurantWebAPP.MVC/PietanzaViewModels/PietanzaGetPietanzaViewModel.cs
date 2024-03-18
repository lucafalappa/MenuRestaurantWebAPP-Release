using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PietanzaViewModels
{
    public class PietanzaGetPietanzaViewModel
    {
        public List<Pietanza> Pietanze { get; set; }
        public PietanzaGetPietanzaViewModel()
        {
            Pietanze = new List<Pietanza>();
        }
        public PietanzaGetPietanzaViewModel(List<Pietanza> pietanze)
        {
            Pietanze = pietanze;
        }
    }
}
