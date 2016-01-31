using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Library1;
using System.Xml.Linq;


namespace WindowsFormsApplicationXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {


            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                string folderPath = FBD.SelectedPath.ToString();

                XElement xmltree1 = Execute.CreateFileSystemXmlTree(folderPath);
                long s = Execute.TotalFIleSize(folderPath);
                Execute.CreateXmlFile(folderPath, xmltree1);

                label2.Text = "Total size: " + s.ToString() + " bytes";
                label2.Visible = true;

                string filePath = folderPath + @"\XMLFolder\XmlFajl.xml";
                dataSet1.ReadXml(filePath);
                dataGridView1.DataSource = dataSet1;
                dataGridView1.DataMember = "Dir";

            }

        }


    }
}
