using System.Collections.Generic;
using System.IO;

public class AssetVersionInfo
{
    public int res = -1;
    public List<AssetBundleInfo> assets = new List<AssetBundleInfo>();

    public static AssetVersionInfo LoadFrom(FileInfo file)
    {
        try
        {
            List<string> content = new List<string>();
            StreamReader reader = file.OpenText();
            while (reader.Peek() > -1)
            {
                content.Add(reader.ReadLine());
            }
            int res = int.Parse(content[0].Replace("res=", ""));
            List<AssetBundleInfo> assets = new List<AssetBundleInfo>();
            for (int i = 1; i < content.Count; i++)
            {
                AssetBundleInfo info = AssetBundleInfo.Parse(content[i]);
                if (info != null) assets.Add(info);
            }
            AssetVersionInfo r = new AssetVersionInfo() { res = res, assets = assets };
            return r;
        }
        catch
        {
            return null;
        }
    }
}
