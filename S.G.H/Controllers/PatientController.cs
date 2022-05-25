using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S.G.H.Models;
using S.G.H.Models.Repositories;
using S.G.H.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace S.G.H.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientRepository<Patient> _patientRepository;
        private readonly IDocteurRepository<Docteur> _docteurRepository;


        public PatientController(IPatientRepository<Patient> patientRepository, IDocteurRepository<Docteur> docteurRepository)
        {
            _patientRepository = patientRepository;
            _docteurRepository = docteurRepository;
        }

        
        [HttpGet]
        public ActionResult Index()
        {
            IList<Patient> patients = _patientRepository.GetPatientsList();
            return View("Index",patients);
        }

        
        [HttpGet]
        public ActionResult Details(int id)
        {
            var patient = _patientRepository.Find(id);
            return View("Details",patient);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var model = new PatientDocteurViewModel
            {
                Docteurs = _docteurRepository.GetDocteursList().ToList()
            };
            return View("Create",model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientDocteurViewModel model)
        {
            try
            {
                var docteur = _docteurRepository.Find(model.DocteurMatricule);
                Patient patient = new Patient
                {
                    Matricule = model.Matricule,
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Genre = model.Genre,
                    Adresse = model.Adresse,
                    DateNaissance = model.DateNaissance,
                    Email = model.Email,
                    Telephone = model.Telephone,
                    SonDocteur = docteur
                };
                _patientRepository.Add(patient);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }

        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var patient = _patientRepository.Find(id);
            PatientDocteurViewModel viewmodel;

            // had jouj stora kayakhdo la valeur li kayna deja o ki7atha comme valeur pa default
            //int docteurMatricule = patient.SonDocteur.Matricule; // == null ? patient.SonDocteur.Matricule = 0 : patient.SonDocteur.Matricule;

            var genre = patient.Genre == null ? patient.Genre = "" : patient.Genre;

            if(patient.SonDocteur != null)
            {
                viewmodel = new PatientDocteurViewModel
                {
                    Matricule = patient.Matricule,
                    Nom = patient.Nom,
                    Prenom = patient.Prenom,
                    Adresse = patient.Adresse,
                    DateNaissance = patient.DateNaissance,
                    Email = patient.Email,
                    Genre = genre,
                    Telephone = patient.Telephone,
                    DocteurMatricule = patient.SonDocteur.Matricule,
                    Docteurs = _docteurRepository.GetDocteursList().ToList()
                };
            }
            else
            {
                viewmodel = new PatientDocteurViewModel
                {
                    Matricule = patient.Matricule,
                    Nom = patient.Nom,
                    Prenom = patient.Prenom,
                    Adresse = patient.Adresse,
                    DateNaissance = patient.DateNaissance,
                    Email = patient.Email,
                    Genre = genre,
                    Telephone = patient.Telephone,
                    DocteurMatricule = 0,
                    Docteurs = _docteurRepository.GetDocteursList().ToList()
                };
            }

            return View("Edit",viewmodel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientDocteurViewModel ViewModel)
        {
            try
            {
                var docteur = _docteurRepository.Find(ViewModel.DocteurMatricule);

                Patient patient = new Patient
                {
                    Matricule = ViewModel.Matricule,
                    Nom = ViewModel.Nom,
                    Prenom = ViewModel.Prenom,
                    Adresse = ViewModel.Adresse,
                    DateNaissance = ViewModel.DateNaissance,
                    Email = ViewModel.Email,
                    Genre = ViewModel.Genre,
                    Telephone = ViewModel.Telephone,
                    SonDocteur = docteur
                };

                _patientRepository.Update(patient, ViewModel.Matricule);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Patient patient = _patientRepository.Find(id);
            return View("Delete",patient);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _patientRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Search(string prenom)
        {
            var result = _patientRepository.Search(prenom);

            return View("Index",result);
        }
    }
}