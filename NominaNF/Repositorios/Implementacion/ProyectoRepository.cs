using NominaNF.Models;
using NominaNF.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NominaNF.Repositorios.Implementacion
{
    public class ProyectoRepository : IGenericRepository<Proyecto>
    {
        private readonly string _cadenaSql = "";
        public ProyectoRepository(IConfiguration configuration)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            _cadenaSql = configuration.GetConnectionString("cadenaConexion");
#pragma warning restore CS8601 // Possible null reference assignment.

        }

        public async Task<List<Proyecto>> Consultar()
        {
                List<Proyecto> _lista = new List<Proyecto>();
            
                using(var conexion = new SqlConnection(_cadenaSql)) { 
            
                conexion.Open();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFConsultaProyectoProc", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using(var dr = await cmd.ExecuteReaderAsync()) {
                
                    while (await dr.ReadAsync())
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
                        _lista.Add(new Proyecto
                        {
                            ClaProyecto = Convert.ToInt32(dr["ClaProyecto"]),
                            NomProyecto = dr["NomProyecto"].ToString(),
                            BajaLogica = Convert.ToInt32(dr["BajaLogica"]),
                            ClaUsuarioMod = Convert.ToInt32(dr["ClaUsuarioMod"]),
                            NombrePcMod = dr["NombrePcMod"].ToString()


                        }); ;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
                    }
                }

                return _lista;

            }
        }

        public async Task<bool> Agregar(Proyecto modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSql))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFAgregaProyectoProc", conexion);
                cmd.Parameters.AddWithValue("pnClaProyecto", modelo.ClaProyecto);
                cmd.Parameters.AddWithValue("psNomProyecto", modelo.NomProyecto);
                cmd.Parameters.AddWithValue("pnClaUsuarioMod", modelo.ClaUsuarioMod);
                cmd.Parameters.AddWithValue("psNombrePcMod", "Pruebas");
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Modificar(Proyecto modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSql))
                {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFModificaProyectoProc", conexion);
                cmd.Parameters.AddWithValue("pnClaProyecto", modelo.ClaProyecto);
                cmd.Parameters.AddWithValue("psNomProyecto", modelo.NomProyecto);
                cmd.Parameters.AddWithValue("pnClaUsuarioMod", modelo.ClaUsuarioMod);
                cmd.Parameters.AddWithValue("psNombrePcMod", "Pruebas");
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSql))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFEliminaProyectoProc", conexion);
                cmd.Parameters.AddWithValue("pnClaProyecto", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
        }


    }
}
