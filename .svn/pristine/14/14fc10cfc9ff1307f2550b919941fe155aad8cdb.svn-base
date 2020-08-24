using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEWELRY_NUBES.Util
{
    class Utils
    {
        public static string FormatDate(string value)
        {
            string result = "";

            result = value.Substring(6, 2) + "/" + value.Substring(4, 2) + "/" + value.Substring(0, 4);

            return result;
        }

        public static string FormatDate(string value, int length)
        {
            string result = "";
            if (length == 2)
            {
                result = value.Substring(6, 2) + "/" + value.Substring(4, 2) + "/" + value.Substring(2, 2);
            }
            else
            {
                result = value.Substring(6, 2) + "/" + value.Substring(4, 2) + "/" + value.Substring(0, 4);
            }
            return result;
        }


        public static string FormatTime(string value)
        {
            string result = "";

            result = value.Substring(0, 2) + ":" + value.Substring(2, 2) + ":" + value.Substring(4, 2);

            return result;
        }

        public static string FormatTime(string value, string type)
        {
            string result = "";

            string hh = "";
            string timeband = "";
            if (type == "12")
            {
                hh = value.Substring(0, 2);
                if (int.Parse(hh) > 12)
                {
                    hh = (int.Parse(hh) - 12).ToString();
                    timeband = "pm";
                }
                else
                {
                    timeband = "am";
                }
                result = hh + "." + value.Substring(2, 2) + timeband;
            }
            else
            {
                result = value.Substring(0, 2) + "." + value.Substring(2, 2) + "." + value.Substring(4, 2);
            }

            return result;
        }

        public static string FormatDocId(string value)
        {
            string result = "";

            result = value.Substring(0, 6) + "." + value.Substring(6, 5) + "." + value.Substring(11, 4) + "." + value.Substring(15, 5);

            return result;
        }

        public static string FormatConvertDate(string value)
        {
            string result = "";

            result = value.Replace("/", "").Replace("-", "");
            result = result.Substring(4, 4) + result.Substring(2, 2) + result.Substring(0, 2);

            return result;
        }

        public static string FormatConvertDateSG(string value)
        {
            string result = "";

            result = value.Replace("-", "/");
            return result;
        }

        public string getFullDate(string strBirth)
        {
            string strRet = "20000101";
            int nBirth = Int32.Parse(strBirth.Substring(0, 2));
            if ((nBirth + 2000) > (Int32.Parse(DateTime.Now.ToString("yyyy")) - 10))
            {
                strRet = "19" + strBirth;
            }
            else
            {
                strRet = "20" + strBirth;
            }
            return strRet;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }

    public class SimpleHashTable
    {
        private const int INITIAL_SIZE = 16;
        private int size;
        private Node[] buckets;

        public SimpleHashTable()
        {
            this.size = INITIAL_SIZE;
            this.buckets = new Node[size];
        }

        public SimpleHashTable(int capacity)
        {
            this.size = capacity;
            this.buckets = new Node[size];
        }

        public void Put(object key, object value)
        {
            int index = HashFunction(key);
            if (buckets[index] == null)
            {
                buckets[index] = new Node(key, value);
            }
            else
            {
                Node newNode = new Node(key, value);
                newNode.Next = buckets[index];
                buckets[index] = newNode;
            }
        }

        public object Get(object key)
        {
            int index = HashFunction(key);

            if (buckets[index] != null)
            {
                for (Node n = buckets[index]; n != null; n = n.Next)
                {
                    if (n.Key == key)
                    {
                        return n.Value;
                    }
                }
            }
            return null;
        }

        public bool Contains(object key)
        {
            int index = HashFunction(key);
            if (buckets[index] != null)
            {
                for (Node n = buckets[index]; n != null; n = n.Next)
                {
                    if (n.Key == key)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected virtual int HashFunction(object key)
        {
            return Math.Abs(key.GetHashCode() + 1 +
            (((key.GetHashCode() >> 5) + 1) % (size))) % size;
        }

        private class Node
        {
            public object Key { get; set; }
            public object Value { get; set; }
            public Node Next { get; set; }

            public Node(object key, object value)
            {
                this.Key = key;
                this.Value = value;
                this.Next = null;
            }
        }
    }
}
