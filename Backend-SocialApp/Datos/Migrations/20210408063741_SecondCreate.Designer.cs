﻿// <auto-generated />
using System;
using Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Datos.Migrations
{
    [DbContext(typeof(SocialAppContext))]
    [Migration("20210408063741_SecondCreate")]
    partial class SecondCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entity.Comentario", b =>
                {
                    b.Property<string>("IdComentario")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContenidoComentario")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("IdUsuario")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("PublicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdComentario");

                    b.HasIndex("IdUsuario");

                    b.HasIndex("PublicacionId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("Entity.Publicacion", b =>
                {
                    b.Property<string>("IdPublicacion")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContenidoPublicacion")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("IdUsuario")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(25)");

                    b.HasKey("IdPublicacion");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Publicacions");
                });

            modelBuilder.Entity("Entity.Usuario", b =>
                {
                    b.Property<string>("Correo")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Apellidos")
                        .HasColumnType("varchar(25)");

                    b.Property<byte[]>("ImagePerfil")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("KeyPasswordDesEncriptar")
                        .HasColumnType("varchar(16)");

                    b.Property<string>("Nombres")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Sexo")
                        .HasColumnType("varchar(9)");

                    b.HasKey("Correo");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Entity.Comentario", b =>
                {
                    b.HasOne("Entity.Usuario", null)
                        .WithMany()
                        .HasForeignKey("IdUsuario");

                    b.HasOne("Entity.Publicacion", null)
                        .WithMany("Comentarios")
                        .HasForeignKey("PublicacionId");
                });

            modelBuilder.Entity("Entity.Publicacion", b =>
                {
                    b.HasOne("Entity.Usuario", null)
                        .WithMany()
                        .HasForeignKey("IdUsuario");
                });

            modelBuilder.Entity("Entity.Publicacion", b =>
                {
                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
