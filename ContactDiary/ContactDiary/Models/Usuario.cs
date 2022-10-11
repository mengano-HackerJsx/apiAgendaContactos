using System;
using System.Collections.Generic;

namespace ContactDiary.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Contactos = new HashSet<Contacto>();
        }

        public int IdUser { get; set; }
        public string? NombreUser { get; set; }
        public string? ApellidoUser { get; set; }
        public string? Usuario1 { get; set; }
        public int? Clave { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
