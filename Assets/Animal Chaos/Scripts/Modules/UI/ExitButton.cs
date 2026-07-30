using UnityEngine;
using UnityEngine.UI;

namespace GDPP2.UjiKom.UI {
    public class ExitButton : MonoBehaviour {
        [SerializeField] private Button _button;

        private void Start() {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() {
            Application.Quit();
        }
    }
}
