﻿// <auto-generated />
using System;
using API.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Persistencia.Migrations
{
    [DbContext(typeof(APIContext))]
    [Migration("20230715052708_usuario")]
    partial class usuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Dominio.Calificaciones", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<decimal?>("CalificacionFinal")
                        .HasColumnType("numeric");

                    b.Property<int>("EstudiantesNoControl")
                        .HasColumnType("integer");

                    b.Property<int>("MateriaId")
                        .HasColumnType("integer");

                    b.Property<int>("PeriodoId")
                        .HasColumnType("integer");

                    b.Property<int?>("Unidad1")
                        .HasColumnType("integer");

                    b.Property<int?>("Unidad2")
                        .HasColumnType("integer");

                    b.Property<int?>("Unidad3")
                        .HasColumnType("integer");

                    b.Property<int?>("Unidad4")
                        .HasColumnType("integer");

                    b.Property<int?>("Unidad5")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("EstudiantesNoControl");

                    b.HasIndex("PeriodoId");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("API.Dominio.Carreras", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NombreCarrera")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NombreReducido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Reticula")
                        .HasColumnType("integer");

                    b.Property<string>("Siglas")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("API.Dominio.Estudiantes", b =>
                {
                    b.Property<int>("NoControl")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("NoControl"));

                    b.Property<string>("Apellido_Materno")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Apellido_Paterno")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CarreraId")
                        .HasColumnType("integer");

                    b.Property<string>("Curp")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("character varying(18)");

                    b.Property<int>("Edad")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha_Nacimiento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("NoControl");

                    b.HasIndex("CarreraId");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("API.Dominio.Materias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CarreraId")
                        .HasColumnType("integer");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NoUnidades")
                        .HasColumnType("integer");

                    b.Property<string>("NombreMateria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CarreraId");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("API.Dominio.PeriodoEscolar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Abreviacion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaTemino")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Periodos");
                });

            modelBuilder.Entity("API.Dominio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("API.Dominio.Calificaciones", b =>
                {
                    b.HasOne("API.Dominio.Estudiantes", "Estudiantes")
                        .WithMany()
                        .HasForeignKey("EstudiantesNoControl")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Dominio.PeriodoEscolar", "Periodo")
                        .WithMany()
                        .HasForeignKey("PeriodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiantes");

                    b.Navigation("Periodo");
                });

            modelBuilder.Entity("API.Dominio.Estudiantes", b =>
                {
                    b.HasOne("API.Dominio.Carreras", "Carrera")
                        .WithMany()
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrera");
                });

            modelBuilder.Entity("API.Dominio.Materias", b =>
                {
                    b.HasOne("API.Dominio.Carreras", "Carrera")
                        .WithMany()
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrera");
                });
#pragma warning restore 612, 618
        }
    }
}
