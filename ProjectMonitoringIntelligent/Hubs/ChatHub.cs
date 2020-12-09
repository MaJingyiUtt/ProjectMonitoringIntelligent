using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        public async Task SendData()
        {
            var reader = new StreamReader(File.OpenRead(@"C:\Users\majingyi\source\repos\ProjectMonitoringIntelligent\ProjectMonitoringIntelligent\csv\temperature.csv"));
                
            List<string> listA = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length != 0 & line!= "temperature;EnqueuedTime;ConnectionDeviceId")
                {
                    var temp = line.Split(';')[0];
                    System.Threading.Thread.Sleep(1000);
                    await Clients.All.SendAsync("ReceiveData", temp);
                }
                
            }
        }
    }
}