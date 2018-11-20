using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GetData : MonoBehaviour {

    public static event System.Action<Ticker> OnTickerChanged;

    [SerializeField] RawImage image;

    GraphInfo graphInfo;
    Ticker ticker;

    string urlText = "https://graphs2.coinmarketcap.com/currencies/bitcoin/1542700740000/1542708868000/";
    string urlTicker = "https://api.coinmarketcap.com/v2/ticker/1/";
    string urlTexture = "https://s2.coinmarketcap.com/generated/sparklines/web/7d/usd/1.png";
    
    void Start () {
        StartCoroutine(GetText(urlText));
        StartCoroutine(GetTicker(urlTicker));
        StartCoroutine(GetTexture(urlTexture));
    }


    IEnumerator GetTicker(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            ticker = JsonConvert.DeserializeObject<Ticker>(www.downloadHandler.text);
            OnTickerChanged?.Invoke(ticker);
        }
    }

    IEnumerator GetText(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            graphInfo = JsonConvert.DeserializeObject<GraphInfo>(www.downloadHandler.text);
        }
    }

    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.SetNativeSize();
        }
    }
}
