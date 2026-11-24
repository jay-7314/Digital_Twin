using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TcpReceiver : MonoBehaviour
{
    [SerializeField] private int port = 9000;
    [SerializeField] private MachineManager machineManager;

    private TcpListener listener;
    private TcpClient client;
    private Thread thread;
    private ConcurrentQueue<string> mailbox = new ConcurrentQueue<string>();

    private void Start()
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Debug.Log("TCP 대기 시작: " + port);

        thread = new Thread(WaitForClient);
        thread.IsBackground = true;
        thread.Start();
    }

    private void Update()
    {
        while (mailbox.TryDequeue(out string json))
        {
            machineManager.ApplyJson(json);
        }
    }

    private void WaitForClient()
    {
        try
        {
            client = listener.AcceptTcpClient();
            Debug.Log("연결됨");

            StreamReader reader = new StreamReader(client.GetStream());

            while (true)
            {
                string line = reader.ReadLine();

                if (line == null)
                {
                    break;
                }

                mailbox.Enqueue(line);
            }

            Debug.Log("연결 끊김");
        }
        catch (SocketException)
        {
        }
        catch (IOException)
        {
        }
        catch (System.ObjectDisposedException)
        {
        }
    }

    private void OnDestroy()
    {
        if (client != null)
        {
            client.Close();
        }

        if (listener != null)
        {
            listener.Stop();
        }
    }
}