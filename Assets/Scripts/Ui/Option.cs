using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Option : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ScoreManager.instance.gamePause)
            {

                ScoreManager.instance.SetGameResume();
                Debug.Log("Mouse Click Button : Left");
            }
            else
            {

                ScoreManager.instance.SetGamePause();
                Debug.Log("Mouse Click Button : Left");
            };
        }
    }
}
