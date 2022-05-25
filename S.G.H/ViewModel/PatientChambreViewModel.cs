using S.G.H.Models;
using System.Collections.Generic;

namespace S.G.H.ViewModel
{
    public class PatientChambreViewModel
    {
        public int Id { get; set; }
        public int Nombre { get; set; }
        public string Type { get; set; }
        public string Statu { get; set; }

        // classe Patient
        public int Matricule { get; set; }
        public string Nom { get; set; }

        // additional
        public List<Patient> patients { get; set; }

    }
}
