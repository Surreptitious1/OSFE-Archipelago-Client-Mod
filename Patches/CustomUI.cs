using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Archipelago.MultiClient.Net;
using ArchipelagoClient;
using HarmonyLib;
using TMPro;
using TwitchLib.Api.Models.v5.Games;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;


public class ArchiConfig
{
    private static readonly object initlock = new object();
    public bool settings_ui_initialized = false;
    public bool mainmenu_ui_initialized = false;
    
    public TMP_InputField hostname_field = null;
    public TMP_InputField port_field = null;
    public TMP_InputField slotname_field = null;
    public TMP_InputField password_field = null;

    public string hostname
    {
        get
        {
            String defaultHostname = "archipelago.gg";
            if (!settings_ui_initialized) { return SaveDataCtrl.Get("ArchiConfig.hostname", defaultHostname); }
            return hostname_field.text != "" ?  hostname_field.text : SaveDataCtrl.Get("ArchiConfig.hostname", defaultHostname);
        }
        set
        {
            if (!settings_ui_initialized) { return; }
            hostname_field.text = value;
        }
    }
    public string port
    {
        get
        {
            String defaultPort = "38281";
            if (!settings_ui_initialized) { return SaveDataCtrl.Get("ArchiConfig.port", defaultPort); }
            return port_field.text != "" ?  port_field.text : SaveDataCtrl.Get("ArchiConfig.port", defaultPort);
        }
        set
        {
            if (!settings_ui_initialized) { return; }
            port_field.text = value;
        }
    }
    public string slotname
    {
        get
        {
            if (!settings_ui_initialized) { return SaveDataCtrl.Get("ArchiConfig.slotname", ""); }
            return slotname_field.text != "" ?  slotname_field.text : SaveDataCtrl.Get("ArchiConfig.slotname", "");
        }
        set
        {
            if (!settings_ui_initialized) { return; }
            slotname_field.text = value;
        }
    }
    public string password
    {
        get
        {
            if (!settings_ui_initialized) { return SaveDataCtrl.Get("ArchiConfig.password", ""); }

            return password_field.text != "" ? password_field.text : SaveDataCtrl.Get("ArchiConfig.password", "");
        }
        set
        {
            if (!settings_ui_initialized) { return; }
            password_field.text = value;
        }
    } 

    private static ArchiConfig _AC = null;
    private ArchiConfig() {}
    
    public static ArchiConfig I
    {
        get
        {
            lock (initlock)
            {
                if (_AC == null)
                {
                    _AC = new ArchiConfig();
                } 
                return _AC;
            }
        }
    }
}

/**
 * Enormous shoutouts to @goldenepsilon's MoreLuaPower, from which a substantial
 * amount of UI boilerplate was taken
 */
[HarmonyPatch]
public class UIPatches
{
    private static SettingsPane archiSettingsPane = null;

    [HarmonyPatch(typeof(OptionCtrl))]
    [HarmonyPatch(nameof(OptionCtrl.Open))]
    public class OptionCtrlOpenPatch
    {

        public static void Prefix(OptionCtrl __instance)
        {
            if (ArchiConfig.I.settings_ui_initialized)
            {
                return;
            }

            Debug.Log("Initializing Settings UI");
            var settingsBtn = Object.Instantiate(__instance.navButtonGrid.GetChild(1), __instance.navButtonGrid);
            settingsBtn.GetComponent<UIButton>().tmpText.text = "ARCHIPELAGO";
            settingsBtn.GetChild(0).GetComponent<I2.Loc.Localize>().Term = "ARCHIPELAGO";
            if (settingsBtn.parent.childCount >= 4)
            {
                settingsBtn.SetSiblingIndex(3);
            }

            var archiSettingsMenu = Object.Instantiate(__instance.settingsPane.gameObject);
            archiSettingsMenu.transform.position = __instance.settingsPane.transform.position;
            archiSettingsMenu.transform.SetParent(__instance.settingsPane.transform.parent);

            // Pull from existing fields to propagate into a new pane
            var genericSettingsPane = archiSettingsMenu.GetComponent<SettingsPane>();
            GameObject _content = genericSettingsPane.content;
            SlideBody _slideBody = genericSettingsPane.slideBody;
            Animator _anim = genericSettingsPane.anim;
            TMP_Text _title = genericSettingsPane.title;
            UIButton _defaultButton = genericSettingsPane.defaultButton;
            UIButton _backButton = genericSettingsPane.backButton;
            UIButton _originButton = genericSettingsPane.originButton;
            //Object.DestroyImmediate(genericSettingsPane);

            archiSettingsPane = archiSettingsMenu.AddComponent<SettingsPane>();
            archiSettingsPane.content = _content;
            archiSettingsPane.slideBody = _slideBody;
            archiSettingsPane.anim = _anim;
            archiSettingsPane.title = _title;
            archiSettingsPane.defaultButton = _defaultButton;
            archiSettingsPane.backButton = _backButton;
            archiSettingsPane.originButton = _originButton;

            Object.DestroyImmediate(settingsBtn.GetComponent<Button>());
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((data) =>
                {
                    S.I.optCtrl.OpenPanel(archiSettingsPane.GetComponent<NavPanel>());
                });
                EventTrigger trigger = settingsBtn.gameObject.AddComponent<EventTrigger>();
                trigger.triggers.Add(entry);
                trigger.enabled = false;
            }

