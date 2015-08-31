using System;
using System.Collections.Generic;
using Faker.Extensions;

namespace Faker
{
    public static class Company
    {
        public static string Name()
        {
            return NameFormats.Random();
        }

        /// <summary>
        /// Data sourced from http://www.fakenamegenerator.com
        /// </summary>
        /// <returns>1 of 3000 random Company Names</returns>
        public static string CompanyName()
        {
            
            return Resources.Company.CompanyNames.Split(Config.Separator).Random().Trim();
        }

        /// <summary>
        /// Data sourced from http://www.fakenamegenerator.com
        /// </summary>
        /// <returns>1 of 3000 random Job Titles</returns>
        public static string JobTitle()
        {

            return Resources.Company.JobTitles.Split(Config.Separator).Random().Trim();
        }

        /// <summary>
        /// </summary>
        /// <returns>1 of 4000+ random Products</returns>
        public static string Product()
        {

            return Resources.Company.Products.Split(Config.Separator).Random().Trim();
        }

        public static string Suffix()
        {
            return Resources.Company.Suffix.Split(Config.Separator).Random();
        }

        /// <summary>
        /// Generate a buzzword-laden catch phrase.
        /// Wordlist from http://www.1728.com/buzzword.htm
        /// </summary>
        public static string CatchPhrase()
        {
            return String.Join(" ",
                               new[]
                               {
                                   Resources.Company.Buzzwords1.Split(Config.Separator).Random(),
                                   Resources.Company.Buzzwords2.Split(Config.Separator).Random(),
                                   Resources.Company.Buzzwords3.Split(Config.Separator).Random()
                               });
        }

        /// <summary>
        /// When a straight answer won't do, BS to the rescue!
        /// Wordlist from http://dack.com/web/bullshit.html
        /// </summary>
        public static string BS()
        {
            return String.Join(" ",
                               new[]
                               {
                                   Resources.Company.BS1.Split(Config.Separator).Random(),
                                   Resources.Company.BS2.Split(Config.Separator).Random(),
                                   Resources.Company.BS3.Split(Config.Separator).Random()
                               });
        }

        #region Format Mappings
        private static readonly IEnumerable<Func<string>> NameFormats = new List<Func<string>>
        {
            () => string.Format("{0} {1}", Faker.Name.Last(), Suffix()),
            () => string.Format("{0}-{1}", Faker.Name.Last(), Faker.Name.Last()),
            () => string.Format("{0}, {1} {2} {3}", Faker.Name.Last(), Faker.Name.Last(), Resources.Company.And, Faker.Name.Last()),
        };
        #endregion
    }
}