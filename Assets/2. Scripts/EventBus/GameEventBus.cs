using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Jongmin
{
    public class GameEventBus : MonoBehaviour
    {
        private static readonly IDictionary<GameEventType, UnityEvent> m_events
            = new Dictionary<GameEventType, UnityEvent>();

        // 특정한 게임 상태 이벤트를 이벤트 딕셔너리에 삽입하는 메소드
        public static void Subscribe(GameEventType event_type, UnityAction listener)
        {
            UnityEvent this_event;

            if(m_events.TryGetValue(event_type, out this_event))
            {
                this_event.AddListener(listener);
            }
            else
            {
                this_event = new UnityEvent();
                this_event.AddListener(listener);
                m_events.Add(event_type, this_event);
            }

            Debug.Log($"{event_type.ToString()}이(가) 성공적으로 구독되었습니다.");
        }

        // 특정한 게임 상태 이벤트를 이벤트 딕셔너리에서 제거하는 메소드
        public static void Unsubscribe(GameEventType event_type, UnityAction listener)
        {
            UnityEvent this_event;

            if(m_events.TryGetValue(event_type, out this_event))
            {
                this_event.RemoveListener(listener);
                Debug.Log($"{event_type.ToString()}이(가) 성공적으로 구독 취소되었습니다.");
            }
        }

        // 특정한 게임 상태 이벤트를 실행하는 메소드
        public static void Publish(GameEventType event_type)
        {
            UnityEvent this_event;

            if(m_events.TryGetValue(event_type, out this_event))
            {
                this_event.Invoke();
                Debug.Log($"{event_type.ToString()}이(가) 성공적으로 실행되었습니다.");
            }
        }
    }
}

