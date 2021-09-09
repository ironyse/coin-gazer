using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public Button ResourceBtn;
    public Image ResourceImg;
    public Text ResourceDesc;
    public Text ResourceUpCost;
    public Text ResourceUnCost;

    private ResourceConfig _config;

    private int _level = 1;
    public bool IsUnlocked { get; private set; }
    
    public double GetOutput(){
        return _config.Output * _level;
    }

    public double GetUpgradeCost(){
        return _config.UpgradeCost * _level;
    }

    public double GetUnlockCost(){
        return _config.UnlockCost;
    }

    public void SetConfig(ResourceConfig config){
        _config = config;
        ResourceDesc.text = $"{_config.Name} Lv.{_level}\n+{GetOutput().ToString("0")}";
        ResourceUnCost.text = $"Unlock Cost\n{_config.UnlockCost}";
        ResourceUpCost.text = $"Upgrade Cost\n{GetUpgradeCost()}";

        SetUnlocked(_config.UnlockCost == 0);
    }
    
    public void UpgradeLevel()
    {        
        double upgradeCost = GetUpgradeCost();
        if (GameController.Instance.TotalGold < upgradeCost)
        {
            return;
        }
        GameController.Instance.AddGold(-upgradeCost);
        _level++;

        ResourceUpCost.text = $"Upgrade Cost\n{GetUpgradeCost()}";
        ResourceDesc.text = $"{_config.Name} Lv.{_level}\n+{GetOutput().ToString("0")}";
    }

    public void UnlockResource()
    {
        double unlockCost = GetUnlockCost();
        if (GameController.Instance.TotalGold < unlockCost) return;
        SetUnlocked(true);
        GameController.Instance.ShowNextResource();
        AchievementController.Instance.UnlockAchievement(AchievementType.UnlockResource, _config.Name);
    }

    public void SetUnlocked(bool unlocked)
    {
        IsUnlocked = unlocked;
        ResourceImg.color = IsUnlocked ? Color.white : Color.grey;
        ResourceUnCost.gameObject.SetActive(!unlocked);
        ResourceUpCost.gameObject.SetActive(unlocked);

    }

    void Start(){
        ResourceBtn.onClick.AddListener(()=> {            
            if (IsUnlocked) { UpgradeLevel(); }
            else { UnlockResource(); }
        });
    }
}
