using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region SerializeField Setting
    [Header("Setting")]
    public BonusType bonusType;
    public SpriteRenderer doorSprite;
    public TextMeshProUGUI bonus_TMP;
    public Color bonusColor;

    #endregion

    #region Variable private
    private int bonusAmount;

    #endregion

    #region Methods
    void Awake()
    {
        if(doorSprite == null || bonus_TMP == null){
            doorSprite = GetComponentInChildren<SpriteRenderer>();
            bonus_TMP = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Start()
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                bonusAmount = Random.Range(0, 5);
                doorSprite.color = bonusColor;
                bonus_TMP.text = "+" + bonusAmount.ToString();
                break;
            case BonusType.Difference:
            bonusAmount = Random.Range(1, 5);
                doorSprite.color = bonusColor;
                bonus_TMP.text = "-" + bonusAmount.ToString();
                break;
            case BonusType.Product:
                bonusAmount = 2;
                doorSprite.color = bonusColor;
                bonus_TMP.text = "x" + bonusAmount.ToString();
                break;
            case BonusType.Division:
            bonusAmount = Random.Range(2, 4);
                doorSprite.color = bonusColor;
                bonus_TMP.text = "/" + bonusAmount.ToString();
                break;
            default:
                break;
        }
    }

    #endregion
}