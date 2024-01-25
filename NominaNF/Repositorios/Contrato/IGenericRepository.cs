using NominaNF.Models;

namespace NominaNF.Repositorios.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Consultar();
        Task<bool> Agregar(T modelo);
        Task<bool> Modificar(T modelo);
        Task<bool> Eliminar(int id);
       
    }
}
