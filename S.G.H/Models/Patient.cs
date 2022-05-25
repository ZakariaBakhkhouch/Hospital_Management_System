using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S.G.H.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Chambres = new HashSet<Chambre>();
            Factures = new HashSet<Facture>();
        }

        [Key]
        public int Matricule { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string DateNaissance { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Adresse { get; set; }
        
        //-----------------------------------------------------------------------
        public int? DocteurMatricule { get; set; }
        public virtual Docteur SonDocteur { get; set; }
        public virtual ICollection<Chambre> Chambres { get; set; }
        public virtual ICollection<Facture> Factures { get; set; }
    }
}
