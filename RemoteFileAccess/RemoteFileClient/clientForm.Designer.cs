namespace RemoteFileClient
{
    partial class clientForm
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
            this.components = new System.ComponentModel.Container();
            this.fileList = new System.Windows.Forms.ListView();
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileAttributes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSettingsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.свойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_server_port = new System.Windows.Forms.TextBox();
            this.txt_server_address = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.txt_file_name = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.fileSettingsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileList
            // 
            this.fileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName,
            this.fileAttributes});
            this.fileList.ContextMenuStrip = this.fileSettingsMenu;
            this.fileList.FullRowSelect = true;
            this.fileList.GridLines = true;
            this.fileList.Location = new System.Drawing.Point(12, 104);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(660, 346);
            this.fileList.TabIndex = 0;
            this.fileList.UseCompatibleStateImageBehavior = false;
            this.fileList.View = System.Windows.Forms.View.Details;
            this.fileList.DoubleClick += new System.EventHandler(this.fileList_DoubleClick);
            // 
            // fileName
            // 
            this.fileName.Text = "File Name";
            this.fileName.Width = 400;
            // 
            // fileAttributes
            // 
            this.fileAttributes.Text = "File Attributes";
            this.fileAttributes.Width = 250;
            // 
            // fileSettingsMenu
            // 
            this.fileSettingsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.свойстваToolStripMenuItem});
            this.fileSettingsMenu.Name = "fileSettingsMenu";
            this.fileSettingsMenu.Size = new System.Drawing.Size(126, 26);
            this.fileSettingsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.fileSettingsMenu_Opening);
            // 
            // свойстваToolStripMenuItem
            // 
            this.свойстваToolStripMenuItem.Name = "свойстваToolStripMenuItem";
            this.свойстваToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.свойстваToolStripMenuItem.Text = "Свойства";
            this.свойстваToolStripMenuItem.Click += new System.EventHandler(this.свойстваToolStripMenuItem_Click);
            // 
            // txt_server_port
            // 
            this.txt_server_port.Location = new System.Drawing.Point(78, 32);
            this.txt_server_port.Name = "txt_server_port";
            this.txt_server_port.Size = new System.Drawing.Size(115, 20);
            this.txt_server_port.TabIndex = 1;
            this.txt_server_port.Text = "8787";
            this.txt_server_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_server_address
            // 
            this.txt_server_address.Location = new System.Drawing.Point(78, 6);
            this.txt_server_address.Name = "txt_server_address";
            this.txt_server_address.Size = new System.Drawing.Size(115, 20);
            this.txt_server_address.TabIndex = 2;
            this.txt_server_address.Text = "192.168.1.107";
            this.txt_server_address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Server Port";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(199, 3);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(97, 25);
            this.btn_connect.TabIndex = 5;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // txt_file_name
            // 
            this.txt_file_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_file_name.Enabled = false;
            this.txt_file_name.Location = new System.Drawing.Point(12, 61);
            this.txt_file_name.Multiline = true;
            this.txt_file_name.Name = "txt_file_name";
            this.txt_file_name.Size = new System.Drawing.Size(554, 37);
            this.txt_file_name.TabIndex = 6;
            this.txt_file_name.TextChanged += new System.EventHandler(this.txt_command_TextChanged);
            // 
            // btn_search
            // 
            this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_search.Enabled = false;
            this.btn_search.Location = new System.Drawing.Point(575, 61);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(97, 37);
            this.btn_search.TabIndex = 7;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Enabled = false;
            this.btn_disconnect.Location = new System.Drawing.Point(199, 29);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(97, 25);
            this.btn_disconnect.TabIndex = 8;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // clientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_file_name);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_server_address);
            this.Controls.Add(this.txt_server_port);
            this.Controls.Add(this.fileList);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "clientForm";
            this.Text = "Client Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.clientForm_FormClosing);
            this.fileSettingsMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView fileList;
        private System.Windows.Forms.TextBox txt_server_port;
        private System.Windows.Forms.TextBox txt_server_address;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox txt_file_name;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader fileAttributes;
        private System.Windows.Forms.ContextMenuStrip fileSettingsMenu;
        private System.Windows.Forms.ToolStripMenuItem свойстваToolStripMenuItem;
    }
}

