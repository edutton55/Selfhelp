using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Hashing:IHashing
    {
        public string GetHash(string statement)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create(); //Generates the Hash
            byte[] inputbytes = System.Text.Encoding.ASCII.GetBytes(statement); //convert the input into string byte array
            byte[] hashbytes = md5.ComputeHash(inputbytes); // computes hash
            StringBuilder sb = new StringBuilder(); //mutable string of characters
            for (int index = 0; index < hashbytes.Length; index++)
            {
                sb.Append(hashbytes[index].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
