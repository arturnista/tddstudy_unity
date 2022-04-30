using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuBoot : MonoBehaviour
{
    
    private MenuManager _menuManager;
    private MusicSystem _musicSystem;
    private ILocalizationSystem _localizationSystem;

    [Inject]
    private void Construct(MenuManager menuManager, MusicSystem musicSystem, ILocalizationSystem localizationSystem)
    {
        _menuManager = menuManager;
        _musicSystem = musicSystem;
        _localizationSystem = localizationSystem;
    }
    
    private void Start()
    {
        _menuManager.OpenMenu(MenuManager.UI_MAIN);
        _musicSystem.PlayMusic("music_menu");
    }

}
