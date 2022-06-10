using System;
using System.Collections.Generic;

namespace S.G.H.Models
{
    public partial class Facture
    {
        public int Id { get; set; }
        public int Nombre { get; set; }
        public decimal Montant { get; set; }
        public DateTime? DatePaiement { get; set; }
        public string TypePaiement { get; set; }
        public int PatientMatricule { get; set; }
        public DateTime? DateDépart { get; set; }
        

        public virtual Patient Patient { get; set; }
    }
}
