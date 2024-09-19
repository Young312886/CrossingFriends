using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private GameObject gameOverPanel;
    [HideInInspector]
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;

        Invoke(nameof(ShowGameOverPanel), 1f);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void hasCollided(GameObject gameObject)
    {

    }

    public void GetItem(GameObject gameObject)
    {
        Character character = FindObjectOfType<Character>();
        Item item = FindAnyObjectByType<Item>();

        if (character != null)
        {
            character.UpgradeItem(item);
        }
    }
}