            var navPanelBackground = archiSettingsMenu.transform.GetChild(0).GetChild(0);
            var navPanelButtons = archiSettingsMenu.transform.GetChild(0).GetChild(1);
            var navPanelTitle = archiSettingsMenu.transform.GetChild(0).GetChild(2);
            var navPanelExit = archiSettingsMenu.transform.GetChild(0).GetChild(3);

            navPanelTitle.GetComponent<TextMeshProUGUI>().text = "ARCHIPELAGO SETTINGS";
            navPanelTitle.GetComponent<I2.Loc.Localize>().Term = "ARCHIPELAGO SETTINGS";

            for (int i = 0; i < navPanelButtons.transform.childCount; i++)
            {
                navPanelButtons.transform.GetChild(i).gameObject.SetActive(false);
            }

            int fieldCount = 0;

            TMP_InputField CreateTextField(string fieldName, string placeholder = "")
            {
                Transform genericTextField = S.I.heCtrl.content.transform.Find("SeedField");
                TextMeshProUGUI genericTMPUGUI = navPanelButtons.transform.GetChild(0).GetChild(0)
                    .GetComponentInChildren<TextMeshProUGUI>();
                NavTextfield genericTextbox =
                    S.I.heCtrl.content.transform.Find("SeedField").GetComponent<NavTextfield>();

                var obj = Object.Instantiate(genericTextField.gameObject, navPanelButtons.transform);
                obj.SetActive(true);
                obj.transform.GetComponent<NavTextfield>().name = "CustomArchi" + fieldName;

                Object.DestroyImmediate(obj.GetComponent<Image>());
                obj.AddComponent<TextMeshProUGUI>();

                var TMPUGUI = obj.GetComponent<TextMeshProUGUI>();
                TMPUGUI.text = fieldName + ":";
                TMPUGUI.fontSize = genericTMPUGUI.fontSize;
                TMPUGUI.font = genericTMPUGUI.font;

                var navTextfield = obj.GetComponent<NavTextfield>();
                navTextfield.useParentTransformNav = true;
                navTextfield.transform.GetChild(0).GetChild(0).Translate(0, -1f, 0);
                navTextfield.down = null;
                navTextfield.up = null;

                var tmpInputField = obj.GetComponent<TMP_InputField>();
                tmpInputField.textComponent.fontSize = genericTMPUGUI.fontSize;
                tmpInputField.textComponent.color = new Color(0.8f, 0.8f, 0.8f);
                tmpInputField.textComponent.font = genericTMPUGUI.font;
                tmpInputField.textComponent.alignment = TextAlignmentOptions.Left;
                tmpInputField.placeholder.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                tmpInputField.placeholder.GetComponent<TextMeshProUGUI>().fontSize = genericTMPUGUI.fontSize;
                tmpInputField.placeholder.GetComponent<TextMeshProUGUI>().SetText(placeholder);
                tmpInputField.placeholder.GetComponent<I2.Loc.Localize>().Term = "";

                tmpInputField.characterLimit = 256;

                TMPUGUI.ForceMeshUpdate();
                obj.transform.Translate(0, ++fieldCount * -30f, 0);
                float xLeft = TMPUGUI.GetTextInfo(fieldName).characterInfo[0].topLeft.x;
                float xRight = TMPUGUI.GetTextInfo(fieldName).characterInfo[fieldName.Length - 1].bottomRight.x;
                float width = xRight - xLeft;
                float fieldEnd = TMPUGUI.transform.position.x + width - navTextfield.transform.GetChild(0).position.x;
                // The field is not vertically aligned, so also shift it up
                navTextfield.transform.GetChild(0).Translate(fieldEnd + 5f, 2.5f, 0);
                return tmpInputField;
            }

