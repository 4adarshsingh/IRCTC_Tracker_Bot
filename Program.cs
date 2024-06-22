// API's Used- Un-Oficial IRCTC API and official Telegram API

using System;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots.Extensions.Polling;
using Telegram.Bot.Types.InlineQueryResults;

string lastInput="start";

var botClient = new TelegramBotClient("Enter your telegram bot client id here");

var me = await botClient.GetMeAsync();
Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

using CancellationTokenSource cts = new ();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
 ReceiverOptions receiverOptions = new ()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;

    var chatId = message.Chat.Id;
    var username = message.Chat.FirstName;

    // Only process text messages
    if (message.Text is not { } messageText)
    {
        Console.WriteLine($"Received input other than text message in chat {chatId}.");

        // Echo message to prompt user to enter only text
        Message sentIncorrectInputMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "You entered input other than text.\nPlease enter only text.",
        replyToMessageId: update.Message.MessageId,
        cancellationToken: cancellationToken);

        return;
    }

    //Echo received message text to console
    Console.WriteLine($"\nReceived a '{messageText}' message in chat {chatId}.");

    // Multi row keyboard with all possible search options
    ReplyKeyboardMarkup commandsKeyboard = new(new[]
    {
        new KeyboardButton[] { "/pnr"},

        new KeyboardButton[] { "/searchstation",  "/trainbetweenstation" },

        new KeyboardButton[] { "/bookticket" },
    })
    {   ResizeKeyboard = true}  ;

    //Keyboard with help command option
    ReplyKeyboardMarkup helpKeyboard = new(new[]
    {
        new KeyboardButton[] { "/help" },
    })
    {   ResizeKeyboard = true}  ;

    
    //Process different inputs
    switch(messageText)
    {
        case "/start": 
        {
            string welcomeMessage = @"
I'm PNR STATUS CHECKER! 👋
I can help you find out your Railway Ticket's current status and other IRCTC information easily! 📌 
And that too in a very fast speed and without any Login or Captcha Problems. 😉

/start - to start using bot.
/help - to display a list of available commands.
/pnr - to get the PNR status of a train ticket.
/searchstation - to search for a station by name.
/trainbetweenstation - to get the trains between stations.
/bookticket - to go to Official IRCTC Website to book tickets.";

            Message sentMessage1 = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Hello {username},\n{welcomeMessage}",
                replyToMessageId: update.Message.MessageId,
                cancellationToken: cancellationToken);
            
            Console.WriteLine($"{me.FirstName} sent message for /start");
            break;
        }
        case "/pnr": 
        {
            lastInput="/pnr";
            Console.WriteLine("Last Input is " + lastInput);

            //Removing Custom Keyboard
            Message RemoveKeyboard = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You have selected to Check PNR Status: ",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            Message sentMessage2 = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Enter 10 digit PNR number to check its status:",
                replyToMessageId: update.Message.MessageId,
                cancellationToken: cancellationToken);
            
            Console.WriteLine($"{me.FirstName} sent message for /pnr");
            break;
        }
        case "/searchstation": 
        {
            lastInput="/searchstation";
            Console.WriteLine("Last Input is " + lastInput);

            //Removing Custom Keyboard
            Message RemoveKeyboard = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You have selected to Search Station Details: ",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            Message sentMessage2 = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Enter a full/partial station name or code to check Station details (Ex: Delhi, NDLS, Del):",
                replyToMessageId: update.Message.MessageId,
                cancellationToken: cancellationToken);
            
            Console.WriteLine($"{me.FirstName} sent message for /searchstation");
            break;
        }
        case "/trainbetweenstation": 
        {
            lastInput="/trainbetweenstation";
            Console.WriteLine("Last Input is " + lastInput);

            //Removing Custom Keyboard
            Message RemoveKeyboard = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You have selected to Search Trains Between Station Details: ",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            Message sentMessage2 = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Enter from station code, to station code and date of journey to find trains between stations.\nTo check station codes, you can type /searchstation\nFor example, to search for trains between New Delhi and Lucknow on 27 June, enter:",
                replyToMessageId: update.Message.MessageId,
                cancellationToken: cancellationToken);

            Message sentMessage3 = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "NDLS,EKMA,2024-06-27",
                replyToMessageId: update.Message.MessageId,
                cancellationToken: cancellationToken);
            
            Console.WriteLine($"{me.FirstName} sent message for /trainbetweenstation");
            break;
        }
        case "/bookticket":
        {
            lastInput="/bookticket";
            Console.WriteLine("Last Input is " + lastInput);

            //Removing Custom Keyboard
            Message RemoveKeyboard = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You have selected to Book Ticket: ",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            // Reply to user with received text and an inline keyboard button having external link
            Message sentmessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Below link will take you to Official IRCTC website where you can book tickets\n",
                parseMode: ParseMode.MarkdownV2,
                disableNotification: true,
                replyToMessageId: update.Message.MessageId,
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                    text: "IRCTC Official Website",
                    url: "https://www.irctc.co.in/nget/train-search")),
                cancellationToken: cancellationToken);

            Console.WriteLine($"{me.FirstName} sent message for /bookticket");
            
            break;
        }
        case "/help": 
        {
            lastInput="/help";
            Console.WriteLine("Last Input is " + lastInput);

            Message sentMessage1 = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "What do you want to search?",
                replyMarkup: commandsKeyboard,
                cancellationToken: cancellationToken);
            
            Console.WriteLine($"{me.FirstName} sent message for /help");
            break;
        }
        default: 
        {
            switch(lastInput)
            {
                case "/pnr":
                {
                    Console.WriteLine("Handling /pnr case...");
                    // Check if input is a 10-digit number for PNR
                    if (messageText.Length == 10 && IsNumeric(messageText))
                    {
                        string result = await CheckPNRAsync(messageText);
                        Console.WriteLine(result);
                        
                        Message sentMessage1 = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: result,
                        replyMarkup: helpKeyboard,
                        cancellationToken: cancellationToken);
                    }
                    else
                    {
                        Message sentMessage1 = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Incorrect input. Please enter a 10-digit number to check PNR Status.\nOr type /help to get a list of all commands",
                        replyMarkup: helpKeyboard,
                        cancellationToken: cancellationToken);
                    }
                    break;
                }
                case "/searchstation":
                {
                    Console.WriteLine("Handling /searchstation case...");
                    // Code to handle /searchstation case
                    // Check if input is valid station code or name
                    if (IsValidStationCode(messageText))
                    {
                        string result = await SearchStationAsync(messageText);
                        Console.WriteLine(result);
                        
                        Message sentMessage2 = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: result,
                        replyMarkup: helpKeyboard,
                        cancellationToken: cancellationToken);
                    }
                    else
                    {
                        Message sentMessage1 = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Incorrect input. Please enter a full/partial station name or code to check Station details (3 to 6 characters).\nOr type /help to get a list of all commands",
                        replyMarkup: helpKeyboard,
                        cancellationToken: cancellationToken);
                    }
                    break;
                }    
                case "/trainbetweenstation":
                {
                    Console.WriteLine("Handling /trainbetweenstation case...");
                    // Code to handle /trainbetweenstation case
                    // Check if input is valid station code or name
                    if (IsCorrectFormat(messageText))
                    {
                        // Split the input string by comma
                        string[] parts = messageText.Split(',');

                        //string result = parts[0] +" "+ parts[1] +" "+ parts[2];
                        string result = await CheckTrainBetweenStationsAsync(parts[0], parts[1], parts[2]);
                        
                        Message sentMessage2 = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: result,
                        replyMarkup: helpKeyboard,
                        cancellationToken: cancellationToken);

                        Console.WriteLine(result);
                    }
                    else
                    {
                        Message sentMessage1 = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Incorrect Input. Please enter from station code, to station code and date of journey to find trains between stations.\nTo check station codes, you can type /searchstation\nFor example, to search for trains between New Delhi and Lucknow on 27 June, enter:",
                        replyMarkup: helpKeyboard,
                        cancellationToken: cancellationToken);

                        Message sentMessage2 = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "NDLS,LJN,2024-06-27",
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken);
                    }
                    break;
                }
                default:
                {
                    //Removing Custom Keyboard
                    Message RemoveKeyboard = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{messageText} is not a recognized command. Kindly enter a valid commmand.\n Type /help to get a list of possible commands.",
                        //replyMarkup: new ReplyKeyboardRemove(),
                        replyMarkup: helpKeyboard,
                        cancellationToken: cancellationToken);

                    Console.WriteLine("Response sent for invalid command");
                    Console.WriteLine("Last Input is " + lastInput);

                    break;
                }
            }
            break;
        }
    }
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

