using Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Prefabs
{
    public class Course:UnityPoolObject
    {
        public int Id;
        public Image Image;
        public TMP_Text Name;
    }
}