            ArchiConfig.I.hostname_field = CreateTextField("Server", SaveDataCtrl.Get("ArchiConfig.hostname", "archipelago.gg"));
            ArchiConfig.I.port_field = CreateTextField("Port", SaveDataCtrl.Get("ArchiConfig.port", "38281"));
            ArchiConfig.I.slotname_field = CreateTextField("Slot Name", SaveDataCtrl.Get("ArchiConfig.slotname", ""));
            ArchiConfig.I.password_field = CreateTextField("Password", SaveDataCtrl.Get("ArchiConfig.password", ""));
            ArchiConfig.I.settings_ui_initialized = true;

            Debug.Log("UI Init Success");
        }
    }

    [HarmonyPatch(typeof(MainCtrl))]
    [HarmonyPatch(nameof(MainCtrl.Open))]
    class MainCtrlOpenPatch
    {
        public static void Prefix(MainCtrl __instance)
        {
            void EnableButton(NavButton btn)
            {
                btn.gameObject.SetActive(true);
                btn.GetComponent<Animator>().SetBool("visible", true);
            }
            
            if (ArchiConfig.I.mainmenu_ui_initialized)
            {
                return;
            } 
            
            Debug.Log("Initializing Main Menu UI");
            
            IEnumerator enumerator = (IEnumerator) __instance.mainMenuLeftButtonGrid.GetEnumerator();
            Transform child;
            try
            {
                while (enumerator.MoveNext())
                {
                    child = (Transform) enumerator.Current;
                    if (child) child.GetComponentInChildren<NavButton>().gameObject.SetActive(false);
                }
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                    disposable.Dispose();
            }

            EnableButton(__instance.libraryButton);
            EnableButton(__instance.optionButton);
            EnableButton(__instance.quitButton);
            
            var archiBtn = Object.Instantiate(__instance.mainMenuLeftButtonGrid.GetChild(6), __instance.mainMenuLeftButtonGrid);
            var archiUIBtn = archiBtn.GetComponent<UIButton>();
            archiUIBtn.tmpText.text = "CONNECT";
            archiBtn.GetChild(0).GetComponent<I2.Loc.Localize>().Term = "CONNECT";
            archiBtn.SetSiblingIndex(2);
            archiBtn.GetComponent<Animator>().SetBool("visible", true);
            
            
            Object.DestroyImmediate(archiBtn.GetComponent<Button>());
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((data) => {
                    if (ArchiConfig.I.hostname == "" || ArchiConfig.I.port == "" || ArchiConfig.I.slotname == "")
                    {
                        if (!ArchiConfig.I.settings_ui_initialized) { OptionCtrlOpenPatch.Prefix(__instance.ctrl.optCtrl); } 
                        S.I.optCtrl.Open();
                        S.I.optCtrl.OpenPanel(archiSettingsPane.GetComponent<NavPanel>());
                        S.I.heCtrl.Close();
                        return;
                    }

                    if (ArchiNet.I.session != null)
                    {
                        return; // Already connected
                    }
                    // TODO: this fails to display due to Connect() blocking
                   archiUIBtn.tmpText.text = "CONNECTING...";
                    bool success = ArchiNet.I.Connect(ArchiConfig.I.hostname + ":" + ArchiConfig.I.port,
                        ArchiConfig.I.slotname);
                    if (!success)
                    {
                        archiUIBtn.tmpText.text = "FAILED TO CONNECT";
                        return;
                    }
                    archiUIBtn.tmpText.text = "CONNECTED";
                    EnableButton(__instance.soloButton);
                    if (__instance.runCtrl.LoadedRunExists())
                    {
                        EnableButton(__instance.continueButton);
                    }
                    
                    SaveDataCtrl.Set("ArchiConfig.hostname", ArchiConfig.I.hostname);
                    SaveDataCtrl.Set("ArchiConfig.port", ArchiConfig.I.port);
                    SaveDataCtrl.Set("ArchiConfig.slotname", ArchiConfig.I.slotname);
                    SaveDataCtrl.Set("ArchiConfig.password", ArchiConfig.I.password);

                    /*
                    if (__instance.runCtrl.loadedRun != null)
                    {
                        __instance.ClickContinueRun();
                    }
                    else
                    {
                        __instance.OpenPanelSingpleplayer(__instance.heCtrl);
                    }
                    */
                });
                EventTrigger trigger = archiBtn.gameObject.AddComponent<EventTrigger>();
                trigger.triggers.Add(entry);
                trigger.enabled = false;
            }
            //archiBtn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();

            ArchiConfig.I.mainmenu_ui_initialized = true;
        }
    }
}
