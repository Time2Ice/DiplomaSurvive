using System.Globalization;
using TMPro;
using UiScenario;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class TopLobbyMenuView:Contractor.View
    {

        [SerializeField] private TMP_Text _universityCountText;
        [SerializeField] TMP_Text _balanceText;
        [SerializeField] TMP_Text _semesterText;
        [SerializeField] TMP_Text _positionText;
        [SerializeField] TMP_Text _personalLifeText;
        [SerializeField] TMP_Text _marksText;

        [SerializeField] private Slider _personalLifeSlider;
        [SerializeField] private Slider _marksSlider;

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _shopButton;
        public override void Initialize(IWindowInfrastructure infrastructure, RectTransform parent)
        {
            base.Initialize(infrastructure, parent);

            // Infrastructure.BinderAgg.Bind(this);
        }
        
        public void SetBalance(int value)
        {
            _balanceText.text = value.ToString(CultureInfo.InvariantCulture);
        }

        public void SetUniversityCount(int value)
        {
            _universityCountText.text = value.ToString(CultureInfo.InvariantCulture)+"university";
        }
        public void SetSemester(int value)
        {
            _semesterText.text = value.ToString(CultureInfo.InvariantCulture)+"semester";
        }
        public void SetPosition(int value)
        {
            _positionText.text = "Position: "+value.ToString(CultureInfo.InvariantCulture);
        }
        
        public void SetPersonalLife(int currentValue, int maxValue)
        {
            _personalLifeSlider.value = (float) currentValue / maxValue;
            _personalLifeText.text = currentValue.ToString(CultureInfo.InvariantCulture) +
                                 "/" + maxValue.ToString(CultureInfo.InvariantCulture);
        }
        
        public void SetMarks(int currentValue, int maxValue)
        {
            _marksSlider.value = (float) currentValue / maxValue;
            _marksText.text = currentValue.ToString(CultureInfo.InvariantCulture) +
                                     "/" + maxValue.ToString(CultureInfo.InvariantCulture);
        }
        

    }
}