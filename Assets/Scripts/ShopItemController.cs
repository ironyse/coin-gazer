using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public Button ItemBtn;    
    public Image ItemImg;
    public Text ItemName;
    public Text ItemPrice;

    private ShopItem _shopItem;   

    private bool IsAvailable { get; set; }
    private double GetItemCost() { return _shopItem.Cost; }
    
    public void SetItem(ShopItem item)
    {
        _shopItem = item;
        ItemImg.sprite = _shopItem.sprite;
        ItemName.text = _shopItem.Name;
        ItemPrice.text = _shopItem.Cost.ToString("0");
        IsAvailable = _shopItem.Available;

    }

    void BuyItems()
    {
        double itemCost = GetItemCost();
        if (GameController.Instance.TotalGold < itemCost) return;
        IsAvailable = false;
        ItemBtn.enabled = false;
        ItemBtn.image.color = Color.gray;
        GameController.Instance.AddGold(-GetItemCost());
        AchievementController.Instance.UnlockAchievement(AchievementType.ItemBuy, _shopItem.Name);
    }

    void Start()
    {
        ItemBtn.onClick.AddListener(() =>
        {
            if (IsAvailable) { BuyItems(); }
        });
    }
}
