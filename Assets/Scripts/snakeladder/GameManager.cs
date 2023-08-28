using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource audiosource;
    public  GameObject player1, player2, player3, player4;
    public DiceRoller diceObject;
    public Coroutine myCoroutine,RedPlayerCoroutine,BluePlayerCoroutine,YellowPlayerCoroutine,GreenPlayerCoroutine;
    public IconBlink iconBlink1,iconBlink2,iconBlink3,iconBlink4;
    public List<Ladder> LadderList;
    public List<Snakes> SnakeList;
    public static Dictionary<string, List<GameObject>> Ladderdictionary = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> snakedictionary = new Dictionary<string, List<GameObject>>();
    public GameObject Rolling_3Ddice;
    public void Start()
    {

        Rolling_3Ddice.SetActive(false);
        audiosource.Play();
        myCoroutine= StartCoroutine(startGame());
 
        for (int j = 0; j < SnakeList.Count; j++)
        {
            snakedictionary.Add(SnakeList[j].Snakekey, SnakeList[j].SnakePoints);
        }
        

        for (int i = 0; i < LadderList.Count; i++)
        {
            Ladderdictionary.Add(LadderList[i].Ladderkey, LadderList[i].LadderPoints);
            print("" + Ladderdictionary);
        }
    }

    public void StopCoroutine()
    {
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
            
        }
    }
    public void StartMyCoroutine()
    {
        myCoroutine = StartCoroutine(startGame());
        
    }
    

    public int count = 1;
    public IEnumerator startGame()
    {
        
       
        while (true)
        {
            print("count:" + count);
            if (count == 1)
            {
                player1.GetComponent<Player>().isPlayerTurn = true;
                player2.GetComponent<Player>().isPlayerTurn = false;
                player3.GetComponent<Player>().isPlayerTurn = false;
                player4.GetComponent<Player>().isPlayerTurn = false;
                diceObject.currentPlayer = diceObject.redPlayer;
                print("player1 turn");
                RedPlayerCoroutine = StartCoroutine(iconBlink1.startBlink());           

            }
            else if (count == 2)
            {
                player1.GetComponent<Player>().isPlayerTurn = false;
                player2.GetComponent<Player>().isPlayerTurn = true;
                player3.GetComponent<Player>().isPlayerTurn = false;
                player4.GetComponent<Player>().isPlayerTurn = false;
                diceObject.currentPlayer = diceObject.bluePlayer;
                print("player2 turn");
                BluePlayerCoroutine = StartCoroutine(iconBlink2.startBlink());

            }
            else if (count == 3)
            {
                player1.GetComponent<Player>().isPlayerTurn = false;
                player2.GetComponent<Player>().isPlayerTurn = false;
                player3.GetComponent<Player>().isPlayerTurn = true;
                player4.GetComponent<Player>().isPlayerTurn = false;
                diceObject.currentPlayer = diceObject.greenPlayer;
                print("player3 turn");
                GreenPlayerCoroutine = StartCoroutine(iconBlink3.startBlink());




            }
            else
            {
                player1.GetComponent<Player>().isPlayerTurn = false;
                player2.GetComponent<Player>().isPlayerTurn = false;
                player3.GetComponent<Player>().isPlayerTurn = false;
                player4.GetComponent<Player>().isPlayerTurn = true;
                diceObject.currentPlayer = diceObject.yellowPlayer;
                print("player4 turn");
                YellowPlayerCoroutine = StartCoroutine(iconBlink4.startBlink());


                count = 0;
            }
            yield return new WaitForSeconds(60f);
            count += 1;
          
            
            
        }
    }
}

[Serializable]
public class Ladder
{
    public string Ladderkey;
    public List<GameObject> LadderPoints;
}

[Serializable]
public class Snakes
{

    public string Snakekey;
    public List<GameObject> SnakePoints;
}