namespace RemoteFileServer
{
    partial class serverForm
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
            this.serverLog = new System.Windows.Forms.TextBox();
            this.usersList = new System.Windows.Forms.ListView();
            this.endPoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastCommand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastReceiveTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.currentPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // serverLog
            // 
            this.serverLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverLog.Location = new System.Drawing.Point(12, 345);
            this.serverLog.Multiline = true;
            this.serverLog.Name = "serverLog";
            this.serverLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.serverLog.Size = new System.Drawing.Size(660, 105);
            this.serverLog.TabIndex = 1;
            // 
            // usersList
            // 
            this.usersList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.endPoint,
            this.ID,
            this.lastCommand,
            this.lastReceiveTime,
            this.currentPath});
            this.usersList.FullRowSelect = true;
            this.usersList.GridLines = true;
            this.usersList.Location = new System.Drawing.Point(12, 12);
            this.usersList.Name = "usersList";
            this.usersList.Size = new System.Drawing.Size(660, 327);
            this.usersList.TabIndex = 2;
            this.usersList.UseCompatibleStateImageBehavior = false;
            this.usersList.View = System.Windows.Forms.View.Details;
            // 
            // endPoint
            // 
            this.endPoint.Text = "End Point";
            this.endPoint.Width = 110;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 110;
            // 
            // lastCommand
            // 
            this.lastCommand.Text = "Last Command";
            this.lastCommand.Width = 100;
            // 
            // lastReceiveTime
            // 
            this.lastReceiveTime.Text = "Last Receive Time";
            this.lastReceiveTime.Width = 120;
            // 
            // currentPath
            // 
            this.currentPath.Text = "Current Path";
            this.currentPath.Width = 200;
            // 
            // serverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.usersList);
            this.Controls.Add(this.serverLog);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "serverForm";
            this.Text = "Remote Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.serverForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverLog;
        private System.Windows.Forms.ListView usersList;
        private System.Windows.Forms.ColumnHeader endPoint;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader lastCommand;
        private System.Windows.Forms.ColumnHeader lastReceiveTime;
        private System.Windows.Forms.ColumnHeader currentPath;
    }
}

