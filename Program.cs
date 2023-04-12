using System.Reflection;
using System.Text;

namespace ProducerConsumeThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SyncBuffer _printerBuffer = new SyncBuffer(5);
            Random _random = new Random();
            int nThread = 2;
            Thread[] clients = new Thread[nThread];
            Thread[] printers = new Thread[nThread];
            for (int i = 0; i < nThread; i++)
            {
                clients[i] = new Thread(new Client(_printerBuffer, _random).print);
                clients[i].Name = new StringBuilder("Client").Append(i.ToString()).ToString();
                clients[i].Start();

            }
            for (int i = 0; i < nThread; i++)
            {
                printers[i] = new Thread(new Printer(_printerBuffer).print);
                printers[i].Name = new StringBuilder("Printer").Append(i.ToString()).ToString();
                printers[i].Start();
            }
            for (int i = 0; i < nThread; i++)
            {
                clients[i].Join();
                printers[i].Join();
            }
            Console.WriteLine("Done printing all Documents");

        }
    }
}
