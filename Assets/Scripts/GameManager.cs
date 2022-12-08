using System;
using System.Collections;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isPaused;
    public bool isBathingInLight;
    public bool isLookingAtAngel;
    public bool areSubtitlesActivated = true;

    public bool areMirrorsStep;
    public bool isInteractingWithMirror;

    //health System implementation
    public float health = 100; 

    [HideInInspector] public StarterAssetsInputs playerInput;
    
    public static GameManager Instance;

    public AK.Wwise.Event PauseVA;
    public AK.Wwise.Event ResumeVA;

    private float wTime;

    [SerializeField] private CinemachineVirtualCamera mainVC;
    [SerializeField] private CinemachineVirtualCamera socketVC;
    [SerializeField] private CinemachineVirtualCamera pillarVC;

    [SerializeField] private bool _isIntro;
    
    public bool debug;
    public bool hasPickedLantern;

    public bool canPlayChatterTwo = false;
    public VoiceLineSequenceSO chatterTwo;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    private void Start()
    {
        if (_isIntro) return;
        playerInput = FindObjectOfType<StarterAssetsInputs>();
        TooltipManager.Instance.ToggleTooltip("Use the WASD keys to move");
        TooltipManager.Instance.currentTooltip = TooltipTypes.WASDMove;
    }

    void Update()
    {
		if (_isIntro) return;

        if (health <= 40 && hasPickedLantern && canPlayChatterTwo)
        {
            PlayerSoundController.Instance.InitVoiceLineSequence(chatterTwo);
            canPlayChatterTwo = false;
        }

        if (Keyboard.current.wKey.isPressed && wTime < 1.2f)
        {
            wTime += Time.deltaTime;
        }

        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            wTime = 0;
        }

        if (wTime > 1.2f && TooltipManager.Instance.currentTooltip == TooltipTypes.WASDMove)
        {
            TooltipManager.Instance.CloseTooltip();
        }
        
        if (playerInput.pause)
        {
            isPaused = !isPaused;
            PauseManager.Instance.ToggleMenu(isPaused);
            if (isPaused)
            {
                PauseVA.Post(PlayerSoundController.Instance.gameObject);
            }
            else
            {
                ResumeVA.Post(PlayerSoundController.Instance.gameObject);
            }
            playerInput.pause = false;
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction *= 100, Color.green);
        RaycastHit hit;   
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.CompareTag("ChargingTarget"))
            {
                isLookingAtAngel = true;
            }
            else
            {
                isLookingAtAngel = false;
            }
        }
        else if (isLookingAtAngel)
        {
            //has exited charging target
            LanternManager.Instance.canPlayChargeSound = true;
            isLookingAtAngel = false;
        }
    }

    public void InitSocketEvent()
    {
        StartCoroutine(StartSocketEvent());
    }
    
    public void InitPillarEvent()
    {
        StartCoroutine(StartPillarEvent());
    }

    private IEnumerator StartSocketEvent()
    {
        TooltipManager.Instance.currentTooltip = TooltipTypes.SeeSocket;
        TooltipManager.Instance.ToggleTooltip("Place the Lantern in the Socket to shine a light beam");
        socketVC.gameObject.SetActive(true);
        mainVC.gameObject.SetActive(false);
        isInteractingWithMirror = true;
        yield return new WaitForSecondsRealtime(2f);
        mainVC.gameObject.SetActive(true);
        socketVC.gameObject.SetActive(false);
        TooltipManager.Instance.CloseTooltip();
        yield return new WaitForSecondsRealtime(1f);
        canPlayChatterTwo = true;
        isInteractingWithMirror = false;
    }
    
    private IEnumerator StartPillarEvent()
    {
        pillarVC.gameObject.SetActive(true);
        mainVC.gameObject.SetActive(false);
        isInteractingWithMirror = true;
        yield return new WaitForSecondsRealtime(6f);
        mainVC.gameObject.SetActive(true);
        pillarVC.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        isInteractingWithMirror = false;
    }
}
