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
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    foreach (var coloumn1 in listA)
                    {
                        System.Threading.Thread.Sleep(1000);
                var data = 0;
                await Clients.All.SendAsync("ReceiveData", coloumn1);
                    }
                }


                


        }
    }
}