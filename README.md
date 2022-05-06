# Reaction User to CSV
This is a prof of concept for a reddit bot request that I keep getting messages for.

## Discord Commands
```
Commands:
rte! <- Prefix

Command: (you must NOT include the "[]" brackets.)

rte!export [message ID] <- replace messageID with the ID of the message you want to export to CSV.
	- This command exports all usernames / user ID that have reacted with the 🎊 emote.	This emote can be changed in the source on line 77, you will need to rebuild the bot when you do this.
```

## Setup

- [Click Here](https://discordapp.com/developers/applications/) to create a bot and invite it to your server as follows
   - Click the 'New Application' button
         
   ![New Application](https://i.imgur.com/2OQwdyk.png)
         
   - Enter a name for this bot and click create
         
   ![Name Bot](https://imgur.com/wdj544W.png)
         
   - Click the 'Bot' tab under settings on the left toolbar
         
   ![Bot Tab](https://imgur.com/1UCYlma.png)
         
   - Click the 'Add Bot' button
         
   ![Add Bot](https://imgur.com/8AlIHjo.png)
         
   - Click 'Yes do it' in the pop-up
       
   ![Yes do it](https://imgur.com/HWg5AZ8.png)
     
   - Select 'OAuth2' tab under settings on the left toolbar
         
   ![OAuth2](https://imgur.com/z24sHdA.png)
        
   - Select 'Bot' checkbox from the scopes list and copy the link
         
   ![Bot OAuth2](https://imgur.com/yhEg5iw.png)
         
   - Paste the copied link into a new tab / new window
                                
   - Select the server you want to invite the bot to and click 'Authorize'
     
   ![Authorize Bot](https://imgur.com/rFa3MHP.png)
   
   - Go back to the developer portal at  and select the Bot tab again.
   
   ![Bot Tab](https://imgur.com/1UCYlma.png)
   
   - Click 'Copy' button located under the token
         
   ![Copy Token](https://imgur.com/ImHZxNG.png)
        
   - Make sure your discord desktop client is running *(this does not work with discord on webclients like chrome etc)*
   
   - Download the latest version of the bot
      - > https://github.com/OniSensei/DiscordMsg-Reaction-User-to-CSV/releases/download/InitialBuild/Current.Build.zip
    
    ![Downloading](https://imgur.com/KRCFL1n.png)
    
   - Extract the zip to the desktop or my documents, somewhere you can find it easily.
   
   ![Extracted](https://imgur.com/wARredw.png)
   
   - Open the folder that was extracted to the zip and start ReactionToExcel.exe
   - You will ask you for your token.
     - This is the token we coppied in the step above, paste it using CTRL + V and press enter     
     - Once complete the bot should start.
