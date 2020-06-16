using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OpenMinesweeper.Core;
using OpenMinesweeper.Core.Generic;
using OpenMinesweeper.NET.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using OpenMinesweeper.Core.Utils;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// Defines the main data model of the game.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private MinesweeperCore core = null;

        private GameGridViewModel gameGridVM = null;
        public GameGridViewModel GameGridVM
        {
            get => gameGridVM;
            set
            {
                gameGridVM = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<SoftwareConfig.GeneralResources.LanguageResource> languages;
        public ObservableCollection<SoftwareConfig.GeneralResources.LanguageResource> Languages
        {
            get => languages;
            set
            {
                languages = value;
                RaisePropertyChanged();
            }
        }

        private ObservableDictionary<string, string> languageContent;
        public ObservableDictionary<string, string> LanguageContent
        {
            get => languageContent;
            set
            {
                languageContent = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raised once the game is over.
        /// </summary>
        public event EventHandler OnGameOver;
        /// <summary>
        /// Event raised once the player wins the game.
        /// </summary>
        public event EventHandler OnGameWon;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core"></param>
        public MainViewModel(MinesweeperCore core)
        {
            this.core = core;

            GameGridVM = new GameGridViewModel();

            NewGame = new RelayCommand(() => NewGameExecute(), () => true);
            SaveGame = new RelayCommand<object>((obj) => SaveGameExecute(obj), (obj) => true);
            LoadGame = new RelayCommand<object>((obj) => LoadGameExecute(obj), (obj) => true);
            ChangeLanguage = new RelayCommand<object>((obj) => ChangeLanguageExecute(obj), (obj) => true);

            Languages = new ObservableCollection<SoftwareConfig.GeneralResources.LanguageResource>();
            LanguageContent = new ObservableDictionary<string, string>();

            //Loads the supported languages.
            if (LoadLanguages())
            {
                //Loads the current language strings.
                LoadLanguageStrings();
            }

            Messenger.Default.Register<SystemMessage>(this, (action) => OnSystemMessageReceived(action));
        }

        #region Methods

        /// <summary>
        /// Handles messages sent over MVVMLight messaging.
        /// </summary>
        /// <param name="message"></param>
        private void OnSystemMessageReceived(SystemMessage message)
        {
            if(message.Target == GetType())
            {
                if(message.Message == "GameOver")
                {
                    OnGameOver?.Invoke(this, null);
                }
                else if (message.Message == "GameWon")
                {
                    OnGameWon?.Invoke(this, null);
                }
                else if (message.Message == "LoadedGameState")
                {
                    GameState gameState = message.ExtendedData as GameState;
                    if(gameState != null)
                    {
                        string state = string.Empty;
                        GameGrid gameGrid = core.FromGameState(gameState, out state);
                        if(gameGrid != null)
                        {
                            GameGridVM.Load(gameGrid);
                            GameGridVM.LoadState(state);
                        }
                    }
                }
                else if (message.Message == "NewGame")
                {
                    GameGrid gameGrid = message.ExtendedData as GameGrid;
                    if (gameGrid != null)
                    {
                        GameGridVM.Load(gameGrid);
                    }
                }
            }
        }

        private bool LoadLanguages()
        {
            //Set Languages
            var _languages = core.ConfigurationLoader.
                GetFullConfig().
                Resources.
                Where(x => x is SoftwareConfig.GeneralResources.LanguageResource).
                Select(x => x as SoftwareConfig.GeneralResources.LanguageResource);
            if (_languages != null && _languages.Any())
            {
                Languages = new ObservableCollection<SoftwareConfig.GeneralResources.LanguageResource>(_languages);
                return true;
            }

            return false;
        }

        private void LoadLanguageStrings()
        {
            var currentLang = core.ConfigurationLoader.GetFullConfig().CurrentSettings.FirstOrDefault(x => x.Key == "LanguageKey");
            if (currentLang != null)
            {
                var _langRes = core.ConfigurationLoader.
                                GetFullConfig().
                                Resources.
                                FirstOrDefault(x => (x is SoftwareConfig.GeneralResources.LanguageResource) && (x as SoftwareConfig.GeneralResources.LanguageResource).Key == currentLang.Value);
                if (_langRes != null && _langRes.Content != null && _langRes.Content.Any())
                {
                    LanguageContent = _langRes.Content.ToObservableDictionary(x => x.Key, x => x.Value);
                }
            }
            else
            {
                var _langRes = core.ConfigurationLoader.
                               GetFullConfig().
                               Resources.
                               FirstOrDefault(x => (x is SoftwareConfig.GeneralResources.LanguageResource) && (x as SoftwareConfig.GeneralResources.LanguageResource).LanguageName == Languages.FirstOrDefault().LanguageName);
                if (_langRes != null && _langRes.Content != null && _langRes.Content.Any())
                {
                    LanguageContent = _langRes.Content.ToObservableDictionary(x => x.Key, x => x.Value);
                }
            }
        }

        #endregion

        #region Commands

        public ICommand NewGame { get; private set; }
        private void NewGameExecute()
        {
            var newGameGrid = core.NewGame(4);
            GameGridVM.Load(newGameGrid);
        }

        public ICommand SaveGame { get; private set; }
        private void SaveGameExecute(object parameters)
        {
            if (parameters is null) return;

            object[] input = parameters as object[];
            if (input.Length < 2) return;

            string folder = input[0] as string;
            if (string.IsNullOrEmpty(folder)) return;

            string filename = input[1] as string;
            if (string.IsNullOrEmpty(filename)) return;

            string current_state = GameGridVM.GetUIState();
            if (string.IsNullOrEmpty(current_state)) return;

            GameGrid gameGrid = GameGridVM.ToGameGrid();
            if (gameGrid is null) return;

            core.SaveGame(gameGrid, current_state, folder, filename);
        }

        public ICommand LoadGame { get; private set; }
        private void LoadGameExecute(object parameters)
        {
            //if (parameters is null) return;

            //object[] input = parameters as object[];
            //if (input.Length < 2) return;

            //string folder = input[0] as string;
            //if (string.IsNullOrEmpty(folder)) return;

            //string filename = input[1] as string;
            //if (string.IsNullOrEmpty(filename)) return;

            //string current_state = GameGridVM.GetUIState();
            //if (string.IsNullOrEmpty(current_state)) return;

            //GameGrid gameGrid = GameGridVM.ToGameGrid();
            //if (gameGrid is null) return;

            //core.SaveGame(gameGrid, current_state, folder, filename);
        }

        public ICommand ChangeLanguage { get; private set; }
        private void ChangeLanguageExecute(object parameters)
        {
            if (parameters is null) return;

            //Gets the selected language
            var language = parameters as SoftwareConfig.GeneralResources.LanguageResource;
            if(language != null)
            {
                if(core.ConfigurationLoader.UpdateCurrentResource(SoftwareConfigLoader.KEY_LANGUAGE, language.Key))
                {
                    //Update all strings in the game
                    LanguageContent = language.Content.ToObservableDictionary(x => x.Key, x => x.Value);
                }
            }
        }

        #endregion
    }
}
