using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KhTracker
{
    ///TODO: i don't think i need this. remove later??
    public static class DependencyObjectExtensions
    {
        public static object GetDynamicResourceKey(this DependencyObject obj, DependencyProperty prop)
        {
            // get the value entry from the depencency object for the specified dependency property
            var dependencyObject = typeof(DependencyObject);
            var dependencyObject_LookupEntry = dependencyObject.GetMethod("LookupEntry", BindingFlags.NonPublic | BindingFlags.Instance);
            var entryIndex = dependencyObject_LookupEntry.Invoke(obj, new object[] { prop.GlobalIndex });
            var effectiveValueEntry_GetValueEntry = dependencyObject.GetMethod("GetValueEntry", BindingFlags.NonPublic | BindingFlags.Instance);
            var valueEntry = effectiveValueEntry_GetValueEntry.Invoke(obj, new object[] { entryIndex, prop, null, 0x10 });

            // look inside the value entry to find the ModifiedValue object
            var effectiveValueEntry = valueEntry.GetType();
            var effectiveValueEntry_Value = effectiveValueEntry.GetProperty("Value", BindingFlags.Instance | BindingFlags.NonPublic);
            var effectiveValueEntry_Value_Getter = effectiveValueEntry_Value.GetGetMethod(nonPublic: true);
            var rawEntry = effectiveValueEntry_Value_Getter.Invoke(valueEntry, new object[0]);

            // look inside the ModifiedValue object to find the ResourceReference
            var modifiedValue = rawEntry.GetType();
            var modifiedValue_BaseValue = modifiedValue.GetProperty("BaseValue", BindingFlags.Instance | BindingFlags.NonPublic);
            var modifiedValue_BaseValue_Getter = modifiedValue_BaseValue.GetGetMethod(nonPublic: true);
            var resourceReferenceValue = modifiedValue_BaseValue_Getter.Invoke(rawEntry, new object[0]);

            // check the ResourceReference for the original ResourceKey
            var resourceReference = resourceReferenceValue.GetType();
            var resourceReference_resourceKey = resourceReference.GetField("_resourceKey", BindingFlags.NonPublic | BindingFlags.Instance);
            var resourceKey = resourceReference_resourceKey.GetValue(resourceReferenceValue);

            return resourceKey;
        }

        public static void SetDynamicResourceKey(this DependencyObject obj, DependencyProperty prop, object resourceKey)
        {
            var dynamicResource = new DynamicResourceExtension(resourceKey);
            var resourceReferenceExpression = dynamicResource.ProvideValue(null);
            obj.SetValue(prop, resourceReferenceExpression);
        }
    }
}