// Helper method to check if a string is numeric
static bool IsNumeric(string input)
{
    return long.TryParse(input, out _);
}

/*

//To show full data of API response without processing
//string updatedResult = AddNewLineAfterCommaAndRemoveQuotes(body);
//string updatedResult2 = ExtractDataBetweenDelimiters(updatedResult, "data:{", "}}}");

static string AddNewLineAfterCommaAndRemoveQuotes(string input)
{
        // Replace both ',' and '"' with ",\n"
        string result = input.Replace(",", ",\n").Replace("\"", "");

        return result;
}

static string ExtractDataBetweenDelimiters(string input, string startDelimiter, string endDelimiter)
{
    int startIndex = input.IndexOf(startDelimiter);
    if (startIndex == -1)
        return null; // Start delimiter not found

    startIndex += startDelimiter.Length;
    int endIndex = input.IndexOf(endDelimiter, startIndex);
    if (endIndex == -1)
        return null; // End delimiter not found

    return input.Substring(startIndex, endIndex - startIndex).Trim();
}
*/

static string GetDayOfWeek(string date)
{
    DateTime dt = DateTime.ParseExact(date, "dd-MM-yyyy", null);
    return dt.ToString("dddd");
}

static bool IsValidStationCode(string stationCode)
{
    // Regular expression pattern to match IRCTC station code or name
    string pattern = @"^[A-Za-z0-9]{3,6}$";
        
    // Check if the station code matches the pattern
    return Regex.IsMatch(stationCode, pattern);
}

    static bool IsCorrectFormat(string input)
    {
        // Define the regex pattern for the format "NDLS,LJN,2024-06-27"
        string pattern = @"^[A-Z]{3,5},[A-Z]{3,5},\d{4}-\d{2}-\d{2}$";
        
        // Create a regex object with the pattern
        Regex regex = new Regex(pattern);
        
        // Check if the input string matches the pattern
        return regex.IsMatch(input);
    }

