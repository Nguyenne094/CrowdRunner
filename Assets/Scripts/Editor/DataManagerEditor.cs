using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

// [CustomEditor(typeof(DataManager))]
public class DataManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DataManager dataManager = target as DataManager;

        if(GUILayout.Button("Reset Data")){
            PlayerPrefs.DeleteAll();

            // dataManager.UpdateCoinsText();
            ShopManager.Instance.ConfigureSkinButtons();
        }
    }
}