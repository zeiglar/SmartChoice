using FileHelpers;
using Source.Enums;
using Source.Estates;
using Source.Features;
using Source.Webs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.DTO;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        protected string CSVFile { get; set; }

        #region Views
        public ActionResult Index()
        {
            ViewBag.Status = @"Configurate->";
            return View();
        }
        #endregion

        #region Public Funcs
        [HttpPost]
        public ActionResult Initialise(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.SettingFileInfo = "Upload file has no content.";
                return null;
            }

            return Json(GetSettingsInfo(file), JsonRequestBehavior.AllowGet);
        }

        //TODO: udpate with CSVFILE
        public ActionResult CheckCSVFile()
        {
            int error = 0;
            string folder = ConfigurationManager.AppSettings["CSVFileLocation"];
            string file = ConfigurationManager.AppSettings["CSVFileName"];

            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(file))
                error = (int)ErrorCodes.Error_IncorrectAppSetting;

            CSVFile = string.Format("{0}{1}", folder, file);

            try
            {
                if (!System.IO.File.Exists(CSVFile))
                {
                    error = (int)CreateFile(folder, CSVFile);
                }
            }
            catch (Exception)
            {
                error = (int)ErrorCodes.Error_CannotCreateFile;
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSettingsInfo()
        {
            SettingInfo info = new SettingInfo();
            info.Suburbs = new List<Suburb>();

            int part = 0;
            using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings["CSVFile"]))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] temp = line.Split(',');
                    if (temp[0].Equals("-"))
                    {
                        if (temp[1].Equals("Settings"))
                            part = 1;
                        else if (temp[1].Equals("Properties"))
                            part = 2;
                    }
                    else
                        SetSettingInfo(part, temp, info);
                }
            }

            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSuburbs()
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(Suburb));
            return Json(engine.ReadFile(ConfigurationManager.AppSettings["CSVFile"]), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProperties(Target target, Suburb suburb)
        {
            Web web = new Realestate();
            return Json(web.GetProperties(target, suburb));
        }
        #endregion

        #region Private Funcs
        private void SetSettingInfo(int part, string[] temp, SettingInfo info)
        {
            switch (part)
            {
                case 1:
                    int id = 0;
                    int.TryParse(temp[0], out id);
                    decimal value = 0;
                    decimal.TryParse(temp[2], out value);

                    switch (id)
                    {
                        case 1:
                            info.Desposit = value;
                            break;
                        case 2:
                            info.MortgageRate = value;
                            break;
                        case 3:
                            info.Strate = value;
                            break;
                        case 4:
                            info.Water = value;
                            break;
                        case 5:
                            info.CouncilRate = value;
                            break;
                        case 6:
                            info.Rental = value;
                            break;
                        case 7:
                            info.AgentCommission = value;
                            break;
                        case 8:
                            info.LettingFee = value;
                            break;
                    }
                    break;
                case 2:
                    if (temp.Count() == 3)
                        info.Suburbs.Add(new Suburb()
                        {
                            Name = temp[0],
                            State = temp[1],
                            Postcode = temp[2]
                        });
                    break;
                default:
                    break;
            }
        }

        private SettingInfo GetSettingsInfo(HttpPostedFileBase file)
        {
            SettingInfo info = new SettingInfo();
            info.Suburbs = new List<Suburb>();

            int part = 0;
            using (StreamReader reader = new StreamReader(file.InputStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] temp = line.Split(',');
                    if (temp[0].Equals("-"))
                    {
                        if (temp[1].Equals("Settings"))
                            part = 1;
                        else if (temp[1].Equals("Properties"))
                            part = 2;
                    }
                    else
                        SetSettingInfo(part, temp, info);
                }
            }

            return info;
        }

        private List<Suburb> GetSuburbs(HttpPostedFileBase file)
        {
            List<Suburb> suburbs = new List<Suburb>();
            using (StreamReader reader = new StreamReader(file.InputStream))
            {
                string line = reader.ReadLine(); // head
                while ((line = reader.ReadLine()) != null)
                {
                    string[] temp = line.Split(',');
                    if (temp.Count() != 3)
                        suburbs.Add(new Suburb()
                        {
                            Name = temp[0],
                            State = temp[1],
                            Postcode = temp[2]
                        });
                }
            }

            return suburbs;
        }

        private ErrorCodes CreateFile(string folder, string file)
        {
            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(ConfigurationManager.AppSettings["CSVFileHeader"]);
                System.IO.File.WriteAllText(file, sb.ToString());
            }
            catch
            {
                return ErrorCodes.Error_CannotFindFile;
            }

            return ErrorCodes.Default;
        }
        #endregion
    }
}
