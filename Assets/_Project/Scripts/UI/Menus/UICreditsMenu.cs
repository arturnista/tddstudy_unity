using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI.Menus
{

    [System.Serializable]
    struct CreditsMember
    {
        public string name;
        public string link;
        public bool showLink;
    }

    [System.Serializable]
    struct CreditsArea
    {
        public string title;
        public List<CreditsMember> members;
    }

    [System.Serializable]
    struct Credits
    {
        public List<CreditsArea> areas;
    }
    
    public class UICreditsMenu : MonoBehaviour
    {
        
        [SerializeField] private Button _backButton;
        [Space]
        [SerializeField] private Transform _listParent;
        [SerializeField] private TextMeshProUGUI _titleTemplate;
        [SerializeField] private TextMeshProUGUI _memberTemplate;
        [Space]
        [SerializeField] private TextAsset _creditsText;
        
        private MenuManager _menuManager;
        
        [Inject]
        private void Construct(MenuManager menuManager)
        {
            _menuManager = menuManager;
        }
        
        private void Awake()
        {
            var credits = JsonUtility.FromJson<Credits>(_creditsText.text);

            foreach (var area in credits.areas)
            {
                var areaTitle = Instantiate(_titleTemplate, _listParent);
                areaTitle.gameObject.SetActive(true);
                areaTitle.text = area.title;
                foreach (var member in area.members)
                {
                    var memberName = Instantiate(_memberTemplate, _listParent);
                    memberName.text = member.name;
                    if (member.showLink)
                    {
                        memberName.text += $"\n<size=70%>{member.link}</size>";
                    }
                    memberName.GetComponent<UILink>().Construct(member.link);
                    memberName.gameObject.SetActive(true);
                }
            }
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(HandleBackButtonClick);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(HandleBackButtonClick);
        }

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(_backButton.gameObject);
        }

        private void HandleBackButtonClick()
        {
            _menuManager.BackMenu();
        }
        
    }

}