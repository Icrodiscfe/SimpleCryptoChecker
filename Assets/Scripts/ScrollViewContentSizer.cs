using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewContentSizer : MonoBehaviour {

    float size = 0;

    private void OnEnable()
    {
        GetData.OnTickerUpdate += ChangeSize;
    }

    private void OnDisable()
    {
        GetData.OnTickerUpdate -= ChangeSize;

    }

    void ChangeSize () {
        var childs = GetComponentsInChildren<UiTicker>();
        foreach (var item in childs)
        {
            var itemRect = item.GetComponent<RectTransform>();
            size += itemRect.rect.height;
        }
        var rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, size);
	}
}
