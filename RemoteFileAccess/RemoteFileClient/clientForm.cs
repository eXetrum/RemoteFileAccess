﻿using System;
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
        // 
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
                    // Создаем точку подключения, взяв айпи аддресс и порт из текстовых полей
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(txt_server_address.Text), int.Parse(txt_server_port.Text));
                    // Создаем потоковый сокет используюя протокол TCP
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // Соединяемся с удаленным аддрессом
                    socket.Connect(ep);
                    // Если соединение успешно
                    if (socket.Connected)
                    {
                        // Выключаем кнопку "соединиться"
                        btn_connect.Enabled = false;
                        // Включаем кнопку "отсоединиться"
                        btn_disconnect.Enabled = true;
                        // Включаем кнопку поиска файла
                        btn_search.Enabled = true;
                        // Включаем поле ввода для поиска файла
                        txt_file_name.Enabled = true;
                        // Создаем объект соединения, который облегчит обработку отправки данных и приема ответов от сервера
                        client = new Client(socket);
                        // Создаем пустой пакет, и укажим тип сообщения "LIST" - попросим сервер вернуть содержимое текущего каталога
                        Packet p = new Packet(MSG_TYPE.LIST, null);
                        // Отправляем пакет серверу
                        client.SendData(p);
                        // Создаем обработчики событий приема данных и отключения пользователя
                        client.Received += client_Received;
                        client.Disconnected += client_Disconnected;
                        // Запускаем асинхронный прием данных от сервера
                        client.ReceiveAsyncStart();                      
                    }
                }
                    // Отлавливаем ошибки
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        void client_Disconnected(Client sender)
        {
            // Отключение
            Disconnect();
        }

        void client_Received(Client sender, Packet p)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    // Обрабатываем сообщения от сервера
                    switch (p.msg)
                    {
                            // Тип сообщения "LIST" (получаем список файлов/папок)
                        case MSG_TYPE.LIST:
                            {
                                // Очищаем список
                                fileList.Items.Clear();
                                // Получаем массив байт из пакета и преобразуем набор байт в объект типа список
                                List<FileSystemInfo> files = (List<FileSystemInfo>)Packet.ByteArrayToObject(p.data);
                                // Если первый элемент задан (корневой каталог для текущего расположения пользователя на сервере)
                                if (files[0] != null)
                                {
                                    // Создаем новый элемент
                                    ListViewItem li = new ListViewItem("..");
                                    // Прикрепляем объект файловой информации
                                    li.Tag = files[0];
                                    // Добавляем в список
                                    fileList.Items.Add(li);
                                }
                                // Удаляем информацию о корневом каталоге
                                files.RemoveAt(0);
                                // Добавляем оставшиеся файлы и папки в список отображения
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
                            // Тип ответа на запрос поиска
                        case MSG_TYPE.SEARCH:
                            {
                                // Получаем массив строк результатов
                                string[] searchResult = (string[])Packet.ByteArrayToObject(p.data);
                                // Если массив строк пуст
                                if (searchResult.Length == 0)
                                {
                                    MessageBox.Show("Файл не найден !");
                                }
                                    // Иначе выводим все найденные пути для заданного файла
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
            // Отключаемся 
            if (client != null)
            {
                // Закрываем сокет
                client.Close();
                // Вкл/выкл. кнопки управления
                btn_connect.Enabled = true;
                btn_disconnect.Enabled = false;
                btn_search.Enabled = false;
                txt_file_name.Enabled = false;
                // Очищаем список файлов
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
                // Формируем пакет, выставляем тип сообщения - поиск
                Packet p = new Packet(MSG_TYPE.SEARCH, Packet.encoding.GetBytes(txt_file_name.Text));
                // Отправляем данные
                int s = client.SendData(p);
                //if(s > 0)
                    //txt_file_name.Clear();                
            }
                // Отлавливаем ошибки
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void clientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если объект клиента создан - закрываем соединение
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
                // Получаем файловую инфо прикрепленную к выделоенному элементу списка
                FileSystemInfo fsi = lvi.Tag as FileSystemInfo;
                // Проверим папка или нет выделена
                bool isDirectory = ((fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory);
                // Если папка
                if (isDirectory)
                {
                    // Формируем пакет, тип сообщения - смена каталога
                    Packet p = new Packet(MSG_TYPE.CWD, Packet.encoding.GetBytes(fsi.FullName));
                    // Отправляем пакет
                    client.SendData(p);
                }
            }
        }
        // Отображаем меню только если есть выделенные элементы
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
                // Получаем выделенный элемент
                ListViewItem lvi = fileList.SelectedItems[0];
                // Получаем файловую информацию прикрепленную к выделенному пункту списка
                FileSystemInfo fsi = lvi.Tag as FileSystemInfo;
                // Создаем форму для показа аттрибутов файла
                settingsForm settings = new settingsForm(this);
                // Задаем название файла 
                settings.lbl_name.Text = fsi.Name;
                // Задаем путь к файлу на сервере
                settings.lbl_full_path.Text = fsi.FullName;

                fileInfo = fsi;
                // Получаем аттрибуты
                bool isDirectory = ((fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory);
                bool isArchive = ((fsi.Attributes & FileAttributes.Archive) == FileAttributes.Archive);
                bool isCompress = ((fsi.Attributes & FileAttributes.Compressed) == FileAttributes.Compressed);
                bool isEncrypt = ((fsi.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted);
                bool isNormal = ((fsi.Attributes & FileAttributes.Normal) == FileAttributes.Normal);
                bool isHidden = ((fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);
                bool isSystem = ((fsi.Attributes & FileAttributes.System) == FileAttributes.System);
                bool isReadOnly = ((fsi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
                // Запоминаем начальное состояние чекбоксов
                settings.initState.Add(settings.cb_directory.Name, (isDirectory) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_archive.Name, (isArchive) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_compressed.Name, (isCompress ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_encrypted.Name, (isEncrypt ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_normal.Name, (isNormal ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_hidden.Name, (isHidden ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_system.Name, (isSystem ) ? CheckState.Checked : CheckState.Unchecked);
                settings.initState.Add(settings.cb_readonly.Name, (isReadOnly ) ? CheckState.Checked : CheckState.Unchecked);
                // Выставляем начальное состояние чекбоксов
                settings.cb_directory.Checked = isDirectory;
                settings.cb_archive.Checked = isArchive;
                settings.cb_compressed.Checked = isCompress;
                settings.cb_encrypted.Checked = isEncrypt;
                settings.cb_normal.Checked = isNormal;
                settings.cb_hidden.Checked = isHidden;
                settings.cb_system.Checked = isSystem;
                settings.cb_readonly.Checked = isReadOnly;
                // Прикрепляем к каждому чекбоксу обработчик события изменения состояния
                settings.cb_directory.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_archive.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_compressed.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_encrypted.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_normal.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_hidden.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_system.CheckStateChanged += settings.cb_CheckStateChanged;
                settings.cb_readonly.CheckStateChanged += settings.cb_CheckStateChanged;
                // Если выделен файл
                if (!isDirectory)
                {
                    FileInfo fi = (FileInfo)fsi;
                    // Получим и выведем его размер
                    settings.lbl_size.Text = fi.Length.ToString();
                }
                // Вызываем показ диалога
                settings.ShowDialog(this);
                // По закрытии диалога проверяем нужно ли изменять аттрибуты
                if (apply)
                {
                    // Применяем новые аттрибуты
                    fsi.Attributes = fsi.Attributes | fileInfo.Attributes;
                    // Формируем пакет для смены аттрибутов на сервере
                    Packet p = new Packet(MSG_TYPE.SET_ATTRIBUTE, Packet.ObjectToByteArray(fsi));
                    // Отправляем пакет серверу
                    client.SendData(p);
                }
            }
        }

        
    }
}
