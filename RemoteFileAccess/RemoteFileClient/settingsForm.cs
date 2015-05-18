using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteFileClient
{
    public partial class settingsForm : Form
    {
        public Dictionary<string, CheckState> initState = new Dictionary<string, CheckState>();
        private List<CheckBox> boxes = new List<CheckBox>();
        clientForm form;

        public settingsForm(clientForm form)
        {
            InitializeComponent();
            this.form = form;
            boxes.Add(cb_directory);
            boxes.Add(cb_archive);
            boxes.Add(cb_compressed);
            boxes.Add(cb_encrypted);
            boxes.Add(cb_normal);
            boxes.Add(cb_hidden);
            boxes.Add(cb_system);
            boxes.Add(cb_readonly);
        }
        

        public void cb_CheckStateChanged(object sender, EventArgs e)
        {
            form.apply = false;
            CheckBox cb = sender as CheckBox;
            foreach (var b in boxes)
            {
                if (initState[b.Name] != b.CheckState)
                {
                    form.apply = true;
                    break;
                }
            }
            btn_apply.Enabled = form.apply;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //form.apply = false;
            Close();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            //form.apply = false;
            form.fileInfo.Attributes = System.IO.FileAttributes.Normal;
            if (cb_directory.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Directory;
            if (cb_archive.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Archive;
            if (cb_compressed.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Compressed;
            if (cb_encrypted.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Encrypted;
            if (cb_hidden.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Hidden;
            if (cb_system.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.System;
            if (cb_readonly.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.ReadOnly;
            if (cb_normal.Checked)
                form.fileInfo.Attributes = form.fileInfo.Attributes | System.IO.FileAttributes.Normal;
            form.apply = true;
            Close();
        }
    }
}
