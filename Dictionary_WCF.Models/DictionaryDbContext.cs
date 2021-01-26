using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dictionary_WCF.AppConstanst;

namespace Dictionary_WCF.Models
{
    public class DictionaryDbContext : DbContext
    {
        private readonly string connectionString;
        public DictionaryDbContext()
        {

            this.connectionString = AppConstants.CONNECTION_STRING;
        }
        public DictionaryDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DbSet<PersonModel> People { get; set; }
        public DbSet<PhoneNumberModel> PhoneNumbers { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(this.connectionString))
            {
                optionsBuilder.UseSqlServer(this.connectionString);
            }
            else
            {
                throw new ArgumentNullException("ConnectionString is empty!");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            //var PersonEntity = builder.Entity<PersonModel>();
            //PersonEntity.ToTable("dbo.People");
            //PersonEntity.HasKey(x => x.Id);
            //PersonEntity.Property(x => x.Id).HasColumnName("PersonId");
            //PersonEntity.Property(x => x.Name).HasColumnName("Name");
            //PersonEntity.Property(x => x.MiddleName).HasColumnName("Middlename");
            //PersonEntity.Property(x => x.Surname).HasColumnName("Surname");

            //var AdressEntity = builder.Entity<AddressModel>();
            //AdressEntity.ToTable("Addresses");
            //AdressEntity.HasKey("AddressId");
            //AdressEntity.HasAlternateKey("PersonId");
            //AdressEntity.Property(x => x.Id).HasColumnName("AddressId");
            //AdressEntity.Property(x => x.PersonId).HasColumnName("PersonId");
            //AdressEntity.Property(x => x.Address).HasColumnName("Adress");
            //AdressEntity.Property(x => x.IsHomeAddress).HasColumnName("IsHomeAddress");

            //var PhoneNumberEntity = builder.Entity<PhoneNumberModel>();
            //PhoneNumberEntity.ToTable("PhoneNumbers");
            //PhoneNumberEntity.HasKey(x=>x.Id);
            //PhoneNumberEntity.HasAlternateKey(x => x.PersonId);
            //PhoneNumberEntity.Property(x => x.Id).HasColumnName("PhoneNumberId");
            //PhoneNumberEntity.Property(x => x.PersonId).HasColumnName("PersonId");
            //PhoneNumberEntity.Property(x => x.Number).HasColumnName("Number");
            //PhoneNumberEntity.Property(x => x.IsHome).HasColumnName("IsHome");
        }
    }
}