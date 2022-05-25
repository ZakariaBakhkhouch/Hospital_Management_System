using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S.G.H.Models;
using S.G.H.Models.Repositories;
using S.G.H.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace S.G.H.Controllers
{
    [Authorize]
    public class ChambreController : Controller
    {

        private readonly IChambreRepository<Chambre> _chambreRepository;
        private readonly IPatientRepository<Patient> _patientRepository;


        public ChambreController(IChambreRepository<Chambre> chambreRepository,IPatientRepository<Patient> patientRepository)
        {
            _chambreRepository = chambreRepository;
            _patientRepository = patientRepository;
        }


        [HttpGet]
        public ActionResult Index()
        {
            List<Chambre> chambres = _chambreRepository.GetChambres();
            return View("Index",chambres);
        }

        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TempData["id"] = id;

            var chambre = _chambreRepository.Find(id);

            var patient = chambre.Patient;

            PatientChambreViewModel viewmodel;

            if (patient != null)
            {
                viewmodel = new PatientChambreViewModel
                {
                    Id = chambre.Id,
                    Nombre = chambre.Nombre,
                    Statu = chambre.Statu,
                    Type = chambre.Type,
                    Matricule = chambre.Patient.Matricule,
                    patients = _patientRepository.GetPatientsList().ToList(),
                };
            }
            else
            {
                viewmodel = new PatientChambreViewModel
                {
                    Id = chambre.Id,
                    Nombre = chambre.Nombre,
                    Statu = chambre.Statu,
                    Type = chambre.Type,
                    Matricule = 0,
                    patients = _patientRepository.GetPatientsList().ToList(),
                };
            }
            

            return View("Edit", viewmodel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientChambreViewModel ViewModel)
        {
            var patient = _patientRepository.Find(ViewModel.Matricule);

            Chambre chambre = new Chambre
            {
                Id = ViewModel.Id,
                Nombre = ViewModel.Nombre,
                Statu = ViewModel.Statu,
                Type = ViewModel.Type,
                Patient = patient
            };
            _chambreRepository.Update(ViewModel.Id, chambre);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vide(PatientChambreViewModel model)
        {
            var id = TempData["id"];

            Chambre chambre = new Chambre
            {
                Id = Convert.ToInt32(id),
                Nombre = model.Nombre,
                Statu = "Disponible",
                Type = model.Type,
                Patient = null
            };

            _chambreRepository.Update(chambre.Id,chambre);

            return RedirectToAction(nameof(Index));
        }
    }
}
