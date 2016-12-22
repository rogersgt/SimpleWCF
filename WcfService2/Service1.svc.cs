using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Resources;
using System.Web;
using System.IO;

namespace SimpleService
{
    public class Service1 : IService1
    {
        private XElement doc;

        private void FillXML()
        {
            string xmlData = Properties.Resources.users;
            doc = XElement.Parse(xmlData);       
        }

        public string GetUsername(int ID)
        {
            FillXML();
            foreach (XElement el in doc.Elements())
            {
                int id = Int32.Parse(el.Element("ID").Value);
                if (ID == id)
                {
                    return el.Element("username").Value;
                }
            }
            return "ID does not match any existing users";
        }

        private void SaveUser (bool admin, UserType user)
        {
            FillXML();
            XElement el = new XElement("user", new XAttribute("admin", admin));
            el.Add(new XElement("username", user.Username));
            el.Add(new XElement("password", user.Password));
            el.Add(new XElement("firstname", user.Firstname));
            el.Add(new XElement("lastname", user.Lastname));

            doc.Add(el);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "users.xml");
            doc.Save(path);
        }

        public UserType AddUser(bool admin, string username, string password, string firstname, string lastname)
        {
            UserType user = new UserType();
            user.Firstname = firstname;
            user.Lastname = lastname;
            user.Password = password;

            SaveUser(admin, user);
            
            return user;
        }

        public bool Authenticate(string username, string password)
        {
            FillXML();

            foreach (XElement el in doc.Elements())
            {
                if ((el.Element("username").Value == username) && (el.Element("password").Value == password))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
