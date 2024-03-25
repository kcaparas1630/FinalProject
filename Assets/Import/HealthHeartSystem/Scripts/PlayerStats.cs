/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

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

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_HIT, TakeDamage);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_HIT, TakeDamage);
    }

    public void TakeDamage()
    {
        health -= 1;
        ClampHealth();
        hasTakenDamage = true;

        if (hasTakenDamage)
        {
            StartCoroutine(DamageDelay());
        }

        if(health == 0)
        {
            Debug.Break();//Pause Editor; will apply GameOver popup soon
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
        yield return new WaitForSeconds(3f);

        // Reset the flag indicating damage has been taken
        hasTakenDamage = false;
        // Apply damage again after the delay if necessary
        health -= 1;
        ClampHealth();
    }
}
