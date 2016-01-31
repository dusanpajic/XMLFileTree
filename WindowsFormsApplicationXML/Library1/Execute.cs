using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;



namespace Library1
{


    public class Execute
    {

        //xml tree
        public static XElement CreateFileSystemXmlTree(string source)
        {
            DirectoryInfo di = new DirectoryInfo(source);

            XElement xmltree = new XElement("Directory",
                new XAttribute("FolderName", di.Name),
                new XAttribute("NumberOfFiles", di.GetFiles().Count()),
                from d in Directory.GetDirectories(source)

                select CreateFileSystemXmlTree(d),
                from fi in di.GetFiles()
                select new XElement("File",
                    new XElement("FileName", fi.Name),
                    new XElement("FileSize", fi.Length)));

            return xmltree;
        }

        public static long TotalFIleSize(string source)
        {
            long totalFileSize =
                (from f in CreateFileSystemXmlTree(source).Descendants("File")
                 select (long)f.Element("FileSize")).Sum();

            return totalFileSize;
        }



        // creates folder, .xml file, and inserts data
        public static bool CreateXmlFile(string root, XElement fileSystemTree)
        {


            string pathString = System.IO.Path.Combine(root, "XMLFolder");

            System.IO.Directory.CreateDirectory(pathString);

            string fileName = "XMLFile.xml";
            // Using Combine to add the file name to the path.
            pathString = System.IO.Path.Combine(pathString, fileName);


            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {

                    fileSystemTree.Save(fs);
                    return true;
                }
            }
            else
            {
                return false;
            }


        }



    }




}