//IRCTC API Call to check PNR Status
async Task<string> CheckPNRAsync(string PNR)
{
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://irctc1.p.rapidapi.com/api/v3/getPNRStatus?pnrNumber="+PNR),
            Headers =
            {
                { "x-rapidapi-key", "a225e1c1ebmsh8310a95e989ca88p191fedjsn7de9abbb2116" },
                { "x-rapidapi-host", "irctc1.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            try
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                if (content != null)
                {
                    var checkPnrStatus = JsonConvert.DeserializeObject<CheckPnrStatus>(content);

                    // Prepare string which can display the information
                    if (checkPnrStatus == null || checkPnrStatus.Data == null)
                    {
                        Console.WriteLine("Invalid CheckPnrStatus object");
                        return "No PNR data available";
                    }

                    PnrTrainData data = checkPnrStatus.Data;

                    string displayInfo = $@"
PNR: {data.Pnr}
Train name & number: {data.TrainName} - {data.TrainNo}
Journey class: {data.Class}
Journey quota: {data.Quota}
Boarding station: {data.BoardingStationName} - {data.BoardingPoint}
Boarding time: ({GetDayOfWeek(data.SourceDoj)}) {data.SourceDoj} {data.DepartureTime}
Destination station: {data.ReservationUptoName} - {data.ReservationUpto}
Arrival time: ({GetDayOfWeek(data.DestinationDoj)}) {data.DestinationDoj} {data.ArrivalTime}
Duration: {data.Duration}
Expected platform: {data.ExpectedPlatformNo}
Coach position: {data.CoachPosition}
Chart prepared: {(data.ChartPrepared ? "✅" : "❌")}

Passengers' Details:
--------------------
";

                    // Add passenger details
                    foreach (var passenger in data.PassengerStatus)
                    {
                        displayInfo += $@"
⓵.
{passenger.ConfirmTktStatus}: {passenger.BookingStatus}
Chances of confirmation: {passenger.PredictionPercentage}%
";
                    }

                    return displayInfo;
                }
                else
                {
                    Console.WriteLine("No response from API");
                    return "No response from API: Please try again later.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return $"An error occurred: Please try again later.";
            }
        }
}


//IRCTC API Call to check Station details by name or code
async Task<string> SearchStationAsync(string stationName)
{
    var client = new HttpClient();
    var request = new HttpRequestMessage
    {
        Method = HttpMethod.Get,
        RequestUri = new Uri("https://irctc1.p.rapidapi.com/api/v1/searchStation?query="+stationName),
        Headers =
        {
            { "x-rapidapi-key", "a225e1c1ebmsh8310a95e989ca88p191fedjsn7de9abbb2116" },
            { "x-rapidapi-host", "irctc1.p.rapidapi.com" },
        },
    };
    using (var response = await client.SendAsync(request))
    {
        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            if (content != null)
                {
                    // Deserialize JSON to SearchStation object
                    var searchStation = JsonConvert.DeserializeObject<SearchStation>(content);

                    // Prepare string which can display the information
                    if (searchStation == null || searchStation.Data == null)
                    {
                        Console.WriteLine("Invalid SearchStation object");
                        return "No Station data available";
                    }

                    // Prepare the displayInfo string
                    string displayInfo = @"
Stations:
---------";
                    List<Station> data = searchStation.Data;

                    // Iterate through each station in searchStation.Data
                    foreach (var station in data)
                    {
                        displayInfo += $@"
Name: {station.Name}
English Name: {station.EngName}
Code: {station.Code}
State: {station.StateName}
";
                    }

                    return displayInfo;
                }
                else
                {
                    Console.WriteLine("No response from API");
                    return "No response from API: Please try again later.";
                }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return $"An error occurred: Please try again later.";
        }
    }    
}

