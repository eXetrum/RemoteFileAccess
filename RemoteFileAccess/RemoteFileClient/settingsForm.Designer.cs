namespace RemoteFileClient
{
    partial class settingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_full_path = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_size = new System.Windows.Forms.Label();
            this.gb_attributes = new System.Windows.Forms.GroupBox();
            this.cb_readonly = new System.Windows.Forms.CheckBox();
            this.cb_system = new System.Windows.Forms.CheckBox();
            this.cb_normal = new System.Windows.Forms.CheckBox();
            this.cb_hidden = new System.Windows.Forms.CheckBox();
            this.cb_encrypted = new System.Windows.Forms.CheckBox();
            this.cb_directory = new System.Windows.Forms.CheckBox();
            this.cb_compressed = new System.Windows.Forms.CheckBox();
            this.cb_archive = new System.Windows.Forms.CheckBox();
            this.btn_apply = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.gb_attributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(121, 13);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(23, 13);
            this.lbl_name.TabIndex = 1;
            this.lbl_name.Text = "null";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Полный путь";
            // 
            // lbl_full_path
            // 
            this.lbl_full_path.AutoSize = true;
            this.lbl_full_path.Location = new System.Drawing.Point(121, 40);
            this.lbl_full_path.Name = "lbl_full_path";
            this.lbl_full_path.Size = new System.Drawing.Size(23, 13);
            this.lbl_full_path.TabIndex = 3;
            this.lbl_full_path.Text = "null";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Размер";
            // 
            // lbl_size
            // 
            this.lbl_size.AutoSize = true;
            this.lbl_size.Location = new System.Drawing.Point(121, 70);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.Size = new System.Drawing.Size(23, 13);
            this.lbl_size.TabIndex = 5;
            this.lbl_size.Text = "null";
            // 
            // gb_attributes
            // 
            this.gb_attributes.Controls.Add(this.cb_readonly);
            this.gb_attributes.Controls.Add(this.cb_system);
            this.gb_attributes.Controls.Add(this.cb_normal);
            this.gb_attributes.Controls.Add(this.cb_hidden);
            this.gb_attributes.Controls.Add(this.cb_encrypted);
            this.gb_attributes.Controls.Add(this.cb_directory);
            this.gb_attributes.Controls.Add(this.cb_compressed);
            this.gb_attributes.Controls.Add(this.cb_archive);
            this.gb_attributes.Location = new System.Drawing.Point(15, 95);
            this.gb_attributes.Name = "gb_attributes";
            this.gb_attributes.Size = new System.Drawing.Size(323, 177);
            this.gb_attributes.TabIndex = 6;
            this.gb_attributes.TabStop = false;
            this.gb_attributes.Text = "Аттрибуты";
            // 
            // cb_readonly
            // 
            this.cb_readonly.AutoSize = true;
            this.cb_readonly.Location = new System.Drawing.Point(209, 134);
            this.cb_readonly.Name = "cb_readonly";
            this.cb_readonly.Size = new System.Drawing.Size(100, 17);
            this.cb_readonly.TabIndex = 7;
            this.cb_readonly.Text = "Только чтение";
            this.cb_readonly.UseVisualStyleBackColor = true;
            // 
            // cb_system
            // 
            this.cb_system.AutoSize = true;
            this.cb_system.Location = new System.Drawing.Point(209, 99);
            this.cb_system.Name = "cb_system";
            this.cb_system.Size = new System.Drawing.Size(84, 17);
            this.cb_system.TabIndex = 6;
            this.cb_system.Text = "Системный";
            this.cb_system.UseVisualStyleBackColor = true;
            // 
            // cb_normal
            // 
            this.cb_normal.AutoSize = true;
            this.cb_normal.Location = new System.Drawing.Point(16, 29);
            this.cb_normal.Name = "cb_normal";
            this.cb_normal.Size = new System.Drawing.Size(92, 17);
            this.cb_normal.TabIndex = 0;
            this.cb_normal.Text = "Нормальный";
            this.cb_normal.UseVisualStyleBackColor = true;
            // 
            // cb_hidden
            // 
            this.cb_hidden.AutoSize = true;
            this.cb_hidden.Location = new System.Drawing.Point(209, 64);
            this.cb_hidden.Name = "cb_hidden";
            this.cb_hidden.Size = new System.Drawing.Size(72, 17);
            this.cb_hidden.TabIndex = 5;
            this.cb_hidden.Text = "Скрытый";
            this.cb_hidden.UseVisualStyleBackColor = true;
            // 
            // cb_encrypted
            // 
            this.cb_encrypted.AutoSize = true;
            this.cb_encrypted.Location = new System.Drawing.Point(16, 134);
            this.cb_encrypted.Name = "cb_encrypted";
            this.cb_encrypted.Size = new System.Drawing.Size(91, 17);
            this.cb_encrypted.TabIndex = 3;
            this.cb_encrypted.Text = "Зашифрован";
            this.cb_encrypted.UseVisualStyleBackColor = true;
            // 
            // cb_directory
            // 
            this.cb_directory.AutoSize = true;
            this.cb_directory.Enabled = false;
            this.cb_directory.Location = new System.Drawing.Point(16, 64);
            this.cb_directory.Name = "cb_directory";
            this.cb_directory.Size = new System.Drawing.Size(88, 17);
            this.cb_directory.TabIndex = 1;
            this.cb_directory.Text = "Директория";
            this.cb_directory.UseVisualStyleBackColor = true;
            // 
            // cb_compressed
            // 
            this.cb_compressed.AutoSize = true;
            this.cb_compressed.Location = new System.Drawing.Point(16, 99);
            this.cb_compressed.Name = "cb_compressed";
            this.cb_compressed.Size = new System.Drawing.Size(66, 17);
            this.cb_compressed.TabIndex = 2;
            this.cb_compressed.Text = "Сжатый";
            this.cb_compressed.UseVisualStyleBackColor = true;
            // 
            // cb_archive
            // 
            this.cb_archive.AutoSize = true;
            this.cb_archive.Location = new System.Drawing.Point(209, 29);
            this.cb_archive.Name = "cb_archive";
            this.cb_archive.Size = new System.Drawing.Size(76, 17);
            this.cb_archive.TabIndex = 4;
            this.cb_archive.Text = "Архивный";
            this.cb_archive.UseVisualStyleBackColor = true;
            // 
            // btn_apply
            // 
            this.btn_apply.Enabled = false;
            this.btn_apply.Location = new System.Drawing.Point(15, 278);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(92, 28);
            this.btn_apply.TabIndex = 7;
            this.btn_apply.Text = "Применить";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(246, 278);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(92, 28);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "Отмена";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // settingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 318);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_apply);
            this.Controls.Add(this.gb_attributes);
            this.Controls.Add(this.lbl_size);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_full_path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "settingsForm";
            this.Text = "Settings";
            this.gb_attributes.ResumeLayout(false);
            this.gb_attributes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gb_attributes;
        public System.Windows.Forms.Label lbl_name;
        public System.Windows.Forms.Label lbl_full_path;
        public System.Windows.Forms.Label lbl_size;
        public System.Windows.Forms.CheckBox cb_readonly;
        public System.Windows.Forms.CheckBox cb_system;
        public System.Windows.Forms.CheckBox cb_normal;
        public System.Windows.Forms.CheckBox cb_hidden;
        public System.Windows.Forms.CheckBox cb_encrypted;
        public System.Windows.Forms.CheckBox cb_directory;
        public System.Windows.Forms.CheckBox cb_compressed;
        public System.Windows.Forms.CheckBox cb_archive;
        public System.Windows.Forms.Button btn_apply;
        public System.Windows.Forms.Button btn_cancel;
    }
}