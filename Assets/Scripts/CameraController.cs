using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Vairable SerializeField

    [SerializeField, Tooltip("Camera follows this target")] private GameObject target;
    [SerializeField, Tooltip("Camera will find Player follow this tag")] private string targetTag;
    #endregion

    #region Variable private

    private Vector3 dirToTarget;
    #endregion

    void Awake()
    {
        if(target == null){
            target = GameObject.FindGameObjectWithTag(targetTag);
        }
    }

    void Start()
    {
        dirToTarget = gameObject.transform.position - target.transform.position;
    }

    void Update()
    {
        transform.position = target.transform.position + dirToTarget;
    }
}
