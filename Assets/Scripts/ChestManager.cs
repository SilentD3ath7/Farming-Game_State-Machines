using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [HideInInspector] public Animator openAnimator = null;
    public int value = 1600;

    // Start is called before the first frame update
    void Start()
    {
        openAnimator = GetComponent<Animator>();
    }

    public void OpenAnimation()
    {
        openAnimator.SetTrigger("TrOpen");
        EventManager.Instance.AddMoney(value);
    }
}
