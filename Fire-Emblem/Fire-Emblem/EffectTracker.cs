using Fire_Emblem_View;
using System.Collections.Generic;

//namespace Fire_Emblem
//{
//    public class EffectTracker
//    {
//        private readonly Dictionary<EffectType, List<string>> _effectMessagesByType;
//        private readonly List<string> _generalEffectMessages;
//
//        public EffectTracker()
//        {
//            _effectMessagesByType = InitializeEffectMessages();
//            _generalEffectMessages = new List<string>();
//        }
//
//        private Dictionary<EffectType, List<string>> InitializeEffectMessages()
//        {
//            return new Dictionary<EffectType, List<string>>()
//            {
//                { EffectType.FirstAttack, new List<string>() },
//                { EffectType.FollowUp, new List<string>() },
//                { EffectType.Both, new List<string>() }
//            };
//        }
//
//        public void AddEffectMessage(EffectType effectType, string message)
//        {
//            if (effectType == EffectType.Both)
//                _generalEffectMessages.Add(message);
//            else
//                _effectMessagesByType[effectType].Add(message);
//        }
//
//        public void PrintAllEffects(View view)
//        {
//            PrintMessages(view, _generalEffectMessages, string.Empty);
//            PrintMessages(view, _effectMessagesByType[EffectType.FirstAttack], "en su primer ataque");
//            PrintMessages(view, _effectMessagesByType[EffectType.FollowUp], "en su Follow-Up");
//        }
//
//        private void PrintMessages(View view, List<string> messages, string suffix)
//        {
//            foreach (var message in messages)
//            {
//                if (!string.IsNullOrEmpty(suffix))
//                    view.WriteLine($"{message} {suffix}");
//                else
//                    view.WriteLine(message);
//            }
//        }
//
//        public void Reset()
//        {
//            _generalEffectMessages.Clear();
//            foreach (var effectList in _effectMessagesByType.Values)
//            {
//                effectList.Clear();
//            }
//        }
//    }
//}
//