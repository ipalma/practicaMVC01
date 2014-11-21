using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Esta clase de tipo interface va a implementar los métodos abstractos para crear un objeto con los campos 
 * que tiene la vista transformandolo en tipo TModel. También va a obtener los datos que tiene 
 * la base de datos*/

namespace Repositorio
{
    public interface IViewModel<TModel> where TModel:class
    {
        TModel ToModel();//Método para crear un objeto desde la vista a la base de datos.
        void FromModel(TModel data);//Obtención de los datos desde la base de datos.
        void UpdateModel(TModel data);//Actualización de un objeto TModel a la base de datos.
        int[] GetPKint();//Obtenemos un array de claves primarias desde la base de datos.
    }
}
