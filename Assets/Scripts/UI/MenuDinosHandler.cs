using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDinosHandler : MonoBehaviour
{
    [Header("For Dino Animation")]
    public Animator[] dinoAnimators;
    public float delayForDinoAttack;

    void Start()
    {
        StartCoroutine(CallDinoAttack());
    }

    IEnumerator CallDinoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayForDinoAttack);
            foreach (var anim in dinoAnimators)
            {
                anim.SetTrigger("Bite");
            }
        }
    }
}