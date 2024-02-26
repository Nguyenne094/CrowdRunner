using TMPro;
using UnityEngine;

public enum BonusType{
    Addition,
    Difference,
    Product,
    Division
}

public class DoubleDoors : MonoBehaviour
{
    #region SerializeField Setting

    [Header("Left Door Setting")]
    [SerializeField] private BonusType leftBonusType;
    [SerializeField] private SpriteRenderer leftDoorSprite;
    [SerializeField] private TextMeshProUGUI leftBonus_TMP;
    [SerializeField] private int leftBonusAmount;
    [SerializeField] private Color leftBonusColor;

    [Header("Right Door Setting")]
    [SerializeField] private BonusType rightBonusType;
    [SerializeField] private SpriteRenderer rightDoorSprite;
    [SerializeField] private TextMeshProUGUI rightBonus_TMP;
    [SerializeField] private int rightBonusAmount;
    [SerializeField] private Color rightBonusColor;

    #endregion

    #region Variable private
    private Collider col;
    #endregion

    #region Methods
    void Awake()
    {
        col = GetComponent<Collider>();
        if(leftDoorSprite == null || rightDoorSprite == null){
            Debug.LogWarning("Some doors are not assigned");
        }

        if(leftBonus_TMP == null || rightBonus_TMP == null){
            Debug.LogWarning("Some TMPs are not assigned");
        }
    }

    void Start()
    {
        SetupDoors();
    }

    void SetupDoors()
    {
        //* Left door
        switch (leftBonusType)
        {
            case BonusType.Addition:
                leftBonusAmount = Random.Range(0, 5);
                leftDoorSprite.color = leftBonusColor;
                leftBonus_TMP.text = "+" + leftBonusAmount.ToString();
                break;
            case BonusType.Difference:
                leftBonusAmount = Random.Range(1, 5);
                leftDoorSprite.color = leftBonusColor;
                leftBonus_TMP.text = "-" + leftBonusAmount.ToString();
                break;
            case BonusType.Product:
                leftBonusAmount = 2;
                leftDoorSprite.color = leftBonusColor;
                leftBonus_TMP.text = "x" + leftBonusAmount.ToString();
                break;
            case BonusType.Division:
                leftBonusAmount = Random.Range(2, 4);
                leftDoorSprite.color = leftBonusColor;
                leftBonus_TMP.text = "/" + leftBonusAmount.ToString();
                break;
            default:
                break;
        }

        //* Right door
        switch (rightBonusType)
        {
            case BonusType.Addition:
                rightBonusAmount = Random.Range(0, 5);
                rightDoorSprite.color = rightBonusColor;
                rightBonus_TMP.text = "+" + rightBonusAmount.ToString();
                break;
            case BonusType.Difference:
                rightBonusAmount = Random.Range(1, 5);
                rightDoorSprite.color = rightBonusColor;
                rightBonus_TMP.text = "-" + rightBonusAmount.ToString();
                break;
            case BonusType.Product:
                rightBonusAmount = 2;
                rightDoorSprite.color = rightBonusColor;
                rightBonus_TMP.text = "x" + rightBonusAmount.ToString();
                break;
            case BonusType.Division:
                rightBonusAmount = Random.Range(2, 4);
                rightDoorSprite.color = rightBonusColor;
                rightBonus_TMP.text = "/" + rightBonusAmount.ToString();
                break;
            default:
                break;
        }
    }

    public int GetBonusAmount(float x){
        if(x > 0) return rightBonusAmount;
        else return leftBonusAmount;
    }

    public BonusType GetBonusType(float x)
    {
        if(x > 0) return rightBonusType;
        else return leftBonusType;
    }

    internal void DisableCol()
    {
        col.enabled = false;
    }

    #endregion
}