using KeyValueStore.Models;
using KeyValueStore.Services;
using System;
using System.Collections.Generic;
namespace KeyValueStore.Services
{
    public interface IKeyValueStore
    {
        KeyValue Add(KeyValue keyValue);
        IEnumerable<KeyValue> GetAll();
        KeyValue GetByKey(string key);
        void Delete(KeyValue keyValue);
        void Update(KeyValue keyValue);
    }
}
