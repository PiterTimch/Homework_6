using NovaPostServer.Services;
using System.Diagnostics;
using System.Text;

namespace NovaPostServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            NovaPostService novaPoshtaService = new NovaPostService();
            
            Stopwatch stopWatch = new Stopwatch();
            
            stopWatch.Start();
            await novaPoshtaService.SeedAreasAsync();
            await novaPoshtaService.SeedCitiesAsync();
            await novaPoshtaService.SeedDepartmentsAsync();
            
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