async Task<string> CheckTrainBetweenStationsAsync(string fromStation, string toStation, string doj)
{
    //Test for valid data
    //string result = fromStation +" "+ toStation +" "+ doj;
    //return result;

    var client = new HttpClient();
    string url= "https://irctc1.p.rapidapi.com/api/v3/trainBetweenStations?fromStationCode=" + fromStation + "&toStationCode=" + toStation + "&dateOfJourney=" + doj;

    var request = new HttpRequestMessage
    {
        Method = HttpMethod.Get,
        RequestUri = new Uri(url),
        Headers =
        {
            { "x-rapidapi-key", "a225e1c1ebmsh8310a95e989ca88p191fedjsn7de9abbb2116" },
            { "x-rapidapi-host", "irctc1.p.rapidapi.com" },
        },
    };
    using (var response = await client.SendAsync(request))
    {
        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            if (content != null)
                {
                    // Deserialize JSON to CheckTrainBetweenStations object
                    var checkTrainBetweenStations = JsonConvert.DeserializeObject<CheckTrainBetweenStations>(content);

                    // Prepare string which can display the information
                    if (checkTrainBetweenStations == null || checkTrainBetweenStations.Data == null)
                    {
                        Console.WriteLine("Invalid CheckTrainBetweenStations object");
                        return "No Train between Station data available";
                    }

                    // Prepare the displayInfo string
                    string displayInfo = @"
Trains:
---------";
                    List<TrainData> data = checkTrainBetweenStations.Data;

                    // Iterate through each train in checkTrainBetweenStations.Data
                    foreach (var train in data)
                    {
                        displayInfo += $@"
⓵.
Train Number: {train.TrainNumber}
Train Name: {train.TrainName}
Source Station: {train.FromStationName} ({train.From})
Destination Station: {train.ToStationName} ({train.To})
Departure Time: {train.FromStd}
Arrival Time: {train.ToStd}
Duration: {train.Duration}
Train Type: {train.TrainType}
Classes Available: {string.Join(", ", train.ClassType)}
Special Train: {(train.SpecialTrain ? "Yes" : "No")}
";
                    }

                    return displayInfo;
                }
                else
                {
                    Console.WriteLine("No response from API");
                    return "No response from API: Please try again later.";
                }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return $"An error occurred: Please try again later.";
        }
    }
}