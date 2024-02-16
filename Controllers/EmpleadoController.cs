using System.Data;
using System.Data.SqlClient;
using Empresa_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Empresa_api.Controllers;
public class EmpleadoController : ControllerBase
{
    #region Variables
    private readonly IConfiguration _context;
    #endregion

    #region Constructor
    public EmpleadoController(IConfiguration context)
    {
        _context = context;
    }
    #endregion

    #region List empleados
    [HttpPost("GET_Empleados")]
    public async Task<List<EmpleadoModel>> GET_Empleados()
    {
        try
        {
            List<EmpleadoModel> list = new List<EmpleadoModel>();
            string connectionstring = _context["ConnectionString:DefaultConnection"];

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "List_Empleados", conn
                ))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Open();


                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        EmpleadoModel emplo = new EmpleadoModel();
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                        emplo.ID_Empleado = sdr["ID_Empleado"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["ID_Empleado"].ToString());
                        emplo.nombre = sdr["nombre"] == DBNull.Value ? "" : sdr["nombre"].ToString();
                        emplo.apellido_paterno = sdr["apellido_paterno"]== DBNull.Value ? "" : sdr["apellido_paterno"].ToString();
                        emplo.apellido_materno = sdr["apellido_materno"] == DBNull.Value ? "" : sdr["apellido_materno"].ToString();
                        emplo.ID_Estado = sdr["ID_Estado"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["ID_Estado"].ToString());
                        emplo.Estado = sdr["Estado"] == DBNull.Value ? "" : sdr["Estado"].ToString();
                        emplo.CreatedBy = sdr["CreatedBy"] is null ? Guid.Empty : Guid.Parse(sdr["CreatedBy"].ToString());
                        emplo.CreatedDate = sdr["CreatedDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(sdr["CreatedDate"].ToString());
                        emplo.UpdatedBy = sdr["UpdatedBy"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["UpdatedBy"].ToString());
                        emplo.UpdatedDate = sdr["UpdatedDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(sdr["UpdatedDate"].ToString());
                        emplo.Active = sdr["Active"] == DBNull.Value ? false : Boolean.Parse(sdr["Active"].ToString());
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                        list.Add(emplo);
                    }
                }
            }

            return list;
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion
}
