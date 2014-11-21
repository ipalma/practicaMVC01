using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public interface IRepositorio<TViewModel,TEntity>
    {
        TViewModel Add(TViewModel data);
        int Delete(int id);
        int Delete(TViewModel id);
        int Delete(Expression<Func<TEntity, bool>> datlam);
        int Update(TViewModel data);
        List<TViewModel> Get();
        List<TViewModel> Get(Expression<Func<TEntity, bool>> datlam);
        TViewModel Get(int pk);
        TEntity GetModelByPk(TViewModel data);

    }
}
