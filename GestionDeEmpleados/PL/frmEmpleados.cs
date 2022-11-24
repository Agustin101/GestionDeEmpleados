using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionDeEmpleados.DAL;
using GestionDeEmpleados.BLL;
using System.IO;

namespace GestionDeEmpleados.PL
{
    public partial class frmEmpleados : Form
    {
        private byte[] _imagen;

        public frmEmpleados()
        {
            InitializeComponent();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            var departamentos = ConexionDAL.ObtenerDepartamentos();

            cmbDepartamento.DataSource = departamentos;
            cmbDepartamento.DisplayMember = "departamento";
            cmbDepartamento.ValueMember = "id";
            ActualizarRegistros();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                EmpleadoBLL emp = ObtenerInformacion();
                ConexionDAL.AgregarEmpleado(emp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private EmpleadoBLL ObtenerInformacion()
        {
            EmpleadoBLL empleado = new EmpleadoBLL();
            int id = -1;
            int.TryParse(txtId.Text, out id);

            empleado.Id = id;
            empleado.Nombre = txtNombre.Text;
            empleado.PrimerApellido = txtPrimerApellido.Text;
            empleado.SegundoApellido = txtSegundoApellido.Text;
            empleado.Correo = txtCorreo.Text;
            empleado.Departamento = int.Parse(cmbDepartamento.SelectedValue.ToString());
            empleado.FotoEmpleado = _imagen;
            return empleado;
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectorImagen = new OpenFileDialog();
            selectorImagen.Title = "Seleccionar imagen";

            if(selectorImagen.ShowDialog() == DialogResult.OK)
            {
                picFoto.Image = Image.FromStream(selectorImagen.OpenFile());
                MemoryStream memoria= new MemoryStream();
                picFoto.Image.Save(memoria,System.Drawing.Imaging.ImageFormat.Png);    
                _imagen = memoria.ToArray();
            }


        }

        private void ActualizarRegistros()
        {
            dgvEmpleados.DataSource = null;
            dgvEmpleados.DataSource = ConexionDAL.ObtenerEmpleados();
        }
    }
}
