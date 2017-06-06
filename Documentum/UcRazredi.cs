using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using System.Xml.Linq;

using System.Configuration;

namespace Documentum
{
    public partial class UcRazredi : MetroFramework.Controls.MetroUserControl
    {
        private int _smerId;
        private int _razredId;

        public int SmerId { get => _smerId; set => _smerId = value; }
        public int RazredId { get => _razredId; set => _razredId = value; }

        public UcRazredi()
        {
            InitializeComponent();
        }

        public UcRazredi(int smerId)
        {
            
            InitializeComponent();
            _smerId = smerId;


        }

        private void UcRazredi_Load(object sender, EventArgs e)
        {
            using (var context = new documentumEntities())
            {
                mcbSmer.DataSource = context.Smers.ToList();
                mcbSmer.DisplayMember = "naziv";
                mcbSmer.ValueMember = "Id";
            }

            ReloadGridRazrediData();

            ReloadGridUceniciData();

            ReloadGridDokumenta();
        }

        private void ReloadGridRazrediData()
        {

            int smerId = 0;
            if (_smerId != 0)
            {
                smerId = _smerId;
            } else
            try
            {
                smerId = Convert.ToInt32(mcbSmer.SelectedValue.ToString());
            }
            catch
            {
                smerId = 0;
            }

            if (smerId != 0)
            {
                using (var context = new documentumEntities())
                {
                    var razredi = from Razred in context.Razreds.Where(r => r.SmerGodina.Smer.Id == smerId)
                                   select new
                                   {
                                       Razred.Id,
                                       Razred.oznaka,
                                       Razredni_staresina = Razred.Nastavnik.prezime + " "+ Razred.Nastavnik.ime
                                   };
                    metroGridRazredi.DataSource = razredi.ToList();
                    if (razredi.Count() > 0)
                        metroGridRazredi.Columns[0].Visible = false;
                }
            }
        }

        private void ReloadGridUceniciData()
        {
            
            int razredId = DocumentumFactory.GetSelectedGridId(metroGridRazredi);

            if (razredId != -1)
            {
                this.RazredId = razredId;
                using (var context = new documentumEntities())
                {
                    Razred razred = context.Razreds.SingleOrDefault(r => r.Id == razredId);
                     
                    mlRazredNaziv.Text = razred.oznaka;
                    string oznaka = System.Text.RegularExpressions.Regex.Replace(razred.oznaka, @"[^\w\d]", string.Empty);
                    mtbImportFileName.Text = String.Format(DocumentumFactory.ResolveDirectoryPath("IMPORT_EXCEL_FOLDER") + DocumentumFactory.ReadConfigParam("IMPORT_FILE_NAME"),oznaka);
                    
                    var ucenici = from Ucenik in context.Uceniks.Where(u => u.razredId == razredId)
                                  select new
                                  {
                                      Ucenik.Id,
                                      Ucenik.redniBroj,
                                      Ucenik.prezime,
                                      Ucenik.ime,
                                      Ucenik.brojMaticneKnjige,
                                      Ucenik.imeRoditelja,
                                      Ucenik.datum_rodjenja,
                                      neuneteOcene = Ucenik.UcenikOcenas.Where(uo => uo.ocena == -1).Count(),
                                      brojNekreiranih = Ucenik.UcenikDokuments.Where(d=> d.status == (int) StudentDocumentStatus.Nonexistent).Count(),
                                      brojKreiranih = Ucenik.UcenikDokuments.Where(d => d.status == (int)StudentDocumentStatus.Created).Count(),
                                      brojStampanih = Ucenik.UcenikDokuments.Where(d => d.status == (int)StudentDocumentStatus.Printed).Count(),

                                  };
                    metroGridUcenici.DataSource = ucenici.ToList();
                    if (ucenici.Count() > 0)
                        metroGridUcenici.Columns[0].Visible = false;

                }
            }
        }

        private void mcbSmer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _smerId = 0;
            ReloadGridRazrediData();
            ReloadGridDokumenta();
        }

