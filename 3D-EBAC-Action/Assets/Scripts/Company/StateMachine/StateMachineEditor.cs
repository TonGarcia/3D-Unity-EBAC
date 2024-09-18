using System.Linq;
using UnityEditor;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    // foldout is a list GUI component
    public bool showFoldout;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        FSMExample fsm = (FSMExample)target;
        
        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        // it won't be null just in case the Editor PlayMode is ON
        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());
        
        // showFoldout is used by the foldout component to know if is it open or closed to switch state
        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout)
        {
            if (fsm.stateMachine.dictionaryState != null)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField($"{keys[i]} :: {vals[i]}");
                }    
            }
        }
    }
}
