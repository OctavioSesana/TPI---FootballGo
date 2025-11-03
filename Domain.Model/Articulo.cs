namespace Domain.Model
{
    public class Articulo
    {
        public int Id { get; private set; }
        public string Tipo { get; private set; } // Renombré a mayúscula para consistencia de .NET
        public string Marca { get; private set; }
        public string Color { get; private set; }
        public string Estado { get; set; }

        protected Articulo() { }

        public Articulo(int id, string tipo, string marca, string color, string estado)
        {
            // El constructor ahora llama a los setters corregidos y validados
            SetId(id);
            SetTipo(tipo);
            SetMarca(marca);
            SetColor(color);
            SetEstado(estado);
        }

        public void SetId(int id) => Id = id;

        // --- SETTERS CORREGIDOS Y VALIDADOS ---

        public void SetTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El Tipo de artículo no puede ser nulo o vacío.", nameof(tipo));
            Tipo = tipo; // FIX: Asigna al campo privado
        }

        public void SetMarca(string marca)
        {
            if (string.IsNullOrWhiteSpace(marca))
                throw new ArgumentException("La Marca de artículo no puede ser nula o vacía.", nameof(marca));
            Marca = marca; // FIX: Asigna al campo privado
        }

        public void SetColor(string color)
        {
            // ESTA ES LA CLAVE para solucionar el error de NULL en Color
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("El Color de artículo no puede ser nulo o vacío.", nameof(color));
            Color = color; // FIX: Asigna al campo privado
        }

        public void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El Estado de artículo no puede ser nulo o vacío.", nameof(estado));
            Estado = estado; // FIX: Asigna al campo privado
        }
    }

    public class ArticuloCriteria
    {
        public string Texto { get; set; } = string.Empty;
    }
}