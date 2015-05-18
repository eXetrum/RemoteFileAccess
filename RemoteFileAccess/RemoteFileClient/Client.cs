using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace RemoteFileClient
{
    // Класс клиента используем для обмена приватными сообщениями
    public class Client
    {
        // Хендлеры событий: "данные получены", "клиент отсоединен"
        public delegate void DataReceivedEventHandler(Client sender, RemoteFileServer.Packet p);
        public delegate void DisconnectedEventHandler(Client sender);
        public event DataReceivedEventHandler Received;
        public event DisconnectedEventHandler Disconnected;
        // Сетевая точка, откуда пришли данные
        public IPEndPoint EndPoint { get; private set; }
        // Клиентский сокет
        private Socket clientSocket;
        // Размер буфера принимаем как массив байт
        byte[] lenBuffer;
        // Буфер приема
        RemoteFileServer.ReceiveBuffer buffer;
        // Конструктор клиента, принимает открытый сервером сокет для обмена с этим клиентом
        public Client(Socket accepted)
        {
            // Запоминаем сокет
            clientSocket = accepted;
            // Запоминаем откуда законектились
            EndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
            // Создаем буфер размера файла/сообщения
            lenBuffer = new byte[4];
            
        }
        public void ReceiveAsyncStart()
        {
            // Принимаем данные асинхронно
            clientSocket.BeginReceive(lenBuffer, 0, lenBuffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), null);
        }
        public int SendData(byte[] data)
        {
            clientSocket.Send(BitConverter.GetBytes(data.Length), 0, 4, SocketFlags.None);
            int byteSend = clientSocket.Send(data, 0, data.Length, SocketFlags.None);
            return byteSend;
        }
        // Начало приема данных всегда начинается с кусочка о размере последующих данных
        void receiveCallback(IAsyncResult ar)
        {
            try
            {
                // Получаем количество переданных данных
                int rec = clientSocket.EndReceive(ar);
                // Если передано нуль
                if (rec == 0)
                {
                    // Отключаем клиента 
                    if (Disconnected != null)
                    {
                        //MessageBox.Show("something wrong");
                        Disconnected(this);
                        Close();                        
                        return;
                    }
                    // Если размер принятых данных не равен 4 (все пересылки сообщений начинаются с их отправки 4-х байт размера этих сообщений)
                    if (rec != 4)
                    {
                        throw new Exception("Error file size header");
                    }
                }
            }
            // Отлавливаем ошибки
            catch (SocketException se)
            {
                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                        if (Disconnected != null)
                        {
                            Disconnected(this);
                            Close();
                            //MessageBox.Show("SOCKET");
                            return;
                        }
                        break;
                }
            }
            catch (ObjectDisposedException) 
            {
                //MessageBox.Show("Client reciveAsync: objectdisposed");
                return; 
            }
            catch (NullReferenceException) 
            {
                //MessageBox.Show("Client reciveAsync: nullreference");
                return;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Client reciveAsync: " + ex.Message);
                Console.WriteLine("Client reciveAsync: " + ex.Message);
                return;
            }
            // Если размер сообщения принят без ошибок - создаем буфер приема
            buffer = new RemoteFileServer.ReceiveBuffer(BitConverter.ToInt32(lenBuffer, 0));
            // Запускаем ассинхронный прием пакета данных заданного размера
            clientSocket.BeginReceive(buffer.Buffer, 0, buffer.Buffer.Length, SocketFlags.None, new AsyncCallback(receivePacketCallback), null);
        }
        // Калбек функция приема пакета
        void receivePacketCallback(IAsyncResult ar)
        {
            // Получаем размер данных
            int rec = clientSocket.EndReceive(ar);
            if (rec <= 0) {
                // Отключаем клиента
                if (Disconnected != null)
                    Disconnected(this);
                // Закрываем сокет и освобождаем все неиспользуемые объекты
                MessageBox.Show("rec <= 0");
                Close();
                
                return;
            }
            // Добавляем принятые данные в поток
            buffer.memStream.Write(buffer.Buffer, 0, rec);
            // Уменьшаем количество необходимых для приема данных
            buffer.ToReceive -= rec;
            // Если еще не все приняли
            if (buffer.ToReceive > 0)
            {
                // Очищаем маленький буфер приема
                Array.Clear(buffer.Buffer, 0, buffer.Buffer.Length);
                // Запускаем дальнешую процедуру приема
                clientSocket.BeginReceive(buffer.Buffer, 0, buffer.Buffer.Length, SocketFlags.None, receivePacketCallback, null);
                return;
            }
            // Если все приняли - проверим есть ли обработчик события приема
            if (Received != null)
            {
                // Получаем весь принятый массив байт
                byte[] totalReceived = buffer.memStream.ToArray();
                // Формируем полученный пакет, предварительно расшифровав его
                RemoteFileServer.Packet receivedPacket = (RemoteFileServer.Packet)RemoteFileServer.Packet.ByteArrayToObject(totalReceived);
                // Генерируем событие приема
                Received(this, receivedPacket);
            }
            buffer.Dispose();
            // Принимаем данные асинхронно
            clientSocket.BeginReceive(lenBuffer, 0, lenBuffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), null);
        }
        // Метод освобождения ресурсов
        public void Close()
        {
            // Закрываем сокет
            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            // Обнуляем все переменные чтобы сборщик мусора сделал свою работу
            clientSocket = null;
            if(buffer != null)
                buffer.Dispose();
            lenBuffer = null;
            Disconnected = null;
            Received = null;
        }
    }
}
