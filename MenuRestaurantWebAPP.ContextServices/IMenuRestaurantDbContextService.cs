using MenuRestaurantWebAPP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRestaurantWebAPP.ContextServices
{
    public interface IMenuRestaurantDbContextService
    {
        /// <summary>
        /// Metodo che esegue il salvataggio del DbContext
        /// in modo asincrono
        /// </summary>
        /// <returns>Operazione asincrona</returns>
        Task SaveDbContextAsync();

        /// <summary>
        /// Metodo che restituisce tutte le portate presenti,
        /// in modo asincrono
        /// </summary>
        /// <returns>Lista delle portate presenti</returns>
        Task<List<Portata>> GetAllPortate();
        /// <summary>
        /// Metodo che restituisce tutte le portate presenti,
        /// aventi come tipologia la tipologia in parametro,
        /// in modo asincrono
        /// </summary>
        /// <param name="tipologia">Tipologia di portata ricercata</param>
        /// <returns>Lista delle portate aventi la tipologia in parametro</returns>
        Task<List<Portata>> GetAllPortateByTipologia(string tipologia);
        /// <summary>
        /// Metodo che restituisce la portata presente avente come
        /// tipologia la tipologia in parametro, in modo asincrono
        /// </summary>
        /// <param name="tipologia">Tipologia di portata ricercata</param>
        /// <returns>Portata avente la tipologia in parametro</returns>
        Task<Portata> GetPortataByTipologia(string tipologia);
        /// <summary>
        /// Metodo che aggiunge la portata in parametro alle portate
        /// presenti, in modo asincrono
        /// </summary>
        /// <param name="portata">Portata da aggiungere</param>
        /// <returns>Operazione asincrona</returns>
        Task AddPortata(Portata portata);
        /// <summary>
        /// Metodo che rimuove la portata in parametro dalle portate
        /// presenti
        /// </summary>
        /// <param name="portata">Portata da rimuovere</param>
        void RemovePortata(Portata portata);

        /// <summary>
        /// Metodo che restituisce tutte le pietanze presenti,
        /// in modo asincrono
        /// </summary>
        /// <returns>Lista delle pietanze presenti</returns>
        Task<List<Pietanza>> GetAllPietanze();
        /// <summary>
        /// Metodo che restituisce tutte le pietanze presenti,
        /// aventi come nome e tipologia di portata le due stringhe in parametro,
        /// e aventi il prezzo compreso tra il prezzo minimo e il prezzo
        /// massimo in parametro, in modo asincrono
        /// </summary>
        /// <param name="nome">Nome della pietanza ricercata</param>
        /// <param name="pmi">Prezzo minimo</param>
        /// <param name="pma">Prezzo massimo</param>
        /// <param name="tipologia">Tipologia di portata associata alla pietanza</param>
        /// <returns>Lista delle pietanze aventi tali caratteristiche</returns>
        Task<List<Pietanza>> GetAllPietanzeByNomePrezzoTipologia(string nome, double pmi, double pma, string tipologia);
        /// <summary>
        /// Metodo che aggiunge la pietanza in parametro alle pietanze
        /// presenti, in modo asincrono
        /// </summary>
        /// <param name="pietanza">Pietanza da aggiungere</param>
        /// <returns>Operazione asincrona</returns>
        Task AddPietanza(Pietanza pietanza);
        /// <summary>
        /// Metodo che verifica se esiste almento una pietanza avente
        /// come nome il nome in parametro, in modo asincrono
        /// </summary>
        /// <param name="nome">Nome della pietanza ricercata</param>
        /// <returns>True se esiste almeno una pietanza con tale nome, false altrimenti</returns>
        Task<bool> AnyPietanzaByNome(string nome);
        /// <summary>
        /// Metodo che restituisce la pietanza presente avente come
        /// nome il nome in parametro, in modo asincrono
        /// </summary>
        /// <param name="nome">Nome della pietanza ricercata</param>
        /// <returns>Pietanza avente la tipologia in parametro</returns>
        Task<Pietanza> GetPietanzaByNome(string nome);
        /// <summary>
        /// Metodo che rimuove la pietanza in parametro dalle pietanze
        /// presenti
        /// </summary>
        /// <param name="pietanza">Pietanza da rimuovere</param>
        void RemovePietanza(Pietanza pietanza);
    }
}
