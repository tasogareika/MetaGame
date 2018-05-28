using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TxtPusher : MonoBehaviour {

    public GameObject textHolder, bgHolder;
    public Texture2D imageHolder;
    public Text debugText;

    [HideInInspector] List<string> trimList;

	void Start () {
        File.WriteAllText("C:/Users/Admin/Desktop/helloworld.txt", textHolder.GetComponent<Text>().text.ToString());
        SaveTextureToFile(imageHolder, "thisImage");
    }

    void SaveTextureToFile(Texture2D texture, string filename)
    {
        var bytes = texture.EncodeToPNG();
        var file = File.Open("C:/Users/Admin/Desktop/" + filename + ".png", FileMode.Create);
        var binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }

    public void WallpaperSelect()
    {
        Texture2D wallpaperTest = Resources.Load("Final01", typeof(Texture2D)) as Texture2D;
        var bytes = wallpaperTest.EncodeToPNG();
        var file = File.Open(Application.dataPath + "/Resources/wp.png", FileMode.Create);
        var binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
        WallpaperChange.SetParam(Application.dataPath + "/Resources/wp.png");
    }

    public void SetFile()
    {
        var info = new DirectoryInfo("C:/Users/Admin/Pictures");
        var fileInfo = info.GetFiles();
        if (fileInfo.Length < 2)
        {
            trimList = new List<string>();
            var fileSub = info.GetDirectories();
            foreach (var file2 in fileSub)
            {
                var file2Info = file2.GetFiles();
                if (file2Info.Length > 1)
                {
                    trimList.Add(file2.FullName.ToString());
                }
            }

            if (trimList.Count > 1)
            {
                int r = UnityEngine.Random.Range(0, trimList.Count - 1);
                var info2 = new DirectoryInfo(trimList[r]);
                var fileInfo2 = info2.GetFiles();
                int r2 = UnityEngine.Random.Range(0, fileInfo2.Length - 1);
                OpenFile(fileInfo2[r2].ToString());
            } else
            {
                //set own image because i'm not going to search through EVERYTHING g o d
                Debug.Log("welp empty");
            }
        } else
        {
            int ran = UnityEngine.Random.Range(0, fileInfo.Length - 1);
            OpenFile(fileInfo[ran].ToString());
        }
    }

    void OpenFile(string url)
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo = new System.Diagnostics.ProcessStartInfo(url);
        process.Start();
    }
}