using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBlink : MonoBehaviour
{
    public Player playerOb;
    Vector3 Blinkiconposition;
    public DiceRoller dicerollobj;
    public GameObject TurnWisePlayerDiceStore;
    

    public IEnumerator startBlink()
    {
        Debug.Log(" In startBlink function");
        while (playerOb.isPlayerTurn)
        {
            dicerollobj.gameObject.transform.position = TurnWisePlayerDiceStore.transform.position;
            gameObject.SetActive(false); // Disable the GameObject
            yield return new WaitForSeconds(0.5f); // Wait for a specific duration
            gameObject.SetActive(true); // Enable the GameObject
            yield return new WaitForSeconds(0.5f); // Wait for a specific duration
        }

    }
}

