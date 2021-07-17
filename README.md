# Book Keeper bot
## Description
Telegram bot for saving, sorting and searching books.
Book Keeper bot can add or search for books and arrange them into bookshelves (categories).
Also books can be in three different states: planned, in progress, and completed.
The bot supports two languages (English and Russian).

## About the project
The Book Keeper bot is built on Asp Net Core using the Telegram.Bot library. The bot uses SQLite and EF Core to save data.
The bot uses webhooks to get updates. Google Book Api is used to search for books.
## Setup
This is an short description how you can test the bot locally. 
### Replace token
At first you have to set your own token and url for webhook (development only) in AppSetting.json
```
"BotConfiguration":{
    "Token": "<Token>",
    "WebhookUrl": "<Url>"
  }
```
### Ngrok
Ngrok gives you the opportunity to access your local machine from a temporary subdomain provided by ngrok. 
This domain can later send to the telegram API as URL for the webhook. Install ngrock from this page [Ngrok](https://ngrok.com/download) and run command
```
ngrok http 8443 
```
From ngrok you get an URL to your local server. Set the url in AppSetting.json and the webhook will set automatically when the bot starts.
