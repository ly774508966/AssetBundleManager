using UnityEngine;
using System.Collections;
namespace MAssets
{
    public partial class AssetBundleOperation
    {
        public abstract class Load : IEnumerator
        {
            public object Current
            {
                get { return null; }
            }
            public bool MoveNext()
            {
                return !IsDone();
            }
            public void Reset()
            {
            }
            abstract public bool Update();
            abstract public bool IsDone();


        }
        public abstract class Download : Load
        {
            bool done;

            public string assetBundleName { get; private set; }
            public string error { get; protected set; }
            public AssetBundle assetBundle { get; protected set; }

            protected abstract bool downloadIsDone { get; }
            protected abstract void FinishDownload();
            public abstract string GetSourceURL();

            public override bool Update()
            {
                if (!done && downloadIsDone)
                {
                    FinishDownload();
                    done = true;
                }
                return !done;
            }
            public override bool IsDone()
            {
                return done;
            }

            public Download(string assetBundleName)
            {
                this.assetBundleName = assetBundleName;
            }
        }
        public abstract class LoadAsset : Load
        {
            public abstract T GetAsset<T>() where T : UnityEngine.Object;
        }
    }

    public partial class AssetBundleOperation
    {
        public class DownloadFromWeb : Download
        {
            WWW _www;
            string _url;

            public DownloadFromWeb(string assetBundleName, WWW www)
                : base(assetBundleName)
            {
                if (www == null)
                    throw new System.ArgumentNullException("www");
                _url = www.url;
                this._www = www;
            }

            protected override bool downloadIsDone
            {
                get { return _www == null || _www.isDone; }
            }

            protected override void FinishDownload()
            {
                error = _www.error;
                if (!string.IsNullOrEmpty(error))
                    return;

                AssetBundle bundle = _www.assetBundle;
                if (bundle == null)
                    error = string.Format("{0} is not a valid asset bundle.", assetBundleName);
                else
                    assetBundle = bundle;

                _www.Dispose();
                _www = null;
            }

            public override string GetSourceURL()
            {
                return _url;
            }
        }

        public class LoadAssetFull : LoadAsset
        {
            protected string _assetBundleName;
            protected string _assetName;
            protected string _downloadingError;
            protected System.Type _type;
            protected AssetBundleRequest _request = null;

            public LoadAssetFull(string bundleName, string assetName, System.Type type)
            {
                _assetBundleName = bundleName;
                _assetName = assetName;
                _type = type;
            }

            public override T GetAsset<T>()
            {
                if (_request != null && _request.isDone)
                    return _request.asset as T;
                else
                    return null;
            }
            public override bool Update()
            {
                if (_request != null)
                    return false;

                AssetBundle bundle = new AssetBundle();
                if (bundle != null)
                {
                    _request = bundle.LoadAssetAsync(_assetName, _type);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            public override bool IsDone()
            {
                if (_request == null && _downloadingError != null)
                {
                    Debug.LogError(_downloadingError);
                    return true;
                }

                return _request != null && _request.isDone;
            }
        }
    }

}
