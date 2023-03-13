# DarkSouls OBS Overlay
This application reads the current DarkSouls: Remastered stats from memory and provides an SPA for displaying the data in obs and an API (WebSocket) for further processing in other external applications 

# Status
This application was developed only for demo purposes. It is unfinished and wont be further developed. Use at your own risk and knowledge.

However, you are free to use this as a template for your own project. The game memory scans (aob) programmed for Dark Souls are working and should be applicable for any kind of application if you have the corresponding signatures to search the memory.
Also the React web app and websocket API for the use in OBS are working for this demo and need adjustment for the data scanned from memory.

# Development
The Application consist of a [Visual Studio project](DarkSoulsOBSOverlay.sln) in ASP.NET in the root directory and a React app in the subfolder [Frontend](Frontend/).

# Usage
If you started the React app you can open [http://localhost:3000](http://localhost:3000) it in the browser to open up the settings page that will connect via websocket to the backend application you started in Visual Studio. [/obs](http://localhost/obs) will start an to this point empty page where you can start displaying the properties loaded from app memory.

Notice: In debug mode the visual studio project will also start the react app under [http://localhost](http://localhost)

# Credits
[JKAnderson](https://github.com/JKAnderson) for most of the source code wich reads the data from the dark souls application memory via AOBScans

[IllusoryWall](https://twitter.com/illusorywall) for the [event flag list](docs/EventFlagList.txt) found in the docs directory

[Grimrukh](https://github.com/Grimrukh) for the soulstruct python tool for extracting data like id's from the game files