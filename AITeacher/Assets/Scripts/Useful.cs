using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

// a place to put useful static functions that don't have a more suitable home.

// -----------------------------------------------------------------------------
public static class Useful
{
    // verifies if path is valid and creates the target directory if it doesn't exist.
    // can be relative to the application data path, or not.
    // returns the adjusted path name, or empty string on failure
    public static bool ValidateSavePath(ref string path, bool relativeToAppPath = true)
    {
        if (string.IsNullOrEmpty(path)) return false;
        if (relativeToAppPath)
        {
            path = Application.dataPath + "\\" + path;
        }
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log("Created directory " + path);
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to create output directory: " + path + "\r\nError=" + e.ToString());
        }
        return false;
    }

    public static string GetTimestamp() { return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"); }

    // formatted string for time in minutes and seconds, with optional tenths of a second
    public static string FormatTime(float time, bool tenths = false)
    {
        int min = Mathf.FloorToInt(time / 60F);

        if (tenths)
        {
            float sec = Mathf.FloorToInt((time - (min * 60f)) * 10f) / 10.0f;
            return string.Format($"{min}:{sec:00.0}");
        }
        else
        {
            int sec = Mathf.FloorToInt(time - min * 60);
            return string.Format($"{min}:{Mathf.FloorToInt(sec):00}");
        }
    }

    // -----------------------------------------------------------------------------
    // checks to make sure there is a command line parameter specified after a command open 
    // throws a fatal message if it's not there. 
    // public static bool CheckParamAvailable(int a, string[] arguments, Logger log = null)
    // {
    //     if (a == arguments.Length - 1 || arguments[a + 1].Length == 0)
    //     {
    //         log?.Fatal($"No parameter specified for command line argument {arguments[a]}");
    //         return false;
    //     }
    //     return true;

    // }

    // null check with return value, assertion under debug builds
    // returns true if the object is null 
    public static bool NullCheck(object anyObject)
    {
        if (anyObject is null)
        {
            Debug.LogAssertion($"Null object");
            return true;
        }
        return false;
    }

    // replaces characters that are valid in filenames on the current system. 
    public static string FixIllegalFilenameChars(string str, char replacement = '_')
    {
        foreach (char bad in Path.GetInvalidFileNameChars())
        {
            str = str.Replace(bad, replacement);
        }
        return str;
    }

    public static string ReplaceAllChars(string str, string targets, char replacement = '_')
    {
        foreach (char bad in targets)
        {
            str = str.Replace(bad, replacement);
        }
        return str;
    }

    public static bool ContainsAny(string str, string targets)
    {
        if (str.IndexOfAny(targets.ToCharArray()) >= 0)
            return true;
        return false;
    }

    public static bool IsEitherShiftPressed()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    public static bool IsEitherControlPressed()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }

    public static bool IsEitherAltPressed()
    {
        return Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
    }

    public static Transform FindChildRecursive(Transform parent, string name)
    {
        if (parent.name.Equals(name))
            return parent;
        foreach (Transform child in parent)
        {
            if (child.name.Equals(name))
                return child;

            var result = FindChildRecursive(child, name);
            if (result != null)
                return result;
        }
        return null;
    }

    public static TType FindChild<TType>(string objectName, Transform parentXform)
        where TType : UnityEngine.Object
    {
        var child = FindChildRecursive(parentXform, objectName);
        Assert.IsNotNull(child, $"{objectName} not found!");
        TType found = child.GetComponent<TType>();
        Assert.IsNotNull(found, $"component {typeof(TType)} on {objectName} not found!");
        return found;
    }

    public static TFieldType GetFieldValue<TFieldType, TObjectType>(this TObjectType obj, string fieldName)
    {
        var fieldInfo = obj.GetType().GetField(fieldName,
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.Public | BindingFlags.NonPublic);
        return (TFieldType)fieldInfo.GetValue(obj);
    }

    public static string GetLocalIPAddress()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "0.0.0.0";

    }

    public static void PlaySound(AudioClip snd, float volume = 1.0f)
    {
        if (snd == null) return;

        // todo: maybe check global 'sound enabled' flag? 
        AudioSource.PlayClipAtPoint(snd, new Vector3(), volume);
    }

    // returns a color gradient from green (1.0) to yellow (0.5) to red (0.0)
    public static Color GreenYellowRedGradient(float value)
    {

        Color color = Color.black;
        if (value > 0.5f)
            color = Color.Lerp(Color.yellow, Color.green, (value * 2.0f) - 1.0f);
        else if (value >= 0.0f)
            color = Color.Lerp(Color.red, Color.yellow, value * 2.0f);
        return color;

    }

    public static string ColorString(string s, Color c)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(c)}>{s}</color>";
    }

    // convert an int to an enum, with either a default or exception if it's not valid enum value
    public static TEnum SafeEnumFromInt<TEnum>(this object value, bool doNotThrow = false)
    {
        Type type = typeof(TEnum);
        if (Enum.IsDefined(type, value))
            return (TEnum)value;

        string err = $"Invalid enum ID <{value}> for conversion to {type}";
        if (doNotThrow)
        {
            Debug.LogWarning(err);
            return default(TEnum);
        }
        throw new ArgumentException(err);
    }

    public static float RandomValue(this Vector2 span)
    {
        return Random.Range(span.x, span.y);
    }
}
