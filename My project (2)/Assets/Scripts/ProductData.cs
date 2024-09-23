using UnityEngine;

[CreateAssetMenu(fileName = "NewProductData", menuName = "ScriptableObjects/ProductModel")]
public class ProductData : ScriptableObject
{
    public enum PRODUCTTYPE
    {
        Beverages,
        Snacks,
        DairyProducts,
        FrozenFoods,
        PersonalCare,
        Miscellaneous
    }

    public PRODUCTTYPE productType;
    public int Index;
    public string Name;
    public int buyCost;
    public int sellCost;
    public Texture2D Image;
    public GameObject ProductModel;
}
