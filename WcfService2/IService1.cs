using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SimpleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetUsername(int value);

        [OperationContract]
        UserType AddUser(bool admin, string username, string password, string firstname, string lastname);

        [OperationContract]
        bool Authenticate(string username, string password);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class UserType
    {
        bool admin = false;
        string username,
               password,
               firstname,
               lastname;

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        [DataMember]
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        [DataMember]
        public bool Admin
        {
            get { return admin; }
        }
    }
}
