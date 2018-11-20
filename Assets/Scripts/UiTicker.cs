using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiTicker : MonoBehaviour {

    [SerializeField] TMP_Text price;
    [SerializeField] TMP_Text volume_24h;
    [SerializeField] TMP_Text market_cap;
    [SerializeField] TMP_Text percent_change_1h;
    [SerializeField] TMP_Text percent_change_24h;
    [SerializeField] TMP_Text percent_change_7d;

    private void OnEnable()
    {
        GetData.OnTickerChanged += UpdateUi;
    }

    private void OnDisable()
    {
        GetData.OnTickerChanged -= UpdateUi;

    }

    private void UpdateUi(Ticker ticker)
    {
        price.text = ticker.data.quotes.USD.price.ToString();
        volume_24h.text = ticker.data.quotes.USD.volume_24h.ToString();
        market_cap.text = ticker.data.quotes.USD.market_cap.ToString();
        percent_change_1h.text = ticker.data.quotes.USD.percent_change_1h.ToString();
        percent_change_24h.text = ticker.data.quotes.USD.percent_change_24h.ToString();
        percent_change_7d.text = ticker.data.quotes.USD.percent_change_7d.ToString();
    }
}
