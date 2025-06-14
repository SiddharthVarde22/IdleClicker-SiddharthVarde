using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ServiceLocator : MonoBehaviour
{
    private static ServiceLocator m_instance;
    Dictionary<EServiceTypes, IGameService> m_services = new Dictionary<EServiceTypes, IGameService>();

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Initialize();
    }
    private void OnDestroy()
    {
        m_services.Clear();
    }
    public static void RegisterService(EServiceTypes a_serviceType, IGameService a_service)
    {
        if(!m_instance.m_services.ContainsKey(a_serviceType))
        {
            m_instance.m_services.Add(a_serviceType, a_service);
        }
    }
    public static T GetService<T>(EServiceTypes a_serviceType) where T : class
    {
        if(m_instance.m_services.ContainsKey(a_serviceType))
        {
            return (T)m_instance.m_services[a_serviceType];
        }

        return null;
    }
    private void Initialize()
    {
        new ScoreService();
    }
}
