using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using New_WSC_DLL;
using DevExpress.XtraGrid.Views.Grid;

namespace _61adj_StoreToOT_SFC
{
    public partial class Form1 : WSC_Sample_Form
    {
        String Login_Server = "PXWMS_N";
        BindingSource BS_Repl = new BindingSource();
        BindingSource BS_OutT = new BindingSource();
        DataSet DS_Repl = new DataSet();
        DataSet DS_OutT = new DataSet();
        DataTable DT_Repl = new DataTable();
        DataTable DT_OutT = new DataTable();
        string UserWorkNum = "", UserPC = "";

        //此表單的LoaderFormInfo
        XSC.LoaderFormInfo fFormInfo;

        //此LoginUserId所使用的sqlClientAccess
        XSC.ClientAccess.sqlClientAccess sca;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void New_WSC_DLL_Form_Load(object sender, EventArgs e)
        {
            base.New_WSC_DLL_Form_Load(sender, e);
            panel_IKnow.Size = new Size(844, 639);
            panel_IKnow.Location = new Point(61, 38);

            //取得此表單的LoaderFormInfo
            fFormInfo = XSC.ClientLoader.FormInfo(this);
            //透過LoginUserId取得sqlClientAccess
            sca = XSC.ClientAccess.UserAccess.sqlUserAccess(fFormInfo.LoginUserId);

            #region 帳號權限判斷
            Boolean IsCorrectUser = false, IsPXWMS_N = false, IsPXWMS_S = false, IsPXWMS_C = false;
            //取得工號
            string cmdstring = "Select EMPID From XSC_Menu_Userlist where XSC_UserID=@userid";
            object[] objParam = { "@userid", SqlDbType.NVarChar.ToString(), fFormInfo.LoginUserId };
            DataTable dt_87 = sca.GetDataTable("EEPDC", cmdstring, objParam, 0);
            if (dt_87.Rows.Count > 0)
            {
                IsCorrectUser = true;
                UserWorkNum = dt_87.Rows[0][0].ToString();
            }

            if (UserWorkNum != "")
            {
                //取得兩倉權限
                //觀音
                cmdstring = "select top 1 0 from employee_data where S_empd_id=@userid";
                object[] objParam1 = { "@userid", SqlDbType.NVarChar.ToString(), UserWorkNum };
                DataTable dt_PXWMS_N = sca.GetDataTable("PXWMS_N", cmdstring, objParam1, 0);
                if (dt_PXWMS_N.Rows.Count > 0)
                {
                    IsPXWMS_N = true;
                }

                //岡山
                DataTable dt_PXWMS_S = sca.GetDataTable("PXWMS_S", cmdstring, objParam1, 0);
                if (dt_PXWMS_S.Rows.Count > 0)
                {
                    IsPXWMS_S = true;
                }

                //梧棲
                DataTable dt_PXWMS_C = sca.GetDataTable("PXWMS_C_Plus2", cmdstring, objParam1, 0);
                if (dt_PXWMS_C.Rows.Count > 0)
                {
                    IsPXWMS_C = true;
                }
            }

            if (IsPXWMS_N == false && IsPXWMS_S == false && IsPXWMS_C == false)
            {
                IsCorrectUser = false;
            }
            #endregion

            if (!IsCorrectUser)
            {
                MessageBox.Show("帳號無法對應三倉權限，請洽資訊部人員", ":<");
                SetMasterBindingNavigator(null);
                SetButtonEnable("L");
                return;
            }

            txb_ItemNo.Text = txb_Merdid.Text;
            SetMasterBindingNavigator(BS_Repl);
            SetButtonEnable("FL");
            if (IsPXWMS_N)
                comboBox1.Items.Add(new Item("觀音", "PXWMS_N"));
            if (IsPXWMS_S)
                comboBox1.Items.Add(new Item("岡山", "PXWMS_S"));
            if (IsPXWMS_C)
                comboBox1.Items.Add(new Item("梧棲", "PXWMS_C_Plus2"));
            comboBox1.Items.Add(new Item("測試", "PXWMS_200"));
            comboBox1.SelectedIndex = 0;

            cB_SFCkind.Items.Add(new Item("SFCXX01", "SFC%01"));
            cB_SFCkind.Items.Add(new Item("SFCXX02", "SFC%02"));
            cB_SFCkind.Items.Add(new Item("出貨暫存", "%%"));
            cB_SFCkind.Items.Add(new Item("任意儲位", "%%"));
            cB_SFCkind.SelectedIndex = 0;

            ResizeForm.ResizeForm.WSC_Resize(this, 1);

            gridControl1.DataSource = BS_Repl;
            DevExpressGridFunctions.GridviewSetup(gridView1);
            DevExpressGridFunctions.GridviewSetup(gridView2);

        }

