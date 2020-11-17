namespace Tool
{
    partial class FormView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxDrawing = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonClearCounter = new System.Windows.Forms.Button();
            this.buttonRunManual = new System.Windows.Forms.Button();
            this.buttonRunAuto = new System.Windows.Forms.Button();
            this.groupBoxDrawing.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDrawing
            // 
            this.groupBoxDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDrawing.AutoSize = true;
            this.groupBoxDrawing.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxDrawing.Location = new System.Drawing.Point(799, 12);
            this.groupBoxDrawing.Name = "groupBoxDrawing";
            this.groupBoxDrawing.Size = new System.Drawing.Size(173, 687);
            this.groupBoxDrawing.TabIndex = 2;
            this.groupBoxDrawing.TabStop = false;
            this.groupBoxDrawing.Text = "Điều Khiển";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonSelect, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonDraw, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSettings, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonClearCounter, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonRunManual, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonRunAuto, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(157, 659);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.Color.Orange;
            this.buttonSelect.Font = new System.Drawing.Font("MS Outlook", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSelect.Location = new System.Drawing.Point(3, 3);
            this.buttonSelect.MinimumSize = new System.Drawing.Size(150, 75);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(150, 75);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "CHỌN HÌNH \r\nSẢN PHẨM";
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // buttonDraw
            // 
            this.buttonDraw.BackColor = System.Drawing.Color.Orange;
            this.buttonDraw.Font = new System.Drawing.Font("MS Outlook", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDraw.Location = new System.Drawing.Point(3, 112);
            this.buttonDraw.MinimumSize = new System.Drawing.Size(150, 75);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(150, 75);
            this.buttonDraw.TabIndex = 2;
            this.buttonDraw.Text = "VẼ\r\nSẢN PHẨM";
            this.buttonDraw.UseVisualStyleBackColor = false;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.Orange;
            this.buttonSettings.Font = new System.Drawing.Font("MS Outlook", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettings.Location = new System.Drawing.Point(3, 221);
            this.buttonSettings.MinimumSize = new System.Drawing.Size(150, 75);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(150, 75);
            this.buttonSettings.TabIndex = 3;
            this.buttonSettings.Text = "CÀI ĐẶT";
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonClearCounter
            // 
            this.buttonClearCounter.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonClearCounter.Font = new System.Drawing.Font("MS Outlook", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClearCounter.Location = new System.Drawing.Point(3, 330);
            this.buttonClearCounter.MinimumSize = new System.Drawing.Size(150, 75);
            this.buttonClearCounter.Name = "buttonClearCounter";
            this.buttonClearCounter.Size = new System.Drawing.Size(150, 75);
            this.buttonClearCounter.TabIndex = 4;
            this.buttonClearCounter.Text = "XÓA ĐẾM";
            this.buttonClearCounter.UseVisualStyleBackColor = false;
            // 
            // buttonRunManual
            // 
            this.buttonRunManual.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonRunManual.Font = new System.Drawing.Font("MS Outlook", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRunManual.Location = new System.Drawing.Point(3, 439);
            this.buttonRunManual.MinimumSize = new System.Drawing.Size(150, 75);
            this.buttonRunManual.Name = "buttonRunManual";
            this.buttonRunManual.Size = new System.Drawing.Size(150, 75);
            this.buttonRunManual.TabIndex = 5;
            this.buttonRunManual.Text = "CHẠY TAY\r\nOFF";
            this.buttonRunManual.UseVisualStyleBackColor = false;
            this.buttonRunManual.Click += new System.EventHandler(this.buttonRunManual_Click);
            // 
            // buttonRunAuto
            // 
            this.buttonRunAuto.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonRunAuto.Font = new System.Drawing.Font("MS Outlook", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRunAuto.Location = new System.Drawing.Point(3, 548);
            this.buttonRunAuto.MinimumSize = new System.Drawing.Size(150, 75);
            this.buttonRunAuto.Name = "buttonRunAuto";
            this.buttonRunAuto.Size = new System.Drawing.Size(150, 75);
            this.buttonRunAuto.TabIndex = 6;
            this.buttonRunAuto.Text = "TỰ ĐỘNG\r\nOFF\r\n";
            this.buttonRunAuto.UseVisualStyleBackColor = false;
            this.buttonRunAuto.Click += new System.EventHandler(this.buttonRunAuto_Click);
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 711);
            this.Controls.Add(this.groupBoxDrawing);
            this.MinimumSize = new System.Drawing.Size(1000, 750);
            this.Name = "FormView";
            this.Text = "Lotus";
            this.groupBoxDrawing.ResumeLayout(false);
            this.groupBoxDrawing.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxDrawing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonClearCounter;
        private System.Windows.Forms.Button buttonRunManual;
        private System.Windows.Forms.Button buttonRunAuto;
    }
}

