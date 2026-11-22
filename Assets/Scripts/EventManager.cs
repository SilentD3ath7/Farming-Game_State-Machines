using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour, IObserver
{
    private static EventManager _instance = null;
    public static EventManager Instance
    {
        get
        {
            return EventManager._instance;
        }
    }

    public GameObject barnDoorLeft, barnDoorRight, barnVault;
    public float textTimer = 5f;
    public int bankAccount = 0;
    bool timerActive = false;
    public TextMeshProUGUI bankAccountText;
    TextMeshProUGUI currentText = null;
    [HideInInspector] public ParentFarmManager parentFarmManager;
    
    // Start is called before the first frame update
    void Start()
    {
        if (EventManager._instance != null)
        {
            GameObject.Destroy(this.gameObject);
        }

        EventManager._instance = this;

        if (this.parentFarmManager != null) {this.parentFarmManager.notifyEventManger += this.Notify;}
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            textTimer -= Time.deltaTime;
            Debug.Log(Mathf.Ceil(textTimer));

            if (textTimer <= 0)
            {
                DisableText(currentText);
                currentText = null;
                timerActive = false;
            }
        }
    }

    public void Notify(PublisherConditions condition)
    {
        Debug.Log(condition + " recieved!");
        if (condition == PublisherConditions.winGame) winGame();
    }

    public void winGame()
    {
        barnDoorLeft.transform.position = new Vector3(2.8f, 1.3f, 3.1f);
        barnDoorRight.transform.position = new Vector3(-2.8f, 1.3f, 3.1f);
        barnVault.gameObject.SetActive(true);
    }

    public void EnableTextWithTimer(TextMeshProUGUI text, bool timer)
    {
        Debug.Log("Text recieved: " + text.gameObject.name);
        if (timer) timerActive = true;
        text.gameObject.SetActive(true);
        currentText = text;
    }

    public void DisableText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }

    public void AddMoney(int value)
    {
        bankAccount += value;
        bankAccountText.text = $"Bank account balance:\n${bankAccount}";
    }
}
