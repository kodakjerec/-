using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using New_WSC_DLL;
using System.Data.SqlClient;

//1.使用者輸入單號
//2.檢查調整單的儲位是否與驗收單的儲位相同
//  如不相同則改變調整單的儲位
//  ex:mer_list裡面沒有事業單位"1"的儲位則發出警告找資訊部人員修改
//3.調整確認完後檢查recirecord_item有沒有重複資料
//  如有則調換資料
namespace _65adj_addon1
{
    public partial class Form1 : WSC_Sample_Form
    {
        //此表單的LoaderFormInfo
        XSC.LoaderFormInfo fFormInfo;

        //此LoginUserId所使用的sqlClientAccess
        XSC.ClientAccess.sqlClientAccess sca;

        string conn_Sel = "pxwms_n";
        string cmdstring = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetMasterBindingNavigator(null);
            tabPage1.BackColor = this.BackColor;
            tabPage2.BackColor = this.BackColor;
            tabPage3.BackColor = this.BackColor;

            //取得此表單的LoaderFormInfo
            fFormInfo = XSC.ClientLoader.FormInfo(this);
            //透過LoginUserId取得sqlClientAccess
            sca = XSC.ClientAccess.UserAccess.sqlUserAccess(fFormInfo.LoginUserId);
            //fFormInfo.UserId;
            comboBox1.SelectedIndex = 0;

            object[] objParam = { "@userid", SqlDbType.NVarChar.ToString(), fFormInfo.LoginUserId };

            cmdstring = "Select EMPID From XSC_Menu_Userlist where XSC_UserID=@userid";
            DataTable dt = sca.GetDataTable("EEPDC", cmdstring, objParam, 0);
            if (dt.Rows.Count > 0)
                Global.Globalparameter.UserName = dt.Rows[0]["EMPID"].ToString();
            else
            {
                MessageBox.Show("找不到工號，無法使用外掛");
                Global.Globalparameter.UserName = "";
                button_Search.Enabled = false;
            }

            toolStripStatusLabel1.Text = "XSC使用者：" + fFormInfo.UserId;
            toolStripStatusLabel2.Text = " WMS使用者工號：" + Global.Globalparameter.UserName;
        }

        #region 進貨調整
        private void button_Search_Click(object sender, EventArgs e)
        {
            #region 檢驗有沒有調整單存在
            cmdstring = "Select top 1 I_corh_flag from cort_head where L_corh_id=@corhid and I_corh_type=3";
            object[] objParam = { "@corhid", SqlDbType.NVarChar.ToString(), textBox_corhid.Text };

            DataTable dt1 = sca.GetDataTable(conn_Sel, cmdstring, objParam, 0);
            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("無此單號/_\\");
                return;
            }
            if (dt1.Rows[0][0].ToString() == "60")
            {
                MessageBox.Show("單據已經完成");
                return;
            }
            dt1.Clear();
            #endregion

            #region 帶出調整單內容
            cmdstring = @"select b.S_cori_merdid, b.D_cori_newexpdate, b.S_cori_newsbuid, d.D_merl_expdate,d.S_merl_sbuid,d.L_merl_merdsysno
                    from cort_head a inner join cort_item b on a.L_corh_id=b.L_cori_corhid
                    left join recirecord_item c on a.L_corh_jobid=c.L_reri_recrid and b.L_cori_merdsysno=c.L_reri_merdsysno
                    left join mer_list d on c.L_reri_merlsysno=d.L_merl_sysno
                    where L_corh_id=@corhid and I_corh_type=3 ";
            DataTable dt2 = sca.GetDataTable(conn_Sel, cmdstring, objParam, 0);
            if (dt2.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt2;
            }
            else
            {
                MessageBox.Show("無調整資料/_\\");
                return;
            }
            #endregion

