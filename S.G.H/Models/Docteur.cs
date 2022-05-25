using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S.G.H.Models
{
    public partial class Docteur
    {
        public Docteur()
        {
            Patients = new HashSet<Patient>();
        }

        [Key]
        public int Matricule { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Spécialité { get; set; }

        [Required]
        public string Photo { get; set; }

        //---------------------------------------------------------------
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
