using System;
using System.Collections.Generic;

namespace ContactDiary.Models
{
    public partial class Contacto
    {
        public int IdContacto { get; set; }
        public string? NombreContacto { get; set; }
        public string? TelContacto { get; set; }
        public string? DescripcionContacto { get; set; }
        public int? Usuario { get; set; }

        public virtual Usuario? UsuarioNavigation { get; set; }
    }
}
