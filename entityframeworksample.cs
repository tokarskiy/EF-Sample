/**
 ** Nuget packages:
 ** 1) Microsoft.EntityFrameworkCore.Tools
 ** 2) Npgsql.EntityFrameworkCore.PostgreSQL
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ApplicationContext())
            {
                var query =
                    from data in db.DataSet
                    where data.Currency == "Euro"
                    select data;

                query.ToList().ForEach(Console.WriteLine);
            }
        }
    }

    [Table("Data")]
    public class Data
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("credit_card")]
        public string CreditCard { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("catch_phrase")]
        public string CatchPhrase { get; set; }

        [Column("fda_ndc_code")]
        public string FDANDC { get; set; }

        public override string ToString()
        {
            return string.Format("{4,5}|{0,20}|{1,15}|{2,40}|{3,15}|", new string[5]
            {
                CreditCard,
                Currency,
                CatchPhrase.Substring(0, Math.Min(CatchPhrase.Length, 40)),
                FDANDC,
                Id.ToString()
            });
        }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Data> DataSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Lab1Dataset;Username=postgres;Password=tokar1996");
        }
    }
}
