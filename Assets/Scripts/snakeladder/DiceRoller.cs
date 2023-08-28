using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public List<Sprite> spriteList;
   public GameManager gameManagerObject;
  
    public int diceResult;
    public Player redPlayer, yellowPlayer, greenPlayer, bluePlayer, currentPlayer;
    public Image DiceImage;
    Player playerObj = new Player();
    public int setdicevalue;
    public IconBlink iconblinkobj;
    public bool canRoll = true;
    public int AfterDeadIndex;
    public AudioSource audiosource;
    public AudioClip audioclip;

    public void RollDice()
    {

        if (!canRoll)
        {
            return;
        }
        audiosource.PlayOneShot(audioclip);
        
        gameManagerObject.Rolling_3Ddice.SetActive(true);
        
        Invoke(nameof(SetDiceInactive), 0.5f);
        
        diceResult = Random.Range(1, 7);
        
        Sprite LoadedSprite = spriteList[diceResult - 1];   
        DiceImage.sprite = LoadedSprite;
        
        gameObject.SetActive(true);
        print("dice result is:" + diceResult);
    /*    diceResult = setdicevalue;*/
        canRoll = false;
        if (redPlayer.isPlayerTurn)
        {
            Invoke(nameof(redPlayer.PlayerMove),0.6f);
            redPlayer.PlayerMove();
        }
        else if (bluePlayer.isPlayerTurn)
        {
            Invoke(nameof(bluePlayer.PlayerMove), 0.6f);
            bluePlayer.PlayerMove();
        }
        else if (greenPlayer.isPlayerTurn)
        {
          Invoke(nameof(greenPlayer.PlayerMove),0.6f);
            greenPlayer.PlayerMove();
            
        }
        else
        {
            Invoke(nameof(yellowPlayer.PlayerMove), 0.6f);
            yellowPlayer.PlayerMove();
            
        }
    }

    public void SetDiceInactive()
    {
        gameManagerObject.Rolling_3Ddice.SetActive(false);

    }
}



