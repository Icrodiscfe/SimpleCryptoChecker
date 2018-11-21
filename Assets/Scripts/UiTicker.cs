using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UiTicker : MonoBehaviour {

    public Data Data;

    [SerializeField] RawImage m_image;
    [SerializeField] Color m_colorPositive = Color.green;
    [SerializeField] Color m_colorNegative = Color.red;
    [SerializeField] TMP_Text m_price;
    [SerializeField] TMP_Text m_volume_24h;
    [SerializeField] TMP_Text m_market_cap;
    [SerializeField] TMP_Text m_percent_change_1h;
    [SerializeField] TMP_Text m_percent_change_24h;
    [SerializeField] TMP_Text m_percent_change_7d;
    [SerializeField] TMP_InputField m_inputField;
    [SerializeField] TMP_Text m_inputFieldPlaceholder;


    private void OnEnable()
    {
        m_inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
    }

    private void OnDisable()
    {
        m_inputField.onEndEdit.RemoveListener(OnInputFieldEndEdit);
    }

    public void Initialize()
    {
        var text = PlayerPrefs.GetString(Data.symbol);
        if (text != "")
        {
            m_inputField.text = text;
        }
        else
        {
            double d = 0.000098451f;
            m_inputFieldPlaceholder.text = "e.g. " + d.ToString("N9");
        }
        UpdateUi();
    }
    
    private void UpdateUi()
    {
        StartCoroutine(GetTexture());

        var price = Data.quotes.USD.price;
        m_price.text = price.ToString("N") + " $";

        var volume_24h = Data.quotes.USD.volume_24h;
        m_volume_24h.text = volume_24h.ToString("N") + " $";

        var market_cap = Data.quotes.USD.market_cap;
        m_market_cap.text = market_cap.ToString("N") + " $";

        var percent_change_1h = Data.quotes.USD.percent_change_1h;
        m_percent_change_1h.text = percent_change_1h.ToString("N") + " %";
        m_percent_change_1h.color = percent_change_1h > 0 ? m_colorPositive : m_colorNegative;

        var percent_change_24h = Data.quotes.USD.percent_change_24h;
        m_percent_change_24h.text = percent_change_24h.ToString("N") + " %";
        m_percent_change_24h.color = percent_change_24h > 0 ? m_colorPositive : m_colorNegative;

        var percent_change_7d = Data.quotes.USD.percent_change_7d;
        m_percent_change_7d.text = percent_change_7d.ToString("N") + " %";
        m_percent_change_7d.color = percent_change_7d > 0 ? m_colorPositive : m_colorNegative;
    }

    private void OnInputFieldEndEdit(string text)
    {
        PlayerPrefs.SetString("bitcoin", text);
        m_inputField.text = PlayerPrefs.GetString(Data.symbol);
        Debug.Log(PlayerPrefs.GetString(Data.symbol));
    }

    IEnumerator GetTexture()
    {
        string url = $"https://s2.coinmarketcap.com/generated/sparklines/web/7d/usd/{Data.id}.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            m_image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            m_image.color = Color.white;
        }
    }
}
