using MenuRestaurantWebAPP.Contexts;
using MenuRestaurantWebAPP.ContextServices;
using MenuRestaurantWebAPP.Models;
using MenuRestaurantWebAPP.MVC.PietanzaViewModels;
using MenuRestaurantWebAPP.MVC.PortataViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MenuRestaurantWebAPP.MVC.Controllers
{
    [Authorize]
    public class PietanzaController : Controller
    {
        private readonly MenuRestaurantDbContextService _menuRestaurantDbContextService;

        public PietanzaController(MenuRestaurantDbContext menuRestaurantDbContext)
        {
            _menuRestaurantDbContextService = new MenuRestaurantDbContextService(menuRestaurantDbContext);
        }

        /// <summary>
        /// Metodo per ottenere tutte le pietanze esistenti
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/GetAllPietanze")]
        public async Task<IActionResult> GetAllPietanze()
        {
            var viewModel = new PietanzaGetAllPietanzeViewModel(await _menuRestaurantDbContextService.GetAllPietanze());
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che fornisce la vista utile alla creazione di una pietanza
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/CreatePietanza")]
        public async Task<IActionResult> CreatePietanza()
        {
            var viewModel = new PortataGetAllPortateViewModel(await _menuRestaurantDbContextService.GetAllPortate());
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che crea una pietanza e la inserisce nel database
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpPost("/CreatedPietanza")]
        public async Task<IActionResult> CreatedPietanza(string nome, string prezzo, string tipologia)
        {
            var prezzoCorretto = ConvertStringFromInputToDouble(prezzo);
            if (nome == null || tipologia == null || (prezzoCorretto.Item1 <= 0)
                || (prezzoCorretto.Item1 > double.MaxValue) || !OnlyLettersAndWhiteSpace(nome))
            {
                return Redirect("ErrorPage");
            }
            if (await _menuRestaurantDbContextService.AnyPietanzaByNome(nome))
            {
                return Redirect("ErrorPage");
            }
            else
            {
                var portata = await _menuRestaurantDbContextService.GetPortataByTipologia(tipologia);
                var createdPietanza = new Pietanza(nome, prezzoCorretto.Item1, portata.Id, portata.Tipologia);
                await _menuRestaurantDbContextService.AddPietanza(createdPietanza);
                await _menuRestaurantDbContextService.SaveDbContextAsync();
                var viewModel = new PietanzaGetPietanzaViewModel();
                viewModel.Pietanze.Add(createdPietanza);
                return View(viewModel);
            }
        }


        /// <summary>
        /// Metodo che fornisce la vista utile alla ricerca di una pietanza
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/SearchPietanza")]
        public async Task<IActionResult> SearchPietanza()
        {
            var viewModel = new PortataGetAllPortateViewModel(await _menuRestaurantDbContextService.GetAllPortate());
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che restituisce la pietanza ricercata
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpPost("/GetPietanza")]
        public async Task<IActionResult> GetPietanza(string nome, string prezzoMinimo, string prezzoMassimo, string tipologia)
        {
            var pmi = ConvertStringFromInputToDouble(prezzoMinimo);
            var pma = ConvertStringFromInputToDouble(prezzoMassimo);
            if (pmi.Item1 > double.MaxValue || (pmi.Item1 < 0 && pmi.Item2 == true)
                || (pma.Item1 < 0 && pmi.Item2 == true) || pma.Item1 < pmi.Item1)
            {
                return Redirect("ErrorPage");
            }
            pmi.Item1 = (pmi.Item2 == false) ? 0 : pmi.Item1;
            pma.Item1 = (pma.Item2 == false) ? double.MaxValue : pma.Item1;
            nome = (nome != null) ? nome : "";
            tipologia = (tipologia != null) ? tipologia : "";
            var viewModel = new PietanzaGetPietanzaViewModel();
            viewModel.Pietanze = await _menuRestaurantDbContextService.GetAllPietanzeByNomePrezzoTipologia(nome, pmi.Item1, pma.Item1, tipologia);
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che fornisce la vista utile alla modifica di una pietanza
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/UpdatePietanza/{nome}")]
        public async Task<IActionResult> UpdatePietanza(string nome)
        {
            var viewModel = new PietanzaUpdatePietanzaViewModel(await _menuRestaurantDbContextService.GetAllPortate());
            viewModel.Pietanza.Nome = StandardNome(nome);
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che modifica la pietanza selezionata e ne restituisce i dettagli alla vista
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpPost("/UpdatedPietanza")]
        public async Task<IActionResult> UpdatedPietanza(string nome, string prezzo, string tipologia)
        {
            var updatedPietanza = await _menuRestaurantDbContextService.GetPietanzaByNome(nome);
            var prezzoCorretto = ConvertStringFromInputToDouble(prezzo);
            if (updatedPietanza == null || (prezzoCorretto.Item1 <= 0)
                || (prezzoCorretto.Item1 > double.MaxValue))
            {
                return Redirect("ErrorPage");
            }
            else
            {
                var portata = await _menuRestaurantDbContextService.GetPortataByTipologia(tipologia);
                updatedPietanza.Prezzo = prezzoCorretto.Item1;
                updatedPietanza.PortataId = portata.Id;
                updatedPietanza.Tipologia = portata.Tipologia;
                var viewModel = new PietanzaUpdatedPietanzaViewModel(updatedPietanza);
                await _menuRestaurantDbContextService.SaveDbContextAsync();
                return View(viewModel);
            }
        }


        /// <summary>
        /// Metodo che fornisce la vista utile all'eliminazione di una pietanza
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpGet("/DeletePietanza/{nome}")]
        public IActionResult DeletePietanza(string nome)
        {
            var viewModel = new PietanzaDeletePietanzaViewModel();
            viewModel.Pietanza.Nome = StandardNome(nome);
            return View(viewModel);
        }


        /// <summary>
        /// Metodo che rimuove la pietanza selezionata e ne restituisce i dettagli alla vista
        /// </summary>
        /// <returns>ViewResult creato per la risposta, con il modello specificato</returns>
        [HttpPost("/DeletedPietanza")]
        public async Task<IActionResult> DeletedPietanza(string nome)
        {
            var deletedPietanza = await _menuRestaurantDbContextService.GetPietanzaByNome(nome);
            if (deletedPietanza == null)
            {
                return Redirect("ErrorPage");
            }
            else
            {
                _menuRestaurantDbContextService.RemovePietanza(deletedPietanza);
                await _menuRestaurantDbContextService.SaveDbContextAsync();
                var viewModel = new PietanzaDeletedPietanzaViewModel(deletedPietanza);
                return View(viewModel);
            }
        }


        /// <summary>
        /// Metodo che converte una stringa in un valore double valido per il prezzo
        /// impostando un flag a seconda se la stringa e' stata inserita dall'utente
        /// (true ) o no (false)
        /// </summary>
        /// <param name="input">Stringa da convertire</param>
        /// <returns>(Valore convertito, flag relativo alla scrittura)</returns>
        private (double, bool) ConvertStringFromInputToDouble(string input)
        {
            input = (string.IsNullOrEmpty(input)) ? "" : input;
            input = input.Replace(',', '.');
            double result;
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                if (input.ToUpper().Equals("-1.7976931348623157E+308".ToUpper()))
                {
                    // la stringa corrispondente a double.MinValue e' stata scritta 
                    // dall'utente
                    return (double.MinValue, true);
                }
                else
                {
                    // qualsiasi numero inserito, come stringa, dall'utente
                    return (result, true);
                }
            }
            else
            {
                // valore di default nel caso in cui non sia stato inserito alcun valore
                return (double.MinValue, false);
            }
        }

        /// <summary>
        /// Metodo per riscrivere la stringa fornita secondo il seguente pattern:
        /// lettera maiuscola + lettere minuscole
        /// </summary>
        /// <param name="nome">Nome della pietanza</param>
        /// <returns>Stringa in formato standard</returns>
        private string StandardNome(string nome)
        {
            nome = Char.ToUpper(nome[0]) + nome.Substring(1).ToLower();
            return nome;
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
