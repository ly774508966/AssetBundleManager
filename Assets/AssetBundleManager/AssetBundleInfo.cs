public class AssetBundleInfo
{
    public string name;
    public int version;
    public long size;

    public override string ToString()
    {
        return string.Format("{0};{1};{2}", name, version, size);
    }

    public static AssetBundleInfo Parse(string s)
    {
        try
        {
            string[] ss = s.Split(';');
            if (ss.Length != 3) return null;
            AssetBundleInfo info = new AssetBundleInfo();
            info.name = ss[0];
            info.version = int.Parse(ss[1]);
            info.size = long.Parse(ss[2]);
            return info;
        }
        catch
        {
            return null;
        }
    }
}
