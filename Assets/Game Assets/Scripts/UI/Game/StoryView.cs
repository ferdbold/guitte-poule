using UnityEngine;
using UnityEngine.UI;
using Tendresse.Date;

public class StoryView : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private Text titleText;

    public void OnBeginDate() {
        DateStructure date = DateManager.instance.GetCurrentDate();
        titleText.text = "Soirée " + date.relationLevel + " avec Joséphine";
    }
}
