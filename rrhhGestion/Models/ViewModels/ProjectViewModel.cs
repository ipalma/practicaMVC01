using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositorio;

namespace rrhhGestion.Models.ViewModels
{
    public class ProjectViewModel: IViewModel<Proyecto>
    {
        public int idProyecto { get; set; }
        public string nombre { get; set; }
        public string cliente { get; set; }
        public string descripcion { get; set; }
        public List<EmployeeViewModel> NombreEmpleado { get; set; } 


        public Proyecto ToModel()
        {
            var data = new Proyecto()
            {
                idProyecto = idProyecto,
                nombre = nombre,
                cliente = cliente,
                descripcion = descripcion
            };
            return data;
        }

        public void FromModel(Proyecto data)
        {
            idProyecto = data.idProyecto;
            nombre = data.nombre;
            cliente = data.cliente;
            descripcion = data.descripcion;
            try
            {
                NombreEmpleado = data.Empleado.Select(o => new EmployeeViewModel()
                {
                    idEmpleado = o.idEmpleado,
                    nombre = o.nombre,

                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void UpdateModel(Proyecto data)
        {
            data.idProyecto = idProyecto;
            data.nombre = nombre;
            data.cliente = cliente;
            data.descripcion = descripcion;
        }

        public int[] GetPKint()
        {
            return new[] {idProyecto};
        }
    }
}