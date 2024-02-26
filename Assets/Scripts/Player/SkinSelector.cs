using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    private Runner _runner;

    private void Awake()
    {
        _runner = GetComponent<Runner>();
    }

    private void Start()
    {
        ShopManager.onSkinSelected += SelectSkin;
    }

    private void OnDestroy()
    {
        ShopManager.onSkinSelected -= SelectSkin;
    }

    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == skinIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                _runner.SetAnimator(transform.GetChild(i).GetComponent<Animator>());
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

