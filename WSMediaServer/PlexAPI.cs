using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace WSMediaServer
{
    static class PlexAPI
    {
        private static int idleTime = 0;

        public static int IdleTime
        {
            get { return idleTime; }
            set
            {
                if (value < 0)
                    throw new NotImplementedException("IdleTime < 0");
                idleTime = value;
            }
        }


        public static class StreamingNumber
        {
            private static readonly string END_POINT = "http://127.0.0.1:" + Settings.Port + "/status/sessions?X-Plex-Token=" + Settings.Token;

            public static int Get()
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(END_POINT);
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(reader.ReadToEnd());
                    /* Return the number of Video nodes in the XML response */
                    return doc.SelectNodes("MediaContainer/Video").Count;
                }
            }

        }
    }
}
