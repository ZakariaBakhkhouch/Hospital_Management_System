using System;
using System.Collections.Generic;


namespace S.G.H.Models
{
    public partial class Chambre
    {
        public int Id { get; set; }

        public int Nombre { get; set; }

        public string Type { get; set; }

        public string Statu { get; set; }

        // dik ? li kayna 9bal men int hiya li kat7adad bali rah dak patient rah optionel
        // machi obligatoire chambre tkoun 3andha chi patient
        public int? PatientMatricule { get; set; }

        //-----------------------------------------------------------
        public virtual Patient Patient { get; set; }
    }
}
