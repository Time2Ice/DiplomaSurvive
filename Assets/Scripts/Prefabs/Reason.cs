using Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Prefabs
{
    public class Reason:UnityPoolObject
    {
        public string Id;
        public Image Image;
        public TMP_Text Name;
        public Button SelectButton;
        public GameObject Mask;
    }
}