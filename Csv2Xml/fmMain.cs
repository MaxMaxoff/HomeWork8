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
using EditorTrueFalse;

namespace Csv2Xml
{
    public partial class fmMain : Form
    {
        ListOfXMLRecords database = new ListOfXMLRecords();

        public fmMain()
        {
            InitializeComponent();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            tbSource.Text = "";
            tbDestination.Text = "";
        }

        private void tbSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV файлы|*.CSV|Все файлы|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbSource.Text = openFileDialog.FileName;                
            }
        }

        private void tbDestination_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();            
            saveFileDialog.Filter = "XML файлы|*.XML|Все файлы|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbDestination.Text = saveFileDialog.FileName;
                database.FileName = saveFileDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbSource.Text == "")
            {
                MessageBox.Show("Please select Source file!");
                tbSource_Click(new object(), new EventArgs());
            }

            if (tbDestination.Text == "")
            {
                MessageBox.Show("Please select Destination file!");
                tbDestination_Click(new object(), new EventArgs());
            }

            if (tbSource.Text != "" && tbDestination.Text != "")
            {
                FillRecords();
                database.SaveList();
                MessageBox.Show("Done!");
                toolStripProgressBar1.ProgressBar.Value = 0;
            }
            else MessageBox.Show("Files are not specified!");
        }

        private void FillRecords()
        {
            StreamReader sr = null;

            database.Clear();

            try
            {
                toolStripProgressBar1.Maximum = TotalLines(tbSource.Text);

                sr = new StreamReader(tbSource.Text);                
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string[] s = sr.ReadLine().Split(mtbSeparator.Text[0]);
                        database.AddRecord(s);
                        toolStripProgressBar1.ProgressBar.Value++; 
                    }
                    catch (ArgumentNullException exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    catch (ArgumentException exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        int TotalLines(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                if (r != null) r.Close();
                return i;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fm = new EditorTrueFalse.fmAbout();
            fm.Show();
        }
    }
}
