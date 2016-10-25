//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace HashProvider
//{
//    public class CustomHasher
//    {
//        public class CustomHasher
//        {
//            public string Hash(string input)
//            {
//                var sha1 = SHA1.Create();
//                var inputBytes = Encoding.ASCII.GetBytes(input);
//                var hash = sha1.ComputeHash(inputBytes);
//                var sb = new StringBuilder();
//                for (var i = 0; i < hash.Length; i++)
//                {
//                    sb.Append(hash[i].ToString("X2"));
//                }
//                return sb.ToString();
//            }

//            public string HashPassword(User user, string password)
//            {
//                return Hash(password);
//            }

//            public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
//            {
//                providedPassword = Hash(providedPassword);

//                if (hashedPassword.Equals(providedPassword))
//                    return PasswordVerificationResult.Success;
//                else return PasswordVerificationResult.Failed;
//            }
//        }
//    }
//}
