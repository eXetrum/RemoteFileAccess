using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace RemoteFileServer
{
    public partial class serverForm : Form
    {
        Server server;
        public serverForm()
        {
            InitializeComponent();
            try
            {
                server = new Server(Properties.Settings.Default.ServerPort);
                server.ClientAccepted += new Server.ClientAcceptedHandler(server_ClientAccepted);
                server.Start();
                //MessageBox.Show(Directory.GetCurrentDirectory());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void server_ClientAccepted(Socket s)
        {
            try
            {
                // Создаем объект для нового клиента используя выданный сервером сокет
                Client client = new Client(s);
                // Создаем обработчики событий приема данных и отключения пользователя
                client.Received += client_DataReceived;
                client.Disconnected += client_Disconnected;
                client.ReceiveAsyncStart();

                Invoke((MethodInvoker)delegate
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = client.EndPoint.ToString();
                    lvi.Tag = client;
                    lvi.SubItems.Add(client.ID);
                    lvi.SubItems.Add("XX");
                    lvi.SubItems.Add("XX");
                    lvi.SubItems.Add(client.CurrentPath);
                    usersList.Items.Add(lvi);
                    serverLog.AppendText(string.Format("[{0}]: client accepted {1}\r\n", client.EndPoint.ToString(), client.ID));
                });
            }
            // Отлавливаем ошибки
            catch (Exception ex) { Console.WriteLine("Server_Accepted: " + ex.Message); }
        }

        // Клиентский обработчик приема данных
        void client_DataReceived(Client sender, Packet p)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    switch (p.msg)
                    {
                        case MSG_TYPE.LIST:
                            serverLog.AppendText(string.Format("[{0}]: LIST {1}\r\n", sender.EndPoint.ToString(), sender.CurrentPath));
                            sender.SendFolders();
                            break;
                        case MSG_TYPE.CWD:
                            serverLog.AppendText(string.Format("[{0}]: CWD {1}\r\n", sender.EndPoint.ToString(), Packet.encoding.GetString(p.data)));
                            sender.ChangeDir(Packet.encoding.GetString(p.data));
                            sender.SendFolders();
                            break;
                        case MSG_TYPE.SET_ATTRIBUTE:
                            {
                                
                                FileSystemInfo fsi = (FileSystemInfo)Packet.ByteArrayToObject(p.data);
                                serverLog.AppendText(string.Format("[{0}]: SET_ATTRIBUTE for file {1}\r\n", sender.EndPoint.ToString(), fsi.FullName));
                                FileAttributes attr = fsi.Attributes;
                                // clear attr
                                File.SetAttributes(fsi.FullName, FileAttributes.Normal);
                                // apply attr
                                File.SetAttributes(fsi.FullName, attr);           
                                // Refresh file list
                                sender.SendFolders();
                            }
                            break;
                        case MSG_TYPE.SEARCH:
                            {
                                string searchFileName = Packet.encoding.GetString(p.data);
                                serverLog.AppendText(string.Format("[{0}]: SEARCH {1}\r\n", sender.EndPoint.ToString(), searchFileName));                                
                                string[] filePaths = Directory.GetFiles(sender.CurrentPath, searchFileName, SearchOption.AllDirectories);//@"c:\MyDir\", "*.bmp",
                                Packet result = new Packet(MSG_TYPE.SEARCH, Packet.ObjectToByteArray(filePaths));
                                sender.SendData(result);
                            }
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < usersList.Items.Count; ++i)
                    {
                        Client client = usersList.Items[i].Tag as Client;

                        if (client.ID == sender.ID)
                        {
                            usersList.Items[i].SubItems[2].Text = p.msg.ToString();
                            usersList.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                            usersList.Items[i].SubItems[4].Text = client.CurrentPath;
                            break;
                        }
                    }
                    
                }
                // Отлавливаем ошибки
                catch (Exception ex) { MessageBox.Show(ex.Message); Console.WriteLine("Data received: " + ex.Message); }
            });
        }
        // Обработчик события отключения клиента
        void client_Disconnected(Client sender)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    for (int i = 0; i < usersList.Items.Count; ++i)
                    {
                        Client client = usersList.Items[i].Tag as Client;

                        if (client.ID == sender.ID)
                        {
                            serverLog.AppendText(string.Format("[{0}]: client disconnected {1}\r\n", client.EndPoint.ToString(), client.ID));
                            usersList.Items.RemoveAt(i);
                            break;
                        }
                    }
                }
                // Отлавливаем ошибки
                catch (Exception ex) { Console.WriteLine("client_Disconnected event: " + ex.Message); }
            });
        }

        private void serverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                for (int i = 0; i < usersList.Items.Count; ++i)
                {
                    Client client = usersList.Items[i].Tag as Client;
                    client.Close();
                }
                server.Stop();
            }
        }
    }
}
