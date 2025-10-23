using Domain.Model;
using Domain.Services;
using FootballGo.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FootballGo.WebAPI
{
    public static class ArticuloEndpoints
    {
        public static void MapArticuloEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/articulos");

            // GET /articulos
            // Obtiene todos los artículos.
            group.MapGet("/", (ArticuloService service) =>
            {
                var list = service.GetAll();
                return Results.Ok(list);
            })
            .WithName("GetAllArticulos")
            .Produces<IEnumerable<Articulo>>(StatusCodes.Status200OK);

            // GET /articulos/{id}
            // Obtiene un artículo por su ID.
            group.MapGet("/{id:int}", (int id, ArticuloService service) =>
            {
                var articulo = service.Get(id);
                return articulo is null ? Results.NotFound() : Results.Ok(articulo);
            })
            .WithName("GetArticuloById")
            .Produces<Articulo>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // POST /articulos
            group.MapPost("/", (Articulo articulo, ArticuloService service) =>
            {
                // El ID se genera en el repositorio/servicio (o por la DB),
                // por lo que se pasa 0 o se ignora si la DB lo auto-genera.
                // En el ArticuloRepository.Add no se asigna el ID, se asume que lo hace la DB.

                // NOTA: Para este caso, el Articulo tiene un constructor con ID
                // y el repositorio no retorna el objeto con el ID actualizado (se usa void).
                // Si la DB maneja el ID, el servicio/repositorio debería retornarlo para Results.Created.
                // Asumo que el cliente envía un Articulo completo, el repositorio lo inserta.
                service.Add(articulo);

                // Si el repositorio usara Entity Framework, el objeto 'articulo' tendría el ID
                // después de SaveChanges. Como no tengo acceso a la implementación exacta,
                // asumo que el ID es válido para el Created.
                return Results.Created($"/articulos/{articulo.Id}", articulo);
            })
            .WithName("CreateArticulo")
            .Produces<Articulo>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest); // Añadir manejo de errores si se agregan validaciones

            // PUT /articulos/{id}
            // Actualiza el estado de un artículo existente.
            // Se recibe el objeto Articulo completo con el nuevo estado.
            group.MapPut("/{id:int}", (int id, [FromBody] Articulo articulo, ArticuloService service) =>
            {
                // Asegurar que el ID del cuerpo coincida con el ID de la ruta
                if (id != articulo.Id)
                {
                    return Results.BadRequest(new { Message = "El ID de la ruta no coincide con el ID del cuerpo." });
                }

                // El repositorio ArticuloRepository.Update solo actualiza el 'estado'.
                bool ok = service.Update(articulo);

                return ok ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateArticulo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

            // DELETE /articulos/{id}
            // Elimina un artículo por su ID.
            group.MapDelete("/{id:int}", (int id, ArticuloService service) =>
            {
                bool ok = service.Delete(id);
                return ok ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteArticulo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            // GET /articulos/criteria?texto={texto}
            // Busca artículos por el texto de criterio.
            group.MapGet("/criteria", (string? texto, ArticuloService service) =>
            {
                // Creamos el objeto criteria con el texto recibido.
                var criteria = new FootballGo.DTOs.ArticuloCriteria
                {
                    Texto = texto?.Trim() ?? string.Empty
                };

                var lista = service.GetByCriteria(criteria);

                return Results.Ok(lista);
            })
            .WithName("GetArticulosByCriteria")
            .Produces<IEnumerable<Articulo>>(StatusCodes.Status200OK);
        }
    }
}