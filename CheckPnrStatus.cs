using System;
using System.Collections.Generic;

//Initializing to avoid warnings
public class CheckPnrStatus
{
    public bool Status { get; set; }
    public string Message { get; set; } = "";
    public long Timestamp { get; set; }
    public PnrTrainData Data { get; set; } = new PnrTrainData();
}

public class PnrTrainData
{
    public string Pnr { get; set; } = "";
    public string TrainNo { get; set; } = "";
    public string TrainName { get; set; } = "";
    public string Doj { get; set; } = "";
    public string BookingDate { get; set; } = "";
    public string Quota { get; set; } = "";
    public string DestinationDoj { get; set; } = "";
    public string SourceDoj { get; set; } = "";
    public string From { get; set; } = "";
    public string To { get; set; } = "";
    public string ReservationUpto { get; set; } = "";
    public string BoardingPoint { get; set; } = "";
    public string Class { get; set; } = "";
    public bool ChartPrepared { get; set; }
    public string BoardingStationName { get; set; } = "";
    public string TrainStatus { get; set; } = "";
    public bool TrainCancelledFlag { get; set; }
    public string ReservationUptoName { get; set; } = "";
    public int PassengerCount { get; set; }
    public List<PassengerStatus> PassengerStatus { get; set; } = new List<PassengerStatus>();
    public string DepartureTime { get; set; } = "";
    public string ArrivalTime { get; set; } = "";
    public string ExpectedPlatformNo { get; set; } = "";
    public string BookingFare { get; set; } = "";
    public string TicketFare { get; set; } = "";
    public string CoachPosition { get; set; } = "";
    public double Rating { get; set; }
    public double FoodRating { get; set; }
    public double PunctualityRating { get; set; }
    public double CleanlinessRating { get; set; }
    public string SourceName { get; set; } = "";
    public string DestinationName { get; set; } = "";
    public string Duration { get; set; } = "";
    public int RatingCount { get; set; }
    public bool HasPantry { get; set; }
}

#pragma warning disable CS8618 // Non-nullable property is uninitialized.
public class PassengerStatus
{
    public string Pnr { get; set; } = "";
    public int Number { get; set; }
    public object Prediction { get; set; }
    public string PredictionPercentage { get; set; } = "";
    public string ConfirmTktStatus { get; set; } = "";
    public string Coach { get; set; } = "";
    public int Berth { get; set; }
    public string BookingStatus { get; set; } = "";
    public string CurrentStatus { get; set; } = "";
    public object CoachPosition { get; set; }
    public string BookingBerthNo { get; set; } = "";
    public string BookingCoachId { get; set; } = "";
    public string BookingStatusNew { get; set; } = "";
    public string BookingStatusIndex { get; set; } = "";
    public string CurrentBerthNo { get; set; } = "";
    public string CurrentCoachId { get; set; } = "";
    public string BookingBerthCode { get; set; } = "";
    public object CurrentBerthCode { get; set; }
    public string CurrentStatusNew { get; set; } = "";
    public string CurrentStatusIndex { get; set; } = "";
}

#pragma warning restore CS8618 // Non-nullable property is uninitialized.