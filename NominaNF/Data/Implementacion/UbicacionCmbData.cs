using NominaNF.Data.Contrato;
using NominaNF.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NominaNF.Data.Implementacion
{
    public class UbicacionCmbData : IUbicacionCmbData<Ubicacion>
    {
        private readonly string _cadenaSql = "";
        public UbicacionCmbData(IConfiguration configuration)
        {
            _cadenaSql = configuration.GetConnectionString("cadenaConexion")!;
        }

        public async Task<IEnumerable<Ubicacion>> ObtieneUbicacionesCmb()
        {
            List<Ubicacion> ubicaciones = new List<Ubicacion>();

            using (var conexion = new SqlConnection(_cadenaSql))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFObtieneUbicacionesCmb", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        ubicaciones.Add(new Ubicacion
                        {
                            ClaUbicacion = Convert.ToInt32(dr["ClaUbicacion"]),
                            NomUbicacion = dr["NomUbicacion"]?.ToString() ?? ""
                        });
                    }
                }
            }

            return ubicaciones;
        }
    }

    }

