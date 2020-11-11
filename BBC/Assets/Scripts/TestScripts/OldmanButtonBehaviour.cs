using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldmanButtonBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject exchangePanel;

    public void ShowExchangePanel()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        exchangePanel.transform.position = exchangePanel.GetComponent<ExchangePanelBehaviour>().TurnOnPosition;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        exchangePanel = GameObject.Find("Panel_RubyToTheoryExchange");
    }
}
