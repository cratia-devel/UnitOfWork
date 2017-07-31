using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DAL.Models;

namespace DAL.Migrations
{
    [DbContext(typeof(SystemContext))]
    partial class SystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created_At");

                    b.Property<DateTime>("Deleted_At");

                    b.Property<double>("Percentage");

                    b.Property<int?>("PersonId");

                    b.Property<DateTime>("Updated_At");

                    b.Property<int?>("WasteId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("WasteId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("DAL.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created_At");

                    b.Property<DateTime>("Deleted_At");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("FirstName", "LastName")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("DAL.Models.Waste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<DateTime>("Created_At");

                    b.Property<DateTime>("DateTime");

                    b.Property<DateTime>("Deleted_At");

                    b.Property<double>("SalePrice");

                    b.Property<DateTime>("Updated_At");

                    b.Property<int?>("WasteTypeId");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("WasteTypeId");

                    b.ToTable("Wastes");
                });

            modelBuilder.Entity("DAL.Models.WasteType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created_At");

                    b.Property<DateTime>("Deleted_At");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("Updated_At");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.ToTable("TypesOfWaste");
                });

            modelBuilder.Entity("DAL.Models.Partner", b =>
                {
                    b.HasOne("DAL.Models.Person", "Person")
                        .WithMany("Business")
                        .HasForeignKey("PersonId");

                    b.HasOne("DAL.Models.Waste")
                        .WithMany("Partners")
                        .HasForeignKey("WasteId");
                });

            modelBuilder.Entity("DAL.Models.Waste", b =>
                {
                    b.HasOne("DAL.Models.WasteType", "WasteType")
                        .WithMany("Wastes")
                        .HasForeignKey("WasteTypeId");
                });
        }
    }
}
