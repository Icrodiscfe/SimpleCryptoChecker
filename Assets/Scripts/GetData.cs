using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GetData : MonoBehaviour {

    GraphInfo graphInfo;
    [SerializeField] RawImage image;

    string url = "https://graphs2.coinmarketcap.com/currencies/bitcoin/1542700740000/1542708868000/";

    // Use this for initialization
    void Start () {
        StartCoroutine(GetText());
        StartCoroutine(GetTexture());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            graphInfo = JsonConvert.DeserializeObject<GraphInfo>(www.downloadHandler.text);
            Debug.Log(graphInfo);
            // Or retrieve results as binary data

            byte[] results = www.downloadHandler.data;
        }
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://s2.coinmarketcap.com/generated/sparklines/web/7d/usd/1.png");
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
