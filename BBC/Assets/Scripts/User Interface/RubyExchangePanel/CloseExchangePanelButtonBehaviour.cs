using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseExchangePanelButtonBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject exchangePanel;

    public void CloseExchangePanel()
    {
        exchangePanel.transform.position = exchangePanel.GetComponent<ExchangePanelBehaviour>().TurnOffPosition;
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        exchangePanel = GameObject.Find("Panel_RubyToTheoryExchange");
    }
}
