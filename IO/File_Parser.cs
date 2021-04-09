using System.IO;

namespace FlightSimulator.IO
{
    abstract class File_Parser
    {
        protected string filePath;
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        public File_Parser(string filePath)
        {
            this.filePath = filePath;
        }
        public static bool IsValidPath(string filePath)
        {
            return File.Exists(filePath);
        }
        virtual public void Parse()
        {
            // backup validity check
            if (IsValidPath(this.filePath) == false)
                return;
        }
    }
}
