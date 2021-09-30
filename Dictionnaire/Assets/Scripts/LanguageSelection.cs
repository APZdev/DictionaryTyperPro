using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelection : MonoBehaviour
{
    private WordDictionaryManager wordDictionaryManager;

    [SerializeField] private Transform languageItemContainer;
    [SerializeField] private GameObject languageItem;
    [SerializeField] private GameObject languageSelectionPannel;

    private void Start()
    {
        wordDictionaryManager = GetComponent<WordDictionaryManager>();

        LoadDictionnaries();
    }

    public void LoadDictionnaries()
    {
        foreach (Transform child in languageItemContainer)
            Destroy(child.gameObject);


        string path = "";

        if (Application.isEditor)
            path = Application.dataPath + "/Ressources";
        else
        {
            path = Application.dataPath + "/Dictionaries";
            //Put the file in the root folder, not the "<NameOfProject>_Data" folder
            path = path.Replace("/DictionaryTyperPro_Data", "");
        }

        FileInfo[] fileInfo = new DirectoryInfo(path).GetFiles();
        foreach (var file in fileInfo)
        {
            if (!file.ToString().Contains(".meta"))
            {
                GameObject go =  Instantiate(languageItem, languageItemContainer);
                go.GetComponent<LanguageItem>().SetName(file.Name);
                go.GetComponent<Button>().onClick.AddListener(() => { 
                    wordDictionaryManager.StartGame(file.ToString());
                    languageSelectionPannel.SetActive(false);
                });
            }
        }
    }

}
