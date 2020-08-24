using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;
using JEWELRY_NUBES.Util;

namespace JEWELRY_NUBES.Util
{
    class EncryptUtil
    {
        public X509Certificate2 GetCertificateFromStore(string certName)
        {
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                //X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Collection certCollection = new X509Certificate2Collection();
                certCollection.Import(Constants.ENCRYPT_CERT_FILE);

                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectName, certName, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }
        }

        public string getSKID(Org.BouncyCastle.X509.X509Certificate cert)
        {
            DerObjectIdentifier identifier = new DerObjectIdentifier("2.5.29.14");
            byte[] skid = Hex.Encode(cert.GetExtensionValue(identifier).GetOctets());
            string skidstring = new ASCIIEncoding().GetString(skid);
            return skidstring.Substring(4);
        }

        public byte[] encryptRsa(byte[] data, Org.BouncyCastle.X509.X509Certificate cert)
        {
            IBufferedCipher cipher = Org.BouncyCastle.Security.CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
            cipher.Init(true, cert.GetPublicKey());
            return cipher.DoFinal(data);
        }

        public byte[] decryptRsa(byte[] data, System.Security.Cryptography.X509Certificates.X509Certificate2 cert)
        {
            AsymmetricKeyParameter privatekey = Org.BouncyCastle.Security.DotNetUtilities.GetKeyPair(cert.PrivateKey).Private;

            IBufferedCipher cipher = Org.BouncyCastle.Security.CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
            //IBufferedCipher cipher = Org.BouncyCastle.Security.CipherUtilities.GetCipher("RSA/NONE/PKCS1Padding");
            //IBufferedCipher cipher = Org.BouncyCastle.Security.CipherUtilities.GetCipher("RSA/NONE/OAEPWithSHA1AndMGF1Padding");
            cipher.Init(false, privatekey);
            return cipher.DoFinal(data);
        }

        public byte[] hashData(byte[] data)
        {
            IDigest digest = new Sha256Digest();
            var resBuf = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(data, 0, data.Length);
            digest.DoFinal(resBuf, 0);
            return resBuf;
        }

        public int D3CustomerCheckDigit(string idWithoutCheckdigit)
        {
            // allowable characters within identifier
            const string validChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVYWXZ_";

            // remove leading or trailing whitespace, convert to uppercase
            idWithoutCheckdigit = idWithoutCheckdigit.Trim().ToUpper();

            // this will be a running total
            int sum = 0;

            // loop through digits from right to left
            for (int i = 0; i < idWithoutCheckdigit.Length; i++)
            {
                //set ch to "current" character to be processed
                char ch = idWithoutCheckdigit[idWithoutCheckdigit.Length - i - 1];

                // throw exception for invalid characters
                if (validChars.IndexOf(ch) == -1)
                    throw new Exception(ch + " is an invalid character");

                // our "digit" is calculated using ASCII value - 48
                int digit = (int)ch - 48;

                // weight will be the current digit's contribution to
                // the running total
                int weight;
                if (i % 2 == 0)
                {

                    // for alternating digits starting with the rightmost, we
                    // use our formula this is the same as multiplying x 2 and
                    // adding digits together for values 0 to 9.  Using the
                    // following formula allows us to gracefully calculate a
                    // weight for non-numeric "digits" as well (from their
                    // ASCII value - 48).
                    weight = (2 * digit) - (int)(digit / 5) * 9;
                }
                else
                {
                    // even-positioned digits just contribute their ascii
                    // value minus 48
                    weight = digit;
                }

                // keep a running total of weights
                sum += weight;
            }

            // avoid sum less than 10 (if characters below "0" allowed,
            // this could happen)
            sum = Math.Abs(sum) + 10;

            // check digit is amount needed to reach next number
            // divisible by ten
            return (10 - (sum % 10)) % 10;
        }


