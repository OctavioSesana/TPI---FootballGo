using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Data
{
    public class FootballGoDbContext : DbContext
    {
        public FootballGoDbContext() { }

        public FootballGoDbContext(DbContextOptions<FootballGoDbContext> opt) : base(opt) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Cancha> Canchas => Set<Cancha>();
        public DbSet<Reserva> Reservas => Set<Reserva>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Servidor Emilio
                //optionsBuilder.UseSqlServer(@"Server=DESKTOP-URP6JK1\SQLEXPRESS;Database=FootballGoDB;Trusted_Connection=True;TrustServerCertificate=True");

                //Servidor Octa
                optionsBuilder.UseSqlServer(@"Server=OCTAV10\SQLEXPRESS;Database=FootballGoDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Cliente>();
            e.ToTable("Clientes");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Nombre).HasMaxLength(80).IsRequired();
            e.Property(x => x.Apellido).HasMaxLength(80).IsRequired();
            e.Property(x => x.Email).HasMaxLength(200).IsRequired();
            e.Property(x => x.dni).IsRequired();
            e.Property(x => x.telefono).IsRequired();
            e.Property(x => x.FechaAlta).HasColumnType("datetime2");

            var e2 = modelBuilder.Entity<Empleado>();
            e2.ToTable("Empleados");
            e2.HasKey(x => x.IdEmpleado);
            e2.Property(x => x.IdEmpleado).ValueGeneratedOnAdd();
            e2.Property(x => x.Nombre).HasMaxLength(80).IsRequired();
            e2.Property(x => x.Apellido).HasMaxLength(80).IsRequired();
            e2.Property(x => x.SueldoSemanal).HasMaxLength(200).IsRequired();
            e2.Property(x => x.EstaActivo).HasMaxLength(100).IsRequired();
            e2.Property(x => x.FechaIngreso).HasColumnType("datetime2").HasDefaultValueSql("GETDATE()");

            var c = modelBuilder.Entity<Cancha>();
            c.ToTable("Canchas");
            c.HasKey(x => x.NroCancha);
            c.Property(x => x.NroCancha).ValueGeneratedOnAdd();
            c.Property(x => x.NroCancha).IsRequired();
            c.HasIndex(x => x.NroCancha).IsUnique();

            c.Property(x => x.EstadoCancha)
            .HasConversion<string>()          // almacena "Disponible/Mantenimiento/Ocupada"
            .HasMaxLength(20)
            .IsRequired();

            c.HasCheckConstraint("CK_Canchas_Estado",
               "EstadoCancha IN ('Disponible','Mantenimiento','Ocupada')");


            c.Property(x => x.TipoCancha).IsRequired();
            c.Property(x => x.PrecioPorHora)
             .HasColumnType("decimal(10,2)")   // hasta 99999999.99
             .IsRequired();

            var r = modelBuilder.Entity<Reserva>();

            r.ToTable("Reservas");
            r.HasKey(x => x.IdReserva);
            r.Property(x => x.IdReserva)
             .ValueGeneratedOnAdd();
            r.Property(x => x.FechaReserva)
             .HasColumnType("datetime2")
             .HasDefaultValueSql("GETDATE()")
             .IsRequired();
            r.Property(x => x.HoraInicio)
             .HasColumnType("time")
             .IsRequired();
            r.Property(x => x.PrecioTotal)
             .HasColumnType("decimal(10,2)")
             .IsRequired();
            r.Property(x => x.mailUsuario)
             .HasMaxLength(100)
             .IsRequired();
            r.HasOne<Cancha>()
             .WithMany()
              .HasPrincipalKey(c => c.NroCancha) // una cancha puede tener muchas reservas
             .HasForeignKey(x => x.NroCancha)    // FK en Reserva
             .OnDelete(DeleteBehavior.Restrict); // evita borrar una cancha si tiene reservas


        }



    }
}
