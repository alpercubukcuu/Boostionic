using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Helper
{
    public static class TransferHelper
    {
        public static TransferEntryInfoDto DecodeUserId(string encryptedUserId)
        {
            TransferEntryInfoDto transferEncode = new();
            if (string.IsNullOrEmpty(encryptedUserId))
            {
                throw new ArgumentException("Invalid user ID.");
            }

            // Şifrelenmiş kimliği çöz
            string decodedUserId = Cipher.DecryptUserId(encryptedUserId, "v12n5@Zf+rJl9^K3!hD7TsP$#xQ%6!uLmRtY8P*jKw");

            transferEncode.EncodedUserId = encryptedUserId; 
            transferEncode.DecodedUserId = decodedUserId;   
            return transferEncode;
        }
    }
}
