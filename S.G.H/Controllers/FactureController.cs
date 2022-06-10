using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S.G.H.Models;
using S.G.H.Models.Repositories;
using S.G.H.ViewModel;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Diagnostics;
using AutoMapper;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;


namespace S.G.H.Controllers
{
    [Authorize]
    public class FactureController : Controller
    {

        private readonly IFactureRepository<Facture> _factureRepository;
        private readonly IPatientRepository<Patient> _patientRepository;


        public FactureController(IFactureRepository<Facture> factureRepository, IPatientRepository<Patient> patientRepository)
        {
            _factureRepository = factureRepository;
            _patientRepository = patientRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var factures = _factureRepository.GetFactures();
            return View("Index", factures);
        }



        [HttpGet]
        public IActionResult Create()
        {
            var model = new PatientFactureViewModel
            {
                Patients = _patientRepository.GetPatientsList().ToList()
            };
            return View("Create", model);
        }



        [HttpPost]
        public IActionResult Create(PatientFactureViewModel model)
        {
            var patient = _patientRepository.Find(model.PatientMatricule);

            if (ModelState.IsValid)
            {
                Facture facture = new Facture()
                {
                    Id = model.Id,
                    Nombre = _factureRepository.GetFactures().Count + 1,
                    Montant = model.Montant,
                    DatePaiement = model.DatePaiement,
                    TypePaiement = model.TypePaiement,
                    DateDépart = model.DateDépart,
                    Patient = patient,
                };

                _factureRepository.Add(facture);
                return RedirectToAction(nameof(Index));
                
            }
            
            return View("Index");
        }

        

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var facture = _factureRepository.Find(id);

            var patient = _patientRepository.Find(facture.PatientMatricule);

            PatientFactureViewModel viewModel = new PatientFactureViewModel
            {
                Id = facture.Id,
                Nombre = facture.Nombre,
                Montant = facture.Montant,
                DatePaiement = facture.DatePaiement,
                TypePaiement = facture.TypePaiement,
                DateDépart = facture.DateDépart,
                PatientMatricule = facture.PatientMatricule,
                Patients = _patientRepository.GetPatientsList().ToList()
            };
            return View("Edit", viewModel);
        }



        [HttpPost]
        public IActionResult Edit(PatientFactureViewModel model)
        {
            var patient = _patientRepository.Find(model.PatientMatricule);

            if (ModelState.IsValid)
            {
                Facture facture = new Facture
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Montant = model.Montant,
                    DatePaiement = model.DatePaiement,
                    TypePaiement = model.TypePaiement,
                    DateDépart = model.DateDépart,
                    Patient = patient,
                };

                _factureRepository.Update(facture,model.Id);
                return RedirectToAction(nameof(Index));
                
            }
            
            return View("Index");
        }



        [HttpGet]
        public IActionResult Telecharger(int id)
        {   
            TempData["id"] = id;

            try
            {
                Facture facture = _factureRepository.Find(id);
                PatientFactureViewModel model = new PatientFactureViewModel
                {
                    Id = facture.Id,
                    Nombre = facture.Nombre,
                    DatePaiement = facture.DatePaiement,
                    TypePaiement = facture.TypePaiement,
                    Montant = facture.Montant,
                    PatientMatricule = facture.Patient.Matricule,
                };
                return View("Telecharger", model);
            }
            catch
            {
                return View();
            }
        }



        [HttpPost]
        public IActionResult Search(string number)
        {
            var result = _factureRepository.Search(number);

            return View("Index", result);
        }



        public FileContentResult TelechargerFacture_Text(int id)
        {
            var facture = _factureRepository.Find(17);
            // this is the name of the file path .
            string filePath = @"C:\Users\zakaria\Desktop\S.G.H\S.G.H\wwwroot\files\Read.txt";
            // this for writing to this file .
            StreamWriter writer = new StreamWriter(filePath);
            // this is the text
            writer.WriteLine("Nom de Patient : " + facture.Patient.Nom + facture.Patient.Prenom);
            writer.WriteLine("Date de Paiement : " + facture.DatePaiement);
            writer.WriteLine("Type de Paiement : " + facture.TypePaiement);
            writer.WriteLine("Montant : " + facture.Montant);

            // this for closing the wriitter .
            writer.Close();
            // Content type .
            string type = "application/txt";
            // this is for downloading the file .
            return File(System.IO.File.ReadAllBytes(filePath), type , "Read.txt");
        }



        public FileStreamResult TelechargerFacture_Pdf()
        {
            var id = TempData["id"];
            var facture = _factureRepository.Find(Convert.ToInt32(id));
            
            //Create PDF Document
            PdfDocument document = new PdfDocument();

            //You will have to add Page in PDF Document
            PdfPage page = document.AddPage();

            //For drawing in PDF Page you will nedd XGraphics Object
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //For Test you will have to define font to be used
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont font1 = new XFont("Verdana", 15, XFontStyle.Regular);

            XPoint xPoint = new XPoint(10, 10);

           
            //Finally use XGraphics & font object to draw text in PDF Page
            gfx.DrawString("La Facture", font, XBrushes.Black,new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString("Nom de Patient : " + facture.Patient.Nom + " " + facture.Patient.Prenom, font1, XBrushes.Black,20,100, XStringFormats.Default);
            gfx.DrawString("Date de paiement : " + facture.DatePaiement, font1, XBrushes.Black, 20, 130, XStringFormats.Default);
            gfx.DrawString("Type de Paiement : " + facture.TypePaiement, font1, XBrushes.Black, 20, 160, XStringFormats.Default);
            gfx.DrawString("Montant Total : "    + facture.Montant  + " DH"   , font1, XBrushes.Black, 20, 190, XStringFormats.Default);

            //Save the PDF document to stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Download the PDF document in the browser
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            // add the file name .
            fileStreamResult.FileDownloadName = "Sample.pdf";

            return fileStreamResult;
        }


        public void TelechargerFacture_Xml()
        {
            
        }
    }
}
