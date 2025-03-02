# About
This is a BepInEx plugin that allows you to send quick messages to the chat.
QuickChats may be configured in BepInEx\config\plugins.quickchat.cfg
It also provides a shortcut to the console. Just press / like in minecraft and chat will be opened with slash typed in.

# Installation
1. Have BepInEx installed on your Puck client. Instructions are in the Toaster's Rink Discord server.
2. Place the QuickChat.dll file into your `Puck/BepInEx/plugins/` folder.

# Usage
- Available buttons are 1, 2, 3, 4. Press two buttons within 3 seconds to send a corresponding quick chat.
- For example, `1 1` will send "I got it!" quickchat.

# Compilation
- Create a lib folder inside repo folder
- Copy contents of BepInEx/interop into it
- `dotnet build`
- Alternatively, `build_and_move.bat` assumes that repo folder is inside your modded Puck folder. It builds, copies and starts Puck.exe.

## Get Involved
Want to get involved with the Puck modding scene? Join Toaster's Rink - http://discord.puckstats.io/