using NominaNF.Models;

namespace NominaNF.Data.Contrato
{
    public interface IPuestoCmbData<T>
    {
        Task<IEnumerable<Puesto>> ObtienePuestosCmb();
      
    }
}
