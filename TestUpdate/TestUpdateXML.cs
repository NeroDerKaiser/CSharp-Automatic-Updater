using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;

namespace TestUpdate
{
    public class TestUpdateXML
    {
        private Version version;
        private Uri uri;
        private string fileName;
        private string md5;
        private string description;
        private string launchArgs;


        public Version Version
        {
            get { return this.version; }
        }

        public Uri Uri
        {
            get { return this.uri; }
        }

        public string FileName
        {
            get { return this.fileName; }
        }

        public string MD5
        {
            get { return this.md5; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public string LaunchArgs
        {
            get { return this.launchArgs; }
        }

        
        
        
        
        internal TestUpdateXML(Version version, Uri uri, string fileName, string md5, string description, string launchArgs)
        {
            this.version = version;
            this.uri = uri;
            this.fileName = fileName;
            this.md5 = md5;
            this.description = description;
            this.launchArgs = launchArgs;
        }

        
        
        
        
        internal bool IsNewerThan(Version version)
        {
            return this.version > version;
        }

       
        
        
        
        internal static bool ExistsOnServer(Uri location)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(location.AbsoluteUri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch { return false; }
        }

        
        
        
        
        internal static TestUpdateXML Parse(Uri location, string appID)
        {
            Version version = null;
            string url = "", fileName = "", md5 = "", description = "", launchArgs = "";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(location.AbsoluteUri);

                XmlNode updateNode = doc.DocumentElement.SelectSingleNode("//update[@appId='" + appID + "']");

                if (updateNode == null)
                    return null; 

                version = Version.Parse(updateNode["version"].InnerText);
                url = updateNode["Url"].InnerText;
                fileName = updateNode["filename"].InnerText;
                md5 = updateNode["md5"].InnerText;
                description = updateNode["description"].InnerText;
                launchArgs = updateNode["launchArgs"].InnerText;

                return new TestUpdateXML(version, new Uri(url), fileName, md5, description, launchArgs);

            }
            catch { return null; }
        }
    }
}
