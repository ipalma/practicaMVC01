using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace Repositorio
{
    public class Repositorio<TViewModel, TEntity>: IRepositorio<TViewModel,TEntity> where TViewModel: class,IViewModel<TEntity>, new() where TEntity: class 
    {
        protected DbContext Context;

        public Repositorio(DbContext context)
        {
            Context = context;
        }

        protected DbSet<TEntity> DbSet
        {
            get { return Context.Set<TEntity>(); }
        }
        //Implementar Métodos virtuales, para posibles futuros cambios.

        //Método de agregar nuevos elementos a la base de datos directamente.
        public virtual int Add(TEntity data)
        {
            /********************************************************************************************************
             *
             * 
             ********************************************************************************************************/
            DbSet.Add(data);//Se inserta los datos por parámetro en la base de datos(DbSet).
            int n = 0;
            try
            {
                n = Context.SaveChanges();//Realiza commit de la accion de agregado en la base de datos.
            }
            catch (Exception e)
            {

                //Logs.LogError(e);
                //return null;
            }
            return n;

        }

        //Método de agregar de TViewModel desde la Vista.
        public virtual TViewModel Add(TViewModel data)
        {
            /********************************************************************************************************
             * Para insertar los datos a la vista se realizan los siguientes pasos: 
             * 1º. Obtener los datos desde la base de datos para crear un objeto con los campos de la base de datos. 
             * 2º. Insertar en la base de datos con DbSet.Add(objeto).
             * 3º. Commit a la base de datos
             * 4º. Obtener el id(pk) de los nuevos datos insertados
             ********************************************************************************************************/
            var m = data.ToModel();

            DbSet.Add(m);

            try
            {
                Context.SaveChanges();
                data.FromModel(m);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return data;
        }

        //Método de borrado por id
        public virtual int Delete(int id)
        {
            /********************************************************************************************************
             * Para eliminar datos se realizan los siguientes pasos: 
             * 1º. Se inserta en una variable los datos buscados con el método Find, por el id que ha sido insertado.
             * 2º. Tras obtenerlo, se elimina el objeto entero.
             * 3º. Commit a la base de datos.
             * 4º. Se devuelve el resultado del commit.
             ********************************************************************************************************/
            var m = DbSet.Find(id);//Recuperamos el objeto con el id que ha sido pasado por parámetro.
            DbSet.Remove(m);
            int n = 0;
            try
            {
                n = Context.SaveChanges();//Commit de la accion de borrado.
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }
            return n;
        }

        //Método que elimina los elementos que han sido introducidos por la expresion lambda.
        public virtual int Delete(Expression<Func<TEntity, bool>> datlam)
        {
            /********************************************************************************************************
             * Para eliminar datos por expresión se realizará del siguiente modo:
             * 1º. Buscamos los datos solicitados en la expresión por parámetro a través de la funcion Where().
             * 2º. Eliminamos el rango de objetos que cumplían los requisitos de la expresión. RemoveRange().
             * 3º. Commit a la base de datos.
             * 4º. Se devuelve el resultado del commit.
             ********************************************************************************************************/
            var datos = DbSet.Where(datlam); //Recuperamos el objeto obtenido de la expresion lambda pasado por el parámetro datlam.
            DbSet.RemoveRange(datos);
            int n = 0;
            try
            {
                n = Context.SaveChanges();
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }
            return n;

        }

        public TEntity GetModelByPk(TViewModel data)
        {
            var datos = DbSet.Find(data.GetPKint());
            return datos;
        }

        public virtual int Delete(TViewModel data)
        {
            /********************************************************************************************************
             * Para eliminar datos de la vista, se realizará del siguiente modo:
             * 1º. Obtenemos los datos del objeto TViewModel y le buscamos el id(pk) a través de otro método implemen
             *      tado en la interface IViewModel para recuperar los ids(pk).
             * 2º. Se elimina a través del método Remove();
             * 3º. Commit a la base de datos.
             * 4º. Se devuelve el resultado del commit.
             ********************************************************************************************************/
            var datos = GetModelByPk(data);
            DbSet.Remove(datos);

            int n = 0;

            try
            {
                n = Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return n;
        }

        //Método que actualiza los datos de un elemento
        public virtual int Update(TViewModel data)
        {
            /********************************************************************************************************
             * Para actualizar los datos de la vista:
             * 1º. Buscar los datos a los que hace referencia lo pasado por parámetro.
             * 2º. Llamamos al método de actualización de IViewModel(UpdateModel) para actualizar la base de datos.
             * 3º. Commit a la base de datos.
             * 4º. Se devuelve el resultado del commit
             ********************************************************************************************************/
            var datos = GetModelByPk(data);
            data.UpdateModel(datos);
            //Context.Entry(data).State = EntityState.Modified;//Esto muestra el estado del objeto, aquí en estado modificado.
            int n = 0;

            try
            {
                n = Context.SaveChanges();
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }
            return n;
        }

        //Método que devuelve todos los elementos de una tabla.
        public List<TViewModel> Get()
        {
            /********************************************************************************************************
             * Obtener todos los datos que hay en la base de datos:
             * 1º. Insertamos todos los objetos en una variable (datos)
             * 2º. Crear objeto de tipo List<TViewModel>
             * 3º. Se recorren todos los datos que contiene la variable (datos) a través de un foreach
             * 4º. Dentro del foreach vamos introduciendo los valores que se van obteniendo de otro objeto de tipo 
             *      TViewModel desde lo que se ha asignado en la variable entity.
             * 5º. Los datos se van insertando en el objeto de tipo List<TViewModel> de lo obtenido en el objeto de 
             *      tipo TViewModel.
             * 6º. Se devuelven los datos del objeto tipo List<TViewModel>
             ********************************************************************************************************/
            var datos = DbSet;

            List<TViewModel> list = new List<TViewModel>();

            foreach (var entity in datos)
            {
                TViewModel v = new TViewModel();
                v.FromModel(entity);
                list.Add(v);
            }
            return list;

            //return DbSet.ToList();
        }

        //Método que devuelve los elementos que han sido pasados por parámetro como expresion lambda
        public List<TViewModel> Get(Expression<Func<TEntity, bool>> datlam)
        {

            /********************************************************************************************************
             * Obtener todos los datos que hay en la base de datos:
             * 1º. Insertamos todos los objetos en una variable (datos)
             * 2º. Crear objeto de tipo List<TViewModel>
             * 3º. Se recorren todos los datos que contiene la variable (datos) a través de un foreach
             * 4º. Dentro del foreach vamos introduciendo los valores que se van obteniendo de otro objeto de tipo 
             *      TViewModel desde lo que se ha asignado en la variable entity.
             * 5º. Los datos se van insertando en el objeto de tipo List<TViewModel> de lo obtenido en el objeto de 
             *      tipo TViewModel.
             * 6º. Se devuelven los datos del objeto tipo List<TViewModel>
             ********************************************************************************************************/
            var datos = DbSet.Where(datlam);

            List<TViewModel> list = new List<TViewModel>();

            foreach (var entity in datos)
            {
                TViewModel v = new TViewModel();
                v.FromModel(entity);
                list.Add(v);
            }
            return list;

            //return DbSet.Where(datlam).ToList();
        }

        //Método que devuelve el elemento que ha sido consultado por la clave primaria.
        public virtual TViewModel Get(int pk)
        {
            /********************************************************************************************************
             * La obtención de un objeto TViewModel por Primary Key
             * 
             ********************************************************************************************************/
            var v = new TViewModel();
            var entity = DbSet.Find();
            v.FromModel(entity);
            return v;

            //return DbSet.Find(pk);
        }
    }
}