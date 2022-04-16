using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Producto
    {
        public Producto()
        {
            Detalleventa = new HashSet<Detalleventum>();
        }

        public int PkProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double? Precioventa { get; set; }
        public double? Preciocompra { get; set; }
        public int? Cantidad { get; set; }
        public double? Descuento { get; set; }
        public int? FkProveedor { get; set; }
        public int? FkCategoria { get; set; }
        public DateTime? Fechavencimiento { get; set; }
        public int? FkPersonalcrea { get; set; }
        public DateTime? Fechacrea { get; set; }
        public int? FkPersonaledita { get; set; }
        public DateTime? Fechaedita { get; set; }
        public bool? Isdelete { get; set; }
        public string Imagen { get; set; }

        public virtual Categorium FkCategoriaNavigation { get; set; }
        public virtual Proveedor FkProveedorNavigation { get; set; }
        public virtual ICollection<Detalleventum> Detalleventa { get; set; }
    }
}
