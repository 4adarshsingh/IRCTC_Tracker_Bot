# IRCTC_Tracker_Bot
IRCTC Tracker Bot is a Telegram bot that provides various features related to train information.  With IRCTC Tracker Bot, users can easily retrieve information about train schedules, PNR status, and more directly on the Telegram app at super-fast speed🚀 without worrying about any Captchas or logins.

## Technologies Used
-	C# DotNet: A programming language and framework used to build the bot, providing robust and scalable software development capabilities.
  
-	IRCTC API (Un-official) from RapidAPI: An API that allows access to various train-related information such as schedules and PNR status, ensuring accurate and up-to-date data.
  
-	Telegram API: An API that integrates with Telegram to create and manage the bot, enabling seamless interaction with users on the Telegram app.
  
-	Visual Studio Code: An integrated development environment (IDE) used for writing and debugging the bot's code, providing essential tools for efficient development.

-	Additional Packages Added:

      - dotnet add package Telegram.Bot

      - dotnet add package Telegram.Bots.Extensions.Polling --version 5.9.0
  
      - dotnet add package Newtonsoft.json


## Bot Description and Commands
IRCTC Tracker Bot can help users find out their Railway Ticket's status and other IRCTC information easily and quickly, without the need for any Login or Captcha.

IRCTC Tracker Bot supports the following commands:

/start - to start using bot

/help - to display a list of available commands.

/pnr - to get the PNR status of a train ticket.

/searchstation - to search for a station by name.

/trainbetweenstation - to get the trains between stations.

/bookticket - to go to Official IRCTC Website to book tickets.

## Getting Started

To start using irctcBot, follow these steps:

1. **Add irctcBot to Telegram**: Search for "@IRCTC_Tracker_Bot" on Telegram and click on "/start" to initiate a conversation with the bot.

2. **Use Commands**: Type any of the supported commands mentioned above to interact with the bot and retrieve the desired information.
