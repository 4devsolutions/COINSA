using NominaNF.Data.Contrato;
using NominaNF.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NominaNF.Data.Implementacion
{
    public class PuestoCmbData : IPuestoCmbData<Puesto>
    {
        private readonly string _cadenaSql = "";
        public PuestoCmbData(IConfiguration configuration)
        {
            _cadenaSql = configuration.GetConnectionString("cadenaConexion")!;
        }

        public async Task<IEnumerable<Puesto>> ObtienePuestosCmb()
        {
            List<Puesto> puestos = new List<Puesto>();

            using (var conexion = new SqlConnection(_cadenaSql))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFObtienePuestosCmb", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        puestos.Add(new Puesto
                        {
                            ClaPuesto = Convert.ToInt32(dr["ClaPuesto"]),
                            NomPuesto = dr["NomPuesto"]?.ToString() ?? ""
                        });
                    }
                }
            }

            return puestos;
        }
    }

    }

