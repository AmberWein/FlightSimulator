using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlightSimulator.IO
{
    class XMLParser
    {
        private string xmlPath;
        public string XmlPath
        {
            set
            {
                xmlPath = value;
                // check Validity!!!!
            }
        }
        private ArrayList headers;
        public ArrayList Headers
        {
            get
            {
                return headers;
            }
            set
            {
                headers = value;
            }
        }
        public XMLParser()
        {
            this.headers = new ArrayList();
            // not sure - should be default, and change if given a specific path
            this.xmlPath = "C:/Program Files/FlightGear 2020.3.6/data/Protocol/playback_small.xml";
        }

        public void Parse()
        { // maybe path should be given
            string element, lastCopy;
            XmlReader reader = XmlReader.Create(xmlPath);
            reader.ReadToFollowing("input"); // input and output attributes are the same
            bool hasNext = reader.ReadToFollowing("name"); // each header is after th name tag
            while (hasNext)
            {
                element = reader.ReadElementContentAsString();
                if (!headers.Contains(element))
                {
                    headers.Add(element);
                }
                else
                {
                    int index = 2;
                    lastCopy = element + index.ToString();
                    while (headers.Contains(lastCopy))
                    {
                        index++;
                        lastCopy = element + index.ToString();
                    }
                    headers.Add(lastCopy);
                }
                hasNext = reader.ReadToFollowing("name");
            }
        }
    }
}
