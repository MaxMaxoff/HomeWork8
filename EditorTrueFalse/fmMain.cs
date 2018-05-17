using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// /// Максим Торопов
/// Ярославль
/// Домашняя работа 8 урока
/// 
/// Работа с вопросами для игры "Верю - не верю".
/// </summary>
namespace EditorTrueFalse
{
    public partial class fmMain : Form
    {
        TrueFalse database;

        public fmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Update datasource for DataGridView
        /// </summary>
        void UpdateDataSource()
        {
            //dgvQuestions.DataSource = null;
            //dgvQuestions.DataSource = database;
            dgvQuestions.Rows.Clear();
        }

        /// <summary>
        /// Fill DataGridView from database
        /// </summary>
        void FillDGVQuestions()
        {
            UpdateDataSource();
            for (int i = 0; i < database.CountQuestions(); i++)
            {
                dgvQuestions.Rows.Add(database[i].question, database[i].answer);
            }
            dgvQuestions.ReadOnly = false;
        }

        /// <summary>
        /// Save dgv to database
        /// </summary>
        private void SaveDGVQuestionsToTrueFalse()
        {
            database.Clear();
            for (int i = 0; i < dgvQuestions.RowCount; i++)
                if (dgvQuestions.Rows[i].Cells[0].Value != null)
                    database.AddQuestion(dgvQuestions.Rows[i].Cells[0].Value.ToString(), dgvQuestions.Rows[i].Cells[1].Value != null ? (bool)dgvQuestions.Rows[i].Cells[1].Value : false);
        }

        /// <summary>
        /// Click on New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database = new TrueFalse();
            UpdateDataSource();
        }

        /// <summary>
        /// Click on Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML файлы|*.XML|Все файлы|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(openFileDialog.FileName);
                database.LoadQuestions();
                FillDGVQuestions();
            }
            else MessageBox.Show("FileName not specified!");
        }

        /// <summary>
        /// Click on Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvQuestions != null)
            {
                SaveDGVQuestionsToTrueFalse();
                database.SaveQuestions();
            }
            else MessageBox.Show("Nothing to save. There are no questions!");
        }

        /// <summary>
        /// Click on SaveAs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database.FileName = string.Empty;
            saveToolStripMenuItem_Click(new object(), new EventArgs());
        }

        /// <summary>
        /// Click on Button Add
        /// Add record to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, EventArgs e)
        {
            if (database == null)
                newToolStripMenuItem_Click(new object(), new EventArgs());

            dgvQuestions.ReadOnly = false;
            database.AddQuestion("Type new question here...", false);
            FillDGVQuestions();
        }

        /// <summary>
        /// Click on button Remove
        /// Remove record from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRemove_Click(object sender, EventArgs e)
        {
            if (database == null)
                MessageBox.Show("Nothing to delete!");
            else
            {
                if (MessageBox.Show("Selected row will be deleted. Are you sure?", "Warning!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    database.RemoveQuestion(dgvQuestions.CurrentCell.RowIndex);
                    FillDGVQuestions();
                }
            }
        }

        /// <summary>
        /// Remove using StripMenu
        /// Just click on Remove button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btRemove.PerformClick();
        }

        /// <summary>
        /// Close main form / Exit Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Open form About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fm = new fmAbout();
            fm.Show();
        }
    }
}
