using Newtonsoft.Json;

namespace TB5.Shared;

public static class DevCode
{
    public static decimal ToDecimal(this string value)
    {
        return Convert.ToDecimal(value);
    }

    public static string ToJson(this object value)
    {
        return JsonConvert.SerializeObject(value, Formatting.Indented);
    }
}
