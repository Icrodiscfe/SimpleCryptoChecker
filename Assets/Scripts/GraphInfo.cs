using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphInfo
{
    public List<List<object>> market_cap_by_available_supply { get; set; }
    public List<List<double>> price_btc { get; set; }
    public List<List<double>> price_usd { get; set; }
    public List<List<object>> volume_usd { get; set; }
}
