namespace LubrilogixTest.Models
{
    public partial class spLeerInventario_Result
    {
        public int TN_IdOrden { get; set; }
        public DateTime TF_Fecha { get; set; }
        public int TN_IDSucursal { get; set; }
        public string SucursalNombre { get; set; }
        public int TN_IdProducto { get; set; }
        public string ProductoNombre { get; set; }
        public decimal TN_Precio { get; set; }
        public decimal TN_Descuento { get; set; }
        public int TN_IDTipoOperacion { get; set; }
        public string TipoOperacionNombre { get; set; }
        public int TN_IdProveedor { get; set; }
        public string ProveedorNombre { get; set; }
        public decimal TN_Total { get; set; }
        public string TC_Estado { get; set; }
        public int TN_Cantidad { get; set; } // Add cantidad here
    }
}
