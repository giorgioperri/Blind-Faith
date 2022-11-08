using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private Image _healthFadeEffect;
    private float _maxPlayerHealth = 100f;
    [SerializeField]
    private float _loseHealthSpeed = 1f;
    [SerializeField]
    private float _gainHealthSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.health <= 100)
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
        else if(LanternManager.Instance.lanternCharge >= 0 && !LanternManager.Instance.isInsideSocket)
        {
            return;
        }
        else
        {
            GameManager.Instance.health -= Time.deltaTime*_loseHealthSpeed;
        }
    }

    void UpdateHealthFadeEffect()
    {
        Color healthFadeEffectColor = _healthFadeEffect.color;
        //Update alpha based on health
        healthFadeEffectColor.a = 1 - (GameManager.Instance.health / _maxPlayerHealth);
        _healthFadeEffect.color = healthFadeEffectColor;
    }
}
