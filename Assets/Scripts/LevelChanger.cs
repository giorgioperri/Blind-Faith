using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    
    public Animator animator;
    private int _levelToLoad;

    public void FadeToLevel(int indexOfLevel)
    {
        _levelToLoad = indexOfLevel;
        animator.SetTrigger("FadeOut");


    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_levelToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!(animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime) && SceneManager.GetActiveScene().name == "Rafal_intro")
        {
            FadeToLevel(1);
        }*/
    }
}
