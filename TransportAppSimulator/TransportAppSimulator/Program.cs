using System;
using System.Reactive.Linq;

namespace TransportAppSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubClient = new ClientSignalR();
            hubClient.Initialize("http://localhost:63369/transport");
            var locationUpdate = new LocationUpdate()
            {
                DriverName = "Daniel"
            };

            var locationUpdate2 = new LocationUpdate()
            {
                DriverName = "Adam",
                Latitude = 52.28107,
                Longitude = 20.9558243
            };

            Observable
            .Interval(TimeSpan.FromSeconds(2))
            .Subscribe(
                async x =>
                {
                    await hubClient.SendHubMessage("broadcastMessage", locationUpdate);
                    Console.WriteLine("SENDING LOCATION UPDATE: " + locationUpdate.DriverName + " " + locationUpdate.Latitude + " " + locationUpdate.Longitude);
                    await hubClient.SendHubMessage("broadcastMessage", locationUpdate2);
                    Console.WriteLine("SENDING LOCATION UPDATE: " + locationUpdate2.DriverName + " " + locationUpdate2.Latitude + " " + locationUpdate2.Longitude);


                    locationUpdate.Latitude = locationUpdate.Latitude + 0.0008;
                    locationUpdate.Longitude = locationUpdate.Longitude + 0.0008;

                    locationUpdate2.Latitude = locationUpdate2.Latitude + 0.0008;
                    locationUpdate2.Longitude = locationUpdate2.Longitude + 0.0008;
                });

            Console.ReadKey();
        }
    }
}
