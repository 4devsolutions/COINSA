using NominaNF.Data.Contrato;
using NominaNF.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NominaNF.Data.Implementacion
{
    public class ProyectoCmbData : IProyectoCmbData<Proyecto>
    {
        private readonly string _cadenaSql = "";
        public ProyectoCmbData(IConfiguration configuration)
        {
            _cadenaSql = configuration.GetConnectionString("cadenaConexion")!;
        }

        public async Task<IEnumerable<Proyecto>> ObtieneProyectosCmb()
        {
            List<Proyecto> proyectos = new List<Proyecto>();

            using (var conexion = new SqlConnection(_cadenaSql))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFObtieneProyectosCmb", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        proyectos.Add(new Proyecto
                        {
                            ClaProyecto = Convert.ToInt32(dr["ClaProyecto"]),
                            NomProyecto = dr["NomProyecto"]?.ToString() ?? ""
                        });
                    }
                }
            }

            return proyectos;
        }
    }

    }

