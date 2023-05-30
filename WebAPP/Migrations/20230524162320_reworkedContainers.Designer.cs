﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPP;

#nullable disable

namespace WebAPP.Migrations
{
    [DbContext(typeof(WebAPPContext))]
    [Migration("20230524162320_reworkedContainers")]
    partial class reworkedContainers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebAPP.Areas.Identity.Data.UserAccount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Book", b =>
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

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.CategoryBase", b =>
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

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.ContainerDMO", b =>
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

                    b.Property<int>("ParentPageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ParentPageId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.ObjectDMO", b =>
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

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.PageDMO", b =>
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

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.SectionBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("SectionBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SectionBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Tag", b =>
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

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Category", b =>
                {
                    b.HasBaseType("WebAPP.Areas.Organizers.Data.CategoryBase");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("OrganizerId");

                    b.HasIndex("ParentId");

                    b.HasIndex("Name", "ParentId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Category");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Organizer", b =>
                {
                    b.HasBaseType("WebAPP.Areas.Organizers.Data.CategoryBase");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("OwnerId", "Name")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Document", b =>
                {
                    b.HasBaseType("WebAPP.Areas.Organizers.Data.SectionBase");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Title", "CategoryId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Document");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Section", b =>
                {
                    b.HasBaseType("WebAPP.Areas.Organizers.Data.SectionBase");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ParentId");

                    b.HasIndex("Title", "ParentId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Section");
                });

            modelBuilder.Entity("ContainerDMOObjectDMO", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.ContainerDMO", null)
                        .WithMany()
                        .HasForeignKey("ContainerDMOsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPP.Areas.Organizers.Data.ObjectDMO", null)
                        .WithMany()
                        .HasForeignKey("ObjectDMOsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocumentTag", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPP.Areas.Organizers.Data.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebAPP.Areas.Identity.Data.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebAPP.Areas.Identity.Data.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPP.Areas.Identity.Data.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebAPP.Areas.Identity.Data.UserAccount", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Book", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.CategoryBase", "ParentCategory")
                        .WithMany("Books")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.ContainerDMO", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.PageDMO", "ParentPage")
                        .WithMany("ContainerDMOs")
                        .HasForeignKey("ParentPageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentPage");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.PageDMO", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.Book", "ParentBook")
                        .WithMany("PageDMOs")
                        .HasForeignKey("ParentBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentBook");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.SectionBase", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.Organizer", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Category", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.Organizer", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPP.Areas.Organizers.Data.CategoryBase", "Parent")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Organizer", b =>
                {
                    b.HasOne("WebAPP.Areas.Identity.Data.UserAccount", "Owner")
                        .WithMany("Organizers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Document", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.CategoryBase", "Category")
                        .WithMany("Documents")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Section", b =>
                {
                    b.HasOne("WebAPP.Areas.Organizers.Data.SectionBase", "Parent")
                        .WithMany("Sections")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("WebAPP.Areas.Identity.Data.UserAccount", b =>
                {
                    b.Navigation("Organizers");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.Book", b =>
                {
                    b.Navigation("PageDMOs");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.CategoryBase", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Documents");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.PageDMO", b =>
                {
                    b.Navigation("ContainerDMOs");
                });

            modelBuilder.Entity("WebAPP.Areas.Organizers.Data.SectionBase", b =>
                {
                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}