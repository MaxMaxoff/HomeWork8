using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditorTrueFalse;

namespace GameTrueFalse
{
    /// <summary>
    /// Class for Trust Game
    /// </summary>
    class TrustGame
    {
        /// <summary>
        /// Steps in games
        /// </summary>
        int steps;

        /// <summary>
        /// Points in games
        /// </summary>
        int points;

        /// <summary>
        /// Current Question Number
        /// </summary>
        int curNumber;

        /// <summary>
        /// Properties return steps value
        /// </summary>
        public int Steps
        {
            get { return steps; }
        }

        /// <summary>
        /// Properties return points value
        /// </summary>
        public int Points
        {
            get { return points; }
        }

        /// <summary>
        /// Properties number of selected question
        /// </summary>
        public int CurNumber
        {
            get { return curNumber; }
            set { curNumber = value; }
        }

        /// <summary>
        /// initialize new array
        /// </summary>
        /// <param name="n"></param>
        public TrustGame()
        {
            steps = 0;
            points = 0;
        }

        /// <summary>
        /// Method Get Question Number
        /// </summary>
        /// <param name="min">min question number</param>
        /// <param name="max">max question number</param>
        /// <returns></returns>
        public int GetQuestionNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// Method Correct Answer
        /// </summary>
        /// <returns>points</returns>
        public void CorrectAnswer()
        {
            points++;
            steps++;
        }

        /// <summary>
        /// Method Wrong Answer
        /// </summary>
        /// <returns>points</returns>
        public void WrongAnswer()
        {
            steps++;
        }
    }

}
