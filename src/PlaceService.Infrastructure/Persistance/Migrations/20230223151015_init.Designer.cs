﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlaceService.Infrastructure.Persistance.Context;

#nullable disable

namespace PlaceService.Infrastructure.Persistanse.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230223151015_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PlaceService.Domain.Entities.EquipmentContractEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipmentCount")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductionRoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTypeId");

                    b.HasIndex("ProductionRoomId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("PlaceService.Domain.Entities.EquipmentTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<double>("Code")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EquipmentTypes");
                });

            modelBuilder.Entity("PlaceService.Domain.Entities.ProductionRoomEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Code")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("NormativeArea")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("PlaceService.Domain.Entities.EquipmentContractEntity", b =>
                {
                    b.HasOne("PlaceService.Domain.Entities.EquipmentTypeEntity", "EquipmentType")
                        .WithMany("EquipmentContracts")
                        .HasForeignKey("EquipmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceService.Domain.Entities.ProductionRoomEntity", "ProductionRoom")
                        .WithMany("EquipmentContracts")
                        .HasForeignKey("ProductionRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipmentType");

                    b.Navigation("ProductionRoom");
                });

            modelBuilder.Entity("PlaceService.Domain.Entities.EquipmentTypeEntity", b =>
                {
                    b.Navigation("EquipmentContracts");
                });

            modelBuilder.Entity("PlaceService.Domain.Entities.ProductionRoomEntity", b =>
                {
                    b.Navigation("EquipmentContracts");
                });
#pragma warning restore 612, 618
        }
    }
}
