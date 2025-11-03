using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ArticuloRepository
    {
        private FootballGoDbContext CreateContext()
        {
            // Acá usás tu propio DbContext en lugar de TPIContext
            return new FootballGoDbContext();
        }

        public void Add(Articulo articulo)
        {
            using var context = CreateContext();
            context.Articulos.Add(articulo);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var articulo = context.Articulos.Find(id);
            if (articulo != null)
            {
                context.Articulos.Remove(articulo);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Articulo? Get(int id)
        {
            using var context = CreateContext();
            return context.Articulos.Find(id);
        }

        public IEnumerable<Articulo> GetAll()
        {
            using var context = CreateContext();
            return context.Articulos.AsNoTracking().ToList();
        }

        public bool Update(Articulo articulo)
        {
            using var context = CreateContext();
            var existingArticulo = context.Articulos.Find(articulo.Id);
            if (existingArticulo != null)
            {
                //existingArticulo.SetId(articulo.Id);
                //existingArticulo.SetTipo(articulo.tipo);
                //existingArticulo.SetMarca(articulo.marca);
                //existingArticulo.SetColor(articulo.color);
                existingArticulo.SetEstado(articulo.Estado); //solo el estado es actualizable

                context.SaveChanges();
                return true;
            }
            return false;
        }


        public IEnumerable<Articulo> GetByCriteria(FootballGo.DTOs.ArticuloCriteria criteria)
        {
            const string sql = @"
                    SELECT Id, Tipo, marca, color, estado
                    FROM Articulos 
                    WHERE Id LIKE @SearchTerm 
                    ORDER BY marca";

            var articulos = new List<Articulo>();
            string connectionString = new FootballGoDbContext().Database.GetConnectionString();
            string searchPattern = $"%{criteria.Texto}%";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@SearchTerm", searchPattern);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var articulo = new Articulo(
                    reader.GetInt32(0),
                    reader.GetString(10),
                    reader.GetString(10),
                    reader.GetString(10),
                    reader.GetString(10)
                );

                articulos.Add(articulo);
            }

            return articulos;
        }
    }
}
