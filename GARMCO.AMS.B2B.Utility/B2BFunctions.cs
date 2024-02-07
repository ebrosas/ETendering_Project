using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GARMCO.Common.Utility;
using SautinSoft;

namespace GARMCO.AMS.B2B.Utility
{
	sealed public class B2BFunctions : GARMCOFunction
	{
		/// <summary>
		/// This method converts the html format text into Rtf
		/// </summary>
		/// <param name="htmlText"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string ConvertHtmlToRtf(string htmlText, string key)
		{
			string rtfText = String.Empty;

			HtmlToRtf h = new HtmlToRtf();
			h.Serial = key;
			rtfText = h.ConvertString(htmlText);

			return rtfText;
		}

		/// <summary>
		/// This method converts the rtf format text into Html
		/// </summary>
		/// <param name="rtfText"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string ConvertRtfToHtml(string rtfText, string key)
		{
			string htmlText = String.Empty;

			RtfToHtml r = new RtfToHtml();
			r.Serial = key;

			htmlText = r.ConvertString(rtfText);

			return htmlText;
		}

		public static string ConvertBytesToString(byte[] mediaObjectList)
		{
			StringBuilder ascii = new StringBuilder();
			foreach (byte numAscii in mediaObjectList)
			{

				if (numAscii != 0)
					ascii.Append((char)numAscii);

			}

			return ascii.ToString();
		}

		#region Password Encryption/Decryption
		public static string Encrypt(string InputText, string Password)
		{
			RijndaelManaged RijndaelCipher = new RijndaelManaged();

			byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
			byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
			PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
			ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write))
				{

					cryptoStream.Write(PlainText, 0, PlainText.Length);
					cryptoStream.FlushFinalBlock();

					byte[] CipherBytes = memoryStream.ToArray();
					return Convert.ToBase64String(CipherBytes);
				}
			}
		}

		public static string Encrypt(string plainText)
		{
			// Before encrypting data, we will append plain text to a random
			// salt value, which will be between 4 and 8 bytes long (implicitly used defaults).
			RijndaelEnhanced rijndaelKey = new RijndaelEnhanced(B2BConstants.PASSWORD_KEY, B2BConstants.INIT_VECTOR);

			return rijndaelKey.Encrypt(plainText);
		}

		public static string Decrypt(string InputText, string Password)
		{
			RijndaelManaged RijndaelCipher = new RijndaelManaged();

			byte[] EncryptedData = Convert.FromBase64String(InputText);
			byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

			PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
			ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
			using (MemoryStream memoryStream = new MemoryStream(EncryptedData))
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read))
				{

					byte[] PlainText = new byte[EncryptedData.Length];
					int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
					return Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
				}
			}
		}

		public static string Decrypt(string cipherText)
		{
			// Before encrypting data, we will append plain text to a random
			// salt value, which will be between 4 and 8 bytes long (implicitly used defaults).
			RijndaelEnhanced rijndaelKey = new RijndaelEnhanced(B2BConstants.PASSWORD_KEY, B2BConstants.INIT_VECTOR);

			return rijndaelKey.Decrypt(cipherText);
		}

		public static string CreateKey()
		{
			TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
			provider.GenerateKey();
			return BytesToHexString(provider.Key);
		}

		private static string BytesToHexString(byte[] bytes)
		{
			StringBuilder hexString = new StringBuilder(64);

			for (int counter = 0; counter < bytes.Length; counter++)
			{
				hexString.Append(String.Format("{0:X2}", bytes[counter]));
			}
			return hexString.ToString();
		}
		#endregion
	}
}
