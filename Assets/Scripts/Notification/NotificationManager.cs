using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] GameObject _notificationPrefab;

    [SerializeField] List<Notification> _notifications = new List<Notification>();

    [SerializeField] float _spacingY;

    [Button("SpawnNotification")]
    public Notification NewNotificaiton(Mission mission)
    {
        GameObject notificationGO = Instantiate(_notificationPrefab, this.transform);
        Notification notification = notificationGO.GetComponent<Notification>();
        notification.ID = mission.ID;

        //register in event
        mission.OnMissionDone += RemoveNotification;
        mission.OnMissionTimeUp += RemoveNotification;


        notification.Mission = mission;

        foreach(Notification notif in _notifications)
        {
            notif.MoveUp(notification._rectTransform.sizeDelta.y + _spacingY, 0.5f);
        }

        //appearance routine
        notification.gameObject.SetActive(false);
        notification.SetText();
        notification.gameObject.transform.localScale = new Vector3(0, 0, 0);
        notification.gameObject.SetActive(true);
        notification.Appear();
        
        EventsDispatcher.Instance.ReceiveNotification();

        _notifications.Add(notification);
        return notification;
    }

    public void RemoveNotification(int _id)
    {
        int indexInList = 0;
        foreach(Notification notification in _notifications.ToList())
        {
            if(notification.ID == _id)
            {
                _notifications.Remove(notification);
                notification.Dissapear();
                MoveDownNotifications(indexInList);
            }
            indexInList++;
        }
    }

    void MoveDownNotifications(int indexInList)
    {
        for(int i = 0; i < indexInList; i++)
        {
            _notifications[i].MoveUp(- _notifications[i]._rectTransform.sizeDelta.y - _spacingY, 0.5f);
        }
    }
}
