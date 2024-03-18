using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRestaurantWebAPP.Models
{
    public class Pietanza
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public double Prezzo { get; set; }
        public Guid PortataId { get; set; }
        public string Tipologia { get; set; }
        public Pietanza() { }
        public Pietanza(string nome, double prezzo, Guid portataId, string tipologia)
        {
            Id = Guid.NewGuid();
            Nome = StandardNome(nome);
            Prezzo = (prezzo > 0) ? prezzo : throw new ArgumentException("Prezzo nullo o negativo");
            PortataId = portataId;
            Tipologia = (tipologia != null) ? StandardTipologia(tipologia) : "";
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
