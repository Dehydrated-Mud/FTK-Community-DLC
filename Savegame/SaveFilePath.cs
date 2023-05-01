using FTKAPI.Objects.SkillHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;
namespace CommunityDLC.Savegame
{
    public class SaveFilePath : BaseModule
    {
        private static string DIR = "CommunityDLCSaves";
        public override void Initialize()
        {
            On.uiStartGame.GetSavePath += ModdedSavePath;
            On.GameLogic.Save += SaveHook;
            On.GameSerialize.GetGameInfo += GameInfoHook;
            
        }

        private static GameSerialize.GameInfo GameInfoHook (On.GameSerialize.orig_GetGameInfo _orig, string _filename)
        {
            //Prefix: Before loading, make sure the savegame file path has the "CommunityDLCSaves" prefix. If not, grab the last save from that dir.
            string[] tokens = _filename.Split(new[] { FileSystemHelper.DS }, StringSplitOptions.None);
            bool oneOfOurs = false;
            foreach (string token in tokens)
            {
                if (token == DIR)
                {
                    oneOfOurs = true;
                }
                Logger.LogWarning(token);
            }
            if (oneOfOurs)
            {
                Logger.LogInfo("Save file is from CommunityDLC");
                return _orig(_filename);
            }
            else
            {
                string newFilename = PlayerPrefs.GetString("LastSaveCommunityDLC");
                if (!string.IsNullOrEmpty(newFilename))
                {
                    Logger.LogInfo($"Found LastSaveCommunityDLC at {newFilename}");
                    return _orig(newFilename);
                }
                Logger.LogError($"Failed to switch to one of our saves. Attempted to switch to: {newFilename}. Opening {_filename} instead.");
                return _orig(_filename);
            }
        }

        public static string ModdedSavePath(On.uiStartGame.orig_GetSavePath _orig)
        {
            string orig = _orig();
            return orig + FileSystemHelper.DS + DIR;
        }

        private void SaveHook(On.GameLogic.orig_Save _orig, GameLogic _this, bool _quitWhenDone, bool _isAutoSave)
        {
            string text = uiStartGame.Instance.GetResumeFilename();
            if (string.IsNullOrEmpty(text))
            {
                uiStartGame.Instance.GenerateResumeFilename();
                text = uiStartGame.Instance.GetResumeFilename();
            }
            if (_this.m_IsDebugSaveFile)
            {
                int num = text.LastIndexOf('.');
                string text2 = text.Substring(0, num);
                string text3 = text.Substring(num, text.Length - text2.Length);
                text = text2 + "_" + _this.m_DebugSaveFileIndex.ToString("000") + text3;
                _this.m_DebugSaveFileIndex++;
            }
            PlayerPrefs.SetString("LastSaveCommunityDLC", text);
            GameLogic.Instance.SaveGame(text, _quitWhenDone, _isAutoSave);
        }
    }
}
