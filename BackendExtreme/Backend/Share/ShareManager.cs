using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;


public class ShareManager : WebSocketBehavior
{
    private Thread thread;
    private int count;
    private string bean;

    public ShareManager(){}

    protected override void OnOpen()
    {
        Console.WriteLine("share info recieved");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
        string socketID = ID;

        // get the packet with the information that is needed to lookup in the db
        SharePacket sharePacket = JsonConvert.DeserializeObject<SharePacket>(packet.data);
        Tuple<List<ScoresInfo>, ScoresInfo> filledInfo = FillScoresInfo(sharePacket.teamName); 

        // try {
        CreateDetails(filledInfo, null);
        // } catch (Exception e) {
        //     Console.WriteLine("EXCEPTION IN CREATING IMAGE");
        // }
        
    }

    public Tuple<List<ScoresInfo>, ScoresInfo> FillScoresInfo(string teamName) {
        Tuple<List<ScoresInfo>, ScoresInfo> retrievedInfo = SQLConnection.GetTopTeamsAndCurrentTeam(teamName);
        return retrievedInfo;
    }

    public string CreateDetails(Tuple<List<ScoresInfo>, ScoresInfo> filledInfo, Bitmap b) {
        ScoresInfo myTeam = filledInfo.Item2;
        string teamName = myTeam.teamName;
        int score = myTeam.teamScore;
        string scoreInfo = "Best achieving score: " + score;

        PointF firstLocation = new PointF(320f, 400f);
        PointF secondLocation = new PointF(320f, 490f);

        Bitmap bitmap;
        if(b == null) {
            bitmap = new System.Drawing.Bitmap("canvas.png");
        } else {
            bitmap = b;
        }
        

        using(Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont =  new Font("Arial", 50))
                {
                    graphics.DrawString(teamName, arialFont, Brushes.Red, firstLocation);
                    int i = teamName.Length;
                    graphics.DrawString(scoreInfo, arialFont, Brushes.Blue, secondLocation);
                }
        }

        // string imageFilePath = "canvas.bmp";
        // string outputFileName = imageFilePath;
        // using (MemoryStream memory = new MemoryStream())
        // {
        //     using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
        //     {
        //         bitmap.Save(memory, ImageFormat.Jpeg);
        //         byte[] bytes = memory.ToArray();
        //         fs.Write(bytes, 0, bytes.Length);
        //     }
        // }

        
        Bitmap bImage = bitmap;
        System.IO.MemoryStream ms = new MemoryStream();
        bImage.Save(ms, ImageFormat.Jpeg);
        byte[] byteImage = ms.ToArray();
        var encodedImage= Convert.ToBase64String(byteImage); 
        // Console.WriteLine("ENCODED IMAGE " +  encodedImage);

        ImgPacket imgPacket= new ImgPacket();
        imgPacket.data = encodedImage;
        var convertedInfo = JsonConvert.SerializeObject(imgPacket);
        Send(convertedInfo);

        return encodedImage;
    }
}

    
