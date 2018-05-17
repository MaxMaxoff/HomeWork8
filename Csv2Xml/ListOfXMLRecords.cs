using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace Csv2Xml
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Record
    {
        /// <summary>
        /// array of strings
        /// </summary>
        public string[] arr;

        /// <summary>
        /// default ctor
        /// </summary>
        public Record()
        {

        }

        /// <summary>
        /// ctor for initialize array of strings
        /// </summary>
        /// <param name="arr"></param>
        public Record(string[] arr)
        {
            this.arr = arr;
        }
    }

    class ListOfXMLRecords
    {
        /// <summary>
        /// list of records
        /// </summary>
        List<Record> listRecords;

        /// <summary>
        /// File Name
        /// </summary>
        string fileName;

        /// <summary>
        /// default ctor
        /// </summary>
        public ListOfXMLRecords()
        {
            listRecords = new List<Record>();
        }

        /// <summary>
        /// Properties for File Name
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Method add record to list of records
        /// </summary>
        /// <param name="arr"></param>
        public void AddRecord(string[] arr)
        {
            listRecords.Add(new Record(arr));
        }

        /// <summary>
        /// Method for clear list of records
        /// </summary>
        public void Clear()
        {
            listRecords.Clear();
        }

        /// <summary>
        /// Method for save serialized data to the file
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveList()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Record>));
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(fStream, listRecords);
            fStream.Close();
        }
    }
}
