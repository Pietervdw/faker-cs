using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Faker.Extensions;

namespace Faker
{
    // Data sourced from http://www.name-statistics.org/za/
    // 1000 Last names.
    public class SouthAfrican
    {
        /// <summary>
        /// Data sourced from http://www.name-statistics.org/za/prenumecomune.php
        /// </summary>
        /// <returns>1 of 1000 most common South African first anmes</returns>
        public static string Firstname()
        {
            return Resources.SouthAfrican.Firstname.Split(Config.Separator).Random().Trim();
        }


        /// <summary>
        /// Data taken from http://www.name-statistics.org/za/numedefamiliecomune.php
        /// </summary>
        /// <returns>1 of 1000 most common South African surnames</returns>
        public static string Lastname()
        {
            return Resources.SouthAfrican.Lastname.Split(Config.Separator).Random().Trim();
        }

        public static string Province()
        {
            return Resources.SouthAfrican.Province.Split(Config.Separator).Random().Trim();
        }

        public static string IdNumber(int maxAge = 100, int minAge = 18)
        {
            var random = new CryptoRandom();
            int year = random.Next(DateTime.Today.Year - maxAge, DateTime.Today.Year - minAge);
            int month = random.Next(1, 12);
            int day = random.Next(1, DateTime.DaysInMonth(year, month));
            int gender = random.Next(1, 9999); // If < 5000 Then Female. If >= 5000 Then Male
            int countryId = 0; // 0 = South Africa, 1 = Other
            int secondLastDigit = random.Next(0, 9);
            int controlDigit = 0;

            var dateAndGender = string.Format("{0}{1}{2}{3}", year.ToString().Substring(2, 2), month.ToString().PadLeft(2, '0'), day.ToString().PadLeft(2, '0'), gender.ToString().PadLeft(4, '0'));

            string idNumber = dateAndGender + countryId + secondLastDigit;
            controlDigit = GetControlDigit(idNumber);
            idNumber = idNumber + controlDigit;

            return idNumber;
        }

        //http://geekswithblogs.net/willemf/archive/2005/10/30/58561.aspx
        private static int GetControlDigit(string idString)
        {
            int d = -1;
            try
            {
                int a = 0;
                for (int i = 0; i < 6; i++)
                {
                    a += int.Parse(idString[2 * i].ToString());
                }
                int b = 0;
                for (int i = 0; i < 6; i++)
                {
                    b = b * 10 + int.Parse(idString[2 * i + 1].ToString());
                }
                b *= 2;
                int c = 0;
                do
                {
                    c += b % 10;
                    b = b / 10;
                }
                while (b > 0);
                c += a;
                d = 10 - (c % 10);
                if (d == 10) d = 0;
            }
            catch {/*ignore*/} return d;
        }
    }
}

//http://thinketg.com/how-to-generate-better-random-numbers-in-c-net-2/
public class CryptoRandom : RandomNumberGenerator
{
    private static RandomNumberGenerator r;

    ///<summary>
    /// Creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.
    ///</summary>
    public CryptoRandom()
    {
        r = RandomNumberGenerator.Create();
    }

    public override void GetBytes(byte[] data)
    {
        r.GetBytes(data);
    }

    public override void GetNonZeroBytes(byte[] data)
    {
        r.GetNonZeroBytes(data);
    }

    ///<summary>
    /// Returns a random number between 0.0 and 1.0.
    ///</summary>
    public double NextDouble()
    {
        byte[] b = new byte[4];
        r.GetBytes(b);
        return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
    }

    ///<summary>
    /// Returns a random number within the specified range.
    ///</summary>
    ///<param name="minValue">The inclusive lower bound of the random number returned.</param>
    ///<param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
    public int Next(int minValue, int maxValue)
    {
        return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
    }

    ///<summary>
    /// Returns a nonnegative random number.
    ///</summary>
    public int Next()
    {
        return Next(0, Int32.MaxValue);
    }

    ///<summary>
    /// Returns a nonnegative random number less than the specified maximum
    ///</summary>
    ///<param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
    public int Next(int maxValue)
    {
        return Next(0, maxValue);
    }
}