        #region 共用menu操作
        //下拉式選單
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Login_Server = ((Item)comboBox1.SelectedItem).Value;
        }
        //下拉是選單_調整類型
        private void cB_SFCkind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cB_SFCkind.SelectedIndex >= 2)
            {
                MessageBox.Show("**此選項請小心使用**\n"
                               + "請確定來源及目的儲位不在使用中\n"
                               + "變更後會清除分配量及補貨量");
            }
        }
        //查詢
        protected override void SearchButton_Click(object sender, EventArgs e)
        {
            ClearAll();

            FromStoreAdj();
            StockOutTemp();
            tabControl1.SelectedIndex = 0;
        }
        //查詢營業所過來的調整
        private void FromStoreAdj()
        {
            string str_slodlike = ((Item)cB_SFCkind.SelectedItem).Value;
            string cmdstring = "";

            if (cB_SFCkind.SelectedIndex < 2)
            {
                cmdstring =
                @"select chk=0, L_slom_sysno, S_slom_slodid, S_slom_merdid, L_slom_1qty,
                L_slom_jobid=CASE WHEN left(L_slom_listid,2)='47' THEN L_slom_listid ELSE L_slom_jobid END,
                S_slom_container=ISNULL(b.S_cusd_id,b1.S_cusc_cusdid)+','+ ISNULL(b.N_cusd_sname,c.N_cusd_sname),
                adj_slom_1qty=L_slom_1qty
                from slot_mer with(nolock)
                left join customer_data b with(nolock) on S_slom_container=S_cusd_id
                left join customer_chuteslod b1 with(nolock) on S_slom_slodid=b1.S_cusc_outslodid
                left join customer_data c with(nolock) on b1.S_cusc_cusdid=c.S_cusd_id
                where S_slom_merdid=@merdid
                    and S_slom_slodid like @slodid
                    and L_slom_1qty!=0
              order by S_slom_slodid, S_slom_merdid, L_slom_jobid";
            }
            else if (cB_SFCkind.SelectedIndex == 2)
            {
                cmdstring =
                @"select chk=0, L_slom_sysno, S_slom_slodid, S_slom_merdid, L_slom_1qty,
                L_slom_jobid=CASE WHEN left(L_slom_listid,2)='47' THEN L_slom_listid ELSE L_slom_jobid END,
                S_slom_container=S_cusc_cusdid+','+ c.N_cusd_sname,
                adj_slom_1qty=L_slom_1qty
                from slot_mer with(nolock)
                INNER join customer_chuteslod b1 with(nolock) on S_slom_slodid=b1.S_cusc_outslodid
                INNER join customer_data c with(nolock) on b1.S_cusc_cusdid=c.S_cusd_id
                where S_slom_merdid=@merdid
                    and L_slom_1qty!=0
                order by S_slom_slodid, S_slom_merdid, L_slom_jobid";
            }
            else if (cB_SFCkind.SelectedIndex == 3)
            {
                cmdstring =
                @"select chk=0, L_slom_sysno, S_slom_slodid, S_slom_merdid, L_slom_1qty,
                L_slom_jobid=CASE WHEN left(L_slom_listid,2)='47' THEN L_slom_listid ELSE L_slom_jobid END,
                S_slom_container=S_cusc_cusdid+','+ c.N_cusd_sname,
                adj_slom_1qty=L_slom_1qty
                from slot_mer with(nolock)
                left join customer_chuteslod b1 with(nolock) on S_slom_slodid=b1.S_cusc_outslodid
                left join customer_data c with(nolock) on b1.S_cusc_cusdid=c.S_cusd_id
                where S_slom_merdid=@merdid
                    and L_slom_1qty!=0
                order by S_slom_slodid, S_slom_merdid, L_slom_jobid";
            }
            object[] objPram ={"@jobid",SqlDbType.NVarChar.ToString(),txb_ItemNo.Text,
                              "@merdid",SqlDbType.NVarChar.ToString(),txb_Merdid.Text,
                              "@storeNo",SqlDbType.NVarChar.ToString(), txb_NewSlodId.Text,
                              "@slodid","NVarChar",str_slodlike};
            DS_Repl = sca.GetDataSet(Login_Server, cmdstring, objPram, 0);
            DT_Repl = DS_Repl.Tables[0];
            BS_Repl.DataSource = DS_Repl;
            gridControl1.DataSource = DT_Repl;
        }
        //查詢艙內的SFC暫存區
        private void StockOutTemp()
        {
            string str_slodlike = ((Item)cB_SFCkind.SelectedItem).Value;
            string cmdstring = "";
            if (cB_SFCkind.SelectedIndex <=1)
            {
                cmdstring =
                @"select chk=0, L_slom_sysno, S_slom_slodid, S_slom_merdid, L_slom_1qty,S_cusc_chudid=ISNULL(b.S_cusc_chudid,b1.S_cusc_chudid),
                T_slom_updatedate,
                L_slom_jobid=CASE WHEN left(L_slom_listid,2)='47' THEN L_slom_listid ELSE L_slom_jobid END,
                S_slom_container=ISNULL(b.S_cusc_cusdid,a.S_slom_container)+', '+ISNULL(c.N_cusd_sname,c1.N_cusd_sname)
                  from slot_mer a with(nolock)
                  left join customer_chuteslod b with(nolock) on a.S_slom_slodid=b.S_cusc_outslodid
                  left join customer_data c with(nolock) on b.S_cusc_cusdid=c.S_cusd_id
                  left join customer_chuteslod b1 with(NOLOCK) on a.S_slom_container=b1.S_cusc_cusdid
                  left join customer_data c1 with(nolock) on a.S_slom_container=c1.S_cusd_id
                  where S_slom_merdid=@merdid
                        and S_slom_slodid not in (SELECT S_slod_id from slot_data with(nolock) where S_slod_piczid='VTL' and S_slod_virtualType='M')
                  order by S_slom_slodid, S_slom_merdid, L_slom_jobid";
            }
            else if (cB_SFCkind.SelectedIndex == 2)
            {
                //出貨暫存區限定調整
                cmdstring =
                @"select chk=0, L_slom_sysno, S_slom_slodid, S_slom_merdid, L_slom_1qty,S_cusc_chudid=b.S_cusc_chudid,
                T_slom_updatedate,
                L_slom_jobid=CASE WHEN left(L_slom_listid,2)='47' THEN L_slom_listid ELSE L_slom_jobid END,
                S_slom_container=ISNULL(b.S_cusc_cusdid,a.S_slom_container)+', '+N_cusd_sname
                  from slot_mer a with(nolock)
                  inner join customer_chuteslod b with(nolock) on a.S_slom_slodid=b.S_cusc_outslodid
                  inner join customer_data c with(nolock) on b.S_cusc_cusdid=c.S_cusd_id
                  where S_slom_merdid=@merdid  
                  order by S_slom_slodid, S_slom_merdid, L_slom_jobid";
            }
            else
            {
                cmdstring =
                @"select chk=0, L_slom_sysno, S_slom_slodid, S_slom_merdid, L_slom_1qty,S_cusc_chudid=ISNULL(b.S_cusc_chudid,b1.S_cusc_chudid),
                T_slom_updatedate,
                L_slom_jobid=CASE WHEN left(L_slom_listid,2)='47' THEN L_slom_listid ELSE L_slom_jobid END,
                S_slom_container=ISNULL(b.S_cusc_cusdid,a.S_slom_container)+', '+ISNULL(c.N_cusd_sname,c1.N_cusd_sname)
                  from slot_mer a with(nolock)
                  left join customer_chuteslod b with(nolock) on a.S_slom_slodid=b.S_cusc_outslodid
                  left join customer_data c with(nolock) on b.S_cusc_cusdid=c.S_cusd_id
                  left join customer_chuteslod b1 with(NOLOCK) on a.S_slom_container=b1.S_cusc_cusdid
                  left join customer_data c1 with(nolock) on a.S_slom_container=c1.S_cusd_id
                  where S_slom_merdid=@merdid 
                  order by S_slom_slodid, S_slom_merdid, L_slom_jobid";
            }
            object[] objPram = { "@merdid", SqlDbType.NVarChar.ToString(), txb_Merdid.Text };
            DS_OutT = sca.GetDataSet(Login_Server, cmdstring, objPram, 0);
            DT_OutT = DS_OutT.Tables[0];
            BS_OutT.DataSource = DS_OutT;
            gridControl2.DataSource = DT_OutT;
        }

        //設定gridview只能單選
        int checkedRowIndex1 = -1;
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "chk" && (Int32)e.Value == 1)
            {
                int rowHandle = view.GetRowHandle(checkedRowIndex1);
                view.SetRowCellValue(rowHandle, "chk", false);
                //記錄前一筆打勾的
                checkedRowIndex1 = view.GetDataSourceRowIndex(e.RowHandle);
            }
        }
        int checkedRowIndex2 = -1;
        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "chk" && (Int32)e.Value == 1)
            {
                int rowHandle = view.GetRowHandle(checkedRowIndex2);
                view.SetRowCellValue(rowHandle, "chk", false);
                //記錄前一筆打勾的
                checkedRowIndex2 = view.GetDataSourceRowIndex(e.RowHandle);
            }
        }

        //下拉式選單內容
        public class Item
        {
            public string Name;
            public string Value;
            public Item(string name, string value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }
        #endregion

        #region 頁面:來源儲位
        long OutSlomSysno = 0;
        string ItemNo = "",OriStockQty="", AdjQty = "", StoreNo = "", DoNo = "", VirtualSlodId = "";
        private void btn_Nex_Pg1_Click(object sender, EventArgs e)
        {
            bool IsChked = false;
            foreach (DataRow dr in DT_Repl.Rows)
            {
                if (dr["chk"].ToString() == "1")
                {
                    IsChked = true;
                    OutSlomSysno = Convert.ToInt64(dr["L_slom_sysno"]);
                    ItemNo = dr["S_slom_merdid"].ToString();
                    VirtualSlodId = dr["S_slom_slodid"].ToString();
                    OriStockQty = dr["L_slom_1qty"].ToString();

                    #region 錯誤處理
                    try
                    {
                        int CheckOriStockQty = Convert.ToInt32(dr["adj_slom_1qty"]);
                        if (CheckOriStockQty <= 0)
                        {
                            MessageBox.Show("調整量必定為正數", "錯誤");
                            return;
                        }
                        if (CheckOriStockQty > Convert.ToInt32(OriStockQty))
                        {
                            MessageBox.Show("調整量大於庫存量", "錯誤");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("調整量有誤", "錯誤");
                        return;
                    }
                    #endregion

                    if (cB_SFCkind.SelectedIndex == 1)
                    {
                        AdjQty = (0 - Convert.ToInt32(dr["adj_slom_1qty"])).ToString();
                    }
                    else
                    {
                        AdjQty = dr["adj_slom_1qty"].ToString();
                    }
                    StoreNo = dr["S_slom_container"].ToString();
                    DoNo = dr["L_slom_jobid"].ToString();
                    break;
                }
            }
            if (IsChked == false)
            {
                MessageBox.Show("未選擇來源儲位");
            }
            else
            {
                tabControl1.SelectedIndex += 1;
            }
        }
        #endregion

        #region 頁面:出貨暫存區
        long InSlomSysno = 0;
        string OutItemNo = "", OutStockQty = "", OutStoreNo = "", OutSlodid = "";
        private void btn_Nex_Pg2_Click(object sender, EventArgs e)
        {
            bool IsChked = false;
            foreach (DataRow dr in DT_OutT.Rows)
            {
                if (dr["chk"].ToString() == "1")
                {
                    InSlomSysno = Convert.ToInt64(dr["L_slom_sysno"]);
                    OutItemNo = dr["S_slom_merdid"].ToString();
                    OutStockQty = dr["L_slom_1qty"].ToString();
                    OutStoreNo = dr["S_slom_container"].ToString();
                    OutSlodid = dr["S_slom_slodid"].ToString();

                    IsChked = true;
                    break;
                }
            }
            if (IsChked == false)
            {
                MessageBox.Show("未選擇來源儲位");
            }
            else
            {
                #region 錯誤檢查
                string ErrMsg = "";
                if (InSlomSysno == OutSlomSysno)
                {
                    ErrMsg = "移出儲位與移入儲位相同";
                }
                if (ErrMsg != "")
                {
                    MessageBox.Show(ErrMsg, "錯誤");
                    return;
                }
                #endregion

                tabControl1.SelectedIndex += 1;

                #region 來源儲位
                Lbl_ItemNo.Text = ItemNo;
                Lbl_StockQty.Text = OriStockQty;
                Lbl_StoreNo.Text = StoreNo;
                Lbl_DoNo.Text = DoNo;
                lbl_VirtualStockNo.Text = VirtualSlodId;
                #endregion

                #region 目的儲位
                Lbl_OutItemNo.Text = OutItemNo;
                Lbl_OutStockQty.Text = OutStockQty;
                Lbl_OutStoreNo.Text = OutStoreNo;
                Lbl_OutSlodid.Text = OutSlodid;
                #endregion

                lbl_AdjQty.Text = AdjQty;
                lbl_AdjQty2.Text = lbl_AdjQty.Text;

                #region 預測結果:來源儲位
                lbl_ResultItemNo_ori.Text = Lbl_ItemNo.Text;
                lbl_ResultSlodid_ori.Text = lbl_VirtualStockNo.Text;
                try
                {
                    int ResultQty = 0;
                    if (cB_SFCkind.SelectedIndex == 1)
                    {
                        ResultQty = Convert.ToInt32(OriStockQty) + Convert.ToInt32(AdjQty);
                    }
                    else
                    {
                        ResultQty = Convert.ToInt32(OriStockQty) - Convert.ToInt32(AdjQty);
                    }
                    lbl_ResultQty_ori.Text = ResultQty.ToString();
                }
                catch { }
                #endregion

                #region 預測結果:目的儲位
                lbl_ResultItemNo.Text = Lbl_OutItemNo.Text;
                lbl_ResultSlodid.Text = Lbl_OutSlodid.Text;
                try
                {
                    int ResultQty = 0;
                    ResultQty =Convert.ToInt32(OutStockQty)+ Convert.ToInt32(AdjQty) ;
                    lbl_ResultQty.Text = ResultQty.ToString();
                }
                catch { }
                #endregion
            }
        }
        private void btn_Pre_Pg2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex -= 1;
        }


        //新增儲位
        private void btn_Act_NewInSlodId_Click(object sender, EventArgs e)
        {
            string slodid = txb_NewSlodId.Text,
                    itemno = txb_ItemNo.Text,
                    ErrMsg = "",
                    merlsysno = "", //效期序號
                    cusdidsysno="", //營業所序號
                    Boxunit="";    //箱入中文單位
            bool IsNew = false;

            #region 檢查資料正確性
            string cmdstring =
            @"select top 1 0 from slot_data where S_slod_id=@merdid";
            object[] objPram1 = { "@merdid", SqlDbType.NVarChar.ToString(), slodid };
            DataSet DS_OutT1 = sca.GetDataSet(Login_Server, cmdstring, objPram1, 0);
            if (DS_OutT1.Tables[0].Rows.Count <= 0)
            {
                ErrMsg += "儲位不正確\n";
            }

            cmdstring =
            @"select top 1 0 from mer_data where S_merd_id=@merdid";
            object[] objPram2 = { "@merdid", SqlDbType.NVarChar.ToString(), itemno };
            DataSet DS_OutT2 = sca.GetDataSet(Login_Server, cmdstring, objPram2, 0);
            if (DS_OutT2.Tables[0].Rows.Count <= 0)
            {
                ErrMsg += "貨號不正確\n";
            }
            if (ErrMsg != "")
            {
                MessageBox.Show(ErrMsg);
                return;
            }

            cmdstring =
            @"select top 1 0 from slot_mer where S_slom_slodid=@slodid and S_slom_merdid=@merdid";
            object[] objPram3 = { "@slodid", SqlDbType.NVarChar.ToString(), slodid,
                                  "@merdid", SqlDbType.NVarChar.ToString(), itemno};
            DataSet DS_OutT3 = sca.GetDataSet(Login_Server, cmdstring, objPram3, 0);
            if (DS_OutT3.Tables[0].Rows.Count > 0)
            {
                ErrMsg += "已有儲位\n";
            }
            else
            {
                IsNew = true;
            }
            if (ErrMsg != "")
            {
                MessageBox.Show(ErrMsg);
                return;
            }
            #endregion

            #region 產生新儲位
            if (IsNew == true)
            {
                #region 取得merl_sysno
                //先新增
                cmdstring =
                @"Insert into mer_list
                select top 1 
                L_merl_sysno=(select L_merd_sysno from mer_data where S_merd_id=@merdid),
                S_merl_merpgroup='01',
                S_merl_lotno='',
                D_merl_expdate=getdate()+90,
                S_merl_supdid='',
                S_merl_sbuid='1',
                I_merl_mersid=10,
                D_merl_creatdate=GETDATE(),
                D_merl_recdate=NULL
                from mer_list
                where L_merl_merdsysno=(select L_merd_sysno from mer_data where S_merd_id=@merdid)
                and Not exists (select top 1 *
                from mer_list
                where L_merl_merdsysno=(select L_merd_sysno from mer_data where S_merd_id=@merdid)
	                and S_merl_sbuid='1')";
                sca.Update(Login_Server, cmdstring, objPram3, 0);

                //再取得
                cmdstring = @"select top 1 L_merl_sysno
                from mer_list
                where L_merl_merdsysno=(select L_merd_sysno from mer_data where S_merd_id=@merdid)
	                and S_merl_sbuid='1'";
                DataTable Dt_OutT4 = sca.GetDataTable(Login_Server, cmdstring, objPram3, 0);
                if (Dt_OutT4.Rows.Count > 0)

                    merlsysno = Dt_OutT4.Rows[0][0].ToString();
                else
                    merlsysno = "0";
                #endregion

                #region 取得營業所sysno
                cmdstring = @"select b.L_cusd_sysno
                from customer_chuteslod a
                inner join customer_data b on a.S_cusc_cusdid=b.S_cusd_id
                where S_cusc_outslodid=@slodid";
                DataTable Dt_OutT5 = sca.GetDataTable(Login_Server, cmdstring, objPram3, 0);
                if (Dt_OutT5.Rows.Count > 0)
                    cusdidsysno = Dt_OutT5.Rows[0][0].ToString();
                else
                    cusdidsysno = "0";
                #endregion

                #region 取得 unit
                cmdstring = @"select S_merp_unit
                from mer_package
                where S_merp_merdid=@merdid
	                and I_merp_boxflag=1";
                DataTable Dt_OutT6 = sca.GetDataTable(Login_Server, cmdstring, objPram3, 0);
                if (Dt_OutT6.Rows.Count > 0)
                    Boxunit = Dt_OutT6.Rows[0][0].ToString();
                else
                    Boxunit = "箱";
                #endregion
                //新增
                cmdstring = @"Insert Into slot_mer
		        (L_slom_merdsysno,
L_slom_merlsysno,
S_slom_slodid,
S_slom_merdid,
s_slom_owndid,
L_slom_1qty,
L_slom_reserqty,
L_slom_replqty,
L_slom_qty1,
L_slom_qty2,
L_slom_qty3,
S_slom_unit1,
S_slom_unit2,
S_slom_unit3,
L_slom_listid,
T_slom_recidate,
L_slom_recsid,
T_slom_creatdate,
T_slom_updatedate,
F_slom_invbulk,
F_slom_wei,
S_slom_container,
L_slom_jobid,
S_slom_empdid,
S_slom_upempdid)
select 
L_slom_merdsysno=L_merd_sysno
,L_slom_merlsysno=@merlsysno
,S_slom_slodid=@slod_id
,S_slom_merdid=S_merd_id
,S_slom_owndid='PXMART'
,L_slom_1qty=0
,L_slom_reserqty=0
,L_slom_replqty=0
,L_slom_qty1=0
,L_slom_qty2=0
,L_slom_qty3=0
,S_slom_unit1=@Boxunit
,S_slom_unit1=''
,S_slom_unit1=''
,L_slom_listid=@cusdidsysno
,T_slom_recidate=getdate()
,L_slom_recsid=NULL
,T_slom_creatdate=getdate()
,T_slom_updatedate=getdate()
,F_slom_invbulk=0
,F_slom_wei=0
,S_slom_container=''
,L_slom_jobid=0
,S_slom_empdid='XSC61adj'
,S_slom_upempdid='XSC61adj'
from mer_data
where S_merd_id=@merd_id
    and not EXISTS (select top 1 0 from slot_mer where S_slom_slodid=@slod_id and S_slom_merdid=@merd_id)";
                object[] objPram5 = { "@slod_id", SqlDbType.NVarChar.ToString(), slodid,
                                      "@merd_id", SqlDbType.NVarChar.ToString(), itemno,
                                      "@merlsysno", SqlDbType.NVarChar.ToString(), merlsysno,
                                      "@cusdidsysno", SqlDbType.NVarChar.ToString(),cusdidsysno,
                                      "@Boxunit", SqlDbType.NVarChar.ToString(), Boxunit};
                sca.Update(Login_Server, cmdstring, objPram5, 0);
            }
            #endregion

            btn_NewInSlodId.PerformClick();
            StockOutTemp();
        }
        #endregion

        #region 頁面：確認單據內容

        //下一步
        private void btn_Nex_Pg3_Click(object sender, EventArgs e)
        {
            string ErrMsg = "";
            if (InSlomSysno == 0)
                ErrMsg = "未選擇來源儲位";

            if (OutSlomSysno == 0)
                ErrMsg = "未選擇目的儲位";

            if (rtb_AdjReason.Text == "")
                ErrMsg = "未輸入調整原因";

            if (InSlomSysno == OutSlomSysno)
                ErrMsg = "移出儲位與移入儲位相同";

            if (AdjQty == "0")
                ErrMsg = "調整量不可為零";

            if (ErrMsg != "")
            {
                MessageBox.Show(ErrMsg, "錯誤");
                return;
            }
            else
            {
                tabControl1.SelectedIndex += 1;
                if (cB_SFCkind.SelectedIndex < 2)
                    rtb_AdjReason.Text = "XSC錯帳調整," + rtb_AdjReason.Text;
                else if (cB_SFCkind.SelectedIndex == 2)
                    rtb_AdjReason.Text = "XSC錯帳調整出貨暫存," + rtb_AdjReason.Text;
                else if (cB_SFCkind.SelectedIndex == 3)
                    rtb_AdjReason.Text = "XSC錯帳調整任意儲位," + rtb_AdjReason.Text;
                //產生調整單
                object[] objParam1 = { 
                "@memo", "NVarChar",rtb_AdjReason.Text,
                "@UserId", "NVarChar", UserWorkNum,
                "@OutSlomSysno", "NVarChar", OutSlomSysno,
                "@InslomSysno", "NVarChar", InSlomSysno,
                "@Mode","NVarChar",0,
                "@adjqty",SqlDbType.Int.ToString(),Convert.ToInt32(AdjQty)};
                string cmdstring = @"[spXMS_61adj_StoreToOuttemp]";
                DataTable dt1 = sca.GetDataTable(Login_Server, cmdstring, objParam1, 1);

                ClearAll();

                txb_MetrheadId.Text = dt1.Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 清除頁面所有內容
        /// </summary>
        private void ClearAll()
        {
            #region 清除頁面所有內容
            //Common
            InSlomSysno = 0;
            OutSlomSysno = 0;
            AdjQty = "0";

            //Page1,Page2
            DT_Repl.Clear();
            DT_OutT.Clear();
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;

            //Page3
            rtb_AdjReason.Text = "";
            Lbl_ItemNo.Text = "";
            Lbl_StockQty.Text = "";
            Lbl_StoreNo.Text = "";
            Lbl_DoNo.Text = "";
            lbl_VirtualStockNo.Text = "";

            Lbl_OutItemNo.Text = "";
            Lbl_OutStockQty.Text = "";
            Lbl_OutStoreNo.Text = "";
            Lbl_OutSlodid.Text = "";

            lbl_ResultItemNo.Text = "";
            lbl_ResultQty.Text = "";
            lbl_ResultSlodid.Text = "";

            //Page4
            txb_MetrheadId.Text = "";
            Lbl_ConfirmNo.Text = "尚未確認";
            Lbl_FinsihNo.Text = "尚未完成";
            btn_CfmPage.Enabled = true;
            btn_Finishpage.Enabled = true;
            #endregion

        }

        //前一步
        private void btn_Pre_Pg3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex -= 1;
        }
        #endregion

        #region 頁面：確認並完成單據
        private void btn_ConfirmAndOver_Click(object sender, EventArgs e)
        {
            if (txb_MetrheadId.Text == ""
                || txb_MetrheadId.Text.Length != 13
                || txb_MetrheadId.Text.Substring(0, 2) != "61")
            {
                MessageBox.Show("調整單格式錯誤", "錯誤");
                return;
            }
            else
            {
                btn_CfmPage.PerformClick();
                btn_Finishpage.PerformClick();
            }

        }
        private void btn_CfmPage_Click(object sender, EventArgs e)
        {
            if (txb_MetrheadId.Text == ""
                || txb_MetrheadId.Text.Length != 13
                || txb_MetrheadId.Text.Substring(0, 2) != "61")
            {
                MessageBox.Show("調整單格式錯誤", "錯誤");
                return;
            }
            else
            {
                //確認單據
                string MetrHeadId = txb_MetrheadId.Text;

                object[] objParam1 = { 
                "@memo", "NVarChar",MetrHeadId,
                "@UserId", "NVarChar", UserWorkNum,
                "@OutSlomSysno", "NVarChar", 0,
                "@InslomSysno", "NVarChar", 0,
                "@Mode","NVarChar",1};
                DataTable dt1 = sca.GetDataTable(Login_Server, "[dbo].[spXMS_61adj_StoreToOuttemp]", objParam1, 1);
                if (dt1.Rows[0][0].ToString() == "0")
                {
                    Lbl_ConfirmNo.Text = dt1.Rows[0][1].ToString();
                    btn_CfmPage.Enabled = false;
                }
                else
                {
                    Lbl_ConfirmNo.Text = dt1.Rows[0][1].ToString() + "\n確認單據失敗";
                    return;
                }
            }
        }

        private void btn_Finishpage_Click(object sender, EventArgs e)
        {
            if (txb_MetrheadId.Text == ""
                || txb_MetrheadId.Text.Length != 13
                || txb_MetrheadId.Text.Substring(0, 2) != "61")
            {
                MessageBox.Show("調整單格式錯誤", "錯誤");
                return;
            }
            else
            {
                //完成單據
                string MetrHeadId = txb_MetrheadId.Text;

                //判斷來源及目的儲位名稱, 非SFC的則走特製的一般調整單確認
                string cmdstring =
                @"select MEMO=CASE WHEN left(N_METH_MEMO,8)='XSC錯帳調整,' THEN 1
                    WHEN left(N_METH_MEMO,12)='XSC錯帳調整出貨暫存,' THEN 2
                    WHEN left(N_METH_MEMO,12)='XSC錯帳調整任意儲位,' THEN 3
                    ELSE 0 END
                    from metr_head
                    where L_METH_ID=@MetrHeadId";
                object[] objPram = { "@MetrHeadId", SqlDbType.NVarChar.ToString(), MetrHeadId };
                DataTable dt_CheckMetrHeadType = sca.GetDataTable(Login_Server, cmdstring, objPram, 0);
                if (dt_CheckMetrHeadType.Rows.Count > 0)
                {
                    string MetrHeadType = dt_CheckMetrHeadType.Rows[0][0].ToString();
                    if (MetrHeadType == "1")
                    {
                        object[] objParam2 = { 
                        "@memo", "NVarChar",MetrHeadId,
                        "@UserId", "NVarChar", UserWorkNum,
                        "@OutSlomSysno", "NVarChar", 0,
                        "@InslomSysno", "NVarChar", 0,
                        "@Mode","NVarChar",2};
                        DataTable dt1 = sca.GetDataTable(Login_Server, "[dbo].[spXMS_61adj_StoreToOuttemp]", objParam2, 1);
                        if (dt1.Rows[0][0].ToString() == "0")
                        {
                            Lbl_FinsihNo.Text = dt1.Rows[0][1].ToString();
                            btn_Finishpage.Enabled = false;
                        }
                        else
                        {
                            Lbl_FinsihNo.Text = dt1.Rows[0][1].ToString() + "\n完成單據失敗";
                            return;
                        }
                    }
                    else if (MetrHeadType == "2")
                    {
                        object[] objParam2 = { 
                        "@memo", "NVarChar",MetrHeadId,
                        "@UserId", "NVarChar", UserWorkNum,
                        "@OutSlomSysno", "NVarChar", 0,
                        "@InslomSysno", "NVarChar", 0,
                        "@Mode","NVarChar",4};
                        DataTable dt1 = sca.GetDataTable(Login_Server, "[dbo].[spXMS_61adj_StoreToOuttemp]", objParam2, 1);
                        if (dt1.Rows[0][0].ToString() == "0")
                        {
                            Lbl_FinsihNo.Text = dt1.Rows[0][1].ToString();
                            btn_Finishpage.Enabled = false;
                        }
                        else
                        {
                            Lbl_FinsihNo.Text = dt1.Rows[0][1].ToString() + "\n完成單據失敗";
                            return;
                        }
                    }
                    else if (MetrHeadType == "3")
                    {
                        object[] objParam2 = { 
                        "@memo", "NVarChar",MetrHeadId,
                        "@UserId", "NVarChar", UserWorkNum,
                        "@OutSlomSysno", "NVarChar", 0,
                        "@InslomSysno", "NVarChar", 0,
                        "@Mode","NVarChar",4};
                        DataTable dt1 = sca.GetDataTable(Login_Server, "[dbo].[spXMS_61adj_StoreToOuttemp]", objParam2, 1);
                        if (dt1.Rows[0][0].ToString() == "0")
                        {
                            Lbl_FinsihNo.Text = dt1.Rows[0][1].ToString();
                            btn_Finishpage.Enabled = false;
                        }
                        else
                        {
                            Lbl_FinsihNo.Text = dt1.Rows[0][1].ToString() + "\n完成單據失敗";
                            return;
                        }
                    }
                    else
                    {
                        Lbl_FinsihNo.Text = "\n判斷單據類別失敗，可能非外掛調整單";
                        return;
                    }
                }
                else
                {
                    Lbl_FinsihNo.Text = "\n判斷單據類別失敗，可能非外掛調整單";
                    return;
                }


            }
        }
        #endregion

        #region 頁面：新增目的儲位
        private void btn_NewInSlodId_Click(object sender, EventArgs e)
        {
            txb_ItemNo.Text = txb_Merdid.Text;

            Button button = sender as Button;
            int x = button.Location.X - panel2.Width;
            int y = button.Location.Y;
            panel2.Location = new Point(x, y);
            panel2.Visible = !panel2.Visible;
        }
        #endregion

        #region UI
        //覆寫panel的外框
        private void Details_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.Red, 5);
            e.Graphics.DrawRectangle(pen1,
              e.ClipRectangle.Left,
              e.ClipRectangle.Top,
              e.ClipRectangle.Width - 1,
              e.ClipRectangle.Height - 1);
            base.OnPaint(e);
        }

        /// <summary>
        /// 說明視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_IKnow_Click(object sender, EventArgs e)
        {
            panel_IKnow.Visible = false;
        }
        #endregion

    }
}
