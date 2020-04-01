using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace CRUDsederhana
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class koneksi
        {
            private static string oradb = "Data Source=(DESCRIPTION="
                                     + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.23.201)(PORT=1521)))"
                                     + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=svrins)));"
                                     + "User Id=itins;Password=itins789;";
            public static OracleConnection con = new OracleConnection(oradb);
        }

        //TAMPIL DATA KARYAWAN DARI DATABASE
        public void TampilDataKaryawan()
        {
            try
            {
                if (koneksi.con.State == ConnectionState.Open)
                {
                    koneksi.con.Close();
                }
                koneksi.con.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = koneksi.con;
                OracleDataAdapter adp = new OracleDataAdapter(@"SELECT NIK, NAMA, JK, ALAMAT, DEPARTEMEN, CASE AKTIF WHEN 0 THEN 'Tidak Aktif' WHEN 1 THEN 'Aktif' END AS AKTIF
                                                                FROM T_CRUD_KRY ORDER BY NIK", koneksi.con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "T_CRUD_KRY");
                gridControl1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilDataKaryawan();
        }

        //MENYIMPAN DATA KARYAWAN KE DATABASE
        public void SimpanDataKaryawan()
        {
            //membuat koneksi_manual
            if (koneksi.con.State == ConnectionState.Open)
            {
                koneksi.con.Close();
            }
            koneksi.con.Open();
            
            if (txtNIK.Text == "")
            {
                MessageBox.Show("NIK MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtNama.Text == "")
            {
                MessageBox.Show("NAMA MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbJK.Text == "")
            {
                MessageBox.Show("JENIS KELAMIN MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtAlamat.Text == "")
            {
                MessageBox.Show("ALAMAT MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbDept.Text == "")
            {
                MessageBox.Show("DEPARTEMEN MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int val;
                    if (checkAktif.Checked == true)
                    {
                        val = 1;
                    }
                    else
                    {
                        val = 0;
                    }
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = @"INSERT INTO T_CRUD_KRY (NIK, NAMA, JK, ALAMAT, DEPARTEMEN, AKTIF) VALUES ('"+txtNIK.Text+ "', '"+
                                        txtNama.Text+ "', '"+cbJK.Text+ "', '"+txtAlamat.Text+ "', '"+cbDept.Text+ "', '"+val+"')";
                    cmd.Connection = koneksi.con;
                    cmd.ExecuteReader();
                    MessageBox.Show("DATA TELAH DISIMPAN");
                    koneksi.con.Close();
                    TampilDataKaryawan();                    
                }
                catch (Exception ex)
                {
                    // Memunculkan pesan error
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        //UBAH USER
        public void TampilKaryawanEdit()
        {
            if (gridView1.DataRowCount == 0)
            {
                MessageBox.Show("DATA MASIH KOSONG");
            }
            else
            {
                try
                {
                    //membuat koneksi_manual
                    if (koneksi.con.State == ConnectionState.Open)
                    {
                        koneksi.con.Close();
                    }
                    koneksi.con.Open();

                    // Get your currently selected grid row
                    var rowHandle = gridView1.FocusedRowHandle;

                    // Get the value for the given column - convert to the type you're expecting
                    var obj = gridView1.GetRowCellValue(rowHandle, "NIK");

                    //koneksi_manual.con.Open();
                    OracleCommand cmd = new OracleCommand();
                    OracleDataReader dr;

                    cmd.CommandText = @"SELECT NIK, NAMA, JK, ALAMAT, DEPARTEMEN, AKTIF
                                        FROM T_CRUD_KRY
                                        WHERE NIK = '" + obj + "'";
                    cmd.Connection = koneksi.con;
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        txtNIK.Text = dr["NIK"].ToString();
                        txtNIK.ReadOnly = true;
                        txtNama.Text = dr["NAMA"].ToString();
                        cbJK.Text = dr["JK"].ToString();
                        txtAlamat.Text = dr["ALAMAT"].ToString();
                        cbDept.Text = dr["DEPARTEMEN"].ToString();
                        if (dr["AKTIF"].ToString() == "1")
                        {
                            checkAktif.Checked = true;
                        }
                        else
                        {
                            checkAktif.Checked = false;
                        }
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //UBAH DATA KARAYWAN DI DATABASE
        public void UbahDataKaryawan()
        {
            //membuat koneksi_manual
            if (koneksi.con.State == ConnectionState.Open)
            {
                koneksi.con.Close();
            }
            koneksi.con.Open();


            if (txtNIK.Text == "")
            {
                MessageBox.Show("NIK MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtNama.Text == "")
            {
                MessageBox.Show("NAMA MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbJK.Text == "")
            {
                MessageBox.Show("JENIS KELAMIN MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtAlamat.Text == "")
            {
                MessageBox.Show("ALAMAT MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbDept.Text == "")
            {
                MessageBox.Show("DEPARTEMEN MASIH KOSONG", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int val;
                    if (checkAktif.Checked == true)
                    {
                        val = 1;
                    }
                    else
                    {
                        val = 0;
                    }

                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = @"UPDATE T_CRUD_KRY SET NAMA = '"+txtNama.Text+ "', JK = '"+cbJK.Text+ "', ALAMAT = '"+txtAlamat.Text+ "', DEPARTEMEN = '"+cbDept.Text+ "', AKTIF = '"+val+"' WHERE NIK = '" + txtNIK.Text + "'";
                    cmd.Connection = koneksi.con;
                    cmd.ExecuteReader();
                    MessageBox.Show("DATA TELAH DIUPDATE");
                    koneksi.con.Close();
                    TampilDataKaryawan();
                }
                catch (Exception ex)
                {
                    // Memunculkan pesan error
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        //HAPUS DATA KARYAWAN
        public void HapusDataKaryawan()
        {
            try
            {
                if (gridView1.DataRowCount == 0)
                {
                    MessageBox.Show("DATA MASIH KOSONG");
                }
                else
                {
                    if (MessageBox.Show("APAKAH KAMU INGIN MENGHAPUS DATA KARYAWAN INI?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //membuat koneksi_manual
                        if (koneksi.con.State == ConnectionState.Open)
                        {
                            koneksi.con.Close();
                        }
                        koneksi.con.Open();

                        //koneksi_manual.con.Open();
                        OracleCommand cmd = new OracleCommand();

                        // Get your currently selected grid row
                        var rowHandle = gridView1.FocusedRowHandle;

                        // Get the value for the given column - convert to the type you're expecting
                        var obj = gridView1.GetRowCellValue(rowHandle, "NIK");

                        //koneksi_manual.con.Open();
                        cmd.CommandText = "DELETE FROM T_CRUD_KRY WHERE NIK = '" + obj + "'";
                        cmd.Connection = koneksi.con;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Memunculkan pesan error
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void ResetInputan()
        {
            txtNIK.ResetText();
            txtNIK.ReadOnly = false;
            txtNama.ResetText();
            cbJK.ResetText();
            txtAlamat.ResetText();
            cbDept.ResetText();
            checkAktif.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SimpanDataKaryawan();
            ResetInputan();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UbahDataKaryawan();
            ResetInputan();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            TampilDataKaryawan();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ResetInputan();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            HapusDataKaryawan();
            TampilDataKaryawan();
            ResetInputan();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            TampilKaryawanEdit();
        }
    }
}
