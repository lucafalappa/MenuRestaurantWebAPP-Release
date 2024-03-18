using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRestaurantWebAPP.Models
{
    public class Portata
    {
        public Guid Id { get; set; }
        public string Tipologia { get; set; }
        public Portata() { }
        public Portata(string tipologia)
        {
            Id = Guid.NewGuid();
            Tipologia = (tipologia != null) ? StandardTipologia(tipologia) : "";
        }
        /// <summary>
        /// Metodo per riscrivere la stringa fornita secondo il seguente pattern:
        /// lettera maiuscola + lettere minuscole
        /// </summary>
        /// <param name="tipologia">Tipologia della portata</param>
        /// <returns>Stringa in formato standard</returns>
        private string StandardTipologia(string tipologia)
        {
            if (tipologia.Length > 0) { tipologia = Char.ToUpper(tipologia[0]) + tipologia.Substring(1).ToLower(); }
            else { tipologia = ""; }
            return tipologia;
        }
    }
}
