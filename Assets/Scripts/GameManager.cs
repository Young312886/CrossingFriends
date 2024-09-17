using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int coin = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void IncreaseCoin()
    {
        coin += 1;
        scoreText.SetText(coin.ToString());
    }
}