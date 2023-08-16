using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    private const string _itemResourcesPath = "Prefab/Items/";

    [SerializeField] private Player _player;
    [SerializeField] private ProgressSaveLoader _progressSaveLoader;

    public int CurrentPoints { get; private set; }

    private int _lastEvent;
    private Item _lastItem;

    public Player Player => _player;
    public int LastEvent => _lastEvent;
    public Item LastItem => _lastItem;

    private void Start()
    {
        /*var data = _progressSaveLoader.GetData();

        Player.SetLevel(data.Level);
        Player.AddCoins(data.Coins);

        Player.Skills.Power.ChangeLevel(data.SkillsData.Power);
        Player.Skills.Agility.ChangeLevel(data.SkillsData.Agility);
        Player.Skills.Endurance.ChangeLevel(data.SkillsData.Endurance);
        Player.Skills.Luck.ChangeLevel(data.SkillsData.Luck);

        Player.Inventory.AddKey(Key.KeyType.silver, data.InventoryData.SilverKeys);
        Player.Inventory.AddKey(Key.KeyType.gold, data.InventoryData.GoldKeys);
        Player.Inventory.AddPotion(data.InventoryData.Potions);

        if (data.HasWeapon)
            Player.UseWeapon(Resources.Load<Weapon>(_itemResourcesPath + "Weapons/" + data.WeaponData.WeaponID));

        if (data.HasCostume)
            Player.AddArmor(Resources.Load<Armor>(_itemResourcesPath + "Armor/Costume/" + data.ArmorData.CostumeID));

        if (data.HasHelm)
            Player.AddArmor(Resources.Load<Armor>(_itemResourcesPath + "Armor/Helm/" + data.ArmorData.HelmID));

        if (data.HasShoes)
            Player.AddArmor(Resources.Load<Armor>(_itemResourcesPath + "Armor/Shoes/" + data.ArmorData.ShoesID));

        Player.UpdateParameters();*/
    }

    private void OnEnable()
    {
        _player.ExperienceAdded.AddListener(AddPoints);
    }

    private void OnDisable()
    {
        _player.ExperienceAdded.RemoveListener(AddPoints);
    }

    public void AddPoints(int count)
    {
        CurrentPoints += count;
    }

    public int SetLevel()
    {
        int playerLevel = _player.GetLevel();

        if (playerLevel <= 1)
            return playerLevel;

        return Random.Range(playerLevel - 1, playerLevel + 1);
    }

    public void SetLastEvent(int previousEventNumber)
    {
        _lastEvent = previousEventNumber;
    }

    public void SetLastItem(Item item)
    {
        _lastItem = item;
    }
}