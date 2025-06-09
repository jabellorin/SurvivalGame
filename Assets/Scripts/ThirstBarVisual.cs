using UnityEngine;
using UnityEngine.UI;

public class ThirstBarVisual : MonoBehaviour
{
    [SerializeField] Image bar;


    private void Update() {
        bar.fillAmount = Player.Instance.Thirst/100;
    }
}
