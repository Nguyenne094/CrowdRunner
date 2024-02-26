using UnityEngine;

public class Chunk : MonoBehaviour
{
    public static uint chunkWidth = 12;
    [SerializeField] private Transform chunkRender;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, chunkRender.localScale.y/2, 0), chunkRender.localScale);
    }

    public float GetLength(){
        return chunkRender.localScale.z;
    }
}
