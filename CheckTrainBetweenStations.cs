using System;
using System.Collections.Generic;
using Newtonsoft.Json; // Ensure Newtonsoft.Json package is installed via NuGet

public class CheckTrainBetweenStations
{
    [JsonProperty("status")]
    public bool Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = "";

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("data")]
    public List<TrainData> Data { get; set; } = new List<TrainData>();
}

public class TrainData
{
    [JsonProperty("train_number")]
    public string TrainNumber { get; set; } = "";

    [JsonProperty("train_name")]
    public string TrainName { get; set; } = "";

    [JsonProperty("run_days")]
    public List<string> RunDays { get; set; } = new List<string>();

    [JsonProperty("train_src")]
    public string TrainSrc { get; set; } = "";

    [JsonProperty("train_dstn")]
    public string TrainDstn { get; set; } = "";

    [JsonProperty("from_std")]
    public string FromStd { get; set; } = "";

    [JsonProperty("from_sta")]
    public string FromSta { get; set; } = "";

    [JsonProperty("local_train_from_sta")]
    public int LocalTrainFromSta { get; set; }

    [JsonProperty("to_sta")]
    public string ToSta { get; set; } = "";

    [JsonProperty("to_std")]
    public string ToStd { get; set; } = "";

    [JsonProperty("from_day")]
    public int FromDay { get; set; }

    [JsonProperty("to_day")]
    public int ToDay { get; set; }

    [JsonProperty("d_day")]
    public int DDay { get; set; }

    [JsonProperty("from")]
    public string From { get; set; } = "";

    [JsonProperty("to")]
    public string To { get; set; } = "";

    [JsonProperty("from_station_name")]
    public string FromStationName { get; set; } = "";

    [JsonProperty("to_station_name")]
    public string ToStationName { get; set; } = "";

    [JsonProperty("duration")]
    public string Duration { get; set; } = "";

    [JsonProperty("special_train")]
    public bool SpecialTrain { get; set; }

    [JsonProperty("train_type")]
    public string TrainType { get; set; } = "";

    [JsonProperty("train_date")]
    public string TrainDate { get; set; } = "";

    [JsonProperty("class_type")]
    public List<string> ClassType { get; set; } = new List<string>();
}