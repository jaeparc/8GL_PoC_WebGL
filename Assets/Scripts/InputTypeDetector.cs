using UnityEngine;

public class InputTypeDetector : MonoBehaviour
{
    public enum LastInputType { None, Mouse, Touch }
    public LastInputType lastInput = LastInputType.None;
    public GameObject MobileUI;

    private float touchTimestamp = -1f;       // Quand un touch a eu lieu
    private const float touchBlockDelay = 1f; // Délai en secondes pour ignorer les clics après un touch

    void Update()
    {
        if (lastInput == LastInputType.None)
        {
            // Vérifier le touch en priorité
            if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                lastInput = LastInputType.Touch;
                touchTimestamp = Time.time;
                Debug.Log("Dernier input : Doigt");
                MobileUI.SetActive(true);
                return; // on sort direct pour éviter la souris dans ce frame
            }

            // Vérifier clic souris seulement si pas de touch récent
            if (Input.GetMouseButtonDown(0) || Input.anyKey)
            {
                if (Time.time - touchTimestamp > touchBlockDelay)
                {
                    lastInput = LastInputType.Mouse;
                    MobileUI.SetActive(false);
                    Debug.Log("Dernier input : Souris");
                }
            }
        }
    }
}
