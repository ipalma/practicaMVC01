//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rrhhGestion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empleado
    {
        public Empleado()
        {
            this.Proyecto = new HashSet<Proyecto>();
        }
    
        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public string dni { get; set; }
        public int idCargo { get; set; }
        public Nullable<decimal> salario { get; set; }
    
        public virtual Cargo Cargo { get; set; }
        public virtual ICollection<Proyecto> Proyecto { get; set; }
    }
}
