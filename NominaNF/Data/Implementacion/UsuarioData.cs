using NominaNF.Data.Contrato;
using NominaNF.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NominaNF.Data.Implementacion
{
    public class UsuarioData : IGenericData<Usuario>
    {
        private readonly string _cadenaSql = "";
        public UsuarioData(IConfiguration configuration)
        {
            _cadenaSql = configuration.GetConnectionString("cadenaConexion")!;
        }

        public async Task<List<Usuario>> Obtiene(int? ClaUsuario = null)
        {
            List<Usuario> _lista = new List<Usuario>();

            using (var conexion = new SqlConnection(_cadenaSql))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("NomNFSch.NomNFObtieneUsuariosSel", conexion);
                cmd.CommandType = CommandType.StoredProcedure;


                if (ClaUsuario.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pnClaUsuario", ClaUsuario);
                }

                using (var dr = await cmd.ExecuteReaderAsync())
                {

                    while (await dr.ReadAsync())
                    {

                        _lista.Add(new Usuario
                        {
                            AccionSp = Convert.ToInt32(dr["AccionSp"]),
                            ClaUsuario = Convert.ToInt32(dr["ClaUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                            ClaPuesto = Convert.ToInt32(dr["ClaPuesto"]),
                            NomPuesto = Convert.ToString(dr["NomPuesto"]),
                            ClaProyecto = Convert.ToInt32(dr["ClaProyecto"]),
                            NomProyecto = Convert.ToString(dr["NomProyecto"]),
                            ClaUbicacion = Convert.ToInt32(dr["ClaUbicacion"]),
                            NomUbicacion = Convert.ToString(dr["NomUbicacion"]),
                            BajaLogica = Convert.ToInt32(dr["BajaLogica"]),

                        }); ;

                    }
                }

                return _lista;

            }
        }

        public async Task<bool> Agrega(Usuario modelo)
        {
            try
            {

                using (var conexion = new SqlConnection(_cadenaSql))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("NomNFSch.NomNFGrabaUsuariosIU", conexion);
                    cmd.Parameters.AddWithValue("@pnAccionSp", 1);
                    cmd.Parameters.AddWithValue("@pnClaUsuario", modelo.ClaUsuario);
                    cmd.Parameters.AddWithValue("@psNombre", modelo.Nombre);
                    cmd.Parameters.AddWithValue("@psApellidoPaterno", modelo.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@psApellidoMaterno", modelo.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@pnClaPuesto", modelo.ClaPuesto);
                    cmd.Parameters.AddWithValue("@pnClaProyecto", modelo.ClaProyecto);
                    cmd.Parameters.AddWithValue("@pnClaUbicacion", modelo.ClaUbicacion);
                    cmd.Parameters.AddWithValue("@pnClaUsuarioMod", modelo.ClaUsuarioMod != null ? modelo.ClaUsuarioMod : 0);
                    cmd.Parameters.AddWithValue("@psNombrePcMod", modelo.NombrePcMod ?? "");
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    return filas_afectadas > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in an appropriate way
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Edita(Usuario modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("NomNFSch.NomNFGrabaUsuariosIU", conexion);
                    cmd.Parameters.AddWithValue("@pnAccionSp", 2);
                    cmd.Parameters.AddWithValue("@pnClaUsuario", modelo.ClaUsuario);
                    cmd.Parameters.AddWithValue("@psNombre", modelo.Nombre);
                    cmd.Parameters.AddWithValue("@psApellidoPaterno", modelo.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@psApellidoMaterno", modelo.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@pnClaPuesto", modelo.ClaPuesto);
                    cmd.Parameters.AddWithValue("@pnClaProyecto", modelo.ClaProyecto);
                    cmd.Parameters.AddWithValue("@pnClaUbicacion", modelo.ClaUbicacion);
                    cmd.Parameters.AddWithValue("@pnClaUsuarioMod", modelo.ClaUsuarioMod != null ? modelo.ClaUsuarioMod : 0);
                    cmd.Parameters.AddWithValue("@psNombrePcMod", modelo.NombrePcMod ?? "");
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    return filas_afectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Elimina(Usuario modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("NomNFSch.NomNFGrabaUsuariosIU", conexion);
                    cmd.Parameters.AddWithValue("@pnAccionSp", 3);
                    cmd.Parameters.AddWithValue("@pnClaUsuario", modelo.ClaUsuario);
                    cmd.Parameters.AddWithValue("@pnClaUsuarioMod", modelo.ClaUsuarioMod != null ? modelo.ClaUsuarioMod : 0);
                    cmd.Parameters.AddWithValue("@psNombrePcMod", modelo.NombrePcMod ?? "");
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    return filas_afectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


    }
}
