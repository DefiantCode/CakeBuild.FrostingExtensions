using Cake.Core;
using Cake.Frosting;
using System.Collections.Generic;
using System.Dynamic;

namespace DefiantCode.Cake.Frosting
{
    public interface IContext : ICakeContext
    {
        string Target { get; set; }
        bool IsLocalBuild { get; set; }
    }

    public class DynamicContext : FrostingContext, IContext
    {
        public string Target { get; set; }
        public bool IsLocalBuild { get; set; }
        public dynamic Properties { get; set; }

        public void OverrideProperties(IDictionary<string, object> properties)
        {
            Properties = (ExpandoObject)properties;
        }

        public DynamicContext(ICakeContext context) : base(context)
        {
            Properties = new ExpandoObject();
        }

        public T GetProperty<T>(string key)
        {
            return (T)GetProperty(key);
        }

        public object GetProperty(string key)
        {
            return ((IDictionary<string, object>)Properties)[key];
        }

        public void SetProperty<T>(string key, T value)
        {
            SetProperty(new KeyValuePair<string, object>(key, value));
        }


        public void SetProperty(KeyValuePair<string, object> keyValue)
        {
            ((IDictionary<string, object>)Properties)[keyValue.Key] = keyValue.Value;
        }

        public void AddProperty(KeyValuePair<string, object> keyValue)
        {
            ((IDictionary<string, object>)Properties).Add(keyValue);
        }

        public void RemoveProperty(string key)
        {
            ((IDictionary<string, object>)Properties).Remove(key);
        }

        public void Clear()
        {
            ((IDictionary<string, object>)Properties).Clear();
        }
    }
}