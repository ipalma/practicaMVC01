using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositorio;


namespace rrhhGestion.Models.ViewModels
{
    public class EmployeeViewModel: IViewModel<Empleado>
    {
        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public string dni { get; set; }
        public int idCargo { get; set; }
        public Nullable<decimal> salario { get; set; }

        public Empleado ToModel()
        {
            var data = new Empleado()
            {
                idEmpleado = idEmpleado,
                nombre = nombre,
                dni = dni,
                idCargo = idCargo,
                salario = salario
            };
            return data;
        }

        public void FromModel(Empleado data)
        {
            idEmpleado = data.idEmpleado;
            nombre = data.nombre;
            dni = data.dni;
            idCargo = data.idCargo;
            salario = data.salario;



        }

        public void UpdateModel(Empleado data)
        {
            data.idEmpleado = idEmpleado;
            data.nombre = nombre;
            data.dni = dni;
            data.idCargo = idCargo;
            data.salario = salario;
        }

        public int[] GetPKint()
        {
            return new[] {idEmpleado};
        }
    }

}