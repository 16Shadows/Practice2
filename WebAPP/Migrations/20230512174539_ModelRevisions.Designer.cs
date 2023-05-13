﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPP;

#nullable disable

namespace WebAPP.Migrations
{
    [DbContext(typeof(DMOrganizerDBContext))]
    [Migration("20230512174539_ModelRevisions")]
    partial class ModelRevisions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", true)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("ContainerDMOObjectDMO", b =>
                {
                    b.Property<int>("ContainerDMOsID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ObjectDMOsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ContainerDMOsID", "ObjectDMOsId");

                    b.HasIndex("ObjectDMOsId");

                    b.ToTable("ContainerDMOObjectDMO");
                });

            modelBuilder.Entity("ContainerDMOPageDMO", b =>
                {
                    b.Property<int>("ContainerDMOsID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PageDMOsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ContainerDMOsID", "PageDMOsId");

                    b.HasIndex("PageDMOsId");

                    b.ToTable("ContainerDMOPageDMO");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.CategoryBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CategoryBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CategoryBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DMOrganizerDomainModel.ContainerDMO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoordX")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoordY")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.ObjectDMO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LinkToObject")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Objects");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.PageDMO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParentBookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentBookId");

                    b.HasIndex("Position", "ParentBookId")
                        .IsUnique();

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.SectionBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SectionBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SectionBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("DocumentTag", b =>
                {
                    b.Property<int>("DocumentsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DocumentsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("DocumentTag");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Category", b =>
                {
                    b.HasBaseType("DMOrganizerDomainModel.CategoryBase");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ParentId");

                    b.HasIndex("Name", "ParentId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Category");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Organizer", b =>
                {
                    b.HasBaseType("DMOrganizerDomainModel.CategoryBase");

                    b.Property<int>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("OwnerId", "Name")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Document", b =>
                {
                    b.HasBaseType("DMOrganizerDomainModel.SectionBase");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ParentId");

                    b.HasIndex("Title", "ParentId")
                        .IsUnique();

                    b.ToTable("SectionBase", t =>
                        {
                            t.Property("ParentId")
                                .HasColumnName("Document_ParentId");
                        });

                    b.HasDiscriminator().HasValue("Document");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Section", b =>
                {
                    b.HasBaseType("DMOrganizerDomainModel.SectionBase");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ParentId");

                    b.HasIndex("Title", "ParentId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Section");
                });

            modelBuilder.Entity("ContainerDMOObjectDMO", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.ContainerDMO", null)
                        .WithMany()
                        .HasForeignKey("ContainerDMOsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DMOrganizerDomainModel.ObjectDMO", null)
                        .WithMany()
                        .HasForeignKey("ObjectDMOsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContainerDMOPageDMO", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.ContainerDMO", null)
                        .WithMany()
                        .HasForeignKey("ContainerDMOsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DMOrganizerDomainModel.PageDMO", null)
                        .WithMany()
                        .HasForeignKey("PageDMOsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Book", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.CategoryBase", "ParentCategory")
                        .WithMany("Books")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.PageDMO", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.Book", "ParentBook")
                        .WithMany("PageDMOs")
                        .HasForeignKey("ParentBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentBook");
                });

            modelBuilder.Entity("DocumentTag", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DMOrganizerDomainModel.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Category", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.CategoryBase", "Parent")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Organizer", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.Account", "Owner")
                        .WithMany("Organizers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Document", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.CategoryBase", "Parent")
                        .WithMany("Documents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Section", b =>
                {
                    b.HasOne("DMOrganizerDomainModel.SectionBase", "Parent")
                        .WithMany("Sections")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Account", b =>
                {
                    b.Navigation("Organizers");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.Book", b =>
                {
                    b.Navigation("PageDMOs");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.CategoryBase", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Documents");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("DMOrganizerDomainModel.SectionBase", b =>
                {
                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}