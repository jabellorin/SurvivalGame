using UnityEngine;
using UnityEngine.UI;

public class HungerBarVisual : MonoBehaviour
{
    [SerializeField] Image bar;


    private void Update() {
        bar.fillAmount = Player.Instance.Hunger / 100;
    }
}
