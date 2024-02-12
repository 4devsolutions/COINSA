using NominaNF.Data.Contrato;
using NominaNF.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NominaNF.Data.Implementacion
{
    public class ProyectoData : IGenericData<Proyecto>
    {
        private readonly string _cadenaSql = "";
        public ProyectoData(IConfiguration configuration)
        {
            _cadenaSql = configuration.GetConnectionString("cadenaConexion")!;
        }

        public async Task<List<Proyecto>> Obtiene(int? ClaUsuario = null)
        {
            List<Proyecto> _lista = new List<Proyecto>();

            using (var conexion = new SqlConnection(_cadenaSql))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFObtieneProyectosSel", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {

                    while (await dr.ReadAsync())
                    {

                        _lista.Add(new Proyecto
                        {
                            ClaProyecto = Convert.ToInt32(dr["ClaProyecto"]),
                            NomProyecto = dr["NomProyecto"]?.ToString() ?? "",
                            BajaLogica = Convert.ToInt32(dr["BajaLogica"])
                            //ClaUsuarioMod = Convert.ToInt32(dr["ClaUsuarioMod"]),
                            //NombrePcMod = dr["NombrePcMod"]?.ToString() ?? ""


                        }); ;

                    }
                }

                return _lista;

            }
        }

        public async Task<bool> Agrega(Proyecto modelo)
        {
            try
            {

                using (var conexion = new SqlConnection(_cadenaSql))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("NomNFSch.NomNFGrabaProyectosIU", conexion);
                    cmd.Parameters.AddWithValue("pnAccionSp", 1);
                    cmd.Parameters.AddWithValue("pnClaProyecto", modelo.ClaProyecto);
                    cmd.Parameters.AddWithValue("psNomProyecto", modelo.NomProyecto);
                    cmd.Parameters.AddWithValue("pnClaUsuarioMod", modelo.ClaUsuarioMod != null ? modelo.ClaUsuarioMod : 0);
                    cmd.Parameters.AddWithValue("psNombrePcMod", modelo.NombrePcMod ?? "");
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    return filas_afectadas > 0;
                }
            }catch (Exception ex)
            {
                // Log the exception or handle it in an appropriate way
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Edita(Proyecto modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("NomNFSch.NomNFGrabaProyectosIU", conexion);
                    cmd.Parameters.AddWithValue("pnAccionSp", 2);
                    cmd.Parameters.AddWithValue("pnClaProyecto", modelo.ClaProyecto);
                    cmd.Parameters.AddWithValue("psNomProyecto", modelo.NomProyecto);
                    cmd.Parameters.AddWithValue("pnClaUsuarioMod", modelo.ClaUsuarioMod != null ? modelo.ClaUsuarioMod : 0);
                    cmd.Parameters.AddWithValue("psNombrePcMod", modelo.NombrePcMod ?? "");
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    return filas_afectadas > 0;
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Elimina(Proyecto modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("NomNFSch.NomNFGrabaProyectosIU", conexion);
                    cmd.Parameters.AddWithValue("pnAccionSp", 3);
                    cmd.Parameters.AddWithValue("pnClaProyecto", modelo.ClaProyecto);
                    cmd.Parameters.AddWithValue("pnClaUsuarioMod", modelo.ClaUsuarioMod != null ? modelo.ClaUsuarioMod : 0);
                    cmd.Parameters.AddWithValue("psNombrePcMod", modelo.NombrePcMod ?? "");
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                   return filas_afectadas > 0;
                }
            }catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public async Task<List<Proyecto>> ObtieneProyectosCmb(int? ClaUsuario = null)
        {
            List<Proyecto> _lista = new List<Proyecto>();

            using (var conexion = new SqlConnection(_cadenaSql))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFObtieneProyectosCmb", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {

                    while (await dr.ReadAsync())
                    {

                        _lista.Add(new Proyecto
                        {
                            ClaProyecto = Convert.ToInt32(dr["ClaProyecto"]),
                            NomProyecto = dr["NomProyecto"]?.ToString() ?? "",
                         
                        }); ;

                    }
                }

                return _lista;

            }
        }

    }
}