        private void metroGridRazredi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void metroTextBox1_ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "Excel files|*.xlsx";
            theDialog.InitialDirectory = @"C:\Temp\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(theDialog.FileName.ToString());
            }
            mtbImportFileName.Text = theDialog.FileName.ToString();
        }

        private void ImportFileUcenici(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show(String.Format("Ne postoji fajl {0} ", fileName), "Greska prilikom ucitavanja fajla",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int stepStatus = 5;
            int studentsCount = 0;
            ProgressForm progressForm = new ProgressForm();

            progressForm.Show();
            progressForm.SetProgress(stepStatus, "Brisanje liste studenata...");
            
            using (var context = new documentumEntities())
            {
                var classIdParameter = new SqlParameter("@ClassId", this.RazredId);

                var result = context.Database
                    .SqlQuery<StandardExecutionResult>("DeleteStudents @ClassId", classIdParameter)
                    .ToDictionary(t => t.ErrCode, t=> t.ErrMessage);
                context.SaveChanges();
            }
            stepStatus += 5;
            progressForm.SetProgress(stepStatus, "Ucitavanje liste ucenika iz excel dokumenta...");
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringTable sst = sstpart.SharedStringTable;

                    //WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    //Worksheet sheet = worksheetPart.Worksheet;

                    string sheetName = "подаци о ученицима";
                    
                    string relId = workbookPart.Workbook.Descendants<Sheet>().First(s => sheetName.Equals(s.Name)).Id;
                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(relId);
                    Worksheet sheet = worksheetPart.Worksheet;

                    var cells = sheet.Descendants<Cell>();
                    var rows = sheet.Descendants<Row>();

                    Console.WriteLine("Row count = {0}", rows.LongCount());
                    Console.WriteLine("Cell count = {0}", cells.LongCount());

                    long rowCount = rows.LongCount();
                    
                    using (var context = new documentumEntities())
                    {
                        foreach (Row row in rows)
                        {
                            bool newUcenik = true;
                            int rowNumber = int.Parse(row.RowIndex.ToString());

                            if (rowNumber == 1)
                                continue;

                            
                            Ucenik ucenik = null;
                            string datumRodjenja = "";
                            int redniBroj = 0;
                            foreach (Cell c in row.Elements<Cell>())
                            {
                                if (!newUcenik)
                                    continue;
                                string cellReference = c.CellReference.ToString();
                                string columnReference = System.Text.RegularExpressions.Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);
                                string cellValue = "";
                                if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
                                {
                                    int ssid = int.Parse(c.CellValue.Text);
                                    cellValue = sst.ChildElements[ssid].InnerText;
                                    Console.WriteLine("Shared string {0}: {1}", ssid, cellValue);

                                }
                                else if (c.CellValue != null)
                                {
                                    cellValue = c.CellValue.Text.ToString();
                                    Console.WriteLine("Cell contents: {0}", c.CellValue.Text);
                                }
                                if (columnReference == "B")
                                    {
                                        if (cellValue.Equals("0") || cellValue.Trim().Equals("")) 
                                        {
                                            newUcenik = false;
                                            continue;
                                        }
                                        else
                                        {
                                            newUcenik = true;
                                            ucenik = new Ucenik();
                                    }
                                    }

                                switch (columnReference)
                                {
                                    case "A":
                                        redniBroj = int.Parse(cellValue);
                                        break;
                                    case "B":
                                        ucenik.razredId = this.RazredId;
                                        ucenik.redniBroj = redniBroj;
                                        ucenik.prezime = cellValue;
                                        break;
                                    case "C":
                                        ucenik.ime = cellValue;
                                        break;
                                    case "D":
                                        ucenik.brojMaticneKnjige = cellValue;
                                        break;
                                    case "E":
                                        ucenik.imeRoditelja = cellValue;
                                        break;
                                    case "F":
                                        datumRodjenja = cellValue;
                                        break;
                                    case "G":
                                        datumRodjenja = datumRodjenja + cellValue;
                                        DateTime myDate = DateTime.ParseExact(datumRodjenja, "dd.MM.yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                                        ucenik.datum_rodjenja = myDate;
                                        break;
                                    case "H":
                                        ucenik.mestoRodjenja = cellValue;
                                        break;
                                    case "I":
                                        ucenik.opstina = cellValue;
                                        break;
                                    case "J":
                                        ucenik.drzava = cellValue;
                                        break;
                                    case "K":
                                        break;
                                    case "L":
                                        ucenik.kojiput = cellValue;
                                        break;
                                    case "M":
                                        ucenik.delovodniBroj = cellValue;
                                        break;
                                    default:
                                        break;


                                }

                            }

                            if (newUcenik)
                            {
                                context.Uceniks.Add(ucenik);
                                context.SaveChanges();
                                UcenikGrupa ucenikGrupa = new UcenikGrupa();
                                ucenikGrupa.ucenikId = ucenik.Id;
                                ucenikGrupa.grupaId = context.Grupas.SingleOrDefault(s => s.naziv == "Подразумевано").Id;
                                
                                context.UcenikGrupas.Add(ucenikGrupa);
                                studentsCount += 1;
                                stepStatus += (int) (50/rowCount);
                                progressForm.SetProgress(stepStatus, String.Format("Ucitavanje liste ucenika iz excel dokumenta...{0}", studentsCount));



                            }
                        }
                        context.SaveChanges();
                    }
                }
            }
            Thread.Sleep(1000);
            progressForm.SetProgress(stepStatus, "Azuriranje liste predmeta za ucenike...");

            Sync();

            progressForm.Close();
        }

        private void ImportFileOcene(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show(String.Format("Ne postoji fajl {0} ", fileName), "Greska prilikom ucitavanja fajla",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int stepStatus = 5;
            int studentsCount = 0;
            ProgressForm progressForm = new ProgressForm();

            progressForm.Show();
            progressForm.SetProgress(stepStatus, "Brisanje liste ocena...");
            
            if (!File.Exists(fileName))
            {
                return;
            }

            using (var context = new documentumEntities())
            {
                var classIdParameter = new SqlParameter("@ClassId", this.RazredId);

                var result = context.Database
                    .SqlQuery<StandardExecutionResult>("DeleteMarks @ClassId", classIdParameter)
                    .ToDictionary(t => t.ErrCode, t => t.ErrMessage);
                context.SaveChanges();
            }
            stepStatus += 5;
            progressForm.SetProgress(stepStatus, "Ucitavanje ocena iz excel dokumenta...");
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringTable sst = sstpart.SharedStringTable;

                    string sheetName = "оцене ученика";

                    string relId = workbookPart.Workbook.Descendants<Sheet>().First(s => sheetName.Equals(s.Name)).Id;
                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(relId);
                    Worksheet sheet = worksheetPart.Worksheet;

                    var cells = sheet.Descendants<Cell>();
                    var rows = sheet.Descendants<Row>();

                    Console.WriteLine("Row count = {0}", rows.LongCount());
                    Console.WriteLine("Cell count = {0}", cells.LongCount());

                    long rowCount = rows.LongCount();
                    IDictionary subjects = new Dictionary<string, string>();
                    
                    using (var context = new documentumEntities())
                    {
                        foreach (Row row in rows)
                        {
                            bool newUcenik = true;
                            int rowNumber = int.Parse(row.RowIndex.ToString());

                            if (rowNumber == 1)
                                continue;


                            Ucenik ucenik = null;
                            
                            int redniBroj = 0;
                            string imeUcenika = "";
                            string prezimeUcenika = "";
                            foreach (Cell c in row.Elements<Cell>())
                            {
                                if (!newUcenik)
                                    continue;
                                string cellReference = c.CellReference.ToString().Trim();
                                string columnReference = System.Text.RegularExpressions.Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);
                                string cellValue = "";
                                if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
                                {
                                    int ssid = int.Parse(c.CellValue.Text);
                                    cellValue = sst.ChildElements[ssid].InnerText.Trim();
                                    Console.WriteLine("Shared string {0}: {1}", ssid, cellValue);

                                }
                                else if (c.CellValue != null)
                                {
                                    cellValue = c.CellValue.Text.ToString().Trim() ;
                                    Console.WriteLine("Cell contents: {0}", c.CellValue.Text);
                                }
                                if (columnReference == "B")
                                {
                                    if ((rowNumber>=3)&&(cellValue.Equals("0") || cellValue.Trim().Equals("")))
                                    {
                                        newUcenik = false;
                                        continue;
                                    }
                                    else
                                    {
                                        newUcenik = true;
                                      
                                    }
                                }
                                if (!newUcenik)
                                    continue;
                                switch (columnReference)
                                {
                                    case "A":
                                        if (rowNumber>=3 && !cellValue.Equals(""))
                                            redniBroj = int.Parse(cellValue);
                                        break;
                                    case "B":
                                        if (rowNumber >= 3)
                                            prezimeUcenika = cellValue;
                                        break;
                                    case "C":
                                        if (rowNumber >= 3)
                                        {
                                            imeUcenika = cellValue;
                                            ucenik = context.Uceniks.SingleOrDefault(u => u.razredId == RazredId && u.redniBroj == redniBroj && u.ime.ToUpper().Trim() == imeUcenika.ToUpper().Trim());
                                        }
                                        break;
                                    case "D":
                                    case "E":
                                    case "F":
                                    case "G":
                                    case "H":
                                    case "I":
                                    case "J":
                                    case "K":
                                    case "L":
                                    case "M":
                                    case "N":
                                    case "O":
                                    case "P":
                                    case "Q":
                                    case "R":
                                    case "S":
                                    case "T":
                                    case "U":
                                    case "V":
                                    case "W"://versko
                                    case "X"://gradjansko
                                    case "Y"://vladanje
                                        if (rowNumber == 2)
                                        {
                                            subjects.Add(columnReference, cellValue.Trim().ToUpper());
                                        }
                                        else
                                        {
                                            string subject = subjects[columnReference].ToString();
                                            if (subject.Equals(""))
                                                continue;
                                            int smerGodinaId = (int)ucenik.Razred.smerGodinaId;
                                            SmerGodinaPredmet smerGodinaPredmet = context.SmerGodinaPredmets.SingleOrDefault(s => s.smerGodinaId == smerGodinaId && s.Predmet.naziv.ToUpper().Equals(subject));
                                            if (smerGodinaPredmet == null)
                                                smerGodinaPredmet = context.SmerGodinaPredmets.SingleOrDefault(s => s.smerGodinaId == smerGodinaId && s.Predmet.naziv.ToUpper().StartsWith(subject));
                                            if (smerGodinaPredmet != null)
                                            {

                                                var ucenikOcena = context.UcenikOcenas.SingleOrDefault(u => u.ucenikId == ucenik.Id && u.smerGodinaPredmetId == smerGodinaPredmet.Id);
                                                
                                                if (ucenikOcena != null) { 
                                                    if (!(new[] { "W", "X" }.Contains(columnReference)))
                                                    {
                                                        int grupa = (int) ucenikOcena.SmerGodinaPredmet.grupaId;
                                                        if  (grupa > 0)
                                                        {
                                                            UpdateUcenikGrupa(context, ucenik, ucenikOcena);
                                                        }
                                                        ucenikOcena.ocena = int.Parse(cellValue);
                                                        ucenikOcena.ocenaOpis = DocumentumFactory.marks[ucenikOcena.ocena].ToString();
                                                    }
                                                    else
                                                    {
                                                        if (!cellValue.Equals(""))
                                                        {
                                                            UpdateUcenikGrupa(context, ucenik, ucenikOcena);

                                                            ucenikOcena.ocena = 0;
                                                            ucenikOcena.ocenaOpis = cellValue;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    default:
                                        break;

                                }

                            }

                            if (newUcenik)
                            {
                                
                                studentsCount += 1;
                                stepStatus += (int)(50 / rowCount);
                                progressForm.SetProgress(stepStatus, String.Format("Ucitavanje liste ocena iz excel dokumenta...{0}", studentsCount));
                            }
                        }
                        context.SaveChanges();

                        var classIdParameter = new SqlParameter("@ClassId", this.RazredId);

                        var result = context.Database
                            .SqlQuery<StandardExecutionResult>("SinhronizeStudentsGroups @ClassId", classIdParameter)
                            .ToDictionary(t => t.ErrCode, t => t.ErrMessage);



                        context.SaveChanges();
                    }
                }
            }
            Thread.Sleep(1000);
            progressForm.Close();
        }

        private static void UpdateUcenikGrupa(documentumEntities context, Ucenik ucenik, UcenikOcena ucenikOcena)
        {
            UcenikGrupa ucenikGrupa = context.UcenikGrupas.SingleOrDefault(u => u.ucenikId == ucenik.Id && u.grupaId == (int)ucenikOcena.SmerGodinaPredmet.grupaId);
            if (ucenikGrupa == null)
            {
                ucenikGrupa = new UcenikGrupa();
                ucenikGrupa.ucenikId = ucenik.Id;
                ucenikGrupa.grupaId = (int)ucenikOcena.SmerGodinaPredmet.grupaId;
                context.UcenikGrupas.Add(ucenikGrupa);
            }
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            string fileName = mtbImportFileName.Text.ToString();
            ImportFileUcenici(fileName);
            ReloadGridUceniciData();
            ReloadGridDokumenta();
        }

        private void MetroGridRazredi_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void MetroGridUcenici_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            

            int ucenikId = DocumentumFactory.GetSelectedGridId(metroGridUcenici);

            if (ucenikId != -1)
            {
                FormDetaljiUcenika formDetaljiUcenika = new FormDetaljiUcenika();
                formDetaljiUcenika.UcenikId = ucenikId;
                
                formDetaljiUcenika.ShowDialog();
            }
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            string fileName = mtbImportFileName.Text.ToString();
            ImportFileOcene(fileName);
            ReloadGridUceniciData();
        }

        private void ReloadGridDokumenta()
        {
            int smerGodinaId = 0;
            try
            {
                using (var context = new documentumEntities())
                {
                    smerGodinaId = (int) context.Razreds.SingleOrDefault(r => r.Id == RazredId).smerGodinaId;
                }
            }
            catch
            {
                smerGodinaId = 0;
            }
            if (smerGodinaId != 0)
            {
                using (var context = new documentumEntities())
                {
                    var dokumenta = from SmerGodinaDokument in context.SmerGodinaDokuments.Where(s => s.smerGodinaId == smerGodinaId)
                                    select new
                                    {
                                        SmerGodinaDokument.Id,
                                        DokumentTipId = SmerGodinaDokument.dokumentTipId,
                                        SmerGodinaDokument.DokumentTip.naziv,
                                        SmerGodinaDokument.DokumentTip.templatePath,
                                        SmerGodinaDokument.DokumentTip.outputPath,
                                        brojNekreiranih = SmerGodinaDokument.DokumentTip.UcenikDokuments.Where(u => u.Ucenik.razredId == RazredId && u.status == 0).Count(),
                                        brojKreiranih = SmerGodinaDokument.DokumentTip.UcenikDokuments.Where(u => u.Ucenik.razredId == RazredId && u.status == 1).Count(),
                                        brojStampanih = SmerGodinaDokument.DokumentTip.UcenikDokuments.Where(u => u.Ucenik.razredId == RazredId && u.status == 2).Count()
                                    };
                    metroGridDokumenta.DataSource = dokumenta.ToList();
                    if (dokumenta.Count() > 0)
                    {
                        metroGridDokumenta.Columns[0].Visible = false;
                        using (var c = metroGridDokumenta.Columns["brojNekreiranih"])
                        {
                            c.HeaderText = "Broj nekreiranih";
                            c.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                          
                        }

                        using (var c = metroGridDokumenta.Columns["brojKreiranih"])
                        {
                            c.HeaderText = "Broj kreiranih";
                            c.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                           
                        }

                        using (var c = metroGridDokumenta.Columns["brojStampanih"])
                        {
                            c.HeaderText = "Broj stampanih";
                            c.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                           
                        }

                    }
                        

                }
            }

        }

        private void MbPreview_Click(object sender, EventArgs e)
        {
            
            int ucenikId = DocumentumFactory.GetSelectedGridId(metroGridUcenici);
            
            int documentTipId = DocumentumFactory.GetSelectedGridId(metroGridDokumenta, "DokumentTipId");

            if (ucenikId == -1 || documentTipId == -1)
                return;
            Ucenik ucenik;
            using (var context = new documentumEntities())
            {
                ucenik = context.Uceniks.SingleOrDefault(u => u.Id == ucenikId);
            }
            
            string docOutputPath = DocumentumFactory.GenerateDocument(ucenik, documentTipId, true);
            if (File.Exists(docOutputPath))
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                wordApp.Visible = true;
                Microsoft.Office.Interop.Word.Document docPrint = wordApp.Documents.Open(docOutputPath);
            }

        }

        private void MbGenerate_Click(object sender, EventArgs e)
        {
            int documentTipId = DocumentumFactory.GetSelectedGridId(metroGridDokumenta, "DokumentTipId");

            using (var context = new documentumEntities())
            {
                
                int ucenikCount = context.Uceniks.Where(u => u.razredId == RazredId && u.UcenikOcenas.Where(uo => uo.ocena == -1).Count() == 0).Count();

                ProgressForm progressForm = new ProgressForm();
                progressForm.Step = (int) 100 / ucenikCount;
                progressForm.Show();
                
                foreach (Ucenik ucenik in context.Uceniks.Where(u=> u.razredId == RazredId && u.UcenikOcenas.Where(uo => uo.ocena == -1).Count() == 0))
                {
                    string docOutputPath = DocumentumFactory.GenerateDocument(ucenik, documentTipId, false);
                    UcenikDokument ucenikDokument = context.UcenikDokuments.SingleOrDefault(d => d.ucenikId == ucenik.Id && d.dokumentTipId == documentTipId);
                    if (ucenikDokument == null)
                    {
                        ucenikDokument = new UcenikDokument()
                        {
                            ucenikId = ucenik.Id,
                            dokumentTipId = documentTipId,
                            dokumentPath = docOutputPath,
                            status = (int) StudentDocumentStatus.Created
                        };
                        context.UcenikDokuments.Add(ucenikDokument);
                    } else
                    {
                        ucenikDokument.dokumentPath = docOutputPath;
                        ucenikDokument.status = (int)StudentDocumentStatus.Created;
                    }
                    progressForm.StepProgress();
                }
                context.SaveChanges();
                progressForm.Close();
            }
            ReloadGridUceniciData();
        }

        private void MbPrint_Click(object sender, EventArgs e)
        {
            int documentTipId = DocumentumFactory.GetSelectedGridId(metroGridDokumenta, "DokumentTipId");

            using (var context = new documentumEntities())
            {

                int ucenikCount = context.UcenikDokuments.Where(ud => ud.dokumentTipId == documentTipId && ud.status == (int)StudentDocumentStatus.Created && ud.Ucenik.razredId == RazredId).Count();

                if (ucenikCount == 0)
                {
                    MessageBox.Show("Ne postoje dokumenta za stampu u odgovarajucem statusu!", "Greska prilikom stampanja",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ProgressForm progressForm = new ProgressForm();
                progressForm.Step = (int)100 / ucenikCount;
                progressForm.Show();

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

                foreach (UcenikDokument ucenikDokument in context.UcenikDokuments.Where(ud => ud.dokumentTipId == documentTipId && ud.status == (int)StudentDocumentStatus.Created && ud.Ucenik.razredId == RazredId))
                {
                    //TODO:print

                    if (File.Exists(ucenikDokument.dokumentPath))
                    {
                        
                        wordApp.Visible = false;
                        Microsoft.Office.Interop.Word.Document docPrint = wordApp.Documents.Open(ucenikDokument.dokumentPath);

                        object oMissing = System.Reflection.Missing.Value;
                         
                        wordApp.ActiveDocument.PrintOut();
                        docPrint.Close(SaveChanges: false);

                        docPrint = null;
                        
                    }

                    ucenikDokument.status = (int)StudentDocumentStatus.Printed;
                    progressForm.StepProgress();
                }
                

                context.SaveChanges();
                progressForm.Close();

                // <EDIT to include Jason's suggestion>
                ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(SaveChanges: false);
                // </EDIT>

                // Original: wordApp.Quit(SaveChanges: false);
                wordApp = null;
            }
            ReloadGridUceniciData();
        }

        private void metroGridUcenici_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.metroGridUcenici.Columns[e.ColumnIndex].Name == "brojNekreiranih")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    int value = int.Parse(e.Value.ToString());
                     
                    if (value > 0)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                    }

                }
            } else 
            if (this.metroGridUcenici.Columns[e.ColumnIndex].Name == "brojKreiranih")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    int value = int.Parse(e.Value.ToString());

                    if (value > 0)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                    }

                }
            }
            else
            if (this.metroGridUcenici.Columns[e.ColumnIndex].Name == "brojStampanih")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    int value = int.Parse(e.Value.ToString());

                    if (value > 0)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Green;
                    }

                }
            }
            else
            if (this.metroGridUcenici.Columns[e.ColumnIndex].Name == "neuneteOcene")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    int value = int.Parse(e.Value.ToString());

                    if (value > 0)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                    }

                }
            }
            
        }

        private void metroGridDokumenta_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (metroGridDokumenta.Columns[e.ColumnIndex].Name == "brojNekreiranih")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    int value = int.Parse(e.Value.ToString());

                    if (value > 0)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                    }

                }
            }
            else
            if (metroGridDokumenta.Columns[e.ColumnIndex].Name == "brojKreiranih")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    int value = int.Parse(e.Value.ToString());

                    if (value > 0)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                    }

                }
            }
            else
            if (metroGridDokumenta.Columns[e.ColumnIndex].Name == "brojStampanih")
            {
                if (e.Value != null)
                {
                    // Check for the string "pink" in the cell.
                    int value = int.Parse(e.Value.ToString());

                    if (value > 0)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Green;
                    }

                }
            }
        }

        private void mbSync_Click(object sender, EventArgs e)
        {
            Sync();
        }

        private void Sync()
        {
            using (var context = new documentumEntities())
            {
                var classIdParameter = new SqlParameter("@ClassId", this.RazredId);

                var result = context.Database
                    .SqlQuery<StandardExecutionResult>("SinhronizeStudentsSubjects @ClassId", classIdParameter)
                    .ToDictionary(t => t.ErrCode, t => t.ErrMessage);

                var classDocsIdParameter = new SqlParameter("@ClassId", this.RazredId);
                result = context.Database
                    .SqlQuery<StandardExecutionResult>("SinhronizeStudentsDocuments @ClassId", classDocsIdParameter)
                    .ToDictionary(t => t.ErrCode, t => t.ErrMessage);

                var classBookmarksIdParameter = new SqlParameter("@ClassId", this.RazredId);
                result = context.Database
                    .SqlQuery<StandardExecutionResult>("SinhronizeStudentsBookmarks @ClassId", classBookmarksIdParameter)
                    .ToDictionary(t => t.ErrCode, t => t.ErrMessage);

                context.SaveChanges();
            }
        }

        private void metroGridRazredi_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ReloadGridUceniciData();
            ReloadGridDokumenta();
        }
    }

}
