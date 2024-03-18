using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC.PietanzaViewModels
{
    public class PietanzaGetAllPietanzeViewModel
    {
        public List<Pietanza> Pietanze { get; set; }
        public PietanzaGetAllPietanzeViewModel()
        {
            Pietanze = new List<Pietanza>();
        }
        public PietanzaGetAllPietanzeViewModel(List<Pietanza> pietanze)
        {
            Pietanze = pietanze;
        }
    }
}
