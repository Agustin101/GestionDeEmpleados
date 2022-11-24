using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GestionDeEmpleados.BLL;
using System.Data;

namespace GestionDeEmpleados.DAL
{
    internal static class ConexionDAL
    {
        private static string _connectionString;
        private static SqlConnection _sqlConnection;
        private static SqlCommand _sqlCommand;

        static ConexionDAL()
        {
            _connectionString = "Server=localhost;Database=GestionEmpleados_DB;Trusted_Connection=True;";
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandType = System.Data.CommandType.Text;
        }

        public static void AgregarDepartamento(DepartamentoBLL departamento)
        {
            try
            {
                _sqlCommand.Parameters.Clear();
                _sqlCommand.CommandText = "insert into Departamentos(departamento) values(@departamento)";
                _sqlCommand.Parameters.AddWithValue("@departamento", departamento.Departamento);
                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }

        }

        public static List<DepartamentoBLL> ObtenerDepartamentos()
        {
            List<DepartamentoBLL> departamentos = new List<DepartamentoBLL>();

            try
            {
                _sqlCommand.Parameters.Clear();
                _sqlCommand.CommandText = "select * from Departamentos";
                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();
                SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    departamentos.Add(new DepartamentoBLL()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Departamento = sqlDataReader["departamento"].ToString()
                    }) ;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close(); 
            }

            return departamentos;
        }

        public static void EliminarDepartamento(int id)
        {
            try
            {
                _sqlCommand.Parameters.Clear();
                _sqlCommand.CommandText = "delete from Departamentos where id = @id";
                _sqlCommand.Parameters.AddWithValue("@id", id);
                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }


        }

        public static void ActualizarDepartamento(DepartamentoBLL departamento)
        {
            try
            {
                _sqlCommand.Parameters.Clear();
                _sqlCommand.CommandText = "update Departamentos set departamento = @departamento where id = @id";
                _sqlCommand.Parameters.AddWithValue("@id", departamento.Id);
                _sqlCommand.Parameters.AddWithValue("@departamento", departamento.Departamento);
                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        internal static void AgregarEmpleado(EmpleadoBLL emp)
        {
            try
            {
                _sqlCommand.Parameters.Clear();
                _sqlCommand.CommandText = "insert into Empleados(nombre,primerApellido,segundoApellido,correo,foto) values(@nombre,@pApellido,@sApellido,@correo,@foto)";
                _sqlCommand.Parameters.AddWithValue("@nombre", emp.Nombre);
                _sqlCommand.Parameters.AddWithValue("@pApellido", emp.PrimerApellido);
                _sqlCommand.Parameters.AddWithValue("@sApellido", emp.SegundoApellido);
                _sqlCommand.Parameters.AddWithValue("@correo", emp.Correo);
                _sqlCommand.Parameters.AddWithValue("@foto", emp.FotoEmpleado);
                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        internal static List<EmpleadoBLL> ObtenerEmpleados()
        {
            List<EmpleadoBLL> empleados = new List<EmpleadoBLL>();

            try
            {
                _sqlCommand.Parameters.Clear();
                _sqlCommand.CommandText = "select * from Empleados";
                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();
                SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    empleados.Add(new EmpleadoBLL()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Nombre = sqlDataReader["nombre"].ToString(),
                        PrimerApellido = sqlDataReader["primerApellido"].ToString(),
                        SegundoApellido = sqlDataReader["segundoApellido"].ToString(),
                        Correo = sqlDataReader["correo"].ToString(),
                        FotoEmpleado = Encoding.ASCII.GetBytes(sqlDataReader["foto"].ToString())
                    });
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return empleados;
        }
    }
}
