using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Oracle.ManagedDataAccess.Client;

namespace Nomina
{
    public partial class Mainpage : Form
    {
        OracleConnection ora = new OracleConnection("User Id=SYSTEM;Password=1935;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=CristianAS)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)))");


        public Mainpage()
        {
            InitializeComponent();
            submenu();
            



        }

        private void submenu()
        {
            panelsubmenu.Visible = false;
            panel1.Visible = false;
        }
        

        private void ocultarmenu()
        {
            if(panelsubmenu.Visible==true) {
            panelsubmenu.Visible = false;
            }

        }
        private void ocultarmenu1()
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
            }

        }
        private void mostrarmenu(Panel submenu)
        {
            if(panelsubmenu.Visible==false) {
                ocultarmenu();
            panelsubmenu.Visible = true;}
            else
            {
                panelsubmenu.Visible = false;
            }
        }
        private void mostrarmenu1(Panel submenu)
        {
            if (panel1.Visible == false)
            {
                ocultarmenu1();
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }






        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Mainpage_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void horafecha_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();
        }

       

        private void btnCuadrito_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Normal;
            
            btnMaximizar.Visible = true;
            btnCuadrito.Visible = false;

        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnCuadrito.Visible = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelmenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelcontenedor_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelcontenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mainpage_Load(object sender, EventArgs e)
        {
            if (panelsubmenu.Visible == false)
            {

                iconButton9.Top -= iconButton9.Height * 4;
                btnContratos.Top -= btnContratos.Height * 4;
                btnDeducciones.Top -= btnDeducciones.Height * 4;
                btnUsuarios.Top -= btnUsuarios.Height * 4;
                btnAyuda.Top -= btnAyuda.Height * 4;
            }
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            mostrarmenu(panelsubmenu);
            if (panelsubmenu.Visible == false)
            {

                iconButton9.Top -= iconButton9.Height * 4;
                btnContratos.Top -= btnContratos.Height * 4;
                btnDeducciones.Top -= btnDeducciones.Height * 4;
                btnUsuarios.Top-= btnUsuarios.Height * 4;
                btnAyuda.Top -= btnAyuda.Height * 4;
            }

            if (panelsubmenu.Visible == true)
            {

                iconButton9.Top += iconButton9.Height * 4;
                btnContratos.Top += btnContratos.Height * 4;
                btnDeducciones.Top += btnDeducciones.Height * 4;
                btnUsuarios.Top += btnUsuarios.Height * 4;
                btnAyuda.Top += btnAyuda.Height * 4;
            }
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
            }
          
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            openChildForm(new RepNomina());
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            openChildForm(new RepUsuarios());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            mostrarmenu1(panel1);
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            ocultarmenu1();
            Documentacion documentacion = new Documentacion();
            documentacion.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new Empleados());
   
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new Nomina());
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            openChildForm(new Departamentos());
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void pnlEmpleados_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }
        private Form activeForm = null;
        private void openChildForm(Form ChildForm)
        {
            if(activeForm!=null)
                activeForm.Close();
            activeForm = ChildForm;
            ChildForm.TopLevel = false;
        
            ChildForm.Dock = DockStyle.Fill;
            panelcontenedor.Controls.Add(ChildForm);
            panelcontenedor.Tag = ChildForm;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            label1.SendToBack();
            label2.SendToBack();   
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void btnDeducciones_Click(object sender, EventArgs e)
        {
            openChildForm(new Deducciones());
        }

        private void btnContratos_Click(object sender, EventArgs e)
        {
            openChildForm(new Contratos());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
        }

        private void iconButton3_Click_1(object sender, EventArgs e)
        {
            openChildForm(new RepEmpleado());
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            ocultarmenu1();
            Empresa empresa = new Empresa();
            empresa.Show();

        }
    }
}
