using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CycleCountGoldPro
{

    // See if should inherit from FileStream
    class TagFile 
    {
        // fields
        // Static TagFileCount  ?  or only work with one at a time
        public static string LocalNewFolder = @"C:\Tag\New";  // Make this a constant?
        public const string LocalNewFolder2 = @"C:\Tag\New";

        private int _recordCount;
        private string _name;
        private string _fullPathName;
       

        // properties
        //.LineCount or SNCount or RecordCount
        //.Name or Path
        //.State  New, open, closed (is this needed?)
        public string Name
        {
            get { return _name; }
        }

        public string FullPathName
        {
            get { return _fullPathName; }
        }

        public int RecordCount
        {
            get { return _recordCount; }
        }

        public string State { get; set; }  // Change this to enum?

        // constructors
        // Create file, receive path
        public TagFile()
        {
            this._recordCount = 0;
            this._name = makeTimeBasedFileName();
            this._fullPathName = LocalNewFolder + Path.PathSeparator + this._name;  // May need / added. Use String.Format or String.Builder here?
            File.Create(_fullPathName);  // Check options, if this is complicated use private .create method
            this.WriteLine("Header");

        }


        // methods
        // .Create here or do in constructor?
        public void Create(string somePath)
        {
            if (Directory.Exists(somePath) == false)
            {
                Directory.CreateDirectory(LocalNewFolder);
            }
        }

        // .Close or do in deconstructor
        public void Close()
        {
            this.WriteLine("footer");
            // Close File
        }

        // .WriteLine
        public void WriteLine(string line_of_data)
        {
            // This creates a new file if necessary.
            using (FileStream fs = new FileStream(_fullPathName, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //  Our actual write of a single line of data appended to EOF
                    sw.WriteLine(line_of_data);
                }
            }

            this._recordCount++;
        }


        // .Get/Set state (necessary?)
        public void SetState(string someState)
        {

        }

        private string makeTimeBasedFileName()
        {
            DateTime dateNow = DateTime.Now;
            return dateNow.ToString("MMddHHmm") + "." + dateNow.ToString("ss");
        }

        // destructor
        ~TagFile()
        {
            this.WriteLine("footer");
            // Close File
            //or just do this.Close();
        }
        
    }
}
