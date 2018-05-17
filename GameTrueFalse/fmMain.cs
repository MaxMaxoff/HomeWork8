using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EditorTrueFalse;

namespace GameTrueFalse
{
    public partial class fmMain : Form
    {
        /// <summary>
        /// new list of questions
        /// </summary>
        TrueFalse database;

        /// <summary>
        /// new game
        /// </summary>
        TrustGame game;


        public fmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close main form
        /// exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// New game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML файлы|*.XML|Все файлы|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                // initialize new list of questions
                database = new TrueFalse(openFileDialog.FileName);
                database.LoadQuestions();

                // enable radio buttons
                rbtNo.Enabled = true;
                rbtYes.Enabled = true;

                // initialize new game
                game = new TrustGame();
                
                // update status bar
                UpdateStatusStrip();

                // start the game
                Game();
            }
        }

        /// <summary>
        /// Method Game
        /// </summary>
        private void Game()
        {
            // reset radio buttons
            UnCkeckRadioButtons();

            if (game.Steps < 5)
            {
                // set question number and put question into text box
                game.CurNumber = game.GetQuestionNumber(0, database.CountQuestions());
                tbQuestion.Text = database.GetQuestion(game.CurNumber);
            }
            else
            {
                rbtNo.Enabled = false;
                rbtYes.Enabled = false;
                tbQuestion.Text = "";
                MessageBox.Show(game.Points == game.Steps ? "Поздравляем! Вы выиграли!" : "Вы проиграли, попробуйте еще раз!");
            }
        }

        /// <summary>
        /// Method reset radio button
        /// </summary>
        private void UnCkeckRadioButtons()
        {
            rbtNo.Checked = false;
            rbtYes.Checked = false;
        }

        /// <summary>
        /// Method update status bar
        /// </summary>
        private void UpdateStatusStrip()
        {
            toolStripStatusLabel1.Text = $"Вопрос: {game.Steps} ### Вы уже угадали: {game.Points}.";
        }

        /// <summary>
        /// check if we click Yes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtYes_Click(object sender, EventArgs e)
        {
            if (database.GetAnswer(game.CurNumber))
                game.CorrectAnswer();
            else game.WrongAnswer();

            // update status bar
            UpdateStatusStrip();

            Game();
        }

        /// <summary>
        /// check if we click No
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtNo_Click(object sender, EventArgs e)
        {
            if (!database.GetAnswer(game.CurNumber))
                game.CorrectAnswer();
            else game.WrongAnswer();

            // update status bar
            UpdateStatusStrip();

            Game();
        }

        /// <summary>
        /// Форма About
        /// Новую рисовать лень, поэтому берем из EditorTrueFalse :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fm = new EditorTrueFalse.fmAbout();
            fm.Show();
        }
    }
}
