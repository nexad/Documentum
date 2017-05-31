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

            DataGridViewRow selectedRow = null;
            if (metroGridRazredi.SelectedRows.Count > 0)
            {
                selectedRow = metroGridRazredi.SelectedRows[0];
            }
            if (selectedRow == null)
                return;

            int razredId = Convert.ToInt32(selectedRow.Cells["Id"].Value.ToString());
            
            if (razredId != 0)
            {
                this.RazredId = razredId;

                string oznaka = selectedRow.Cells["oznaka"].Value.ToString();
                mlRazredNaziv.Text = oznaka;
                oznaka = System.Text.RegularExpressions.Regex.Replace(oznaka, @"[^\w\d]", string.Empty);
                mtbImportFileName.Text = DocumentumFactory.ResolveDirectoryPath("IMPORT_EXCEL_FOLDER") + oznaka+"-"+ DocumentumFactory.ReadConfigParam("IMPORT_FILE_NAME");

                using (var context = new documentumEntities())
                {
                    var ucenici = from Ucenik in context.Uceniks.Where(u => u.razredId == razredId)
                                  select new
                                  {
                                      Ucenik.Id,
                                      Ucenik.prezime,
                                      Ucenik.ime,
                                      Ucenik.brojMaticneKnjige,
                                      Ucenik.imeRoditelja,
                                      Ucenik.datum_rodjenja,
                                      Ucenik.mestoRodjenja,
                                      Ucenik.opstina,
                                      Ucenik.drzava
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
                                        DateTime myDate = DateTime.ParseExact(datumRodjenja, "dd.mm.yyyy",
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
                                        break;
                                    case "K":
                                        ucenik.drzava = cellValue;
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
            using (var context = new documentumEntities())
            {
                var classIdParameter = new SqlParameter("@ClassId", this.RazredId);

                var result = context.Database
                    .SqlQuery<StandardExecutionResult>("SinhronizeStudentsSubjects @ClassId", classIdParameter)
                    .ToDictionary(t => t.ErrCode, t => t.ErrMessage);
                context.SaveChanges();
            }
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string fileName = mtbImportFileName.Text.ToString();
            ImportFileUcenici(fileName);
            ReloadGridUceniciData();
            ReloadGridDokumenta();
        }

        private void metroGridRazredi_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ReloadGridUceniciData();
            ReloadGridDokumenta();
        }

        private void metroGridUcenici_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = null;
            if (metroGridUcenici.SelectedRows.Count > 0)
            {
                selectedRow = metroGridUcenici.SelectedRows[0];
            }
            if (selectedRow == null)
                return;

            int ucenikId = Convert.ToInt32(selectedRow.Cells["Id"].Value.ToString());

            if (ucenikId != 0)
            {
                FormDetaljiUcenika formDetaljiUcenika = new FormDetaljiUcenika();
                formDetaljiUcenika.UcenikId = ucenikId;
                
                formDetaljiUcenika.ShowDialog();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string fileName = mtbImportFileName.Text.ToString();
            ImportFileOcene(fileName);
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
                                        SmerGodinaDokument.DokumentTip.outputPath
                                    };
                    metroGridDokumenta.DataSource = dokumenta.ToList();
                    if (dokumenta.Count() > 0)
                        metroGridDokumenta.Columns[0].Visible = false;
                }
            }

        }

        private void mbPreview_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = null;
            if (metroGridUcenici.SelectedRows.Count > 0)
            {
                selectedRow = metroGridUcenici.SelectedRows[0];
            }
            if (selectedRow == null)
                return;
            int ucenikId = Convert.ToInt32(selectedRow.Cells["Id"].Value.ToString());

            selectedRow = null;
            if (metroGridDokumenta.SelectedRows.Count > 0)
            {
                selectedRow = metroGridDokumenta.SelectedRows[0];
            }
            if (selectedRow == null)
                return;
            int documentTipId = Convert.ToInt32(selectedRow.Cells["DokumentTipId"].Value.ToString());

            string docOutputPath = "";
            docOutputPath = DocumentumFactory.GenerateDocument(ucenikId, documentTipId, true);
            if (File.Exists(docOutputPath))
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                wordApp.Visible = true;
                Microsoft.Office.Interop.Word.Document docPrint = wordApp.Documents.Add(docOutputPath);
            }

        }

        
    }

}
