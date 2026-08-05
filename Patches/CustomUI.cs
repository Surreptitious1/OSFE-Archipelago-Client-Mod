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


public class ArchiConfig
{
    private static readonly object initlock = new object();
    public bool ui_initialized = false;
    
    public TMP_InputField hostname_field = null;
    public TMP_InputField port_field = null;
    public TMP_InputField slotname_field = null;
    public TMP_InputField password_field = null;

    public string hostname
    {
        get
        {
            if (!ui_initialized) { return ""; }
            return hostname_field.text != "" ?  hostname_field.text : "archipelago.gg";
        }
        set
        {
            if (!ui_initialized) { return; }
            hostname_field.text = value;
        }
    }
    public string port
    {
        get
        {
            if (!ui_initialized) { return ""; }
            return port_field.text != "" ?  port_field.text : "38281";
        }
        set
        {
            if (!ui_initialized) { return; }
            port_field.text = value;
        }
    }
    public string slotname
    {
        get
        {
            if (!ui_initialized) { return ""; }
            return slotname_field.text;
        }
        set
        {
            if (!ui_initialized) { return; }
            slotname_field.text = value;
        }
    }
    public string password
    {
        get
        {
            if (!ui_initialized) { return ""; }
            return password_field.text;
        }
        set
        {
            if (!ui_initialized) { return; }
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
            if (ArchiConfig.I.ui_initialized)
            {
                return;
            }

            Debug.Log("Initializing UI");
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

            ArchiConfig.I.hostname_field = CreateTextField("Server", "archipelago.gg");
            ArchiConfig.I.port_field = CreateTextField("Port", "38281");
            ArchiConfig.I.slotname_field = CreateTextField("Slot Name");
            ArchiConfig.I.password_field = CreateTextField("Password");
            ArchiConfig.I.ui_initialized = true;

            Debug.Log("UI Init Success");
        }

        /*
        [HarmonyPatch(typeof(NavTextfield))]
        [HarmonyPatch(nameof(NavTextfield.OnEndEdit))]
        [HarmonyPostfix]
        private static void NavFieldEndExitSave(NavTextfield __instance)
        {
            if (__instance.name.StartsWith("ArchiCustom"))
            {
                // TODO: Persistent storage of user fields
                return;
            }
        }
        */

    }

    [HarmonyPatch(typeof(MainCtrl))]
    [HarmonyPatch(nameof(MainCtrl.Open))]
    class MainCtrlOpenPatch
    {
        public static void Prefix(MainCtrl __instance)
        {
            __instance.soloButton.tmpText.text = "ARCHIPELAGO";
            // UnityEventBase::RemoveAllListeners() does not work, so completely replace the event
            __instance.soloButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            __instance.soloButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Running Custom Button Handler");
                if (ArchiConfig.I.hostname == "" || ArchiConfig.I.port == "" || ArchiConfig.I.slotname == "")
                {
                    if (!ArchiConfig.I.ui_initialized) { OptionCtrlOpenPatch.Prefix(__instance.ctrl.optCtrl); } 
                    S.I.optCtrl.Open();
                    S.I.optCtrl.OpenPanel(archiSettingsPane.GetComponent<NavPanel>());
                    S.I.heCtrl.Close();
                    return;
                }

                // TODO: this fails to display due to Connect() blocking
                __instance.soloButton.tmpText.text = "CONNECTING...";
                bool success = ArchiNet.I.Connect(ArchiConfig.I.hostname + ":" + ArchiConfig.I.port,
                    ArchiConfig.I.slotname);
                if (!success)
                {
                    __instance.soloButton.tmpText.text = "FAILED TO CONNECT";
                    return;
                }

                if (__instance.runCtrl.loadedRun != null)
                {
                    __instance.ClickContinueRun();
                }
                else
                {
                    __instance.OpenPanelSingpleplayer(__instance.heCtrl);
                }
            });
        }
    }
}
