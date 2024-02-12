using NominaNF.Models;

namespace NominaNF.Data.Contrato
{
    public interface IProyectoCmbData<T>
    {
        Task<IEnumerable<Proyecto>> ObtieneProyectosCmb();
      
    }
}
