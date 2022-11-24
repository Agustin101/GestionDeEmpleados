using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionDeEmpleados.BLL;
using GestionDeEmpleados.DAL;

namespace GestionDeEmpleados.PL
{
    public partial class frmDepartamentos : Form
    {
        private List<DepartamentoBLL> _departamentos;

        public frmDepartamentos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var depto = ObtenerInformacion();
                ConexionDAL.AgregarDepartamento(depto);
                MessageBox.Show("Departamento agregado con exito.");
                ActualizarRegistros();
                LimpiarEntradas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DepartamentoBLL ObtenerInformacion()
        {
            DepartamentoBLL departamento = new DepartamentoBLL();
            int.TryParse(txtId.Text, out int idResult);
            departamento.Id = idResult;
            departamento.Departamento = txtDepartamento.Text;
            return departamento;
        }

        private void frmDepartamentos_Load(object sender, EventArgs e)
        {
            ActualizarRegistros();
            LimpiarEntradas();
        }

        private void ActualizarRegistros()
        {
            _departamentos = ConexionDAL.ObtenerDepartamentos();
            dgvDepartamentos.DataSource = null;
            dgvDepartamentos.DataSource = _departamentos;
        }

        private void dgvDepartamentos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if(rowIndex >= 0)
            {
                txtId.Text = dgvDepartamentos.Rows[rowIndex].Cells[0].Value.ToString();
                txtDepartamento.Text = dgvDepartamentos.Rows[rowIndex].Cells[1].Value.ToString();
                btnAgregar.Enabled = !true;
                btnBorrar.Enabled = !false;
                btnModificar.Enabled = !false;
                btnCancelar.Enabled = !false;
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if(txtId.Text.Length > 0)
            {
                try
                {
                    int id = int.Parse(txtId.Text);
                    ConexionDAL.EliminarDepartamento(id);
                    ActualizarRegistros();
                    LimpiarEntradas();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese un id o seleccione el departamento a eliminar.");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                DepartamentoBLL depto = ObtenerInformacion();
                ConexionDAL.ActualizarDepartamento(depto);
                ActualizarRegistros();
                LimpiarEntradas();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimpiarEntradas()
        {
            txtDepartamento.Text = "";
            txtId.Text = "";

            btnAgregar.Enabled = true;
            btnBorrar.Enabled = false;
            btnModificar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarEntradas();
        }
    }
}
