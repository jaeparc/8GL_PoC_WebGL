using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Networking;

public class CampusInfos : MonoBehaviour
{
    [Header("--- REFERENCES ---")]
    public TextMeshProUGUI CampusName;
    public TextMeshProUGUI CampusDescription;
    public Image CampusImage;
    [Header("--- VALUES ---")]
    public string BaseCampusID;

    private List<CatalogItem> CampusList = new List<CatalogItem>();
    // Start is called before the first frame update
    void Start()
    {
        GetCatalog();
    }

    void GetCatalog()
    {
        var request = new PlayFab.ClientModels.GetCatalogItemsRequest
        {
            CatalogVersion = "Campus" // Le nom que tu as donné dans PlayFab (Catalogs -> Nom)
        };
        PlayFabClientAPI.GetCatalogItems(request, OnCatalogSuccess, OnCatalogFailure);
    }


    void OnCatalogSuccess(PlayFab.ClientModels.GetCatalogItemsResult result)
    {
        CampusList = result.Catalog;
        DisplayCampusInfo(BaseCampusID);
    }

    void OnCatalogFailure(PlayFabError error)
    {
        Debug.LogError("Erreur lors de la récupération du catalogue : " + error.GenerateErrorReport());
    }

    public void DisplayCampusInfo(string id)
    {
        foreach (var item in CampusList)
        {
            if (item.ItemId == id)
            {
                CampusName.text = item.DisplayName;
                CampusDescription.text = item.Description;
                StartCoroutine(LoadImage(item.ItemImageUrl));
                return;
            }
        }
    }

    private IEnumerator LoadImage(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur lors du chargement de l'image : " + request.error);
            }
            else
            {
                byte[] bytes = request.downloadHandler.data;
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(bytes); // Convertit les bytes en texture
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                                              new Vector2(0.5f, 0.5f));
                CampusImage.sprite = sprite;
                CampusImage.preserveAspect = true;
            }
        }
    }
}
