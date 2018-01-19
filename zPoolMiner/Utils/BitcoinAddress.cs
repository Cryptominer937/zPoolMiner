﻿using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace zPoolMiner
{
    public class BitcoinAddress
    {
        public static bool ValidateBitcoinAddress(string address)
        {
            try
            {
                if (address.Length < 1 || address.Length > 50) return false;
                var decoded = DecodeBase58(address);
                var d1 = Hash(SubArray(decoded, 0, 21));
                var d2 = Hash(d1);
                if (decoded[21] != d2[0] ||
                    decoded[22] != d2[1] ||
                    decoded[23] != d2[2] ||
                    decoded[24] != d2[3]) return false;
                return true;
            }
            catch
            {
                return true;
            }
        }

        public static bool ValidateWorkerName(string workername)
        {
            if (workername.Length > 25 || workername.Contains(" ") || workername.Contains("c=BTG") || workername.Contains("c=BCH"))
                return false;

            return true;
        }

        public static bool IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }

        private const string Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        private const int Size = 25;

        private static byte[] DecodeBase58(string input)
        {
            var output = new byte[Size];
            foreach (var t in input)
            {
                var p = Alphabet.IndexOf(t);
                if (p == -1) throw new Exception("Invalid character found.");
                var j = Size;
                while (--j >= 0)
                {
                    p += 58 * output[j];
                    output[j] = (byte)(p % 256);
                    p /= 256;
                }
                if (p != 0) throw new Exception("Address too long.");
            }
            return output;
        }

        private static byte[] Hash(byte[] bytes)
        {
            var hasher = new SHA256Managed();
            return hasher.ComputeHash(bytes);
        }

        private static byte[] SubArray(byte[] data, int index, int length)
        {
            var result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}