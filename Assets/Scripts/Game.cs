using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    #region Singleton:Game

    public static Game Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public int coins;

    public int diamonds;

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
    }

    public void UseCoins (int amount, bool premium)
    {
        if (!premium)
            coins -= amount;
        else
            diamonds -= amount;
    }

    public bool HasEnoughCoins(int amount, bool premium)
    {
        if (!premium)
            return (coins >= amount);
        else
            return (diamonds >= amount);

    }
}
