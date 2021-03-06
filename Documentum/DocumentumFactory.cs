﻿using DocumentFormat.OpenXml.Packaging;
using MetroFramework.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using System.Configuration;

namespace Documentum
{

    class DocumentumFactory
    {
        public static IDictionary marks = new Dictionary<int, string>() { { 0, "Неоцењен" },{1, "Недовољан" }, {2, "Довољан"}, {3, "Добар"}, {4, "Врло добар"}, {5, "Одличан"}};

        private static Nastavnik login;

        public static Nastavnik Login { get => login; set => login = value; }


        public static void InitializeApplication()
        {
            

           
            Directory.CreateDirectory(ResolveDirectoryPath("TEMPLATE_FOLDER"));
                
            Directory.CreateDirectory(ResolveDirectoryPath("OUTPUT_FOLDER"));
                
            Directory.CreateDirectory(ResolveDirectoryPath("TEMP_FOLDER"));
                
            Directory.CreateDirectory(ResolveDirectoryPath("IMPORT_EXCEL_FOLDER"));
            
        }

        public static string ReadConfigParam(string paramName)
        {
            using (var context = new documentumEntities())
            {
                return context.Configs.SingleOrDefault(c => c.param_name == paramName).param_value;
            }
        }

        public static string ResolveDirectoryPath(string directoryKey)
        {
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = "";
            path = ReadConfigParam(directoryKey);
            path = userProfilePath + "\\Documentum" + path+"\\";
            return path;
        }

        public static string GenerateDocument(Ucenik ucenik, int documentTipId, bool preview)
        {
            string docOutputPath;
            using (var context = new documentumEntities())
            {
                var classIdParameter = new SqlParameter("@ClassId", -1);
                var documentTypeIdParameter = new SqlParameter("@DocumentTypeId", documentTipId);
                var studentIdParameter = new SqlParameter("@StudentId", ucenik.Id);

                DokumentTip dokumentTip = context.DokumentTips.SingleOrDefault(d => d.Id == documentTipId);

                var result = context.Database
                    .SqlQuery<BookmarkResult>(dokumentTip.dataSP + " @DocumentTypeId, @ClassId, @StudentId", documentTypeIdParameter, classIdParameter, studentIdParameter)
                    .ToDictionary(t => t.BookmarkName, t => t.BookmarkValue);
                XElement el = new XElement("root",
                    result.Select(kv => new XElement(kv.Key, kv.Value)));
                string docTemplatePath = "";
                //get path to template and instance output
                if (preview)
                {
                    docTemplatePath = ResolveDirectoryPath("TEMPLATE_FOLDER") + dokumentTip.previewTemplatePath;
                    docOutputPath = String.Format(ResolveDirectoryPath("TEMP_FOLDER") + dokumentTip.outputPath, ucenik.Id, documentTipId, ucenik.prezime+ucenik.ime);
                } else
                {
                    docTemplatePath = ResolveDirectoryPath("TEMPLATE_FOLDER") + dokumentTip.templatePath;
                    docOutputPath = String.Format(ResolveDirectoryPath("OUTPUT_FOLDER") + dokumentTip.outputPath, ucenik.Id, documentTipId, ucenik.prezime + ucenik.ime);
                }
                //create copy of template so that we don't overwrite it
                try
                {
                    if (File.Exists(docOutputPath))
                    {
                        File.Delete(docOutputPath);
                    }

                }
                catch (Exception ex)
                {

                }
                File.Copy(docTemplatePath, docOutputPath);

                Console.WriteLine("Created copy of template ...");

                //stand up object that reads the Word doc package
                using (WordprocessingDocument doc = WordprocessingDocument.Open(docOutputPath, true))
                {

                    MainDocumentPart main = doc.MainDocumentPart;
                    main.DeleteParts<CustomXmlPart>(main.CustomXmlParts);

                    //add and write new XML part
                    CustomXmlPart customXml = main.AddCustomXmlPart(CustomXmlPartType.CustomXml);
                    using (StreamWriter ts = new StreamWriter(customXml.GetStream()))
                    {

                        ts.Write(el);
                        ts.Flush();
                        ts.Close();
                    }

                    doc.Package.Flush();
                    //closing WordprocessingDocument automatically saves the document
                }

            }

            return docOutputPath;
        }

        public static int GetSelectedGridId(MetroGrid metroGrid, string field = "Id")
        {

            DataGridViewRow selectedRow = null;
            if (metroGrid.SelectedRows.Count > 0)
            {
                selectedRow = metroGrid.SelectedRows[0];
            }
            if (selectedRow == null)
                return -1;
            int selectedId = Convert.ToInt32(selectedRow.Cells[field].Value.ToString());
            return selectedId;
        }

        public static object GetSelectedGridCellValue(MetroGrid metroGrid, string field = "Id")
        {

            DataGridViewRow selectedRow = null;
            if (metroGrid.SelectedRows.Count > 0)
            {
                selectedRow = metroGrid.SelectedRows[0];
            }
            if (selectedRow == null)
                return "";
            var selected = "";
            try
            {
                selected = selectedRow.Cells[field].Value.ToString();
            } catch (Exception ex)
            {
                selected = "";
            }
            
            return selected;
        }
    }
}
