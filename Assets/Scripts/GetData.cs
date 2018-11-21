using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GetData : MonoBehaviour {

    public static event System.Action OnTickerUpdate;

    [SerializeField] GameObject m_uiTickerPrefab;
    [SerializeField] Transform m_uiTickerPrefabparent;

    GraphInfo graphInfo;
    Ticker ticker;

    string urlText = "https://graphs2.coinmarketcap.com/currencies/bitcoin/1542700740000/1542708868000/";
    string urlTicker = "https://api.coinmarketcap.com/v2/ticker/?limit=10&structure=array";
    
    void Start () {
        StartCoroutine(GetText(urlText));
        StartCoroutine(GetTicker(urlTicker));
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

            foreach (var item in ticker.data)
            {
                var obj = Instantiate(m_uiTickerPrefab, m_uiTickerPrefabparent);
                obj.SetActive(true);
                var ui = obj.GetComponent<UiTicker>();
                ui.Data = item;
                ui.Initialize();
            }

            OnTickerUpdate?.Invoke();
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


}
