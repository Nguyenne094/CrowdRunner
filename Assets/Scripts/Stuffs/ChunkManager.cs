using UnityEngine;

// Summary:
//      Manage Chunks
public class ChunkManager : Singleton<ChunkManager>
{
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private GameObject finishLine;

    void Start()
    {
        GenerateLevel();
        
        //Batching draw calls for optimization
        StaticBatchingUtility.Combine(this.gameObject);
        
        finishLine = GameObject.FindWithTag("Finish");
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel();
        currentLevel %= levels.Length;

        LevelSO level = levels[currentLevel];
        CreateLevel(level.chunks);
    }

    private void CreateLevel(Chunk[] orderedLevelChunks){
        Vector3 chunkPosition = transform.position;

        for(int i = 0; i < orderedLevelChunks.Length; i++){
            Chunk currentChunkType = orderedLevelChunks[i];

            chunkPosition.z += currentChunkType.GetLength() / 2;

            Chunk newChunk = Instantiate(currentChunkType, chunkPosition, transform.rotation, transform);

            chunkPosition.z += newChunk.GetLength() / 2;
        }
    }

    public float GetFinishZ(){
        return finishLine.transform.position.z;
    }

    public int GetLevel(){
        return PlayerPrefs.GetInt("level");
    }
}
