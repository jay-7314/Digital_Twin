using System.Globalization;
using System.Net.Sockets;

TcpClient client = new TcpClient("127.0.0.1", 9000);
StreamWriter writer = new StreamWriter(client.GetStream());
writer.AutoFlush = true;

Random random = new Random();
string[] ids = { "Conveyor01", "Robot01", "Inspector01" };

Console.WriteLine("Unity에 연결됨");

while (true)
{
    foreach (string id in ids)
    {
        double temp = 50 + random.NextDouble() * 40;

        string state = "Running";
        if (temp > 85)
        {
            state = "Error";
        }
        else if (temp < 55)
        {
            state = "Stopped";
        }

        string tempText = temp.ToString("F1", CultureInfo.InvariantCulture);
        string json = "{\"id\": \"" + id + "\", \"temp\": " + tempText + ", \"state\": \"" + state + "\"}";

        writer.WriteLine(json);
        Console.WriteLine(json);
    }

    Thread.Sleep(1000);
}