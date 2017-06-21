namespace _65adj_addon1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_StartAdj = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_corhid = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox_65adj = new System.Windows.Forms.RichTextBox();
            this.button_65adj = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_65adj = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lbl_POConfirm_Step4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_POConfirm_Step3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txb_POConfirm_PONO = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(855, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(129, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(111, 17);
            this.toolStripStatusLabel2.Text = "WMS使用者工號：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.ItemSize = new System.Drawing.Size(76, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 112);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(855, 432);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.button_Reset);
            this.tabPage1.Controls.Add(this.button_StartAdj);
            this.tabPage1.Controls.Add(this.button_Search);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox_corhid);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(847, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "進貨調整";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Location = new System.Drawing.Point(521, 213);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 157);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "流程狀態(顯示程式結果,免操作)";
            this.groupBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "<--程式一定要通過這關";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoCheck = false;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 26);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(139, 20);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "無空白事業單位";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoCheck = false;
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(6, 130);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(155, 20);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "刪除多餘驗收資料";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoCheck = false;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 52);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(123, 20);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "更改調整儲位";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoCheck = false;
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 104);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(155, 20);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "改回原始調整儲位";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoCheck = false;
            this.checkBox3.AutoSize = true;
            this.checkBox3.ForeColor = System.Drawing.Color.Red;
            this.checkBox3.Location = new System.Drawing.Point(6, 78);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(91, 20);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "確認單據";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView1.Location = new System.Drawing.Point(12, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(784, 128);
            this.dataGridView1.TabIndex = 19;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "S_cori_merdid";
            this.Column1.HeaderText = "貨號";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "D_cori_newexpdate";
            this.Column2.HeaderText = "調整單日期";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "S_cori_newsbuid";
            this.Column3.HeaderText = "調整單事業單位";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "D_merl_expdate";
            this.Column4.HeaderText = "驗收日期";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "S_merl_sbuid";
            this.Column5.HeaderText = "驗收事業單位";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "L_merl_merdsysno";
            this.Column6.HeaderText = "系統碼";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // button_Reset
            // 
            this.button_Reset.Enabled = false;
            this.button_Reset.Location = new System.Drawing.Point(12, 265);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(99, 36);
            this.button_Reset.TabIndex = 18;
            this.button_Reset.Text = "3.重置";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_StartAdj
            // 
            this.button_StartAdj.Enabled = false;
            this.button_StartAdj.Location = new System.Drawing.Point(12, 213);
            this.button_StartAdj.Name = "button_StartAdj";
            this.button_StartAdj.Size = new System.Drawing.Size(99, 36);
            this.button_StartAdj.TabIndex = 17;
            this.button_StartAdj.Text = "2.開始調整";
            this.button_StartAdj.UseVisualStyleBackColor = true;
            this.button_StartAdj.Click += new System.EventHandler(this.button_StartAdj_Click);
            // 
            // button_Search
            // 
            this.button_Search.Location = new System.Drawing.Point(245, 46);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(75, 27);
            this.button_Search.TabIndex = 16;
            this.button_Search.Text = "1.查詢";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "調整單號";
            // 
            // textBox_corhid
            // 
            this.textBox_corhid.Location = new System.Drawing.Point(89, 46);
            this.textBox_corhid.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_corhid.MaxLength = 20;
            this.textBox_corhid.Name = "textBox_corhid";
            this.textBox_corhid.Size = new System.Drawing.Size(148, 27);
            this.textBox_corhid.TabIndex = 14;
            this.textBox_corhid.Text = "6515030200009";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox_65adj);
            this.tabPage2.Controls.Add(this.button_65adj);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBox_65adj);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(847, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "退廠調整";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox_65adj
            // 
            this.richTextBox_65adj.Location = new System.Drawing.Point(12, 60);
            this.richTextBox_65adj.Name = "richTextBox_65adj";
            this.richTextBox_65adj.Size = new System.Drawing.Size(827, 231);
            this.richTextBox_65adj.TabIndex = 20;
            this.richTextBox_65adj.Text = "";
            // 
            // button_65adj
            // 
            this.button_65adj.Location = new System.Drawing.Point(322, 18);
            this.button_65adj.Name = "button_65adj";
            this.button_65adj.Size = new System.Drawing.Size(110, 27);
            this.button_65adj.TabIndex = 19;
            this.button_65adj.Text = "產生調整單";
            this.button_65adj.UseVisualStyleBackColor = true;
            this.button_65adj.Click += new System.EventHandler(this.button_65adj_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = " 營業所退倉單號";
            // 
            // textBox_65adj
            // 
            this.textBox_65adj.Location = new System.Drawing.Point(141, 18);
            this.textBox_65adj.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_65adj.MaxLength = 20;
            this.textBox_65adj.Name = "textBox_65adj";
            this.textBox_65adj.Size = new System.Drawing.Size(174, 27);
            this.textBox_65adj.TabIndex = 17;
            this.textBox_65adj.Text = "R1234567890123456789";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.comboBox2);
            this.tabPage3.Controls.Add(this.lbl_POConfirm_Step4);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.lbl_POConfirm_Step3);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.txb_POConfirm_PONO);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(847, 394);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "進貨單確認";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "執行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "日結取消",
            "未報到驗收"});
            this.comboBox2.Location = new System.Drawing.Point(128, 46);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 24);
            this.comboBox2.TabIndex = 22;
            // 
            // lbl_POConfirm_Step4
            // 
            this.lbl_POConfirm_Step4.AutoSize = true;
            this.lbl_POConfirm_Step4.ForeColor = System.Drawing.Color.Red;
            this.lbl_POConfirm_Step4.Location = new System.Drawing.Point(17, 255);
            this.lbl_POConfirm_Step4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_POConfirm_Step4.Name = "lbl_POConfirm_Step4";
            this.lbl_POConfirm_Step4.Size = new System.Drawing.Size(248, 16);
            this.lbl_POConfirm_Step4.TabIndex = 25;
            this.lbl_POConfirm_Step4.Text = "修改並確認調整單65160601000123";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 228);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 16);
            this.label9.TabIndex = 24;
            this.label9.Text = "4.接下來你要做甚麼";
            // 
            // lbl_POConfirm_Step3
            // 
            this.lbl_POConfirm_Step3.AutoSize = true;
            this.lbl_POConfirm_Step3.ForeColor = System.Drawing.Color.Blue;
            this.lbl_POConfirm_Step3.Location = new System.Drawing.Point(17, 113);
            this.lbl_POConfirm_Step3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_POConfirm_Step3.Name = "lbl_POConfirm_Step3";
            this.lbl_POConfirm_Step3.Size = new System.Drawing.Size(248, 80);
            this.lbl_POConfirm_Step3.TabIndex = 23;
            this.lbl_POConfirm_Step3.Text = "完成    新增報到單31160601000123\r\n完成    新增驗收單33160601000123\r\n完成    新增驗收明細數量=0\r\n完成    上傳關" +
    "貿\r\n完成    新增調整單65160601000123";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 16);
            this.label7.TabIndex = 22;
            this.label7.Text = "3.處理過程如下";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 16);
            this.label6.TabIndex = 21;
            this.label6.Text = "2.選擇異常原因";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "1.輸入PO單號";
            // 
            // txb_POConfirm_PONO
            // 
            this.txb_POConfirm_PONO.Location = new System.Drawing.Point(128, 8);
            this.txb_POConfirm_PONO.Margin = new System.Windows.Forms.Padding(4);
            this.txb_POConfirm_PONO.MaxLength = 13;
            this.txb_POConfirm_PONO.Name = "txb_POConfirm_PONO";
            this.txb_POConfirm_PONO.Size = new System.Drawing.Size(130, 27);
            this.txb_POConfirm_PONO.TabIndex = 19;
            this.txb_POConfirm_PONO.Text = "P160505000559";
            this.txb_POConfirm_PONO.Validated += new System.EventHandler(this.txb_POConfirm_PONO_Validated);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "DC1,觀音",
            "DC2,岡山",
            "DC3,梧棲"});
            this.comboBox1.Location = new System.Drawing.Point(93, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 21;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "倉別";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 309);
            this.panel1.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("新細明體", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "錯帳調整外掛";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Button button_StartAdj;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_corhid;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_65adj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_65adj;
        private System.Windows.Forms.RichTextBox richTextBox_65adj;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_POConfirm_PONO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_POConfirm_Step4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_POConfirm_Step3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}

