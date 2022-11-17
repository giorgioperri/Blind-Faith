using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private Image _healthFadeEffect;
    private float _maxPlayerHealth = 100f;
    [SerializeField]
    private float _loseHealthSpeed = 2f;
    [SerializeField]
    private float _gainHealthSpeed = 10f;

    public static HealthSystem Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
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
