using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using System.Reflection;


namespace AAA.UI.CustomUI
{
    public class CustomButtonAttributeProcessor : OdinAttributeProcessor<CustomButton>
    {
        public override void ProcessChildMemberAttributes(
            InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.Name == "m_Interactable")
            {
                attributes.Add(new PropertyOrderAttribute(-8));
            }
            if (member.Name == "m_Navigation")
            {
                attributes.Add(new TabGroupAttribute("Properties"));
                attributes.Add(new PropertyOrderAttribute(-2));
            }

            if (member.Name == "m_Transition" || member.Name == "m_Colors" || member.Name == "m_SpriteState" || member.Name == "m_AnimationTriggers" || member.Name == "m_TargetGraphic")
            {
                attributes.Add(new HideInPlayModeAttribute());
                attributes.Add(new HideInEditorModeAttribute());
            }
        }
    }
}