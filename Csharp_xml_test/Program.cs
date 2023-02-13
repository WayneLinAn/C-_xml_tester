using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Csharp_xml_test
{
    class Program
    {
        static void Main(string[] args)
        {

            //XmlDocument doc = new XmlDocument();
            ////doc.Load(@"D:\Wayne_D\2.Technique\SPC_tranceiver\test.xml");
            //doc.Load(@"D:\Wayne_D\2.Technique\SPC_tranceiver\FROM ERP.xml");

            //var itemsNode = doc.SelectSingleNode("VHEP02");

            //var itemNodeList = itemsNode.SelectNodes("OUTPUTAREA");

            //foreach (XmlNode item in itemNodeList)
            //{
            //    Console.WriteLine(item["RESULT"]?.InnerText);
            //}
            //Console.Write("\r\nPress any key to continue....");
            //Console.Read();

            string url = "http://localhost:8000";

            WebClient client = new WebClient();
            client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
            client.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            string data = client.DownloadString(url);


            XmlDocument doc = new XmlDocument();
            //doc.Load(@"D:\Wayne_D\2.Technique\SPC_tranceiver\test.xml");
            doc.LoadXml(data);

            var itemsNode = doc.SelectSingleNode("VHEP02");

            var itemNodeList = itemsNode.SelectNodes("OUTPUTAREA");

            foreach (XmlNode item in itemNodeList)
            {
                Console.WriteLine(item["RESULT"]?.InnerText);
            }
            Console.Write("\r\nPress any key to continue....");
            Console.Read();


            //String URLString = @"http://localhost:8000";

            //XmlTextReader reader = new XmlTextReader(URLString);
            //while (reader.Read())
            //{
            //    // Do some work here on the data.
            //    Console.WriteLine(reader.Name);
            //}
            //Console.ReadLine();
        }
    }
}
//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.IO;
//using System.Web;

//public class WebServer
//{
//    public static void Main()
//    {
//        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 8085);

//        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//        newsock.Bind(ipep);
//        newsock.Listen(10);

//        while (true)
//        {
//            Socket client = newsock.Accept();
//            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;

//            // create a new thread and then receive message.
//            HttpListener listener = new HttpListener(client);
//            Thread thread = new Thread(new ThreadStart(listener.run));
//            thread.Start();
//        }
//    }
//}

//class HttpListener
//{
//    String[] map ={"mpeg=video/mpeg", "mpg=video/mpeg", "wav=audio/x-wav", "jpg=image/jpeg",
//"gif=image/gif", "zip=application/zip", "pdf=application/pdf", "xls=application/vnd.ms-excel",
//"ppt=application/vnd.ms-powerpoint", "doc=application/msword", "htm=text/html",
//"html=text/html", "css=text/plain", "vbs=text/plain", "js=text/plain", "txt=text/plain",
//"java=text/plain"};
//    Socket socket;
//    NetworkStream stream;
//    String header;
//    String root = ".";

//    public HttpListener(Socket s)
//    {
//        socket = s;
//    }

//    public void run()
//    {
//        stream = new NetworkStream(socket);
//        request();
//        response();
//        stream.Close();
//    }

//    public void send(String str)
//    {
//        socket.Send(Encoding.UTF8.GetBytes(str));
//    }

//    public static String innerText(String pText, String beginMark, String endMark)
//    {
//        int beginStart = pText.IndexOf(beginMark);
//        if (beginStart < 0) return null;
//        int beginEnd = beginStart + beginMark.Length;
//        int endStart = pText.IndexOf(endMark, beginEnd);
//        if (endStart < 0) return null;
//        return pText.Substring(beginEnd, endStart - beginEnd);
//    }

//    public void request()
//    {
//        try
//        {
//            StreamReader reader = new StreamReader(stream);
//            header = "";
//            while (true)
//            {
//                String line = reader.ReadLine();
//                Console.WriteLine(line);
//                if (line.Trim().Length == 0)
//                    break;
//                header += line + "\n";
//            }
//        }
//        catch
//        {
//            Console.WriteLine("request error!");
//        }
//    }

//    void response()
//    {
//        try
//        {
//            Console.WriteLine("========response()==========");
//            String path = innerText(header, "GET ", "HTTP/").Trim(); // 取得檔案路徑 : GET 版。
//            HttpUtility.UrlDecode(path);
//            String fullPath = root + path;
//            FileInfo info = new FileInfo(fullPath);
//            if (!info.Exists)
//                throw new Exception("File not found !");
//            send("HTTP/1.0 200 OK\n");
//            send("Content-Type: " + type(fullPath) + "\n");
//            send("Content-Length: " + info.Length + "\n");
//            send("\n");
//            byte[] buffer = new byte[4096];
//            FileStream fileStream = File.OpenRead(fullPath);
//            while (true)
//            {
//                int len = fileStream.Read(buffer, 0, buffer.Length);
//                socket.Send(buffer, 0, len, SocketFlags.None);
//                if (len < buffer.Length) break;
//            }
//            fileStream.Close();
//        }
//        catch
//        {
//            try
//            {
//                send("HTTP/1.0 404 Error\n");
//                send("\n");
//            }
//            catch
//            {
//                Console.WriteLine("Send Error Msg fail!");
//            }
//        }
//    }

//    String type(String path)
//    {
//        String type = "*/*";
//        path = path.ToLower();
//        for (int i = 0; i < map.Length; i++)
//        {
//            String[] tokens = map[i].Split('=');
//            String ext = tokens[0], mime = tokens[1];
//            if (path.EndsWith("." + ext)) type = mime;
//        }
//        return type;
//    }
//}
