using MenuRestaurantWebAPP.Contexts;
using MenuRestaurantWebAPP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRestaurantWebAPP.ContextServices
{
    public class MenuRestaurantDbContextService : IMenuRestaurantDbContextService
    {
        public MenuRestaurantDbContext _menuRestaurantDbContext { get; set; }
        public MenuRestaurantDbContextService(MenuRestaurantDbContext menuRestaurantDbContext)
        {
            _menuRestaurantDbContext = menuRestaurantDbContext;
        }

        public async Task SaveDbContextAsync()
        {
            await _menuRestaurantDbContext.SaveChangesAsync();
        }

        public async Task<List<Portata>> GetAllPortate()
        {
            return await _menuRestaurantDbContext._portate.ToListAsync();
        }
        public async Task<List<Portata>> GetAllPortateByTipologia(string tipologia)
        {
            var result = new List<Portata>();
            foreach (var p in await _menuRestaurantDbContext._portate.ToListAsync())
            {
                if (p.Tipologia.ToUpper().Contains(tipologia.ToUpper()))
                {
                    result.Add(p);
                }
            }
            return result;
        }
        public async Task<Portata> GetPortataByTipologia(string tipologia)
        {
            return await _menuRestaurantDbContext._portate.FirstOrDefaultAsync(p => 
            p.Tipologia.ToUpper().Equals(tipologia.ToUpper()));
        }
        public async Task AddPortata(Portata portata)
        {
            await _menuRestaurantDbContext._portate.AddAsync(portata);
        }
        public void RemovePortata(Portata portata)
        {
            _menuRestaurantDbContext._portate.Remove(portata);
        }

        public async Task<List<Pietanza>> GetAllPietanze()
        {
            return await _menuRestaurantDbContext._pietanze.ToListAsync();
        }
        public async Task<List<Pietanza>> GetAllPietanzeByNomePrezzoTipologia(string nome, 
            double pmi, double pma, string tipologia)
        {
            var result = new List<Pietanza>();
            foreach (var p in await _menuRestaurantDbContext._pietanze.ToListAsync())
            {
                if ((p.Nome.ToUpper().Contains(nome.ToUpper())) 
                    && (p.Prezzo >= pmi) && (p.Prezzo <= pma) 
                    && (p.Tipologia.ToUpper().Contains(tipologia.ToUpper())))
                {
                    result.Add(p);
                }
            }
            return result;
        }
        public async Task AddPietanza(Pietanza pietanza)
        {
            await _menuRestaurantDbContext._pietanze.AddAsync(pietanza);
        }
        public async Task<bool> AnyPietanzaByNome(string nome)
        {
            return await _menuRestaurantDbContext._pietanze.AnyAsync(p => 
            p.Nome.ToUpper().Equals(nome.ToUpper()));
        }

        public async Task<Pietanza> GetPietanzaByNome(string nome)
        {
            return await _menuRestaurantDbContext._pietanze.FirstOrDefaultAsync(p => 
            p.Nome.ToUpper().Equals(nome.ToUpper()));
        }
        public void RemovePietanza(Pietanza pietanza)
        {
            _menuRestaurantDbContext._pietanze.Remove(pietanza);
        }
    }
}
