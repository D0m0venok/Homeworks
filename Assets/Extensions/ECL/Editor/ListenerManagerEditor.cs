using System.Linq;
using System.Reflection;
using UnityEditor;

namespace VG.Utilites
{
    [CustomEditor(typeof(ListenersManager))]
    public class ListenerManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var manager = target as ListenersManager;
            var field = (Listeners<IManagerListener>)manager.GetType().
                GetField("_listeners", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).GetValue(target);
            var listeners = field.Count;
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Total:");
            EditorGUILayout.LabelField(listeners.Sum(pair => pair.Value).ToString());
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            
            foreach (var (type, count) in listeners)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{type.Name}:");
                EditorGUILayout.LabelField(count.ToString());
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}