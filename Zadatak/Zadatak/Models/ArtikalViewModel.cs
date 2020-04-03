using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak.Models
{
    public class ArtikalViewModel
    {
        public int ArtikalID { get; set; }
        public string ArtikalNaziv { get; set; }
        public string Opis { get; set; }
        public string Kategorija { get; set; }
        public string Proizvodjac { get; set; }
        public string Dobavljac { get; set; }
        public decimal Cena { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}