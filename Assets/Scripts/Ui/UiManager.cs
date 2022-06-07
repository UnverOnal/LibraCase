using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{   
    public static UiManager instance;

    [Header("Panels")]
    public MainPanel mainPanel;
    public GamePanel gamePanel;

    [HideInInspector] public UnityEvent onGamePanelLoaded = new UnityEvent();
    [HideInInspector] public UnityEvent onGamePanelUnloaded = new UnityEvent();
    [HideInInspector] public UnityEvent onMainPanelLoaded = new UnityEvent();

    public Vector2 referenceResolution => GetComponent<CanvasScaler>().referenceResolution;
    
    private void Awake() 
    {
        instance = Singleton.GetInstance<UiManager>();
    }

    private void OnEnable()
    {
        //Game panel listeners
        onGamePanelUnloaded.AddListener(gamePanel.DisableSmoothly);
        onGamePanelLoaded.AddListener(gamePanel.EnableSmoothly);
        
        //End panel listeners
        onMainPanelLoaded.AddListener(mainPanel.EnableSmoothly);
        onGamePanelLoaded.AddListener(mainPanel.DisableSmoothly);
        onGamePanelUnloaded.AddListener(mainPanel.EnableSmoothly);
    }    
    
    private void OnDisable()
    {
        //Game panel listeners
        onGamePanelUnloaded.RemoveListener(gamePanel.DisableSmoothly);
        onGamePanelLoaded.RemoveListener(gamePanel.EnableSmoothly);
        
        //End panel listeners
        onMainPanelLoaded.RemoveListener(mainPanel.EnableSmoothly);
        onGamePanelLoaded.RemoveListener(mainPanel.DisableSmoothly);
        onGamePanelUnloaded.RemoveListener(mainPanel.EnableSmoothly);

    }
}