        public int checkCardBIN(long card_no)
        {

            int check_flag = 0;

                 if (card_no >= (long)4000000000000000L && card_no <= (long)4999999999999999L) { check_flag = 1; } //C VISA CREDITCARD LUHN hashed True
            else if (card_no >= (long)5100000000000000L && card_no <= (long)5599999999999999L) { check_flag = 1; } //C MASTER CREDITCARD LUHN hashed True
            else if (card_no >= (long)4000000000000L && card_no <= (long)4999999999999L) { check_flag = 1; } //C VISA CREDITCARD LUHN hashed True
            else if (card_no >= (long)340000000000000L && card_no <= (long)349999999999999L) { check_flag = 1; } //C AMEX CREDITCARD LUHN hashed True
            else if (card_no >= (long)370000000000000L && card_no <= (long)379999999999999L) { check_flag = 1; } //C AMEX CREDITCARD LUHN hashed True
            else if (card_no >= (long)622126000000000000L && card_no <= (long)622925999999999999L) { check_flag = 1; } //C CUP CREDITCARD NONE hashed True
            else if (card_no >= (long)6221260000000000000L && card_no <= (long)6229259999999999999L) { check_flag = 1; } //C CUP CREDITCARD NONE hashed True
            else if (card_no >= (long)62212600000000000L && card_no <= (long)62292599999999999L) { check_flag = 1; } //C CUP CREDITCARD NONE hashed True
            else if (card_no >= (long)622126000000000L && card_no <= (long)622925999999999L) { check_flag = 1; } //C CUP CREDITCARD NONE hashed True
            else if (card_no >= (long)6221260000000000L && card_no <= (long)6229259999999999L) { check_flag = 1; } //C CUP CREDITCARD NONE hashed True
            else if (card_no >= (long)213100000000000L && card_no <= (long)213199999999999L) { check_flag = 1; } //C JCB CREDITCARD LUHN hashed True
            else if (card_no >= (long)38000000000000L && card_no <= (long)38999999999999L) { check_flag = 1; } //C DINERS CREDITCARD LUHN hashed True
            else if (card_no >= (long)36000000000000L && card_no <= (long)36999999999999L) { check_flag = 1; } //C DINERS CREDITCARD LUHN hashed True
            else if (card_no >= (long)30000000000000L && card_no <= (long)30599999999999L) { check_flag = 1; } //C DINERS CREDITCARD LUHN hashed True
            else if (card_no >= (long)180000000000000L && card_no <= (long)180099999999999L) { check_flag = 1; } //C JCB CREDITCARD LUHN hashed True
            else if (card_no >= (long)3500000000000000L && card_no <= (long)3599999999999999L) { check_flag = 1; } //C JCB CREDITCARD LUHN hashed True
            else if (card_no >= (long)660000000000000000L && card_no <= (long)669999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)6600000000000000000L && card_no <= (long)6699999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)6600000000000000L && card_no <= (long)6699999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)66000000000000000L && card_no <= (long)66999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)66000000000000L && card_no <= (long)66999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)660000000000000L && card_no <= (long)669999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)6600000000000L && card_no <= (long)6699999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)6700000000000L && card_no <= (long)6799999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)67000000000000L && card_no <= (long)67999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)67000000000000000L && card_no <= (long)67999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)670000000000000000L && card_no <= (long)679999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)6700000000000000L && card_no <= (long)6799999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)670000000000000L && card_no <= (long)679999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)6700000000000000000L && card_no <= (long)6799999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)560000000000000L && card_no <= (long)589999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)56000000000000L && card_no <= (long)58999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)5600000000000L && card_no <= (long)5899999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)5600000000000000L && card_no <= (long)5899999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)5600000000000000000L && card_no <= (long)5899999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)560000000000000000L && card_no <= (long)589999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)56000000000000000L && card_no <= (long)58999999999999999L) { check_flag = 2; } //B OTHER DEBITCARD LUHN hashed False
            else if (card_no >= (long)6300000000000000L && card_no <= (long)6399999999999999L) { check_flag = 3; } //O STORECARD STORECARD NONE plain False
            else if (card_no >= (long)7042776300000000L && card_no <= (long)7042776399999999L) { check_flag = 3; } //O HARRODS STORECARD LUHN plain False
            else if (card_no >= (long)7042776400000000000L && card_no <= (long)7448999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7450000000000L && card_no <= (long)7450999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)704277640000000000L && card_no <= (long)744899999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7042776400000000L && card_no <= (long)7448999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)70427764000000000L && card_no <= (long)74489999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)74500000000000000L && card_no <= (long)74509999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)745000000000000000L && card_no <= (long)745099999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7450000000000000L && card_no <= (long)7450999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)74500000000000L && card_no <= (long)74509999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)745000000000000L && card_no <= (long)745099999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7000000000000000L && card_no <= (long)7042776299999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)70000000000000000L && card_no <= (long)70427762999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)700000000000000L && card_no <= (long)704277629999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7000000000000L && card_no <= (long)7042776299999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)70000000000000L && card_no <= (long)70427762999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)70427764000000L && card_no <= (long)74489999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)704277640000000L && card_no <= (long)744899999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7042776400000L && card_no <= (long)7448999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)700000000000000000L && card_no <= (long)704277629999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7000000000000000000L && card_no <= (long)7042776299999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)900000000000000000L && card_no <= (long)929999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)9300000000000L && card_no <= (long)9999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)90000000000000000L && card_no <= (long)92999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)900000000000000L && card_no <= (long)929999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)9000000000000000L && card_no <= (long)9299999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)930000000000000L && card_no <= (long)999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)93000000000000L && card_no <= (long)99999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)9300000000000000L && card_no <= (long)9999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)930000000000000000L && card_no <= (long)999999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)93000000000000000L && card_no <= (long)99999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)800000000000000L && card_no <= (long)899999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)8000000000000000L && card_no <= (long)8999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)80000000000000L && card_no <= (long)89999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)7450000000000000000L && card_no <= (long)7450999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)8000000000000L && card_no <= (long)8999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)9000000000000L && card_no <= (long)9299999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)90000000000000L && card_no <= (long)92999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)8000000000000000000L && card_no <= (long)8999999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)80000000000000000L && card_no <= (long)89999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)800000000000000000L && card_no <= (long)899999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)20000000000000000L && card_no <= (long)20999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)200000000000000000L && card_no <= (long)209999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)200000000000000L && card_no <= (long)209999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)2000000000000000L && card_no <= (long)2099999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)22000000000000L && card_no <= (long)22999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)220000000000000L && card_no <= (long)229999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)2000000000000000000L && card_no <= (long)2099999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)2200000000000L && card_no <= (long)2299999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)20000000000000L && card_no <= (long)20999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)100000000000000L && card_no <= (long)109999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)1000000000000000L && card_no <= (long)1099999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)1000000000000L && card_no <= (long)1099999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)10000000000000L && card_no <= (long)10999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)1000000000000000000L && card_no <= (long)1099999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)2000000000000L && card_no <= (long)2099999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)10000000000000000L && card_no <= (long)10999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)100000000000000000L && card_no <= (long)109999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)2200000000000000L && card_no <= (long)2299999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)65000000000000L && card_no <= (long)65999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)650000000000000L && card_no <= (long)659999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)6000000000000000000L && card_no <= (long)6099999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)6500000000000L && card_no <= (long)6599999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)650000000000000000L && card_no <= (long)659999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)6500000000000000000L && card_no <= (long)6599999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)6500000000000000L && card_no <= (long)6599999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)65000000000000000L && card_no <= (long)65999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)600000000000000000L && card_no <= (long)609999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)2200000000000000000L && card_no <= (long)2299999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)6000000000000L && card_no <= (long)6099999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)22000000000000000L && card_no <= (long)22999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)220000000000000000L && card_no <= (long)229999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)6000000000000000L && card_no <= (long)6099999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)60000000000000000L && card_no <= (long)60999999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)60000000000000L && card_no <= (long)60999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)600000000000000L && card_no <= (long)609999999999999L) { check_flag = 3; } //O OTHER OTHER LUHN plain False
            else if (card_no >= (long)74499900000000L && card_no <= (long)74499909999999L) { check_flag = 4; } //G GLOBALBLUE OTHER LUHN plain False
            else if (card_no >= (long)74493000000000L && card_no <= (long)74493009999999L) { check_flag = 4; } //G GLOBALBLUE OTHER LUHN plain False
            else if (card_no >= (long)74491000000000L && card_no <= (long)74491109999999L) { check_flag = 4; } //G GLOBALBLUE OTHER LUHN plain False
            else if (card_no >= (long)74492000000000L && card_no <= (long)74492109999999L) { check_flag = 4; } //G GLOBALBLUE OTHER LUHN plain False
            else if (card_no >= (long)3086040000000000L && card_no <= (long)3086049999999999L) { check_flag = 4; } //G GLOBALBLUE OTHER LUHN plain False

            return check_flag;
        }
    }
}
