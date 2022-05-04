using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public const string ObjectPoolName = "ObjectPool";

    [SerializeField][Header("Объекты, хранящиеся в пуле")] private GameObject[] prefabs;
    [SerializeField][Header("Начальное количество объектов, хранящихся в пуле")] private int numberObjects;
    [SerializeField][Header("Разрушитель тортов по количеству")] private DestroyerByNumber cakeDestroyerByNumber;

    private Dictionary<string, Stack<GameObject>> pool; // Словарь - пул объектов

    private void Awake()
    {
        pool = new Dictionary<string, Stack<GameObject>>();
        
        // Предварительное заполнение пула
        foreach (GameObject prefab in prefabs)
        {
            Stack<GameObject> stackObjects = new Stack<GameObject>(); // Стек объектов
            for (int i = 0; i < numberObjects; i++)
            {
                GameObject gameObject = Instantiate(prefab, transform); // Создаем объект
                gameObject.SetActive(false); // Скрываем объект
                gameObject.name = prefab.name; // Меняем имя
                stackObjects.Push(gameObject); // Добавляем в стек
            }

            pool.Add(prefab.name, stackObjects); // Добавляем в пул
        }
    }

    /// <summary>
    /// Получает объект из пула объектов.
    /// </summary>
    /// <param name="prefab">Префаб</param>
    /// <returns>Объект на сцене</returns>
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (pool.TryGetValue(prefab.name, out Stack<GameObject> stackObjects)) // Получаем объект из словаря
        {
            if (stackObjects.Count > 0) // Если объекты есть
            {
                GameObject gameObject = stackObjects.Pop(); // Возвращаем и удаляем верхний элемент стека
                gameObject.SetActive(true); // Активируем объект
                SetTranform(gameObject.transform, position, rotation, parent);
                return gameObject;
            }
            else
            {
                GameObject gameObject = Instantiate(prefab); // Создаем новый
                gameObject.name = prefab.name; // Меняем имя
                SetTranform(gameObject.transform, position, rotation, parent);
                return gameObject;
            }
        }
        else
        {
            return null; // Объекта не было в пуле
        }
    }

    /// <summary>
    /// Возвращает объект в пул объектов.
    /// </summary>
    /// <param name="gameObject">Объект на сцене</param>
    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false); // Скрываем объект
        gameObject.transform.parent = transform; // Группируем

        if (pool.TryGetValue(gameObject.name, out Stack<GameObject> stackObjects)) // Получаем объект из словаря
        {
            if (!stackObjects.Contains(gameObject)) // Если в стеке нет такого объекта
            {
                stackObjects.Push(gameObject); // Добавляем в стек
            }
        }
        else
        {
            stackObjects = new Stack<GameObject>(); // Стек объектов
            stackObjects.Push(gameObject); // Добавляем в стек
            pool.Add(gameObject.name, stackObjects); // Добавляем в пул
        }

        // Нужно для удаления тортов по количеству
        if (gameObject.CompareTag("Cake"))
        {
            cakeDestroyerByNumber.DeleteFromCollection(gameObject); // Удаляем из коллекции тортов
        }
    }

    /// <summary>
    /// Устанавливает позицию, поворот и группирует.
    /// </summary>
    private void SetTranform(Transform transform, Vector3 position, Quaternion rotation, Transform parent)
    {
        transform.parent = null; // Снимаем группировку
        transform.SetPositionAndRotation(position, rotation); // Устанавливаем позицию и поворот
        transform.parent = parent; // Группируем
    }
}
