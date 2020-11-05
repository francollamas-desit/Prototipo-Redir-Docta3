using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Prototipo_Redir_Docta3.Services
{
    public class TcpServerService : IHostedService
    {

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var thread = new Thread(() => Init());
            thread.Start();

            return Task.CompletedTask;
        }

        public void Init()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 8000.
                Int32 port = 8000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;


                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i = 0;

                    //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    while (true)
                    {
                        //Console.WriteLine("Leyendo");
                        //stream.Read(bytes, 0, bytes.Length);
                        //Console.WriteLine("Leido");
                        //i = i + 1;
                        //Console.WriteLine(i);

                        if (TramasBuffer.Trama.Length != 0)
                        {

                            //Console.WriteLine(TramasBuffer.Trama);

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(TramasBuffer.Trama);

                            try
                            {
                                stream.Write(msg, 0, msg.Length);

                            }
                            catch (Exception ex)
                            {
                                
                            }
                            
                            
                            TramasBuffer.Trama = "";
                        }

                        
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
