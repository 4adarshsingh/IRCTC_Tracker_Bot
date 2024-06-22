using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SearchStation
{
    public bool Status { get; set; }
    public string Message { get; set; } = "";
    public long Timestamp { get; set; }
    public List<Station> Data { get; set; } = new List<Station>(); // Initialize to empty list
}

public class Station
{
    public string Name { get; set; } = ""; // Initialize to empty string
    [JsonProperty("eng_name")]
    public string EngName { get; set; } = ""; // Initialize to empty string
    public string Code { get; set; } = ""; // Initialize to empty string
    [JsonProperty("state_name")]
    public string StateName { get; set; } = ""; // Initialize to empty string
}