using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageData
{

    public enum MessageState
    {
        Waiting,
        Showing,
        Completed
    }

    public string Text;
    public float Time;
    public Action OnFinish;
    public MessageState State = MessageState.Waiting;

    public void Show()
    {
        State = MessageState.Showing;
    }

    public void Close()
    {
        State = MessageState.Completed;
    }
}

public class UIMessage : MonoBehaviour
{

    private static UIMessage s_Instance;
    public static UIMessage Instance => s_Instance;

    private TextMeshProUGUI _text;
    private Queue<MessageData> _queue;
    private Coroutine _messageCoroutine;

    private void Awake()
    {
        s_Instance = this;
        _queue = new Queue<MessageData>();

        _text = GetComponent<TextMeshProUGUI>();
        Clear();
    }

    public MessageData ShowMessage(string text, Action onFinish)
    {
        var message = new MessageData()
        {
            Text = text,
            Time = -1f,
            OnFinish = onFinish
        };

        return message;
    }
    
    public MessageData ShowTimedMessage(string text)
    {
        return ShowTimedMessage(text, 3f);
    }
    
    public MessageData ShowTimedMessage(string text, float time)
    {
        return ShowTimedMessage(text, time, null);
    }

    public MessageData ShowTimedMessage(string text, float time, Action onFinish)
    {
        var message = new MessageData()
        {
            Text = text,
            Time = time,
            OnFinish = onFinish
        };

        _queue.Enqueue(message);
        if (_messageCoroutine == null)
        {
            _messageCoroutine = StartCoroutine(MessageCoroutine());
        }

        return message;
    }
    
    public void Clear()
    {
        if (_messageCoroutine != null) StopCoroutine(_messageCoroutine);
        _messageCoroutine = null;
        _queue.Clear();

        _text.enabled = false;
        _text.text = "";
    }

    private IEnumerator MessageCoroutine()
    {
        while (_queue.Count > 0)
        {
            var message = _queue.Dequeue();
            _text.text = message.Text;
            _text.enabled = true;

            message.State = MessageData.MessageState.Showing;

            float time = 0f;
            while (time < message.Time && message.State != MessageData.MessageState.Completed)
            {
                time += Time.deltaTime;
                yield return null;
            }

            message.State = MessageData.MessageState.Completed;
            message.OnFinish?.Invoke();
            
            _text.enabled = false;
            yield return new WaitForSeconds(0.3f);
        }

        Clear();
    }

}
