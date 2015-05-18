using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RemoteFileServer
{
    public class Server
    {
        // Серверный сокет
        private Socket serverSocket;
        // Порт сервера
        public int Port { get; private set; }
        // Запущен ли сервер
        public bool Listening { get; private set; }
        // Хендлер присоединения пользователя
        public delegate void ClientAcceptedHandler(Socket s);
        public event ClientAcceptedHandler ClientAccepted;
        
        public Server(int port)
        {
            // Задаем порт
            Port = port;
        }
        // Метод запуска прослушки
        public void Start()
        {
            // Если уже запущена прослушка - выход из метода
            if (Listening) return;
            // Иначе: создаем потоковый сокет используя протокол TCP
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Занимаем порт заданный при создании объекта сервера
            serverSocket.Bind(new IPEndPoint(0, Port));
            // Запускаем прослушку
            serverSocket.Listen(0);
            // Запускаем асинхронный прием пользователей
            serverSocket.BeginAccept(acceptCallback, null);
            // Выставляем маркер в сервер запущен
            Listening = true;
        }
        // Остановка сервера
        public void Stop()
        {
            // Предотвратим попытку остановить сервер если он не запущен
            if (!Listening) return;
            // Закрываем слушающий сокет
            serverSocket.Close();
            // Освобождаем ресурсы занимаемые сокетом
            serverSocket.Dispose();
            // Маркируем сервер как остановленный
            Listening = false;
        }
        // Асинхронный метод приема соединений
        void acceptCallback(IAsyncResult iar)
        {
            try
            {
                // Получаем сокет для нового соединения
                Socket clientSocket = serverSocket.EndAccept(iar);
                // Если хендлер был создан
                if (ClientAccepted != null)
                    // Генерируем сообщение - "клиент соединился"
                    ClientAccepted(clientSocket);
                // Запускаем снова асинхронный прием подключений для последующих пользователей
                serverSocket.BeginAccept(acceptCallback, null);
            }
                // Отлавливаем ошибки и выводим на консоль
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
