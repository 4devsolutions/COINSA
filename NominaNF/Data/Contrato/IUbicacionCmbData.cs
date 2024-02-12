using NominaNF.Models;

namespace NominaNF.Data.Contrato
{
    public interface IUbicacionCmbData<T>
    {
        Task<IEnumerable<Ubicacion>> ObtieneUbicacionesCmb();
      
    }
}
