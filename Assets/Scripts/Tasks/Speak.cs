using UnityEngine;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Speak : Action
{
    [TextArea(2, 10)]
    public string content;
    public Text text;

    int index;

    public override void OnStart()
    {
        index = 0;
        text.text = "";
    }

    public override TaskStatus OnUpdate()
    {
        if (index >= content.Length) return TaskStatus.Success;

        index++;
        text.text = content.Substring(0, index);
        return TaskStatus.Running;
    }
}