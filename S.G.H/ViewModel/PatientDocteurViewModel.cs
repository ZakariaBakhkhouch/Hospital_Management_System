using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using S.G.H.Models;

namespace S.G.H.ViewModel
{
    public class PatientDocteurViewModel
    {
        // Patient classe
        public int Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Genre { get; set; }
        public string DateNaissance { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }


        // docteur classe
        public string FullName { get; set; }
        public string Spécialité { get; set; }
        public string docteurGenre { get; set; }
        public string Photo { get; set; }

        // new property
        public List<Docteur> Docteurs { get; set; }
        public int DocteurMatricule { get; set; }
        public IFormFile File { get; set; }

    }
}
