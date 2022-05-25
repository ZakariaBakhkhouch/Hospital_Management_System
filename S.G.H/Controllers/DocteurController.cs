using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S.G.H.Models;
using S.G.H.Models.Repositories;
using S.G.H.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace S.G.H.Controllers
{
    [Authorize]
    public class DocteurController : Controller
    {
        public readonly IDocteurRepository<Docteur> _docteurRepository;
        public readonly IHostingEnvironment _hosting;


        public DocteurController(IDocteurRepository<Docteur> docteurRepository,IHostingEnvironment hosting)
        {
            _docteurRepository = docteurRepository;
            _hosting = hosting; 
        }

        
        [HttpGet]
        public ActionResult Index()
        {
            IList<Docteur> docteurs = _docteurRepository.GetDocteursList();
            return View("Index",docteurs);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientDocteurViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFile(model.File) ?? string.Empty;

                    Docteur docteur = new Docteur()
                    {
                        Matricule = model.Matricule,
                        FullName = model.FullName,
                        Genre = model.Genre,
                        Spécialité = model.Spécialité,
                        Photo = fileName,

                    };
                    _docteurRepository.Add(docteur);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(_docteurRepository.GetDocteursList());
        }

       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Docteur docteur = _docteurRepository.Find(id);

            PatientDocteurViewModel viewmodel = new PatientDocteurViewModel
            {
                Matricule = docteur.Matricule,
                FullName = docteur.FullName,
                Genre = docteur.Genre,
                Spécialité = docteur.Spécialité,
                Photo = docteur.Photo,
            };
            TempData["file"] = docteur.Photo;
            return View("Edit",viewmodel);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientDocteurViewModel model,int id)
        {
            try
            {
                
                string fileName;
                if (model.File == null)
                {
                    var photo = TempData["file"];
                    fileName = photo.ToString();
                }
                else
                {
                    fileName = UploadFile(model.File);
                }
                Docteur docteur = new Docteur()
                {
                    Matricule = model.Matricule,
                    FullName = model.FullName,
                    Genre = model.Genre,
                    Spécialité = model.Spécialité,
                    Photo = fileName,
                };

                _docteurRepository.Update(docteur, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // daba rah khadama ghir howa dik taswira makatam7ach .

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Docteur docteur = _docteurRepository.Find(id);
            return View("Delete",docteur);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _docteurRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        [HttpPost]
        public IActionResult Search(string fullname)
        {
            var docteur = _docteurRepository.Search(fullname);
            return View("Index",docteur);
        }



        string UploadFile(IFormFile file)
        {
            string uploads = Path.Combine(_hosting.WebRootPath, "Uploads");
            string fullPath = Path.Combine(uploads, file.FileName);
            file.CopyTo(new FileStream(fullPath, FileMode.Create));

            return file.FileName;
        }



        string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }

            return imageUrl;
        }
    }
}
