using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    [SerializeField]private Text levelText;

    [SerializeField] private Text bombCountText;
    
    private void OnEnable()
    {
        GameManager.instance.onLevelLoaded.AddListener(DisplayLevelCount);
    }

    private void OnDisable()
    {
        GameManager.instance.onLevelLoaded.RemoveListener(DisplayLevelCount);
    }

    private void DisplayLevelCount(int levelIndex)
    {
        levelText.text = "LEVEL " + levelIndex.ToString();
    }
    
    public void DisplayBombCount(int bombCount)
    {
        bombCountText.text = bombCount.ToString();
    }
}
