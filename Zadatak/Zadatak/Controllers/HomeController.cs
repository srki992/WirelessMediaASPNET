using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    public class HomeController : Controller
    {
        WirelessZadatakEntities db = new WirelessZadatakEntities();
        public ActionResult Index()
        {
            //List<tblArtikal> ArtList = db.tblArtikals.ToList();

            return View();
        }

        public JsonResult GetArtikalList()
        {
            List<ArtikalViewModel> ArtList = db.tblArtikals.Where(x => x.IsDeleted == false).Select(x => new ArtikalViewModel
            {
                ArtikalID = x.ArtikalID,
                ArtikalNaziv = x.ArtikalNaziv,
                Opis = x.Opis,
                Kategorija = x.Kategorija,
                Proizvodjac = x.Proizvodjac,
                Dobavljac = x.Dobavljac,
                Cena = x.Cena

            }).ToList();

            return Json(ArtList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArtikalById(int ArtikalID)
        {
            tblArtikal model = db.tblArtikals.Where(x => x.ArtikalID == ArtikalID).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(ArtikalViewModel model)
        {
            var result = false;
            try
            {
                if (model.ArtikalID > 0)
                {
                    tblArtikal art = db.tblArtikals.SingleOrDefault(x => x.IsDeleted == false && x.ArtikalID == model.ArtikalID);
                    art.ArtikalID = model.ArtikalID;
                    art.ArtikalNaziv = model.ArtikalNaziv;
                    art.Opis = model.Opis;
                    art.Kategorija = model.Kategorija;
                    art.Proizvodjac = model.Proizvodjac;
                    art.Dobavljac = model.Dobavljac;
                    art.Cena = model.Cena;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    tblArtikal art = new tblArtikal();
                    art.ArtikalID = model.ArtikalID;
                    art.ArtikalNaziv = model.ArtikalNaziv;
                    art.Opis = model.Opis;
                    art.Kategorija = model.Kategorija;
                    art.Proizvodjac = model.Proizvodjac;
                    art.Cena = model.Cena;
                    art.Dobavljac = model.Dobavljac;
                    art.IsDeleted = false;
                    db.tblArtikals.Add(art);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteArtikalRecord(int ArtikalID)
        {
            bool result = false;

            tblArtikal Art = db.tblArtikals.SingleOrDefault(x => x.IsDeleted == false && x.ArtikalID == ArtikalID);
            if (Art != null)
            {
                Art.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}