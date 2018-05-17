using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

/// <summary>
/// Максим Торопов
/// Ярославль
/// Домашняя работа 8 урока
/// </summary>
namespace EditorTrueFalse
{
    /// <summary>
    /// class Questions
    /// </summary>
    [Serializable]
    public class Question
    {
        /// <summary>
        /// question
        /// </summary>
        public string question;

        /// <summary>
        /// answer yes or no
        /// </summary>
        public bool answer;

        /// <summary>
        /// basic ctor
        /// </summary>
        public Question()
        {
                
        }

        /// <summary>
        /// ctor for initialize Question
        /// </summary>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        public Question(string question, bool answer)
        {
            this.question = question;
            this.answer = answer;
        }
    }

    /// <summary>
    /// Class TrueFalse for game
    /// </summary>
    public class TrueFalse
    {
        /// <summary>
        /// name of the file with questions
        /// </summary>
        string fileName;

        /// <summary>
        /// list fo questions
        /// </summary>
        List<Question> listQuestions;

        /// <summary>
        /// basic ctor
        /// </summary>
        public TrueFalse()
        {
            this.fileName = string.Empty;
            listQuestions = new List<Question>();
        }

        /// <summary>
        /// ctor for initialize Questions
        /// </summary>
        /// <param name="filename"></param>
        public TrueFalse(string fileName)
        {
            this.fileName = fileName;
            listQuestions = new List<Question>();
        }

        /// <summary>
        /// Properties return fileName
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Properties to access to ListQuestions element by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Question this[int index]
        {
            get { return listQuestions[index]; }
        }

        /// <summary>
        /// Method GetQuestion
        /// </summary>
        /// <param name="questionNumber">number of the question</param>
        /// <returns>string with question</returns>
        public string GetQuestion(int questionNumber)
        {
            return listQuestions[questionNumber].question;
        }

        /// <summary>
        /// Method GetAnswer
        /// </summary>
        /// <param name="questionNumber">number of the question</param>
        /// <returns>true or false</returns>
        public bool GetAnswer(int questionNumber)
        {
            if (listQuestions[questionNumber].answer) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Method for add question
        /// </summary>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        public void AddQuestion(string question, bool answer)
        {
            listQuestions.Add(new Question(question, answer));
        }

        /// <summary>
        /// Method for remove questions
        /// </summary>
        /// <param name="indexQuestion"></param>
        public void RemoveQuestion(int indexQuestion)
        {
            if (listQuestions != null && indexQuestion >= 0 && indexQuestion < listQuestions.Count) listQuestions.RemoveAt(indexQuestion);
        }

        /// <summary>
        /// Method for save questions into the file
        /// </summary>
        public void SaveQuestions()
        {
            if (fileName == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML файлы|*.XML|Все файлы|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.fileName = saveFileDialog.FileName;
                    Save();
                }
                else MessageBox.Show("FileName not specified!");
            }
            else
                Save();
        }

        /// <summary>
        /// Method for save serialized data to the file
        /// </summary>
        void Save()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Question>));
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(fStream, listQuestions);
            fStream.Close();
        }

        /// <summary>
        /// Method for load questions from the file
        /// </summary>
        public void LoadQuestions()
        {
            if (this.fileName == string.Empty)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML файлы|*.XML|Все файлы|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.fileName = openFileDialog.FileName;
                    Load();
                }
                else MessageBox.Show("FileName not specified!");
            }
            else Load();
        }

        /// <summary>
        /// Method for load serialized data from the file
        /// </summary>
        void Load()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Question>));
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            listQuestions = (List<Question>)xmlFormat.Deserialize(fStream);
            fStream.Close();
        }

        public void Clear()
        {
            listQuestions = new List<Question>();
        }

        /// <summary>
        /// Properties returning count of questions in the list of questions
        /// </summary>
        /// <returns></returns>
        public int CountQuestions()
        {
            return this.listQuestions.Count;
        }
    }
}
