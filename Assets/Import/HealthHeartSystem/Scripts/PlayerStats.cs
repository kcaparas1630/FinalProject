/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

        #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;
    bool hasTakenDamage = false;
    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }
    private bool godMode = false;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_HIT, TakeDamage);
        Messenger.AddListener(GameEvent.GOD_MODE, OnGodMode);
        Messenger.AddListener(GameEvent.NORMAL_MODE, OnNormalMode);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_HIT, TakeDamage);
        Messenger.RemoveListener(GameEvent.GOD_MODE, OnGodMode);
        Messenger.RemoveListener(GameEvent.NORMAL_MODE, OnNormalMode);
    }
    private void OnGodMode()
    {
        godMode = true;
    }
    private void OnNormalMode()
    {
        godMode = false;
    }
    public void TakeDamage()
    {
        if (!godMode)
        {
            StartCoroutine(DamageDelay());
        }
        if(health == 1)
        {
            Messenger.Broadcast(GameEvent.PLAYER_INJURED);
        }
        if (health == 0)
        {
            Debug.Log("GameOver");
            Messenger.Broadcast(GameEvent.GAME_OVER);
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    IEnumerator DamageDelay()
    {
        ////move this to Coroutine
        //health -= 1;
        //ClampHealth();
        //Debug.Log(health);
        //yield return new WaitForSeconds(5f);
        //// Apply damage again after the delay if necessary
        //hasTakenDamage = true;
        //yield return new WaitForSeconds(5f);
        //hasTakenDamage = false;
        //if(!hasTakenDamage)
        //{
        //    health -= 1;
        //    ClampHealth();
        //}
        if(!hasTakenDamage)
        {
            health -= 1;
            ClampHealth();
            hasTakenDamage = true;
        }
        yield return new WaitForSeconds(5f);
        hasTakenDamage = false;
    }
}
