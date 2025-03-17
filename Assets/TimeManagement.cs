using SCPE;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class TimeManagement : MonoBehaviour
{
    public bool isCamActivated;
    public int doorsActive;
    public int lightsActive;

    public bool isLeftDoorClosed;
    public bool isRightDoorClosed;
    public bool warningLeftDoor;
    public bool warningRightDoor;
    public bool enemyLeftDestroyed;
    public bool enemyRightDestroyed;

    public Transform gameOverEnemyPos;

    public PostProcessVolume postProcessVolume;

    [SerializeField] private int hourDuration;

    [SerializeField] private int currHour = 0;
    [SerializeField] private int currNight;

    [SerializeField] private Image usageSlider;

    [SerializeField] private float powerLeft;
    [SerializeField] private float usageSpeed;

    [SerializeField] private MoveController moveController; 

    [SerializeField] private TextType nightPanelText;
    [SerializeField] private TextType gameOverPanelText;

    [SerializeField] private TMP_Text nightPanelTextComponent;
    [SerializeField] private TMP_Text powerLeftText;
    [SerializeField] private TMP_Text hourText;
    [SerializeField] private TMP_Text nightText;

    [SerializeField] private CamButton camButton;

    [SerializeField] private Transform cameraRoot;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject lightSecurity;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject nightPanel;

    [SerializeField] private RandomEvents randomEvents;

    private float _usage;
    private int _eventHour = 3;
    private Danger _danger;
   
    
    
    IEnumerator Hour()
    {
        yield return new WaitForSeconds(hourDuration);
        currHour++;
        if (currHour != 6) 
        {
            StartCoroutine(Hour());
        }
        else
        {
            currHour = 0;
           StartCoroutine(NightScreen());
            

        }
        if(currHour == _eventHour)
        {
            randomEvents.EnableEvent();
        }
        UpdateUI();
        
    }

    private void Start()
    {
        StartCoroutine (Hour());
        randomEvents.EnemySwitchState();
        if (postProcessVolume.profile.TryGetSettings(out _danger))
        {
            _danger.active = false;
        }
    }
    public void StartdangerEffect(float targetValue, float duration)
    {
        if (_danger == null) return;

        _danger.active = true;

        StartCoroutine(ChangedangerValue(0.4f, duration));
    }

    IEnumerator ChangedangerValue(float targetValue, float duration)
    {
        float startValue = 0;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _danger.size.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null;
        }

        _danger.size.value = targetValue;
    }
    private void Update()
    {
        powerLeft -= _usage * Time.deltaTime * usageSpeed;
        
        if (powerLeft <= 0)
            PowerLost();
        else
            UpdateUI();
        if (Input.GetKeyUp(KeyCode.S))
        {
            print("skip");
            EndNight();
        }
    }
    void PowerLost()
    {
        StopAllCoroutines();
        gameOverPanel.SetActive(true);
    }
    float Usage()
    {
        float usageValue = 0;
        
        if (isCamActivated)
        {
            usageValue += 0.2f;
        }
        usageValue += 0.2f * doorsActive + 0.2f * lightsActive;
        _usage = usageValue;
        return usageValue;
    }

    public void UpdateUI()
    {
        hourText.text = currHour.ToString()+" AM";
        nightText.text = currNight.ToString()+" Night";
        powerLeftText.text = "Power left: " + Mathf.RoundToInt(powerLeft).ToString() + "%"; 
    }
    public void UpdateSlider()
    {
        usageSlider.fillAmount = Usage();
    }
    void EndNight()
    {
        powerLeft = 100;
        currNight++;
        
        if(currNight >= 2)
        {
            randomEvents.EnemySwitchState();
        }
        _eventHour = Random.Range(2, 4);
        StartCoroutine (Hour());
    }
    public IEnumerator GameOver(string monsterName)
    {
        lightSecurity.gameObject.SetActive(false);
        ui.SetActive(false);
        StartdangerEffect(0.4f,1);
        moveController.RotateCenter();
        if (camButton.camPanel.activeSelf)
        {
            camButton.SwitchCamButton();
            cameraRoot.eulerAngles = new Vector3(0,180,0); 
        }
        yield return new WaitForSeconds(3);

        //StopAllCoroutines();
        gameOverPanel.SetActive(true);
        gameOverPanelText.message = "Killed by: " + monsterName;
    }
    IEnumerator NightScreen()
    {
        
        nightPanelTextComponent.text = "";
        nightPanelText.message = "Night " + (currNight + 1).ToString();
        nightPanel.SetActive(true);
        nightPanel.GetComponent<Animator>().Play("nightScreen");
        yield return new WaitForSeconds(3);
        EndNight();
        yield return new WaitForSeconds(2);
        nightPanel.SetActive(false);
    }
}
