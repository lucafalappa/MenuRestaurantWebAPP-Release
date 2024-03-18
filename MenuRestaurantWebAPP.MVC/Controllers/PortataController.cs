using MenuRestaurantWebAPP.Contexts;
using MenuRestaurantWebAPP.ContextServices;
using MenuRestaurantWebAPP.Models;
using MenuRestaurantWebAPP.MVC.PortataViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuRestaurantWebAPP.MVC.Controllers
{
    [Authorize]
    public class PortataController : Controller
    {  
        private readonly MenuRestaurantDbContextService _menuRestaurantDbContextService;

        public PortataController(MenuRestaurantDbContext menuRestaurantDbContext)
        {
            _menuRestaurantDbContextService = new MenuRestaurantDbContextService(menuRestaurantDbContext);
        }


        /// <summary>
        /// Metodo per ottenere tutte le portate esistenti
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/GetAllPortate")]
        public async Task<IActionResult> GetAllPortate()
        {
            var viewModel = new PortataGetAllPortateViewModel(await _menuRestaurantDbContextService.GetAllPortate());
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che fornisce la vista utile alla creazione di una portata
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/CreatePortata")]
        public IActionResult CreatePortata()
        {
            var viewModel = new PortataCreatePortataViewModel("");
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che crea una portata e la inserisce nel database
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpPost("/CreatedPortata")]
        public async Task<IActionResult> CreatedPortata(string tipologia)
        {
            if ((tipologia == null) || (await _menuRestaurantDbContextService.GetPortataByTipologia(tipologia) != null)
                || !OnlyLettersAndWhiteSpace(tipologia))
            {
                return Redirect("ErrorPage");
            }
            await _menuRestaurantDbContextService.AddPortata(new Portata(tipologia));
            await _menuRestaurantDbContextService.SaveDbContextAsync();
            var viewModel = new PortataCreatedPortataViewModel(tipologia);
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che restituisce la portata ricercata
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpPost("/GetPortata")]
        public async Task<IActionResult> GetPortata(string tipologia)
        {
            tipologia = (tipologia != null) ? tipologia : "";
            var viewModel = new PortataGetPortataViewModel();
            viewModel.Portate = await _menuRestaurantDbContextService.GetAllPortateByTipologia(tipologia);
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che fornisce la vista utile all'eliminazione di una portata
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/DeletePortata/{tipologia}")]
        public IActionResult DeletePortata(string tipologia)
        {
            var viewModel = new PortataDeletePortataViewModel(tipologia);
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che rimuove la portata selezionata e ne restituisce i dettagli alla vista
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpPost("/DeletedPortata")]
        public async Task<IActionResult> DeletedPortata(string tipologia)
        {
            var deletedPortata = await _menuRestaurantDbContextService.GetPortataByTipologia(tipologia);
            if (deletedPortata != null)
            {
                _menuRestaurantDbContextService.RemovePortata(deletedPortata);
                await _menuRestaurantDbContextService.SaveDbContextAsync();
                var viewModel = new PortataDeletedPortataViewModel(tipologia);
                return View(viewModel);
            }
            else
            {
                return Redirect("ErrorPage");
            }
        }


        /// <summary>
        /// Metodo che controlla se la stringa contiene solo lettere e spazi bianchi
        /// </summary>
        /// <param name="str">Stringa da controllare</param>
        /// <returns></returns>
        private bool OnlyLettersAndWhiteSpace(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