            #region 檢查mer_list有沒有空白事業單位的資料
            foreach (DataRow dr in dt2.Rows)
            {
                if (dr["S_merl_sbuid"].ToString() == "")
                {
                    MessageBox.Show("事業單位不可為空白，請洽資訊部人員");
                    return;
                }
            }
            checkBox1.Checked = true;
            button_StartAdj.Enabled = true;
            #endregion
        }

        private void button_StartAdj_Click(object sender, EventArgs e)
        {
            DataTable dt2 = (DataTable)dataGridView1.DataSource;
            string ErrorString = "";
            StartLoadingShow("更改資料中");
            button_Search.Enabled = false;
            button_StartAdj.Enabled = false;

            try
            {
                #region Adj v1
                /*
                #region 確認儲位編號沒有空白事業單位
                cmdstring = "update mer_list set S_merl_sbuid='1' where L_merl_merdsysno=@merdsysno and S_merl_sbuid='' ";
                foreach (DataRow dr in dt2.Rows)
                {
                    object[] objParam = { "@merdsysno", SqlDbType.NVarChar.ToString(), dr["L_merl_merdsysno"].ToString() };
                    DataTable dt1 = sca.GetDataTable(conn_Sel, cmdstring, objParam, 0);
                }
                #endregion

                #region 更改調整儲位
                cmdstring = "update cort_item set D_cori_newexpdate=@date,S_cori_newsbuid=@sbuid "
                        + "where L_cori_corhid=@corhid	and S_cori_merdid=@item_no;select Cnt=@@rowcount";
                foreach (DataRow dr in dt2.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand(cmdstring, conn))
                    {
                        if (dr["D_merl_expdate"].ToString() == "")
                            cmd.Parameters.AddWithValue("@date", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@date", DateTime.Parse(dr["D_merl_expdate"].ToString()).ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@sbuid", dr["S_merl_sbuid"].ToString());
                        cmd.Parameters.AddWithValue("@corhid", textBox_corhid.Text);
                        cmd.Parameters.AddWithValue("@item_no", dr["S_cori_merdid"].ToString());
                        conn.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            checkBox2.Checked = true;
                        }
                    }
                }
                #endregion

                #region 確認單據
                using (SqlCommand cmd = new SqlCommand("SP_CORT_CFM", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@L_corh_id", textBox_corhid.Text);
                    cmd.Parameters.AddWithValue("@I_inputtype", 0);
                    cmd.Parameters.AddWithValue("@S_empid", Global.Globalparameter.UserName);
                    cmd.Parameters.AddWithValue("@S_Computer", Global.Globalparameter.UserName);
                    cmd.Parameters.AddWithValue("@S_debug", "N");
                    SqlParameter retnVal1 = cmd.Parameters.Add("@I_result", SqlDbType.Int);
                    retnVal1.Direction = ParameterDirection.Output;
                    SqlParameter retnVal2 = cmd.Parameters.Add("@S_result", SqlDbType.VarChar, 1000);
                    retnVal2.Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    if (Convert.ToInt32(retnVal1.Value) == 0)
                    {
                        checkBox3.Checked = true;
                    }
                    else
                    {
                        ErrorString = retnVal2.Value.ToString();
                    }
                }
                #endregion

                #region 調整儲位改回原本數據
                cmdstring = "update cort_item set D_cori_newexpdate=@date,S_cori_newsbuid=@sbuid "
                        + "where L_cori_corhid=@corhid	and S_cori_merdid=@item_no";
                foreach (DataRow dr in dt2.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand(cmdstring, conn))
                    {
                        cmd.Parameters.Add("@date", SqlDbType.Date);
                        cmd.Parameters["@date"].Value = dr["D_cori_newexpdate"];
                        cmd.Parameters.AddWithValue("@sbuid", dr["S_cori_newsbuid"].ToString());
                        cmd.Parameters.AddWithValue("@corhid", textBox_corhid.Text);
                        cmd.Parameters.AddWithValue("@item_no", dr["S_cori_merdid"].ToString());
                        conn.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            checkBox4.Checked = true;
                        }
                        conn.Close();
                    }
                }
                #endregion
                 */
                #endregion

                #region Adj v2
                cmdstring = @"exec spXSC_cort_65adj01 @corh_id, @UserId, @Computer";
                object[] objParam = { "@corh_id", SqlDbType.NVarChar.ToString(), textBox_corhid.Text,
                                      "@UserId",SqlDbType.NVarChar.ToString(),Global.Globalparameter.UserName,
                                      "@Computer",SqlDbType.NVarChar.ToString(),Global.Globalparameter.UserName};
                DataTable dt1 = sca.GetDataTable(conn_Sel, cmdstring, objParam, 0);
                ErrorString = "單據已經確認完成";
                #endregion
            }
            catch (Exception e1)
            {
                ErrorString = e1.Message;
            }
            finally
            {
                CloseLoadingShow();
            }

            #region 檢查是否同一張驗收單有多筆同貨號明細
            #endregion

            button_Reset.Enabled = true;
            if (ErrorString != "")
            {
                MessageBox.Show(ErrorString);
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            button_Search.Enabled = true;
            button_Reset.Enabled = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
        }
        #endregion

        #region 退廠調整
        private void button_65adj_Click(object sender, EventArgs e)
        {
            try
            {
                string strsp = @"Declare @code int,@str varchar(200);exec spWMS_Insert_65adj @work_no,@user,@Directflag,@code output,@str output;select @str";
                object[] objsp = { "@work_no", "NVarChar", textBox_65adj.Text ,
                               "@user","NVarChar", Global.Globalparameter.UserName,
                               "@Directflag","NVarChar",1};
                DataTable dtsp = sca.GetDataTable(conn_Sel, strsp, objsp, 0);
                richTextBox_65adj.Text += DateTime.Now.ToString() + "  ";
                richTextBox_65adj.Text += textBox_65adj.Text + "  Result: " + dtsp.Rows[0][0].ToString() + "\n";
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        #endregion

        /// <summary>
        /// 切換倉別
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //決定南北倉
            if (comboBox1.SelectedIndex == 0)
            {
                conn_Sel = "pxwms_n";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                conn_Sel = "pxwms_s";
            }
            else
            {
                conn_Sel = "PXWMS_C_Plus2";
            }
        }

        private void txb_POConfirm_PONO_Validated(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
