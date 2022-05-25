using S.G.H.Models;
using System;
using System.Collections.Generic;

namespace S.G.H.ViewModel
{
    public class PatientFactureViewModel
    {
        public int Id { get; set; }
        public int Nombre { get; set; }
        public decimal Montant { get; set; }
        public DateTime? DatePaiement { get; set; }
        public string TypePaiement { get; set; }
        public int PatientMatricule { get; set; }
        public virtual Patient Patient { get; set; }

        // the new property
        public List<Patient> Patients { get; set; }
    }
}
