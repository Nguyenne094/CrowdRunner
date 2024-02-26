using System.Collections;
using TMPro;
using UnityEngine;

public class CrowdWorldUI : MonoBehaviour
{
    [SerializeField] private GameObject crowdParent;
    
    IEnumerator Start()
    {
        yield return null;
        transform.GetComponentInChildren<TextMeshProUGUI>().text = crowdParent.transform.childCount.ToString();
        
        Enemy.onRunnerDie += RunnerCountTextUpdate;
        PlayerDetection.onDoorHit += RunnerCountTextUpdate;
    }

    private void OnDestroy()
    {
        Enemy.onRunnerDie -= RunnerCountTextUpdate;
        PlayerDetection.onDoorHit -= RunnerCountTextUpdate;
    }

    private void RunnerCountTextUpdate()
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = crowdParent.transform.childCount.ToString();
    }
}
