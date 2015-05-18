using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteFileServer;
using System.IO;

namespace RemoteFileClient
{
    public partial class clientForm : Form
    {
        Client client;
        public clientForm()
        {
            InitializeComponent();
            
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(txt_server_address.Text), int.Parse(txt_server_port.Text));
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    socket.Connect(ep);
                    if (socket.Connected)
                    {
                        btn_connect.Enabled = false;
                        btn_disconnect.Enabled = true;
                        btn_search.Enabled = true;
                        txt_file_name.Enabled = true;
                        client = new Client(socket);

                        Packet p = new Packet(MSG_TYPE.LIST, null);

                        client.SendData(Packet.ObjectToByteArray(p));
                        // Создаем обработчики событий приема данных и отключения пользователя
                        client.Received += client_Received;
                        client.Disconnected += client_Disconnected;
                        client.ReceiveAsyncStart();                      
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        void client_Disconnected(Client sender)
        {
            Disconnect();
        }

        void client_Received(Client sender, Packet p)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    switch (p.msg)
                    {
                        case MSG_TYPE.LIST:
                            {
                                fileList.Items.Clear();
                                List<FileSystemInfo> files = (List<FileSystemInfo>)Packet.ByteArrayToObject(p.data);
                                if (files[0] != null)
                                {
                                    ListViewItem li = new ListViewItem("..");
                                    li.Tag = files[0];
                                    fileList.Items.Add(li);
                                }
                                files.RemoveAt(0);

                                foreach(var f in files)
                                {
                                    ListViewItem lvi = new ListViewItem();
                                    lvi.Text = f.Name;
                                    lvi.Tag = f;
                                    lvi.SubItems.Add(f.Attributes.ToString());
                                    fileList.Items.Add(lvi);
                                }
                            }
                            break;
                        case MSG_TYPE.SEARCH:
                            {
                                string[] searchResult = (string[])Packet.ByteArrayToObject(p.data);
                                if (searchResult.Length == 0)
                                {
                                    MessageBox.Show("Файл не найден !");
                                }
                                else
                                {
                                    foreach (var s in searchResult)
                                        MessageBox.Show("Файл найден по аддрессу: " + s);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    
                    
                }
                // Отлавливаем ошибки
                catch (Exception ex) { MessageBox.Show(ex.Message); Console.WriteLine("Data received: " + ex.Message); }
            });
        }

        void Disconnect()
        {
            if (client != null)
            {
                client.Close();
                btn_connect.Enabled = true;
                btn_disconnect.Enabled = false;
                btn_search.Enabled = false;
                txt_file_name.Enabled = false;
                fileList.Items.Clear();
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                Packet p = new Packet(MSG_TYPE.SEARCH, Packet.encoding.GetBytes(txt_file_name.Text));
                int s = client.SendData(Packet.ObjectToByteArray(p));
                // Получаем размер пакета и отправляем(сигнализирует начало пересылки пакета)
                // Отправляем пакет                
                //if(s > 0)
                    //txt_file_name.Clear();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void clientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
                client.Close();

        }

        private void txt_command_TextChanged(object sender, EventArgs e)
        {
            btn_search.Enabled = txt_file_name.Text.Length > 0 && client != null;
        }

        private void fileList_DoubleClick(object sender, EventArgs e)
        {
            ListView listView = (sender as ListView);
            if (listView.SelectedItems.Count == 1)
            {
                // Получаем выделенный элемент списка
                ListViewItem lvi = listView.SelectedItems[0];
                FileSystemInfo fsi = lvi.Tag as FileSystemInfo;

                
                
                // check whether a file is read only
                bool isReadOnly = ((fsi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);

                // check whether a file is hidden
                bool isHidden = ((fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);

                // check whether a file has archive attribute
                bool isArchive = ((fsi.Attributes & FileAttributes.Archive) == FileAttributes.Archive);

                // check whether a file is system file
                bool isSystem = ((fsi.Attributes & FileAttributes.System) == FileAttributes.System);

                bool isDirectory = ((fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory);

                if (isDirectory)
                {
                    Packet p = new Packet(MSG_TYPE.CWD, Packet.encoding.GetBytes(fsi.FullName));
                    client.SendData(Packet.ObjectToByteArray(p));
                }

                /*MessageBox.Show(string.Format("directory {0}, readonly {1}, hidden {2}, archive {3}, system {4}",
                    isDirectory, isReadOnly, isHidden, isArchive, isSystem));*/
                
            }
        }

        private void fileSettingsMenu_Opening(object sender, CancelEventArgs e)
        {
            if (fileList.SelectedItems.Count <= 0)
                e.Cancel = true;
        }
        public bool apply = false;
        public FileSystemInfo fileInfo;
        private void свойстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileList.SelectedItems.Count == 1)
            {
                ListViewItem lvi = fileList.SelectedItems[0];
                FileSystemInfo fsi = lvi.Tag as FileSystemInfo;
                settingsForm settings = new settingsForm(this);
                settings.lbl_name.Text = fsi.Name;
                settings.lbl_full_path.Text = fsi.FullName;
                fileInfo = fsi;

                

                bool isDirectory = ((fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory);
                bool isArchive = ((fsi.Attributes & FileAttributes.Archive) == FileAttributes.Archive);
                bool isCompress = ((fsi.Attributes & FileAttributes.Compressed) == FileAttributes.Compressed);
                bool isEncrypt = ((fsi.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted);
                bool isNormal = ((fsi.Attributes & FileAttributes.Normal) == FileAttributes.Normal);
                bool isHidden = ((fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);
                bool isSystem = ((fsi.Attributes & FileAttributes.System) == FileAttributes.System);
                bool isReadOnly = ((fsi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);

                settings.initState.Add(settings.cb_directory.Name, (isDirectory) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_archive.Name, (isArchive) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_compressed.Name, (isCompress ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_encrypted.Name, (isEncrypt ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_normal.Name, (isNormal ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_hidden.Name, (isHidden ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_system.Name, (isSystem ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_readonly.Name, (isReadOnly ) ? CheckState.Checked : CheckState.Unchecked);
                

                settings.cb_directory.Checked = isDirectory;
                settings.cb_archive.Checked = isArchive;
                settings.cb_compressed.Checked = isCompress;
                settings.cb_encrypted.Checked = isEncrypt;
                settings.cb_normal.Checked = isNormal;
                settings.cb_hidden.Checked = isHidden;
                settings.cb_system.Checked = isSystem;
                settings.cb_readonly.Checked = isReadOnly;

                settings.cb_directory.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_archive.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_compressed.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_encrypted.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_normal.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_hidden.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_system.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_readonly.CheckStateChanged += settings.cb_CheckStateChanged;


                if (!isDirectory)
                {
                    FileInfo fi = (FileInfo)fsi;
                    settings.lbl_size.Text = fi.Length.ToString();
                }

                settings.ShowDialog(this);

                if (apply)
                {
                    fsi.Attributes = fsi.Attributes | fileInfo.Attributes;
                    Packet p = new Packet(MSG_TYPE.SET_ATTRIBUTE, Packet.ObjectToByteArray(fsi));
                    byte[] data = Packet.ObjectToByteArray(p);
                    client.SendData(data);
                }
            }
        }

        
    }
}
