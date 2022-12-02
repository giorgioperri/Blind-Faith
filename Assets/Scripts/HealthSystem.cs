using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Audio Parameters")]
    [SerializeField] public AK.Wwise.State stateOn;
    [SerializeField] public AK.Wwise.State stateOff;
    [SerializeField] public AK.Wwise.Event loosingHealth;

    [SerializeField]
    private Image _healthFadeEffect;
    private float _maxPlayerHealth = 100f;
    [SerializeField]
    private float _loseHealthSpeed = 2f;
    [SerializeField]
    private float _gainHealthSpeed = 10f;
    [SerializeField]
    private float playerHealth;
    [SerializeField]
    private bool isPlayerLoosingHealth;
    [SerializeField]
    private float playerHealthASecondAgo;

    public static HealthSystem Instance;

    private void Start()
    {
        AkSoundEngine.PostEvent("LowHealth", gameObject);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        StartCoroutine(CheckForLostHealth());
    }

    //Checks if health has been lost past 2 seconds
    IEnumerator CheckForLostHealth()
    {
        yield return new WaitForSeconds(2);
        if (playerHealth <= playerHealthASecondAgo)
        {
            isPlayerLoosingHealth = true;
        }
        else
        {
            isPlayerLoosingHealth = false;
        }
        playerHealthASecondAgo = playerHealth;
        StartCoroutine(CheckForLostHealth());
        if (isPlayerLoosingHealth == true)
        {
            AkSoundEngine.PostEvent("LoosingHealth", gameObject);
            stateOn.SetValue();
        }
        else
        {
            AkSoundEngine.PostEvent("NotLoosingHealth", gameObject);
            stateOff.SetValue();
        }
    }

    void Update()
    {
        if (GameManager.Instance.health <= 100)
        {
            UpdateHealthFadeEffect();
        }
        //has no health 
        if (GameManager.Instance.health <= 0)
        {
            GameManager.Instance.health = 0;
        }
        //has full health
        else if (GameManager.Instance.health >= 100)
        {
            GameManager.Instance.health = 100;
        }
        // Gain health when standing in the light
        if (GameManager.Instance.isBathingInLight)
        {
            GameManager.Instance.health += Time.deltaTime * _gainHealthSpeed;
        }
        // Don't take damage when holding the lantern that still has charged light
        else if (LanternManager.Instance.lanternCharge >= 0 && !LanternManager.Instance.isInsideSocket)
        {
            return;
        }
        else
        {
            GameManager.Instance.health -= Time.deltaTime * _loseHealthSpeed;
        }
        playerHealth = GameManager.Instance.health;
        //Audio
        AkSoundEngine.SetRTPCValue("PlayerHealth", GameManager.Instance.health, gameObject);

    }

    public void Heal()
    {
        GameManager.Instance.health += Time.deltaTime * _gainHealthSpeed;
    }

    void UpdateHealthFadeEffect()
    {
        ////Not using now, but if Alex can come up with some cool idea of making an additional effect we can use it
        ////aditional effect as png on screen

        //Color healthFadeEffectColor = _healthFadeEffect.color;
        ////Update alpha based on health
        //healthFadeEffectColor.a = 1 - (GameManager.Instance.health / _maxPlayerHealth);
        //_healthFadeEffect.color = healthFadeEffectColor;

        PostProcessingManager.Instance.ChangeVignetteIntensity(math.remap(100,0,0,0.65f, GameManager.Instance.health));
        PostProcessingManager.Instance.ChangeSaturation(math.remap(100,0,0,-80, GameManager.Instance.health));
        PostProcessingManager.Instance.ChangeAbberration(math.remap(100,0,0,0.5f, GameManager.Instance.health));
    }
}
