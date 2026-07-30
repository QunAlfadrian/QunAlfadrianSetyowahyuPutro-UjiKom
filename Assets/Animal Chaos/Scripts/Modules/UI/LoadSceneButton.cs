using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GDPP2.UjiKom.UI {
    public class LoadSceneButton : MonoBehaviour {
        [SerializeField] private Button _button;
        [SerializeField] private string _sceneName;

        private void Start() {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
