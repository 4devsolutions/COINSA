using NominaNF.Models;

namespace NominaNF.Data.Contrato
{
    public interface IGenericData<T> where T : class
    {
        Task<List<T>> Obtiene(int? ClaUsuario = null);
        Task<bool> Agrega(T modelo);
        Task<bool> Edita(T modelo);
        Task<bool> Elimina(T modelo);

    }
}
