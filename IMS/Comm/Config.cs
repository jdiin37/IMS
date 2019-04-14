using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace IMS.Comm
{
  public class Config
  {
    /// <summary>
    /// 分頁值
    /// </summary>
    public static int PageSize = 10;
    
    /// <summary>
    /// 拿取WebConfig AppSetting值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string AppSetting(string key)
    {
      return WebConfigurationManager.AppSettings[key] ?? "";
    }


    /// <summary>
    /// Md5 Salted //無法反解的加密
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Md5Salt(string data)
    {
      var salt = System.Text.Encoding.UTF8.GetBytes(Config.AppSetting("md5SaltKey"));
      var odata = System.Text.Encoding.UTF8.GetBytes(data);
      var hmacMD5 = new HMACMD5(salt);
      var saltedHash = hmacMD5.ComputeHash(odata);
      return Convert.ToBase64String(saltedHash);
    }

    /// <summary>
    /// 亂數值
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string RandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
      if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
      if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

      const int byteSize = 0x100;
      var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
      if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

      // Guid.NewGuid and System.Random are not particularly random. By using a
      // cryptographically-secure random number generator, the caller is always
      // protected, regardless of use.
      using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
      {
        var result = new StringBuilder();
        var buf = new byte[128];
        while (result.Length < length)
        {
          rng.GetBytes(buf);
          for (var i = 0; i < buf.Length && result.Length < length; ++i)
          {
            // Divide the byte into allowedCharSet-sized groups. If the
            // random value falls into the last group and the last group is
            // too small to choose from the entire allowedCharSet, ignore
            // the value in order to avoid biasing the result.
            var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
            if (outOfRangeStart <= buf[i]) continue;
            result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
          }
        }
        return result.ToString();
      }
    }
  }
}