using UnityEngine;
using UnityEngine.UI;


public class HelthBarVisual : MonoBehaviour
{
    [SerializeField] Image bar;


    private void Update() {
        bar.fillAmount = Player.Instance.Health/100;
    }
}
