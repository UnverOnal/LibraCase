using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : Panel
{
    [SerializeField] private RectTransform scrollContent;
    [SerializeField] private Transform levelsPanel;

    private GameObject levelUiObject;

    private ScrollRect scrollRect;
    
    private List<LevelUi> levelUis = new List<LevelUi>();
    private LevelUi levelInProgress;
    
    private int levelCount;
    [HideInInspector] public int clickedLevelUiIndex;

    private void OnEnable()
    {
        UiManager.instance.onGamePanelUnloaded.AddListener(()=> Invoke(nameof(UpdateLevelUis), Time.deltaTime ));
    }

    private void OnDisable()
    {
        UiManager.instance.onGamePanelUnloaded.RemoveListener(()=> Invoke(nameof(UpdateLevelUis), Time.deltaTime ));
    }

    private void Start()
    {
        levelUiObject = Resources.Load<GameObject>("ui/levelUi");

        scrollRect = GetComponentInChildren<ScrollRect>();
        scrollRect.content = scrollContent;

        levelCount = GameManager.instance.totalLevelCount;
        SetLevelUis(levelUiObject,levelCount, ref levelUis);
    }

    private void SetLevelUis(GameObject levelUiGameObject, int _levelCount, ref List<LevelUi> _levelUis)
    {
        LevelUi previousLevelUi = default(LevelUi);
        for (int i = 1; i <= _levelCount; i++)
        {
            GameObject gO = Instantiate(levelUiGameObject, scrollContent);
            LevelUi levelUi = gO.GetComponent<LevelUi>();
            
            //Initialize setting level ui
            levelUi.Initialize(i, previousLevelUi);
            
            //Get previous data
            previousLevelUi = levelUi;
  
            _levelUis.Add(levelUi);
        }
        
        this.levelInProgress = GetProgressLevel();
        levelInProgress?.SetUnlockProgress();
    }

    //Gets a level ui block to update and updates level uis that are in the block. 
    public void UpdateLevelUis()
    {
        int beginingOfBlock = clickedLevelUiIndex - 1;
        int endOfBlock = levelUis.Count;
        for (int i = beginingOfBlock; i < endOfBlock; i++)
        {
            var levelUi = levelUis[i];
            levelUi.UpdateLevelUi();
        }

        this.levelInProgress = GetProgressLevel();
        levelInProgress?.SetUnlockProgress();
    }

    private LevelUi GetProgressLevel()
    {
        int index = 5;

        while (index < levelUis.Count && levelUis[index - 1].previousAverage >= 2)
            index += 5;

        if (index > levelUis.Count) return null;
        return levelUis[index - 1];
    }
}

